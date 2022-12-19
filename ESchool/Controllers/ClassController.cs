using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESchool.Models;
using Microsoft.AspNetCore.Mvc;
using DatabaseLayer.Models;
using DatabaseLayer.Access;
using System.IO;
using Microsoft.AspNetCore.Http;
using ESchool.Need;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Authorization;
using System.Drawing;
using System.Drawing.Imaging;

namespace ESchool.Controllers
{
    [Authorize(AuthenticationSchemes = "AccountAuth")]
    public class ClassController : BaseController
    {
        public IActionResult Index()
        {
            int UserId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
            int UserTypeId = Convert.ToInt32(this.User.FindFirst("UserTypeId").Value);
            
            RoomUserOp roomUserOp = new RoomUserOp();
            List<RoomUser> roomUsers = roomUserOp.GetRoomsUsers(UserId);

            ViewData["Title"] = "کلاس ها";

            return View(roomUsers);
        }

        public IActionResult Week(int id, int step = 0)
        {
           
            try
            {
                if (!IsRoomForUser(id))
                {
                    return Unauthorized();
                }

                DateTime dtStart = Need.Codes.GetLocalDateTime().StartOfWeek(DayOfWeek.Saturday);
                dtStart = dtStart.AddDays(step * 7);
                DateTime dtFinish = dtStart.AddDays(6);
                
                var room = (new RoomOp()).GetRoom(id);
                ViewData["previousStep"] = room.RoomStartDate.Date >= dtStart.Date ? -999999 : step - 1;
                ViewData["nextStep"] = room.RoomFinishDate.Date <= dtFinish.Date ? -999999 : step + 1;

                RoomDetailOp roomDetailOp = new RoomDetailOp();
                List<RoomDetailGrouped> roomDetailGroupeds = roomDetailOp.GetRoomDetailGrouped(id, dtStart, dtFinish);
                roomDetailGroupeds = roomDetailGroupeds.GroupBy(x => new { x.TimeStr, x.DayStr, x.CourseName, x.TeacherName, x.TeacherId, x.Comment, x.RoomId, x.RoomTypeId,x.CourseId })
                    .Select(p => new RoomDetailGrouped
                    {
                        TimeStr = p.Key.TimeStr,
                        DayStr = p.Key.DayStr,
                        CourseName = p.Key.CourseName,
                        TeacherName = p.Key.TeacherName,
                        TeacherId = p.Key.TeacherId,
                        Comment = p.Key.Comment,
                        RoomId = p.Key.RoomId,
                        CourseId = p.Key.CourseId,
                        RoomTypeId = p.Key.RoomTypeId,
                    }).ToList();

                ViewBag.StartHours = Need.SelectList.getHours();
                ViewBag.StartMinutes = Need.SelectList.getMinutes();
                ViewBag.FinishHours = Need.SelectList.getHours();
                ViewBag.FinishMinutes = Need.SelectList.getMinutes();

                CalendarOp calendarOp = new CalendarOp();
                List<Calendar> calendars = calendarOp.GetCalendars(dtStart, dtFinish);
                ViewBag.Holidays = calendars;

                var url = "/Class/Week/?id=" + id;
                if(step != 0)
                {
                    url += "&step=" + step;
                }
                ViewData["url"] = url;
                ViewData["showNavigator"] = true;
                ViewData["Title"] = " برنامه " + room.RoomType.RoomTypeTitle + " " + room.RoomTitle + " از " + Need.Codes.getPersianDay(dtStart) + " " + Need.Codes.getPersianDate(dtStart) + " تا " + Need.Codes.getPersianDay(dtFinish) + " " + Need.Codes.getPersianDate(dtFinish);
                ViewData["Titr"] =Need.Codes.getPersianDate(dtStart) + " تا " + Need.Codes.getPersianDate(dtFinish);
                return View("Weekly", roomDetailGroupeds);
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult SessionShow(int id, string Day, string StartHour, string StartMinute, string FinishHour, string FinishMinute, string returnUrl = "")
        {
            RoomDetailOp roomDetailOp = new RoomDetailOp();
            RoomOp roomOp = new RoomOp();            
            try
            {
                var room = roomOp.GetRoom(id);
                if (room == null)
                {
                    return Content(Need.Codes.GetResource("NoRoomFound"));
                }

                ViewData["RoomId"] = id;
                List<DateTime> Datetimes = new List<DateTime>();
                DayOfWeek day_of_week = Need.Codes.getDayofWeek(Day);
                DateTime StartDate = room.RoomStartDate.Date;
                DateTime FinishDate = room.RoomFinishDate.Date;
                for (DateTime dt = StartDate.Date; dt.Date.CompareTo(FinishDate.Date) <= 0; dt = dt.AddDays(1).Date)
                {
                    if (dt.DayOfWeek == day_of_week)
                    {
                        Datetimes.Add(dt);
                    }
                }

                List<RoomDetail> roomDetails = roomDetailOp.GetRoomDetails(id, StartHour + ":" + StartMinute, FinishHour + ":" + FinishMinute, Datetimes).ToList();
                if (roomDetails.Count() > 0)
                {
                    CalendarOp calendarOp = new CalendarOp();
                    List<Calendar> calendars = calendarOp.GetCalendars(room.RoomStartDate, room.RoomFinishDate, day_of_week);
                    ViewBag.Holidays = calendars;
                    foreach (Calendar calendar in calendars)
                    {
                        var Exist = roomDetails.Any(p => p.RoomDetailDate.Date == calendar.CalendarDate.Date);
                        if (!Exist)
                        {
                            roomDetails.Add(new RoomDetail { RoomDetailDate = calendar.CalendarDate, Comment = "", Room = room, RoomId = room.RoomId, RoomDetailTimeStart = null, RoomDetailTimeFinish = null, UserId = null, CourseId = null });
                        }
                    }
                    ViewData["Title"] = " برنامه های " + Day + " از ساعت " + StartHour + ":" + StartMinute + " تا " + FinishHour + ":" + FinishMinute;
                    ViewData["returnUrl"] = returnUrl;
                    return View("SessionShow", roomDetails.OrderBy(p => p.RoomDetailDate).ThenBy(p => p.RoomDetailTimeStart));
                }
                else
                {
                    return Content(Need.Codes.GetResource("NoRoomDetailFound"));
                }
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }


        [HttpPost]
        public IActionResult LessonShow(int id, string Lesson, string returnUrl = "")
        {
            RoomDetailOp roomDetailOp = new RoomDetailOp();
            RoomOp roomOp = new RoomOp();
            try
            {
                var room = roomOp.GetRoom(id);
                if (room == null)
                {
                    return Content(Need.Codes.GetResource("NoRoomFound"));
                }

                ViewData["RoomId"] = id;
                
                List<RoomDetail> roomDetails = roomDetailOp.GetRoomDetails(id, Lesson).ToList();
                if (roomDetails.Count() > 0)
                {
                    CalendarOp calendarOp = new CalendarOp();
                    List<Calendar> calendars = calendarOp.GetCalendars(room.RoomStartDate, room.RoomFinishDate);
                    ViewBag.Holidays = calendars;         
                    
                    ViewData["Title"] = " برنامه های درس " + Lesson + " در کلاس " + room.RoomTitle;
                    ViewData["returnUrl"] = returnUrl;
                    return View("SessionShow", roomDetails.OrderBy(p => p.RoomDetailDate).ThenBy(p => p.RoomDetailTimeStart));
                }
                else
                {
                    return Content(Need.Codes.GetResource("NoRoomDetailFound"));
                }
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }


        [HttpPost]
        public IActionResult UserShow(int id, string returnUrl = "")
        {
            RoomUserOp roomUserOp = new RoomUserOp();
            RoomOp roomOp = new RoomOp();
            try
            {
                var room = roomOp.GetRoom(id);
                if (room == null)
                {
                    return Content(Need.Codes.GetResource("NoRoomFound"));
                }

                ViewData["RoomId"] = id;

                List<RoomUser> roomUsers = roomUserOp.GetRoomUsers(id).ToList();
                if (roomUsers.Count() > 0)
                {
                    ViewData["Title"] = " دانش آموزان/اعضاء " + " کلاس " + room.RoomTitle;
                    ViewData["returnUrl"] = returnUrl;
                    return View("UserShow", roomUsers);
                }
                else
                {
                    return Content(Need.Codes.GetResource("NoRoomFound"));
                }
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }

        public IActionResult Teacher()
        {
            try
            {
                var UserId = Convert.ToInt32(this.User.FindFirst("UserId").Value);

                UserOp userOp = new UserOp();
                var teacher = userOp.GetUser(UserId);
                
                RoomDetailOp roomDetailOp = new RoomDetailOp();
                List<RoomDetailGrouped> roomDetailGroupeds = roomDetailOp.GetRoomDetailGroupedTeacher(UserId);
                roomDetailGroupeds = roomDetailGroupeds.GroupBy(x => new { x.TimeStr, x.DayStr, x.CourseName, x.TeacherName, x.TeacherId, x.Comment, x.RoomId, x.RoomTypeId,x.CourseId })
                    .Select(p => new RoomDetailGrouped
                    {
                        TimeStr = p.Key.TimeStr,
                        DayStr = p.Key.DayStr,
                        CourseName = p.Key.CourseName,
                        TeacherName = p.Key.TeacherName,
                        TeacherId = p.Key.TeacherId,
                        CourseId = p.Key.CourseId,
                        Comment = p.Key.Comment,         
                        RoomId = p.Key.RoomId,
                        RoomTypeId = p.Key.RoomTypeId,
                    }).ToList();

                ViewBag.StartHours = Need.SelectList.getHours();
                ViewBag.StartMinutes = Need.SelectList.getMinutes();
                ViewBag.FinishHours = Need.SelectList.getHours();
                ViewBag.FinishMinutes = Need.SelectList.getMinutes();

                ViewData["url"] = "/Class/Teacher";
                ViewData["Title"] = " برنامه معلم " + teacher.FirstName + " " + teacher.LastName;
                return View("Weekly", roomDetailGroupeds);
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }


        public IActionResult Edit(int id, int roomid, string returnUrl = "")
        {
            if (!IsRoomForTeacher(roomid))
            {
                return Unauthorized();
            }

            string Title = "برنامه جدید";

            try
            {
                var room = (new RoomOp()).GetRoom(roomid);                

                if (id > 0)
                {
                    RoomDetailOp roomDetailOp = new RoomDetailOp();
                    var roomDetail = roomDetailOp.GetRoomDetail(id);
                    if (roomDetail == null)
                    {
                        return Content(Need.Codes.GetResource("NoRoomDetailFound"));
                    }
                    else
                    {
                        Title = " درج توضیحات " + room.RoomType.RoomTypeTitle + " در روز " + Need.Codes.getPersianDay(roomDetail.RoomDetailDate) + " مورخ "
                            + Need.Codes.getPersianDate(roomDetail.RoomDetailDate) + " از ساعت " + roomDetail.RoomDetailTimeStart + " تا ساعت " + roomDetail.RoomDetailTimeFinish;
                        ViewData["Title"] = Title;
                        ViewData["returnUrl"] = returnUrl;
                        return View("RoomDetailEdit", roomDetail);
                    }
                }
                else
                {
                    return Content(Need.Codes.GetResource("NoRoomDetailFound"));
                }
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }

        public async Task<IActionResult> Edited(int id, int roomid, string Comment, string returnUrl = "")
        {
            if (!IsRoomForTeacher(roomid))
            {
                return Unauthorized();
            }

            try
            {
                var room = (new RoomOp()).GetRoom(roomid);

                if (id > 0)
                {
                    RoomDetailOp roomDetailOp = new RoomDetailOp();
                    var roomDetail = roomDetailOp.GetRoomDetail(id);
                    if (roomDetail == null)
                    {
                        return Content(Need.Codes.GetResource("NoRoomDetailFound"));
                    }
                    else
                    {
                        roomDetail.Comment = Comment;
                        await roomDetailOp.UpdateRoomDetail(roomDetail);
                        return Redirect(returnUrl);
                    }
                }
                else
                {
                    return Content(Need.Codes.GetResource("NoRoomDetailFound"));
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