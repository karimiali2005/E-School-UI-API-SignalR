using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Es.DataLayerCore.DTOs.Exam;
using Es.DataLayerCore.Model;
using ESchool.Need;
using EsServiceCore.DTOs;
using EsServiceCore.Interface;
using EsServiceCore.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.Controllers
{
    [Authorize(AuthenticationSchemes = "AccountAuth")]
    public class ExamController : Controller
    {
        private readonly IExamService _examService;
        private readonly IRoomChatService _roomChatService;

        public ExamController(IExamService examService, IRoomChatService roomChatService)
        {
            _examService = examService;
            _roomChatService = roomChatService;
        }


        public async Task<IActionResult> IndexAsync(string returnUrl = "/")
        {

            try
            {
                ViewBag.returnUrl = returnUrl;
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId);



                var roomTeacherExams = await _examService.RoomTeacherExamShow(userId);





                return View(roomTeacherExams);

            }
            catch (Exception ex)
            {

                ex.ToSaveElmah();
                return View("Error");
            }


        }

        public async Task<IActionResult> Exams(string returnUrl = "/")
        {

            try
            {
                ViewBag.returnUrl = returnUrl;
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId);



                var exams = await _examService.TeacherExamShow(userId);





                return View(exams);

            }
            catch (Exception ex)
            {

                ex.ToSaveElmah();
                return View("Error");
            }


        }

        /// <summary>
        /// create exam by teacher
        /// </summary>
        ///<param></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Create(int? Id)
        {
            try
            {
                var UserId = Convert.ToInt32(this.User.FindFirst("UserId").Value);

                var model = new vm_ExamByQuestionsAndChoices();

                if (Id != null && Id != 0)
                {
                    model = await _examService.GetExamById((int)Id);

                }

                else
                {


                    model.CompanyId = 1;
                    model.UserId = UserId;
                    model.ExamLoginTypeId = 1;
                }
                model.ScoreTypes = await _examService.ScoreTypesDetailsShow();
                model.RoomChats = await _examService.RoomChatGroups(UserId);
                if (Id != null && Id != 0)
                {
                    model.ScoreIsNumber = model.ScoreTypes.Where(e => e.ScoreTypeID == model.ScoreTypeId).FirstOrDefault().IsNumber;
                }
                return View(model);
            }
            catch (Exception ex)
            {

                ex.ToSaveElmah();
                return View("Error");
            }
        }

        [HttpGet]
        public ViewResult AddQuestion(int type, int i, Question question)
        {
            try
            {

                var model = new vm_ExamByQuestionsAndChoices();
                model.Questions = new List<Question>();

                if (question != null && (question.QuestionId != 0 && question.QuestionId != null))
                {
                    var nficurrent = CultureInfo.CurrentCulture.NumberFormat;

                    if (nficurrent.NumberDecimalSeparator != "/")
                    {
                        question.QuestionScoreString = question.QuestionScore.ToString().Replace(nficurrent.NumberDecimalSeparator, "/");

                    }
                    model.Questions.Add(question);

                    model.Indexq = question.QuestionOrder;

                }
                else
                {
                    model.Indexq = i;
                    model.Questions.Add(new Question
                    {

                        QuestionTypeId = type,
                        QuestionId = i,
                        QuestionOrder = i
                    });


                }


                //if id=0  insert random id
                return View("QT" + type, model);


            }
            catch (Exception ex)
            {

                ex.ToSaveElmah();
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm]vm_ExamByQuestionsAndChoices model, IFormFile[] files)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    //if (model.ExamPic_file != null)
                    //{

                    //}
                    if (model.Questions != null)
                    {
                        foreach (var question in model.Questions)
                        {
                            if (!string.IsNullOrEmpty(question.QuestionScoreString))
                            {
                                // double dd = 4.25;
                                var nficurrent = CultureInfo.CurrentCulture.NumberFormat;

                                if (nficurrent.NumberDecimalSeparator != "/" && !string.IsNullOrEmpty(question.QuestionScoreString))
                                {
                                    question.QuestionScore = decimal.Parse(question.QuestionScoreString.Replace("/", nficurrent.NumberDecimalSeparator));

                                }

                            }
                            if (question.QuestionFile_file != null)
                            {
                                question.QuestionFile = await StoreFile(question.QuestionFile_file, "Exam");
                                if (question.QuestionTypeId == 2)
                                {

                                    foreach (var questionchoices in question.QuestionChoice)
                                    {
                                        if (questionchoices.QuestionChoiceFile_file != null)
                                        {
                                            questionchoices.QuestionChoiceFile = await StoreFile(questionchoices.QuestionChoiceFile_file, "Exam");

                                        }
                                    }
                                }
                            }

                        }
                    }
                    DateTime examStartDate = model.ExamStartDateTime.ToGeorgianDateTime();
                    var result = await _examService.AddExam(model, examStartDate);
                    if (result)
                        return RedirectToAction("Exams");
                    else
                    {
                        model.ErrorMsg = "ثبت اطلاعات با خطا مواجه شد";
                    }
                }
                else
                {
                    model.ErrorMsg = "ثبت اطلاعات با خطا مواجه شد";
                }

                model.ScoreTypes = await _examService.ScoreTypesDetailsShow();
                model.RoomChats = await _examService.RoomChatGroups(model.UserId);
                return View(model);
            }
            catch (Exception ex)
            {

                ex.ToSaveElmah();
                return View("Error");
            }

        }

        ///حذف آزمون

        [HttpPost]
        public async Task<IActionResult> DeleteExam(int Id)
        {
            try
            {
                var status = "";
                var userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
                var model = new vm_ExamByQuestionsAndChoices();
                model = await _examService.GetExamById((int)Id);
                var address = SettingContext.PathStoreFiles.Instance.Address;
                var fullAddressFtpCut = address;
                fullAddressFtpCut += SettingContext.PathStoreFiles.Instance.ExamFile;
                if (model.ExamPic != null)
                {
                    Codes.DeleteFile(fullAddressFtpCut, model.ExamPic, SettingContext.PathStoreFiles.Instance.UserId, SettingContext.PathStoreFiles.Instance.Password);
                }
                if (model.Questions!= null) 
                {
                    foreach (var question in model.Questions)
                    {
                        if (question.QuestionFile != null)
                        {
                            Codes.DeleteFile(fullAddressFtpCut, question.QuestionFile, SettingContext.PathStoreFiles.Instance.UserId, SettingContext.PathStoreFiles.Instance.Password);

                            if (question.QuestionTypeId == 2)
                            {

                                foreach (var questionchoices in question.QuestionChoice)
                                {
                                    if (questionchoices.QuestionChoiceFile != null)
                                    {
                                        Codes.DeleteFile(fullAddressFtpCut, questionchoices.QuestionChoiceFile, SettingContext.PathStoreFiles.Instance.UserId, SettingContext.PathStoreFiles.Instance.Password);

                                    }
                                }
                            }
                        }
                    }
                }
                var result = _examService.DeleteExam(userId, Id, ref status);
                if (result)
                {

                    return Json(new { message = status, status = "Success" });

                }
                else
                {
                    return Json(new { message = status, status = "Error" });

                }



            }
            catch (Exception ex)
            {

                ex.ToSaveElmah();
                return View("Error");
            }
        }

        ///حذف آزمون

        [HttpPost]
        public IActionResult TurnOnOffExam(int Id)
        {
            try
            {
                var status = "";
                var userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
                var result = _examService.TurnOnOffExam(userId, Id, ref status);
                if (result)
                {
                    return Json(new { message = status, status = "Success" });

                }
                else
                {
                    return Json(new { message = status, status = "Error" });

                }



            }
            catch (Exception ex)
            {

                ex.ToSaveElmah();
                return View("Error");
            }
        }


        [HttpGet]
        public async Task<IActionResult> CancelExam(int Id)
        {
            var model = await _examService.GetOnlyExamById(Id);
            if (model == null || string.IsNullOrEmpty(model.ExamTitle))
            {
                model = new Exam();
                model.ExamId = Id;
                ViewBag.Message = "کد آزمون وارد شده اشتباه است";

            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CancelExam(Exam model)
        {
            var result = _examService.CancelExam(model.ExamId, model.ExamCancelReason);
            if (result == true)
            {
                return RedirectToAction("Exams");
            }
            else
            {
                ViewBag.Message = "در ثبت اطلاعات خطایی رخ داده است";

            }
            return View(model);
        }
        //لیست پاسخ دهنده گان
        //AzmonTable2.html
        [HttpGet]
        public async Task<IActionResult> Responders(int Id)
        {
            var model = new vm_ExamResponders();
            model = await _examService.GetExamResponders(Id);
            if (model.ExamInfo == null || model.ExamInfo.Count < 1)
            {

                model.ErrorMsg = "کد آزمون وارد شده اشتباه است";

            }
            return View(model);

        }

        //نمره دادن به ازمون
        //Panel2.html


        //آزمون های من
        //azmon.html
        [HttpGet]
        public async Task<IActionResult> MyExams()
        {
            try
            {
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId);

                var model = await _examService.ExamStudentShow(userId);
                return View(model);
            }
            catch (Exception ex)
            {

                ex.ToSaveElmah();
                return View("Error");
            }
        }

        //پاسخ دهی
        //lessonAzmon.html
        [HttpGet]
        public async Task<IActionResult> Response(int Id, int? qId)
        {
            try
            {
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId);

                var model = await _examService.GetExamByQuestionForResponse(userId, Id, qId);
                model.QuestionTypeId = model.Question.QuestionTypeId;
                model.QuestionId = model.Question.QuestionId;

                return View(model);
            }
            catch (Exception ex)
            {

                ex.ToSaveElmah();
                return View("Error");
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Response([FromForm]vm_ResponseByQuestionsAndChoices model)
        {
            try
            {

                if (model.ResponseValue_file != null && model.QuestionTypeId != 4)
                {
                    model.ResponseValue = await StoreFile(model.ResponseValue_file, "ResponseExam");

                }

                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId);
                model.UserId = userId;
                var result = true;
                if (model.QuestionTypeId != 4)
                {
                    result = await _examService.SaveResponse(model);
                }
                if (result && (model.NextQuestionId != null && model.NextQuestionId != 0))
                {
                    return RedirectToAction("Response", new { Id = model.ExamId, qId = model.NextQuestionId });
                }
                if (result && (model.NextQuestionId == null || model.NextQuestionId == 0))
                {
                    return RedirectToAction("MyExams");
                }
                model.Status = false;
                model.ErrorMsg = "ثبت آزمون با خطا روبرو شد";
                return View(model);


            }
            catch (Exception ex)
            {

                ex.ToSaveElmah();
                return View("Error");
            }

        }


        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 2147483648)]
        [RequestSizeLimit(2147483648)]
        public async Task<IActionResult> SaveVoiceResponse(IFormFile ResponseValue_file, int ResponseID, int ExamId, int QuestionId, int QuestionTypeId, int NextQuestionId, int? ResponseQuestionID)
        {
            try
            {
                var question = new Question();
                question.QuestionTypeId = QuestionTypeId;
                question.QuestionId = QuestionId;
                var model = new vm_ResponseByQuestionsAndChoices()
                {
                    ResponseID = ResponseID,
                    ExamId = ExamId,
                    QuestionId = QuestionId,
                    QuestionTypeId = QuestionTypeId,
                    ResponseQuestionID = ResponseQuestionID,
                    Question = question,
                    NextQuestionId = NextQuestionId
                };

                if (QuestionTypeId == 4 && ResponseValue_file != null)
                {
                    var address = SettingContext.PathStoreFiles.Instance.Address;
                    var fullAddressFtp = address;
                    fullAddressFtp += SettingContext.PathStoreFiles.Instance.ResponseExamFile;

                    string filename = ContentDispositionHeaderValue.Parse(ResponseValue_file.ContentDisposition).FileName.Trim('"');
                    filename = this.EnsureCorrectFilename(filename);

                    string savefilename = Codes.GetRandomFileName(filename);
                    var requestUri = fullAddressFtp + savefilename;



                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(requestUri);
                    request.Method = WebRequestMethods.Ftp.UploadFile;
                    request.Credentials = new NetworkCredential(SettingContext.PathStoreFiles.Instance.UserId, SettingContext.PathStoreFiles.Instance.Password);

                    using (Stream requestStream = request.GetRequestStream())
                    {
                        await ResponseValue_file.CopyToAsync(requestStream);
                        model.ResponseValue = savefilename;
                    }

                }

                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId);
                model.UserId = userId;

                var result = await _examService.SaveResponse(model);
                if (result && (model.NextQuestionId != null && model.NextQuestionId != 0))
                {
                    return Json(new { url = "/Exam/Response?Id=" + model.ExamId + "&qId=" + model.NextQuestionId });

                }
                if (result && (model.NextQuestionId == null || model.NextQuestionId == 0))
                {
                    return Json(new { url = "/Exam/MyExams" });

                }
                return Json(new { url = "", ErrorMsg = "ثبت آزمون با خطا روبرو شد" });


            }
            catch (Exception ex)
            {

                ex.ToSaveElmah();
                return Content("Error: " + ex.Message);
            }

        }






        //نمره دادن به ازمون
        //Panel2.html
        //panel2.html
        [HttpGet]
        public async Task<IActionResult> Score(int Id, int uId, int? qId)
        {
            try
            {
                //Id  is responseId;
                //  var userId = 2579;

                var model = await _examService.GetExamByQuestionForScore(Id, uId, qId);
                return View(model);
            }
            catch (Exception ex)
            {

                ex.ToSaveElmah();
                return View("Error");
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Score([FromForm]vm_ScoreResponseByQuestionsAndChoices model)
        {
            try
            {

                var nficurrent = CultureInfo.CurrentCulture.NumberFormat;

                if (nficurrent.NumberDecimalSeparator != "/")
                {
                    if (!string.IsNullOrEmpty(model.ResponseQuestionScoreString))
                        model.ResponseQuestionScore = decimal.Parse(model.ResponseQuestionScoreString.Replace("/", nficurrent.NumberDecimalSeparator));
                    if (!string.IsNullOrEmpty(model.ResponseScoreString))
                        model.ResponseScore = decimal.Parse(model.ResponseScoreString.Replace("/", nficurrent.NumberDecimalSeparator));

                }
                var result = await _examService.SaveScore(model);

                if (result && (model.NextQuestionId != null && model.NextQuestionId != 0))
                {
                    return RedirectToAction("Score", new { Id = model.ResponseID, uId = model.UserId, qId = model.NextQuestionId });
                }
                if (result && (model.NextQuestionId == null || model.NextQuestionId != 0))
                {
                    return RedirectToAction("Responders", new { Id = model.ExamId });
                }
                model.Status = false;
                model.ErrorMsg = "ثبت نمره با خطا روبرو شد";
                return View(model);


            }
            catch (Exception ex)
            {

                ex.ToSaveElmah();
                return View("Error");
            }

        }


        //نمره دهی سوالات چند گزینه ای به صورت اتومات
        [HttpPost]
        public IActionResult ScoreAutoExam(int Id)
        {
            try
            {
                //Id is examId;

                var result = _examService.ExamSetScoreAuto(Id);
                if (result)
                {
                    return Json(new { message = "با موفقیت انجام شد", status = "Success" });

                }
                else
                {
                    return Json(new { message = "خطا رخ داده لطفا دوباره تلاش کنید", status = "Error" });

                }



            }
            catch (Exception ex)
            {

                ex.ToSaveElmah();
                return View("Error");
            }
        }



        //نمایش نتیجه آزمون به دانش آموز
        [HttpGet]
        public async Task<IActionResult> Result(int Id, int? qId)
        {
            try
            {
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId);

                var model = await _examService.GetExamByQuestionForScore(Id, userId, qId);
                if (model.NextQuestionId == null || model.NextQuestionId != 0)
                {
                    return RedirectToAction("MyExams");
                }
                return View(model);
            }
            catch (Exception ex)
            {

                ex.ToSaveElmah();
                return View("Error");
            }

        }

        //[HttpPost]
        //[RequestFormLimits(MultipartBodyLengthLimit = 2147483648)]
        //[RequestSizeLimit(2147483648)]
        public async Task<string> StoreFile(IFormFile file, string type)
        {
            try
            {
                var address = SettingContext.PathStoreFiles.Instance.Address;
                var fullAddressFtp = address;
                if (type == "Exam")
                {
                    fullAddressFtp += SettingContext.PathStoreFiles.Instance.ExamFile;
                }
                if (type == "ResponseExam")
                {
                    fullAddressFtp += SettingContext.PathStoreFiles.Instance.ResponseExamFile;
                }
                string filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                filename = EnsureCorrectFilename(filename);
                string savefilename = Codes.GetRandomFileName(filename);
                var requestUri = fullAddressFtp + savefilename;
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(requestUri);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(SettingContext.PathStoreFiles.Instance.UserId, SettingContext.PathStoreFiles.Instance.Password);
                // request.UsePassive is true by default.
                /*string savefilename = Codes.GetFileName(filename);
                using (var output = new FileStream(Codes.GetPathAndFilename(savefilename), FileMode.Create))
                {
                    await file.CopyToAsync(output);
                }*/
                using (Stream requestStream = request.GetRequestStream())
                {
                    await file.CopyToAsync(requestStream);
                }
                //  return Json(new { Filename = savefilename, MimeType = file.ContentType });
                //  var result = new { status = "ok", path = savefilename, mimtype = file.ContentType };
                // return Ok(Json(result));
                return savefilename;
            }
            catch (Exception ex)
            {
                //  return Content("Error: " + ex.Message);
                //  var result = new { status = "eror", message = ex.Message };
                return null;
            }
        }

        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
            {
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);
            }
            return filename;
        }


        public async Task<IActionResult> GetRoomChatGroups(string q)
        {

            var list = new List<Select2Model>();
            var UserId = Convert.ToInt32(this.User.FindFirst("UserId").Value);

            list = await _examService.RoomChatGroups(UserId);

            if (!(string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q)))
            {
                list = list.Where(x => x.text.ToLower().Contains(q.ToLower())).ToList();
            }
            return Json(new { items = list });
        }
    }
}