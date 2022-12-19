using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Es.DataLayerCore.DTOs.ReportCard;
using Es.DataLayerCore.Model;
using ESchool.Need;
using EsServiceCore.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.Controllers
{
    [Authorize(AuthenticationSchemes = "AccountAuth")]
    public class ReportCardTeacherController : Controller
    {
        private readonly IReportCardService _reportCardService;

        public ReportCardTeacherController(IReportCardService reportCardService)
        {
            _reportCardService = reportCardService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> MangeReportCardTeacher(int id, string returnUrl = "/")
        {
            ViewBag.returnUrl = returnUrl;
            var userId = UserContext.GetClaimValueInteger(ClaimName.UserId);

            var courses = await _reportCardService.ReportCardTeacherCourseByRoomChatGroupID(id, userId);

            var reportCardTeacherView = new ReportCardTeacherViewModel();
            reportCardTeacherView.RoomChatGroupID = id;
            reportCardTeacherView.ReportCardTeacherCourseByRoomChatGroupIDResult = courses;
            if(courses.Count()==1)
            {
                ViewBag.RoomchatGroupId = id;
                ViewBag.CourseId = courses.FirstOrDefault().CourseID;
                ViewBag.CourseTitle = courses.FirstOrDefault().CourseTitle;
                reportCardTeacherView.ReportCardTeacherShowResult = await _reportCardService.ReportCardTeacherShow(id, userId, courses.FirstOrDefault().CourseID); 
            }
            else
            {
                reportCardTeacherView.ReportCardTeacherShowResult = null;
            }
            return View(reportCardTeacherView);
        }

        public async Task<IActionResult> ReportCardDetail(int roomChatGroupID, int courseId)
        {
            try
            {
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId);

                var reportCardTeachers=await _reportCardService.ReportCardTeacherShow(roomChatGroupID, userId, courseId);
                ViewBag.RoomchatGroupId = roomChatGroupID;
                ViewBag.CourseId = courseId;
                return View("ReportCardDetail", reportCardTeachers);
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }

        public async Task<IActionResult> ReportCardTeacherScore(int id, int roomchatGroupId, int courseId,int scoreTypeId, string returnUrl = "/")
        {
            ViewBag.returnUrl = returnUrl;

            var userId = UserContext.GetClaimValueInteger(ClaimName.UserId);

            var reportCardTeacherScores = await _reportCardService.ReportCardTeacherScoreShow(id, roomchatGroupId,userId,courseId);

            ViewBag.Descriptives = await _reportCardService.ScoreTypeDetailShow();

            ViewBag.IsDescriptives = scoreTypeId==1?false:true;
            ViewBag.ReportCardId = id;
            ViewBag.RoomchatGroupId = roomchatGroupId;
            ViewBag.CourseId = courseId;

            return View(reportCardTeacherScores);
        }
     
        [HttpPost]
        //int reportCardId, int roomChatGroupId, int courseId
        public async Task<IActionResult> ReportCardTeacherScoreSave([FromBody] List<ReportCardDetail> listreportCardDetails)
        {
            var userId = UserContext.GetClaimValueInteger(ClaimName.UserId);

           await _reportCardService.ReportCardTeacherScoreSave(listreportCardDetails, userId);
            return Json(new { returnVlaue = true });
        }


        public async Task<IActionResult> ReportCardTeacherFile(int id, int roomchatGroupId, string returnUrl = "/")
        {
            ViewBag.returnUrl = returnUrl;

            var userId = UserContext.GetClaimValueInteger(ClaimName.UserId);

            var reportCardPapers = await _reportCardService.ReportCardPaperShow(id, roomchatGroupId, userId);

          

           
            ViewBag.ReportCardId = id;
            ViewBag.RoomchatGroupId = roomchatGroupId;
        

            return View(reportCardPapers);

        }


        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
            {
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);
            }
            return filename;
        }


        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 2147483648)]
        [RequestSizeLimit(2147483648)]
        public async Task<IActionResult> ReportCardSendFile(IFormFile file,int id,int userId,int reportCardId)
        {
            try
            {
                var dateTimeNow = DateTime.Now;


                var address = SettingContext.PathStoreFiles.Instance.Address;
                var fullAddressFtp = address;

              
                fullAddressFtp += SettingContext.PathStoreFiles.Instance.ReportCard;
               



                string filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                filename = this.EnsureCorrectFilename(filename);

                string savefilename = Codes.GetRandomFileName(filename);
                var requestUri = fullAddressFtp + "Month" + dateTimeNow.ToString("yyyyMM") + "/" + savefilename;



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

             

                var reportCardPaper = new ReportCardPaper()
                {
                    ReportCardPaperId=id,
                    ReportCardId=reportCardId,
                    ReportCardPaperDateTime=DateTime.Now,
                    ReportCardPaperFileName= savefilename,
                    UserId=userId
                };




                await _reportCardService.ReportCardPaperSave(reportCardPaper);
                return Json(new { returnValue = true });
            }
            catch (Exception ex)
            {
                return Content("Error: " + ex.Message);
            }
        }


    }
}