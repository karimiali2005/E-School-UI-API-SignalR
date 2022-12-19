using Es.DataLayerCore.Context;
using Es.DataLayerCore.DTOs.Exam;
using Es.DataLayerCore.Model;
using EsServiceCore.DTOs;
using EsServiceCore.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace EsServiceCore.Service
{
    public class ExamService : IExamService
    {
        private readonly ESchoolContext _context;
        public ExamService(ESchoolContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<RoomTeacherExamShowResult>> RoomTeacherExamShow(int userId)
        {
            try
            {
                var roomTeacherExams = await _context.RoomTeacherExamShowAsync(userId);

                return await Task.FromResult(roomTeacherExams);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<Select2Model>> RoomChatGroups(int userId)
        {
            try
            {
                var roomExams = (from s in _context.RoomChatGroup
                                         where s.CloseChat == false && s.RoomChatGroupType==1 
                                         select new Select2Model
                                         {
                                             value = s.RoomChatGroupId,
                                             text = s.RoomChatGroupTitle
                                         }).ToList();
               // var roomExams = _context.RoomChatGroup.Where(e => e.CloseChat == false && e.RoomChatGroupType == 1).ToList();
               
                return await Task.FromResult(roomExams);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //درج و ویرایش ازمون
        public async Task<bool> AddExam(vm_ExamByQuestionsAndChoices model, DateTime examStartDate)
        {
            try
            {
                var flag = true;
                using (var context = _context)
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            if (model.Questions != null)
                            {
                                model.Questions.Where(e => e.ExamId == 0).ToList().ForEach(e => e.QuestionId = 0);
                            }
                            if (model.ExamId == 0 || model.ExamId == null)
                            {

                                var exam = new Exam();
                                exam.ExamTitle = model.ExamTitle;
                                exam.ExamDescription = model.ExamDescription;
                                exam.ExamPic = model.ExamPic;
                                exam.CompanyId = model.CompanyId;
                                exam.UserId = model.UserId;
                                exam.RoomChatGroupId = model.RoomChatGroupId;
                                exam.ExamLoginTypeId = model.ExamLoginTypeId;
                                exam.ExamCreateDateTime = DateTime.Now;
                                exam.ExamStartDateTime = examStartDate;
                                exam.ExamFinishDateTime = examStartDate;
                                exam.DelayDeadline = model.DelayDeadline;
                                exam.ScoreTypeId = model.ScoreTypeId;
                                exam.ExamTime = model.ExamTime;
                                exam.OpeningNumber = model.OpeningNumber;
                                exam.RandomNumber = model.RandomNumber;
                                exam.ExamOn = false;

                                exam.ExamStartDateTime = examStartDate.Add(TimeSpan.Parse(model.ExamStartTime));
                                exam.ExamFinishDateTime = examStartDate.Add(TimeSpan.Parse(model.ExamFinishTime));


                                await _context.Exam.AddAsync(exam);
                                int result = _context.SaveChanges();
                                if (result > 0)
                                {
                                    if (model.Questions != null)
                                    {
                                        var i = 0;
                                        foreach (var item in model.Questions)
                                        {
                                            var oldId = item.QuestionId;
                                            item.QuestionOrder = i;
                                            item.ExamId = exam.ExamId;
                                            await _context.Question.AddAsync(item);
                                            int result2 = _context.SaveChanges();
                                            if (result2 > 0)
                                            {
                                                i++;

                                            }
                                            else
                                            {
                                                flag = false;
                                            }
                                        }
                                    }

                                }
                                else
                                {
                                    flag = false;

                                }

                            }
                            else
                            {
                                var oldmodel = await _context.Exam.Where(e => e.ExamId == model.ExamId).FirstOrDefaultAsync();

                                if (oldmodel != null)
                                {

                                    oldmodel.ExamTitle = model.ExamTitle;
                                    oldmodel.ExamDescription = model.ExamDescription;
                                    oldmodel.ExamPic = model.ExamPic;
                                    oldmodel.RoomChatGroupId = model.RoomChatGroupId;
                                    oldmodel.ExamLoginTypeId = model.ExamLoginTypeId;
                                    oldmodel.ExamStartDateTime = examStartDate;
                                    oldmodel.ExamFinishDateTime = examStartDate;
                                    oldmodel.DelayDeadline = model.DelayDeadline;
                                    oldmodel.ScoreTypeId = model.ScoreTypeId;
                                    oldmodel.ExamTime = model.ExamTime;
                                    oldmodel.OpeningNumber = model.OpeningNumber;
                                    oldmodel.RandomNumber = model.RandomNumber;

                                    oldmodel.ExamStartDateTime = examStartDate.Add(TimeSpan.Parse(model.ExamStartTime));
                                    oldmodel.ExamFinishDateTime = examStartDate.Add(TimeSpan.Parse(model.ExamFinishTime));

                                    _context.Exam.Update(oldmodel);
                                    int result = await _context.SaveChangesAsync();
                                    if (result > 0)
                                    {
                                        if (model.Questions != null)
                                        {
                                            //حذف سوالاتی که نیستند
                                            var orginalQuestion = _context.Question.Where(e => e.ExamId == model.ExamId).ToList();
                                            var someItemIDs = model.Questions.Select(e => e.QuestionId).ToList();
                                            // Populate someItems
                                            var deletedQuestion = orginalQuestion
                                                              .Where(x => !someItemIDs.Contains(x.QuestionId))
                                                              .ToList();
                                            foreach (var item in deletedQuestion)
                                            {
                                                var choices = _context.QuestionChoice.Where(e => e.QuestionId == item.QuestionId);
                                                _context.QuestionChoice.RemoveRange(choices);
                                                _context.Question.Remove(item);
                                                await _context.SaveChangesAsync();
                                            }


                                            //به روزرسانی سوالات قبلی
                                            foreach (var item in model.Questions.Where(e => e.QuestionId != 0 && e.ExamId != 0))
                                            {
                                                var questionChoices = item.QuestionChoice.ToList();
                                                var x = await _context.Question.Where(e => e.QuestionId == item.QuestionId).FirstOrDefaultAsync();
                                                x.QuestionTitle = item.QuestionTitle;
                                                x.QuestionFile = item.QuestionFile;
                                                x.QuestionScore = item.QuestionScore;
                                                x.IsRandomQuestion = item.IsRandomQuestion;
                                                _context.Question.Update(x);
                                                int result2 = await _context.SaveChangesAsync();

                                                if (result > 0)
                                                {

                                                    if (item.QuestionTypeId == 2)
                                                    {
                                                        //حذف گزینه های حذف شده
                                                        // Populate someItems
                                                        var orginalChoiceQuestion = _context.QuestionChoice.Where(e => e.QuestionId == item.QuestionId);
                                                        var someItemchoiceIDs = item.QuestionChoice.Select(e => e.QuestionChoiceId).ToList();
                                                        var deletedChoiceQuestion = orginalChoiceQuestion
                                                          .Where(e => !someItemchoiceIDs.Contains(e.QuestionChoiceId))
                                                          .ToList();

                                                        _context.QuestionChoice.RemoveRange(deletedChoiceQuestion);
                                                        await _context.SaveChangesAsync();
                                                        foreach (var itemChoice in questionChoices)
                                                        {
                                                            //آپدیت گزینه
                                                            if (itemChoice.QuestionChoiceId != 0 && itemChoice.QuestionChoiceId != null)
                                                            {
                                                                var choice = await _context.QuestionChoice.Where(e => e.QuestionChoiceId == itemChoice.QuestionChoiceId).FirstOrDefaultAsync();
                                                                choice.QuestionChoiceTitle = itemChoice.QuestionChoiceTitle;

                                                                choice.QuestionChoiceFile = itemChoice.QuestionChoiceFile;
                                                                choice.IsAnswer = itemChoice.IsAnswer;
                                                                _context.QuestionChoice.Update(choice);
                                                            }
                                                            else
                                                            {//درج گزینه جدید
                                                                itemChoice.QuestionId = item.QuestionId;
                                                                await _context.QuestionChoice.AddAsync(itemChoice);
                                                            }

                                                            await _context.SaveChangesAsync();
                                                        }

                                                    }
                                                }
                                            }
                                            //ثبت سوالات جدید
                                            var i = model.Questions.Max(e => e.QuestionOrder) + 1;
                                            foreach (var item in model.Questions.Where(e => e.ExamId == 0))
                                            {
                                                item.QuestionId = 0;
                                                item.QuestionOrder = i;
                                                item.ExamId = oldmodel.ExamId;
                                                await _context.Question.AddAsync(item);
                                                int result2 = await _context.SaveChangesAsync();
                                                //if (result > 0)
                                                //{
                                                //    i++;
                                                //    if (item.QuestionTypeId == 2)
                                                //    {
                                                //        foreach (var itemChoice in item.QuestionChoice)
                                                //        {
                                                //            itemChoice.QuestionChoiceId = 0;
                                                //            itemChoice.QuestionId = item.QuestionId;
                                                //            await _context.QuestionChoice.AddAsync(itemChoice);
                                                //            await _context.SaveChangesAsync();
                                                //        }
                                                //    }
                                                //}
                                            }



                                        }
                                    }

                                }

                            }

                            transaction.Commit();
                            return flag;

                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //نمایش ازمون
        public async Task<vm_ExamByQuestionsAndChoices> GetExamById(int examId)
        {
            try
            {
                var enUsCulture = new CultureInfo("en-US");

                var model = new vm_ExamByQuestionsAndChoices();
                var exam = await _context.Exam.FindAsync(examId);
                model.ExamTitle = exam.ExamTitle;
                model.ExamId = exam.ExamId;
                model.CompanyId = exam.CompanyId;
                model.UserId = exam.UserId;
                model.ExamLoginTypeId = exam.ExamLoginTypeId;
                model.ExamDescription = exam.ExamDescription;
                model.ExamStartDateTime = exam.ExamStartDateTime.ToString("yyyy/MM/dd");
                model.ExamTime = exam.ExamTime;
                model.DelayDeadline = exam.DelayDeadline;
                model.RandomNumber = exam.RandomNumber;
                model.OpeningNumber = exam.OpeningNumber;
                model.RoomChatGroupId = exam.RoomChatGroupId;
                model.ScoreTypeId = exam.ScoreTypeId;
                model.ExamStartTime = exam.ExamStartDateTime.ToString("HH:mm");
                model.ExamFinishTime = exam.ExamFinishDateTime.ToString("HH:mm");
                model.HasResponse = _context.Response.Count(e => e.ExamId == examId) > 0 ? true : false;
               
                //  if (model.ExamStartTime.Substring(6, 2)== "ق.ظ")

                var questions = _context.Question.Where(e => e.ExamId == examId).Include(e => e.QuestionChoice).ToList();
                model.Questions = questions;
                return await Task.FromResult(model);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //نمایش ازمون
        public async Task<Exam> GetOnlyExamById(int examId)
        {
            try
            {
                var exam = _context.Exam.Where(e => e.ExamId == examId).FirstOrDefault();
                return await Task.FromResult(exam);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //رد ازمون
        public bool CancelExam(int examId, string reason)
        {
            try
            {
                var exam = _context.Exam.Find(examId);
                if (exam != null)
                {
                    exam.ExamCancel = true;
                    exam.ExamCancelReason = reason;
                    exam.ExamOn = false;
                    _context.Exam.Update(exam);
                    int result = _context.SaveChanges();
                    if (result > 0)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        //لیست ازمونهای هر معلم
        public async Task<IEnumerable<Exam>> TeacherExamShow(int userId)
        {
            try
            {
                var treacherExams = _context.Exam.Where(e => e.UserId == userId).Include(e => e.RoomChatGroup).Include(e => e.Response).OrderByDescending(e=>e.ExamId).ToList();

                return await Task.FromResult(treacherExams);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //حذف ازمون
        public bool DeleteExam(int userId, int examId, ref string status)
        {
            try
            {
                var flag = true;
                using (var context = _context)
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            var isResponsed = _context.Response.Any(e => e.ExamId == examId);
                            if (!isResponsed)
                            {
                                var exam = _context.Exam.Where(e => e.ExamId == examId && e.UserId == userId).FirstOrDefault();
                                if (exam != null)
                                {
                                    var questions = _context.Question.Where(e => e.ExamId == examId).Include(e => e.QuestionChoice).ToList();
                                    foreach (var item in questions)
                                    {
                                        _context.QuestionChoice.RemoveRange(item.QuestionChoice);
                                    }
                                    _context.Question.RemoveRange(questions);
                                    _context.Exam.Remove(exam);
                                    var x = _context.SaveChanges();
                                    if (x > 0)
                                    {
                                        status = "حذف با موفقیت انجام شد";

                                    }
                                    else
                                    {
                                        status = "خطایی رخ داد";
                                        flag = false;
                                    }
                                }
                                else
                                {
                                    status = "آزمون یافت نشد";
                                    flag = false;
                                }
                            }
                            else
                            {
                                status = "آزمون پاسخ دارد و شما نمیتوانید آنرا حذف کنید";
                                flag = false;

                            }

                            transaction.Commit();
                            return flag;

                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }
                }
            }

            catch (Exception ex)
            {

                throw ex;
            }



        }
        //حذف ازمون
        public bool TurnOnOffExam(int userId, int examId, ref string status)
        {
            try
            {
                bool? examOn = false;
                var exam = _context.Exam.Where(e => e.ExamId == examId && e.UserId == userId).FirstOrDefault();
                if (exam != null)
                {
                    examOn = exam.ExamOn;
                    exam.ExamOn = examOn == true ? false : true;
                    var x = _context.SaveChanges();
                    if (x > 0)
                    {
                        status = examOn == false ? "آزمون روشن شد" : "آزمون خاموش شد";
                        return true;
                    }
                    else
                    {
                        status = "خطایی رخ داد";
                        return false;
                    }
                }
                status = "آزمون یافت نشد";
                return false;



            }

            catch (Exception ex)
            {

                throw ex;
            }



        }


        ////تنظیماتازمون
        //Task<bool> SettingExam(int userId, int examId);

        // لیست ازمونهای هر دانش آموز
        public async Task<List<ExamStudentShowResult>> ExamStudentShow(int userId)
        {
            try
            {

                var result = await _context.ExamStudentShowAsync(userId);

                return result;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //نمایش سوالات در پاسخ دهی
        public async Task<vm_ResponseByQuestionsAndChoices> GetExamByQuestionForResponse(int userId, int examId, int? questionId)
        {
            try
            {
                var model = new vm_ResponseByQuestionsAndChoices();
                model.Status = true;

                //آزمون مورد نظر
                var exam = await _context.Exam.Where(e => e.ExamId == examId).Include(e => e.ScoreType).FirstOrDefaultAsync();

                if (exam.ExamFinishDateTime < DateTime.Now && exam.ExamFinishDateTime.ToShortDateString().Equals(DateTime.Now.ToShortDateString()))
                {
                    model.Status = false;
                    model.ErrorMsg = "زمان آزمون شما به پایان رسیده است";
                    return await Task.FromResult(model);
                }
                if (exam.ExamStartDateTime.ToString("yyyy/MM/dd") != DateTime.Now.ToString("yyyy/MM/dd") || (exam.ExamStartDateTime.ToString("yyyy/MM/dd") == DateTime.Now.ToString("yyyy/MM/dd") && exam.ExamStartDateTime.TimeOfDay > DateTime.Now.TimeOfDay))
                {

                    model.Status = false;
                    model.ErrorMsg = "زمان آزمون شما نرسیده است";
                    return await Task.FromResult(model);
                }
                //پاسخ نامه جاری
                Response response = await _context.Response.FirstOrDefaultAsync(e => e.ExamId == examId && e.UserId == userId);
                if (response != null)
                {
                    model.ResponseID = response.ResponseId;
                    //at multi open response
                    if ((questionId == null || questionId == 0) && exam.OpeningNumber >= response.OpeningCount)
                    {
                        response.OpeningCount++;
                        response.StartDateTime = DateTime.Now;
                        _context.Response.Update(response);
                        await _context.SaveChangesAsync();
                    }
                    else if (exam.OpeningNumber < response.OpeningCount)
                    {
                        model.Status = false;
                        model.ErrorMsg = "شما نمیتوانید این ازمون را پاسخ دهید";
                        return await Task.FromResult(model);
                    }


                }
                else
                {
                    TimeSpan delayTime = DateTime.Parse(DateTime.Now.ToString("HH:mm:ss")).Subtract(DateTime.Parse(exam.ExamStartDateTime.ToString("HH:mm:ss")));
                    if (delayTime.TotalMinutes > exam.DelayDeadline)
                    {
                        model.Status = false;
                        model.ErrorMsg = "به علت تاخیر شما نمیتوانید این آزمون را پاسخ دهید";
                        return await Task.FromResult(model);
                    }
                    //اطلاعات پاسخ نامه
                    response = new Response();
                    response.ExamId = examId;
                    response.UserId = userId;
                    response.CreateDateTime = DateTime.Now;
                    response.StartDateTime = DateTime.Now;
                    response.FinishDateTime = DateTime.Now;
                    response.OpeningCount = 1;
                    _context.Response.Add(response);
                    int result = await _context.SaveChangesAsync();

                }

                //اطلاعات آزمون
                TimeSpan resrExamTiem = DateTime.Parse(exam.ExamFinishDateTime.ToString("HH:mm:ss")).Subtract(DateTime.Parse(DateTime.Now.ToString("HH:mm:ss")));
                model.ResponseID = response.ResponseId;
                model.ExamId = exam.ExamId;
                model.CompanyId = exam.CompanyId;
                model.UserId = exam.UserId;
                model.ExamTitle = exam.ExamTitle;
                model.ExamDescription = exam.ExamDescription;
                model.HasNumberScore = exam.ScoreType.IsNumber;
                model.RestExamTime = resrExamTiem.ToString(@"hh\:mm\:ss");
                model.RestExamTimeSeconds = (int)resrExamTiem.TotalSeconds;
                model.QuestionCount = _context.Question.Count(e => e.ExamId == examId);

                var currentOrder = 0;
                var question = new Question();



                var examRandNumber = exam.RandomNumber;
                var questionRandAnswered = _context.ResponseQuestion.Count(e => e.ResponseId == model.ResponseID && e.Question.IsRandomQuestion == true);

                // انتخاب سوال برای نمایش
                if (questionId != null && questionId != 0)
                {
                    question = _context.Question.Where(e => e.ExamId == examId && e.QuestionId == questionId).Include(e => e.QuestionChoice).FirstOrDefault();
                }
                else
                {   // انتخاب اولین سوال
                    question = _context.Question.Where(e => e.ExamId == examId).OrderBy(e => e.QuestionOrder).Include(e => e.QuestionChoice).FirstOrDefault();
                }
                //سوال جاری
                model.Question = question;
                currentOrder = question.QuestionOrder;

                //انتخاب سوال بعدی
                var nextQuestion = new Question();
                var flag = false;

                if (model.Question.IsRandomQuestion == false)
                    nextQuestion = _context.Question.OrderBy(e => e.QuestionOrder).FirstOrDefault(e => e.ExamId == examId && e.IsRandomQuestion == false && e.QuestionOrder > currentOrder);

                var questionRandomAnswerdCount = _context.ResponseQuestion.Count(e => e.ResponseId == model.ResponseID && e.Question.IsRandomQuestion == true);

                //سوالات عادی تمام شده و نیاز به سوال رندوم داریم
                if ((nextQuestion == null || nextQuestion.QuestionId == 0) && examRandNumber != 0)
                {
                    //انتخاب سوال تصادفی
                    var questionRandomAllId = _context.Question.Where(e => e.ExamId == examId && e.IsRandomQuestion == true && e.QuestionId != question.QuestionId).Select(e => e.QuestionId);
                    var questionRandomAnswerdId = _context.ResponseQuestion.Where(e => e.ResponseId == model.ResponseID && e.Question.IsRandomQuestion == true).Select(e => e.QuestionId);

                    var restQuestionRandomId = questionRandomAllId
                                       .Where(x => !questionRandomAnswerdId.Contains(x))
                                       .ToList();
                    if (restQuestionRandomId.Count != 0)
                    {
                        var random = new Random();
                        int index = random.Next(restQuestionRandomId.Count);
                        nextQuestion = _context.Question.FirstOrDefault(e => e.ExamId == examId && e.QuestionId == restQuestionRandomId[index]);
                    }
                }

                model.NextQuestionId = nextQuestion != null && nextQuestion.QuestionId != 0 ? nextQuestion.QuestionId : (int?)null;

                //اگر سوال اخر بود
                if (model.NextQuestionId == null || model.NextQuestionId == 0)
                {
                    response.FinishDateTime = DateTime.Now;
                    _context.Response.Update(response);
                    await _context.SaveChangesAsync();

                }

                //تعداد سوالات پاسخ داده شده و باقی مانده
                var responseAnswerd = _context.ResponseQuestion.Count(e => (e.ResponseValue != null && e.ResponseValue != "") && e.ResponseId == model.ResponseID);
                model.AnsweredQuestionCount = responseAnswerd;
                model.RestQuestionCount = model.QuestionCount - responseAnswerd;

                //جواب سوال جاری
                var responseQuestion = await _context.ResponseQuestion.FirstOrDefaultAsync(e => e.ResponseId == response.ResponseId && e.QuestionId == model.Question.QuestionId);
                model.ResponseQuestionID = responseQuestion?.ResponseQuestionId;
                model.ResponseValue = responseQuestion?.ResponseValue;

                return await Task.FromResult(model);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        //ذخیره پاسخ دهی
        public async Task<bool> SaveResponse(vm_ResponseByQuestionsAndChoices model)
        {
            try
            {
                Response response = await _context.Response.Where(e => e.ExamId == model.ExamId && e.UserId == model.UserId && e.ResponseId == model.ResponseID).Include(e => e.ResponseQuestion).FirstOrDefaultAsync();

                var resultValue = 0;
                var exitedQuestion = response.ResponseQuestion.Where(e => e.QuestionId == model.Question.QuestionId).FirstOrDefault();
                if (exitedQuestion == null)
                {
                    var reponseQuestion = new ResponseQuestion()
                    {
                        ResponseId = (int)model.ResponseID,
                        QuestionId = model.Question.QuestionId,
                        ResponseValue = model.ResponseValue == null ? "" : model.ResponseValue
                    };
                    await _context.ResponseQuestion.AddAsync(reponseQuestion);
                    resultValue = await _context.SaveChangesAsync();
                }
                else
                {
                    exitedQuestion.ResponseValue = model.ResponseValue == null ? "" : model.ResponseValue;
                    _context.ResponseQuestion.Update(exitedQuestion);
                    resultValue = await _context.SaveChangesAsync();
                }
                if (resultValue > 0)
                {


                    return true;
                }
                else
                {

                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        //نمایش سوالات در نمره دهی
        public async Task<vm_ScoreResponseByQuestionsAndChoices> GetExamByQuestionForScore(int responseId, int userId, int? questionId)
        {
            try
            {
                var model = new vm_ScoreResponseByQuestionsAndChoices();
                Response response = await _context.Response.FirstOrDefaultAsync(e => responseId == responseId && e.UserId == userId);
                if (response != null)
                {

                    model.ResponseID = response.ResponseId;
                    model.ResponseScore = response.ResponseScore;
                    model.ResponseDescriptiveID = response.ResponseDescriptiveId;
                    model.TeacherComment = response.TeacherComment;


                    var examId = response.ExamId;
                    var exam = await _context.Exam.Where(e => e.ExamId == examId).Include(e => e.ScoreType).FirstOrDefaultAsync();

                    //update time
                    model.ResponseID = response.ResponseId;
                    model.ExamId = exam.ExamId;
                    model.CompanyId = exam.CompanyId;
                    model.UserId = userId;
                    model.ExamTitle = exam.ExamTitle;
                    model.ExamDescription = exam.ExamDescription;
                    model.HasNumberScore = exam.ScoreType.IsNumber;

                    model.QuestionCount = _context.Question.Count(e => e.ExamId == examId);

                    var currentOrder = 0;
                    var question = new Question();
                    if (questionId != null && questionId != 0)
                    {
                        question = _context.Question.FirstOrDefault(e => e.ExamId == examId && e.QuestionId == questionId);
                    }
                    else
                    {
                        question = _context.Question.Where(e => e.ExamId == examId).OrderBy(e => e.QuestionOrder).Include(e => e.QuestionChoice).FirstOrDefault();
                    }
                    model.Question = question;
                    currentOrder = question.QuestionOrder;


                    var nextQuestion = _context.Question.FirstOrDefault(e => e.ExamId == examId && e.QuestionOrder > currentOrder);
                    model.NextQuestionId = nextQuestion != null ? nextQuestion.QuestionId : (int?)null;

                    var responseAnswerd = _context.ResponseQuestion.Count(e => (e.ResponseValue != null && e.ResponseValue != "") && e.ResponseId == model.ResponseID);
                    model.AnsweredQuestionCount = responseAnswerd;
                    model.RestQuestionCount = model.QuestionCount - responseAnswerd;

                    var responseQuestion = await _context.ResponseQuestion.FirstOrDefaultAsync(e => e.ResponseId == response.ResponseId && e.QuestionId == model.Question.QuestionId);
                    model.ResponseQuestionID = responseQuestion?.ResponseQuestionId;
                    model.ResponseValue = responseQuestion?.ResponseValue;


                    model.ResponseQuestionScore = responseQuestion?.ResponseQuestionScore;
                    model.QuestionTeacherComment = responseQuestion?.QuestionTeacherComment;

                    if (model.HasNumberScore)
                    {
                        var responseScoreCaclulate = _context.ResponseQuestion.Where(e => e.ResponseQuestionScore != null && e.ResponseId == model.ResponseID).Sum(e => e.ResponseQuestionScore);
                        model.ComputingResponseScore = responseScoreCaclulate;
                    }

                    model.ScoreTypes = _context.ScoreTypeDetail.Where(e => e.ScoreTypeId == exam.ScoreTypeId).ToList();

                    var user = _context.User.FirstOrDefault(e => e.UserId == model.UserId);
                    model.FullName = user.FirstName + " " + user.LastName;
                    model.PicName = user.PicName;
                    model.DegreeTitle = _context.Degree.FirstOrDefault(e => e.DegreeId == user.DegreeId).DegreeTitle;
                    model.GradeTitle = _context.Grade.FirstOrDefault(e => e.GradeId == user.GradeId).GradeTitle;
                }
                else
                {
                    model.ErrorMsg = "کد اشتباه است";
                    model.Status = true;

                }
                return await Task.FromResult(model);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }



        //ذخیره نمره دهی
        public async Task<bool> SaveScore(vm_ScoreResponseByQuestionsAndChoices model)
        {


            try
            {
                Response response = await _context.Response.Where(e => e.ExamId == model.ExamId && e.UserId == model.UserId && e.ResponseId == model.ResponseID).Include(e => e.ResponseQuestion).FirstOrDefaultAsync();

                var resultValue = 0;
                var exitedQuestion = response.ResponseQuestion.Where(e => e.QuestionId == model.Question.QuestionId).FirstOrDefault();
                if (exitedQuestion != null)
                {
                    exitedQuestion.ResponseQuestionScore = model.ResponseQuestionScore;
                    exitedQuestion.QuestionTeacherComment = model.QuestionTeacherComment;

                    _context.ResponseQuestion.Update(exitedQuestion);
                    resultValue = await _context.SaveChangesAsync();

                    if (resultValue > 0)
                    {

                        response.TeacherComment = model.TeacherComment;
                        response.ResponseScore = model.ResponseScore;
                        response.ResponseDescriptiveId = response.ResponseDescriptiveId;
                        _context.Response.Update(response);
                        resultValue = await _context.SaveChangesAsync();

                        return true;
                    }
                    else
                    {

                        return false;
                    }
                }
                else
                {

                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //نمایش لیست انواع نمره دهی
        public async Task<List<ScoreTypesDetails>> ScoreTypesDetailsShow()
        {
            try
            {
                //var model = await _context.ScoreType.Include(e => e.ScoreTypeDetail);

                var model = (from st in _context.ScoreType

                             select new ScoreTypesDetails
                             {
                                 ScoreTypeID = st.ScoreTypeId,
                                 ScoreTypeTitle = st.ScoreTypeTitle,
                                 IsNumber = st.IsNumber,
                                 SumScoreTypeDetailTitle = st.IsNumber != true ? String.Join(", ", st.ScoreTypeDetail.Select(n => n.ScoreTypeDetailTitle)) : st.ScoreTypeDetail.FirstOrDefault().ScoreStart.ToString() + "-" + st.ScoreTypeDetail.FirstOrDefault().ScoreFinish.ToString(),
                                 ScoreStart = st.ScoreTypeDetail.FirstOrDefault().ScoreStart,
                                 ScoreFinish = st.ScoreTypeDetail.FirstOrDefault().ScoreFinish,

                             }).ToList();

                return model;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<vm_ExamResponders> GetExamResponders(int examId)
        {
            try
            {
                var model = new vm_ExamResponders();

                model.Responses = await _context.ExamResponseAsync(examId);
                model.ExamInfo = await _context.ExamInfoAsync(examId);
                return model;
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool ExamSetScoreAuto(int examId)
        {
            try
            {
                _context.ExamSetScoreAuto(examId);
                return true;
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
//using (var context = new YourContext()) 
//            { 
//                using (var dbContextTransaction = context.Database.BeginTransaction()) 
//                { 
//                    try 
//                    { 
//                        //your db operations

//                        context.SaveChanges(); 

//                        dbContextTransaction.Commit(); 
//                    } 
//                    catch (Exception) 
//                    { 
//                        dbContextTransaction.Rollback(); 
//                    } 
//                } 
//            }

//using var context = new BloggingContext();
//using var transaction = context.Database.BeginTransaction();

//try
//{
//    context.Blogs.Add(new Blog { Url = "http://blogs.msdn.com/dotnet" });
//    context.SaveChanges();

//    context.Blogs.Add(new Blog { Url = "http://blogs.msdn.com/visualstudio" });
//    context.SaveChanges();

//    var blogs = context.Blogs
//        .OrderBy(b => b.Url)
//        .ToList();

//// Commit transaction if all commands succeed, transaction will auto-rollback
//// when disposed if either commands fails
//transaction.Commit();
//}
//catch (Exception)
//{
//    // TODO: Handle failure
//}
//https://docs.microsoft.com/en-us/ef/core/saving/transactions

//https://codehaks.com/video/1161/%D8%A2%D9%85%D9%88%D8%B2%D8%B4-%D9%85%D9%87%D8%A7%D8%AC%D8%B1%D8%AA-%D8%A7%D8%B2-%D8%AF%D8%A7%D8%AA-%D9%86%D8%AA-3-%D8%A8%D9%87-%D8%AF%D8%A7%D8%AA-%D9%86%D8%AA-5-%D8%AF%D8%B1-asp.net.html