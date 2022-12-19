using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
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
    public class CourseController : Controller
    {

        public IActionResult Index(string q, int CountRow = 10, int pagenumber = 1)
        {
            try
            {
                ViewData["Title"] = "درس ها";
                List<Course> courses = (new CourseOp()).GetCourses(q);
                ViewData["MaxRows"] = courses.Count();
                ViewData["CountRow"] = CountRow;

                int skip = (pagenumber - 1) * CountRow;
                courses = courses.Skip(skip).Take(CountRow).ToList();

                return View(courses);
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }

        }

        public IActionResult New(int? id, string returnUrl = "")
        {
            CourseOp courseOp = new CourseOp();            

            try
            {
                if (id == null)
                {
                    Course course = new Course();              
                    ViewData["returnUrl"] = returnUrl;
                    return View("CourseNew", course);
                }
                else
                {
                    Course course = courseOp.GetCourse(id.Value);
                    if (course != null)
                    {
                        ViewData["returnUrl"] = returnUrl;
                        return View("CourseNew", course);
                    }
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
        public async Task<IActionResult> Newed(Course course, string returnUrl = "")
        {
            if (course == null)
            {
                return Content(Need.Codes.GetResource("NoPersonFound"));
            }            

            CourseOp courseOp = new CourseOp();

            try
            {
                var courseExists = courseOp.GetCourse(course.CourseTitle, course.CourseId);
                if(courseExists!=null)
                {
                    return Content("نام درس تکراری هست");
                }

                if (course.CourseId == 0)
                {
                    string count = await courseOp.InsertCourse(course);
                    returnUrl = returnUrl.IndexOf("?") >= 0 ? returnUrl + "&msg=" + Need.Codes.GetResource("SuccessNew") : returnUrl + "?msg=" + Need.Codes.GetResource("SuccessNew");
                    returnUrl = returnUrl + "&m=Success";
                    return Content("ok," + returnUrl);
                }
                else
                {
                    Course CurrentCourse = courseOp.GetCourse(course.CourseId);
                    if (CurrentCourse != null)
                    {
                        CurrentCourse.CourseTitle = course.CourseTitle;
                        CurrentCourse.ReportCardCourseTitle = course.ReportCardCourseTitle;
                        
                        string count = await courseOp.UpdateCourse(CurrentCourse);
                        returnUrl = returnUrl.IndexOf("?") >= 0 ? returnUrl + "&msg=" + Need.Codes.GetResource("ChangeSuccess") : returnUrl + "?msg=" + Need.Codes.GetResource("ChangeSuccess");
                        returnUrl = returnUrl + "&m=Success";
                        return Content("ok," + returnUrl);
                    }
                    else
                    {
                        return Content(Need.Codes.GetResource("NoPersonFound"));
                    }


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
            CourseOp courseOp = new CourseOp();
            try
            {
                List<int> ids2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(ids);

                if (ids2.Count() <= 0)
                {
                    return Content(Need.Codes.GetResource("NoPersonFound"));
                }

                IEnumerable<Course> courses = courseOp.GetCourses(ids2);
                if (courses.Count() > 0)
                {
                    ViewData["returnUrl"] = returnUrl;
                    return View("CourseDelete", courses);
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
            CourseOp courseOp = new CourseOp();

            try
            {
                List<int> ids2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(ids);

                if (ids2.Count() <= 0)
                {
                    return Content(Need.Codes.GetResource("NoPersonFound"));
                }

                List<Course> courses = new List<Course>();
                foreach (var id in ids2)
                {
                    Course course = courseOp.GetCourse(id);
                    if (course != null)
                    {
                        courses.Add(course);
                    }
                }

                if (courses.Count() > 0)
                {
                    int count = await courseOp.DeleteCourses(courses);
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