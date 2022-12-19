using System.Linq;
using System.Threading.Tasks;
using EsServiceCore.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Es.DataLayerCore.Model;
using System;
using System.Collections.Generic;
using Es.DataLayerCore.DTOs.ReportCard;

namespace ESchool.Areas.Panel.Controllers
{
    [Area("Panel")]
    [Authorize(AuthenticationSchemes = "AdminAuth")]
    public class ReportCardController : Controller
    {
        private readonly IReportCardService _reportCardService;

        public ReportCardController(IReportCardService reportCardService)
        {
            _reportCardService = reportCardService;
        }
        public async Task<IActionResult> Index(string q, int CountRow = 10, int pagenumber = 1)
        {
            var result = await _reportCardService.ReportCardShowAll(q);
            ViewData["MaxRows"] = result.Count();
            ViewData["CountRow"] = CountRow;

            int skip = (pagenumber - 1) * CountRow;
            result = result.Skip(skip).Take(CountRow).ToList();
            return View(result);
        }

        public async Task<IActionResult> New(string returnUrl = "")
        {
            try
            {
                ReportCard reportCard = new ReportCard();
                ViewData["returnUrl"] = returnUrl;
                ViewBag.Grades = (List<Grade>)await _reportCardService.GradeShowAll();
                ViewBag.ScoreTypes = (List<ScoreType>)await _reportCardService.ScoreTypeShowAll();

                string Day = "01";
                string Month = "01";
                string Year = "";

                ViewBag.DaysStart = Need.SelectList.getDays(Day);
                ViewBag.MonthsStart = Need.SelectList.getMonths(Month);


                ViewBag.DaysFinish = Need.SelectList.getDays(Day);
                ViewBag.MonthsFinish = Need.SelectList.getMonths(Month);

                ViewBag.ReportCardShowUser = Need.SelectList.getBoolean();
                ViewBag.ReportCardScoreEnable = Need.SelectList.getBoolean();
                ViewBag.ReportCardAuto = Need.SelectList.getBoolean();
             

                return View("ReportCardNew", reportCard);

            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Newed(ReportCard reportCard, string StartDay, string StartMonth, string StartYear, string FinishDay, string FinishMonth, string FinishYear, string returnUrl = "")
        {
            if (reportCard == null)
            {
                return Content(Need.Codes.GetResource("NoPersonFound"));
            }



            try
            {

                reportCard.ReportCardDateTimeStart = Need.Codes.PersianDateToDate(StartYear, StartMonth, StartDay).Value;
                reportCard.ReportCardDateTimeFinish = Need.Codes.PersianDateToDate(FinishYear, FinishMonth, FinishDay).Value;

                await _reportCardService.ReportCardInsert(reportCard);
                returnUrl = returnUrl.IndexOf("?") >= 0 ? returnUrl + "&msg=" + Need.Codes.GetResource("SuccessNew") : returnUrl + "?msg=" + Need.Codes.GetResource("SuccessNew");
                returnUrl = returnUrl + "&m=Success";
                return Content("ok," + returnUrl);


            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }


        public async Task<IActionResult> Edit(int? id, string returnUrl = "")
        {


            try
            {
                ReportCard reportCard = await _reportCardService.ReportCardGetById(id.Value);

                ViewBag.Grades = (List<Grade>)await _reportCardService.GradeShowAll();
                ViewBag.ScoreTypes = (List<ScoreType>)await _reportCardService.ScoreTypeShowAll();

                


                string Day = "01";
                string Month = "01";
                string Year = "";

                if (reportCard.ReportCardDateTimeStart != null)
                {
                    string[] persiandate = Need.Codes.getPersianDate(reportCard.ReportCardDateTimeStart).Split("/");
                    Day = persiandate[2];
                    Month = persiandate[1];
                    Year = persiandate[0];
                    ViewData["StartYear"] = Year;
                }

                ViewBag.DaysStart = Need.SelectList.getDays(Day);
                ViewBag.MonthsStart = Need.SelectList.getMonths(Month);


                if (reportCard.ReportCardDateTimeFinish != null)
                {
                    string[] persiandate2 = Need.Codes.getPersianDate(reportCard.ReportCardDateTimeFinish).Split("/");
                    Day = persiandate2[2];
                    Month = persiandate2[1];
                    Year = persiandate2[0];
                    ViewData["FinishYear"] = Year;
                }
                ViewBag.DaysFinish = Need.SelectList.getDays(Day);
                ViewBag.MonthsFinish = Need.SelectList.getMonths(Month);


                ViewBag.ReportCardShowUser = Need.SelectList.getBoolean(reportCard.ReportCardShowUser.ToString());
                ViewBag.ReportCardScoreEnable = Need.SelectList.getBoolean(reportCard.ReportCardScoreEnable.ToString());
                ViewBag.ReportCardAuto = Need.SelectList.getBoolean(reportCard.ReportCardAuto.ToString());

                if (reportCard != null)
                {
                    ViewData["returnUrl"] = returnUrl;
                    return View("ReportCardEdit", reportCard);
                }


                return Content(Need.Codes.GetResource("NoPersonFound"));
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Edited(ReportCard reportCard, string StartDay, string StartMonth, string StartYear, string FinishDay, string FinishMonth, string FinishYear, string returnUrl = "")
        {
            if (reportCard == null)
            {
                return Content(Need.Codes.GetResource("NoPersonFound"));
            }



            try
            {

                ReportCard CurrentReportCard = await _reportCardService.ReportCardGetById(reportCard.ReportCardId);
                if (CurrentReportCard != null)
                {
                    CurrentReportCard.ReportCardTitle = reportCard.ReportCardTitle;
                    CurrentReportCard.GradeId = reportCard.GradeId;
                    CurrentReportCard.ReportCardDateTimeStart = Need.Codes.PersianDateToDate(StartYear, StartMonth, StartDay).Value;
                    CurrentReportCard.ReportCardDateTimeFinish = Need.Codes.PersianDateToDate(FinishYear, FinishMonth, FinishDay).Value; ;
                    CurrentReportCard.ScoreTypeId = reportCard.ScoreTypeId;
                    CurrentReportCard.ReportCardShowUser = reportCard.ReportCardShowUser;
                    CurrentReportCard.ReportCardScoreEnable = reportCard.ReportCardScoreEnable;
                    CurrentReportCard.ReportCardAuto = reportCard.ReportCardAuto;

                    await _reportCardService.ReportCardUpdate(CurrentReportCard);
                    returnUrl = returnUrl.IndexOf("?") >= 0 ? returnUrl + "&msg=" + Need.Codes.GetResource("ChangeSuccess") : returnUrl + "?msg=" + Need.Codes.GetResource("ChangeSuccess");
                    returnUrl = returnUrl + "&m=Success";
                    return Content("ok," + returnUrl);
                }
                else
                {
                    return Content(Need.Codes.GetResource("NoPersonFound"));
                }



            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }

        public async Task<IActionResult> ReportCardTeacherCourse(int id, int gradeId, string returnUrl = "")
        {
            try
            {
                var reportCardTeacherCourses = (List<ReportCardTeacherCourseShowResult>)await _reportCardService.ReportCardTeacherCourseShow(id);
               
                if (reportCardTeacherCourses != null)
                {
                    ViewData["returnUrl"] = returnUrl;
                    

                    ViewData["ReportCardID"] = id;
                    //ViewData["NickName"] = user.FirstName + " " + user.LastName;
                   
                    
                    ViewBag.RoomChatGroups = (List<RoomChatGroupByGradeIDResult>)await _reportCardService.RoomChatGroupByGradeID(gradeId);
                    ViewBag.Courses = (List<Course>)await _reportCardService.CourseShowAll();

                    return View("ReportCardTeacherCourseView", reportCardTeacherCourses);
                }
                else
                {
                    return Content(Need.Codes.GetResource("NoPersonFound"));
                }
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ReportCardTeacherCourse(int roomChatGroupId, int courseId, int id, string returnUrl = "")
        {
            try
            {

                var reportCard = await _reportCardService.ReportCardGetById(id);
                if (reportCard != null)
                {
                    ReportCardTeacherCourse reportCardTeacherCourse = new ReportCardTeacherCourse()
                    {
                        CourseId = courseId,
                        ReportCardId = id,
                        RoomChatGroupId = roomChatGroupId,
                    };



                    string url = "/Panel/ReportCard/ReportCardTeacherCourse?id=" + id + "&gradeId=" + reportCard.GradeId + "&returnUrl=" + returnUrl;

                    if (_reportCardService.HasReportCardTeacherCourse(id, roomChatGroupId, courseId))
                    {
                        url = url + "&msg=" + "این درس برای کارنامه " + reportCard.ReportCardTitle + " ثبت شده است";
                        url = url + "&m=Error";
                        return Redirect(url);
                    }


                    bool r = await _reportCardService.ReportCardTeacherCourseInsert(reportCardTeacherCourse);
                    if (r)
                    {

                        url = url + "&msg=" + Need.Codes.GetResource("SuccessAddCourse");
                        url = url + "&m=Success";
                    }
                    else
                    {
                        url = url + "&msg=" + Need.Codes.GetResource("Error");
                        url = url + "&m=Error";
                    }
                    url += "&course=" + courseId;
                    return Redirect(url);
                }
                else
                {
                    return Content(Need.Codes.GetResource("NoPersonFound"));
                }

            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }

        }

        [HttpPost]
        public async Task<IActionResult> ReportCardDetailDelete(int reportCardId, string ids, string returnUrl = "")
        {
           
            try
            {
                List<int> ids2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(ids);
                if (ids2.Count() <= 0)
                {
                    return Content(Need.Codes.GetResource("NoPersonFound"));
                }

                var teacherCourses = await _reportCardService.ReportCardTeacherCourseShow(reportCardId, ids2);
                if (teacherCourses.Count() > 0)
                {
                    ViewData["returnUrl"] = returnUrl;
                    return View("ReportCardDetailDelete", teacherCourses);
                }
                else
                {
                    return Content(Need.Codes.GetResource("NoPersonFound"));
                }
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }


        [HttpPost]
        public async Task<IActionResult> ReportCardDetailDeleted(string ids, string returnUrl = "")
        {
            

            try
            {
                List<int> ids2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(ids);

                if (ids2.Count() <= 0)
                {
                    return Content(Need.Codes.GetResource("NoPersonFound"));
                }

                

                if (ids2.Count() > 0)
                {
                    await _reportCardService.ReportCardTeacherCourseDelete(ids2);
                    string msg = Need.Codes.GetResource("ItemsDelete");
                    msg = string.Format(msg, ids2.Count());
                    returnUrl = returnUrl.IndexOf("?") >= 0 ? returnUrl + "&msg=" + msg : returnUrl + "?msg=" + msg;
                    returnUrl = returnUrl + "&m=Success";
                    return Redirect(returnUrl);
                }
                else
                {
                    return Content(Need.Codes.GetResource("NoPersonFound"));
                }
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }

        }
    }
}