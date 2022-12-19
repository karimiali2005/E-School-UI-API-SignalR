using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using APISms.Need;
using DatabaseLayer.Access;
using DatabaseLayer.Models;
using ESchool.Models;
using ESchool.Need;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ESchool.Areas.Panel.Controllers
{
    [Area("Panel")]
    [Authorize(AuthenticationSchemes = "AdminAuth")]
    public class HolidayController : Controller
    {
        public IActionResult Index(string msg = "", string m = "", int CountRow = 10, int pagenumber = 1)
        {
            try
            {
                CalendarOp calendarOp = new CalendarOp();

                ViewBag.Days = Need.SelectList.getDays();
                ViewBag.Months = Need.SelectList.getMonths();

                var calendars = calendarOp.GetCalendars("");
                int skip = (pagenumber - 1) * CountRow;
                calendars = calendars.Skip(skip).Take(CountRow).ToList();

                ViewData["MaxRows"] = calendars.Count();
                ViewData["CountRow"] = CountRow;
                
                return View("Index", calendars);
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index(string Day, string Month, string Year, string Comment)
        {
            try
            {
                if (!Checks.CheckPersianDate(Year, Month, Day))
                {
                    return Content(Need.Codes.GetResource("InvalidDate").Replace("N", "پایان"));
                }

                CalendarOp calendarOp = new CalendarOp();

                Calendar calendar = new Calendar();
                calendar.CalendarDate = Need.Codes.PersianDateToDate(Year, Month, Day).Value;
                calendar.CalendarComment = Comment;

                string r = await calendarOp.InsertCalendar(calendar);
                MessageModel messageModel = new MessageModel();
                if(r.StartsWith("Error"))
                {
                    string url = "/Panel/Holiday/Index?msg=" + Need.Codes.GetResource("Error");
                    url = url + "&m=Error";
                    return Content("ok," + url);
                }
                else
                {
                    string url = "/Panel/Holiday/Index?msg=" + Need.Codes.GetResource("SuccessAddHoliday");
                    url = url + "&m=Success";
                    return Content("ok," + url);
                }               

            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }

        }

        [HttpPost]
        public IActionResult Delete(string ids, string returnUrl = "")
        {
            CalendarOp calendarOp = new CalendarOp();
            try
            {
                List<int> ids2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(ids);
                if (ids2.Count() <= 0)
                {
                    return Content(Need.Codes.GetResource("NoPersonFound"));
                }

                IEnumerable<Calendar> calendars = calendarOp.GetCalendars(ids2);
                if (calendars.Count() > 0)
                {
                    ViewData["returnUrl"] = returnUrl;
                    return View("HolidayDelete", calendars);
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
        public async Task<IActionResult> Deleted(string ids, string returnUrl = "")
        {
            CalendarOp calendarOp = new CalendarOp();

            try
            {
                List<int> ids2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(ids);

                if (ids2.Count() <= 0)
                {
                    return Content(Need.Codes.GetResource("NoPersonFound"));
                }

                List<Calendar> calendars = new List<Calendar>();
                foreach (var id in ids2)
                {
                    Calendar calendar = calendarOp.GetCalendar(id);
                    if (calendar != null)
                    {
                        calendars.Add(calendar);
                    }
                }

                if (calendars.Count() > 0)
                {
                    int count = await calendarOp.DeleteCalendars(calendars);
                    string msg = Need.Codes.GetResource("ItemsDelete");
                    msg = string.Format(msg, count);
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