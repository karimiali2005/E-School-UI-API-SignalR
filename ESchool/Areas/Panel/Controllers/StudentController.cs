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
using EsServiceCore.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Codes = ESchool.Need.Codes;

namespace ESchool.Areas.Panel.Controllers
{
    [Area("Panel")]
    [Authorize(AuthenticationSchemes = "AdminAuth")]
    public class StudentController : Controller
    {
        private readonly IFinancialService _financialService;
        private readonly IDisciplineService _disciplineService;
        public StudentController(IFinancialService financialService, IDisciplineService disciplineService)
        {
            _financialService = financialService;
            _disciplineService = disciplineService;
        }
        public IActionResult Index(int degree, int grade, int studyField, string q, int CountRow = 10, int pagenumber = 1)
        {
            try
            {
                ViewData["Title"] = "دانش آموزان";
                List<User> users = (new UserOp()).GetUsers(degree, grade, studyField, q);
                ViewData["MaxRows"] = users.Count();
                ViewData["CountRow"] = CountRow;

                UserProperties userProperties = new UserProperties();
                userProperties.Degrees = (new DegreeOp()).GetDegrees();
                userProperties.Grades = (new GradeOp()).GetGrades();
                userProperties.StudyFields = (new StudyFieldOp()).GetStudyFields();
                userProperties.ParentStatuses = (new ParentStatusOp()).GetParentStatuss();
                int skip = (pagenumber - 1) * CountRow;
                userProperties.Users = users.Skip(skip).Take(CountRow).ToList();

                return View(userProperties);
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }          
            
        }

        

        public IActionResult Degree(int id, string returnUrl = "")
        {
            try
            {
                User user = (new UserOp()).GetUser(id);
                if(user != null)
                {
                    UserProperties userProperties = new UserProperties();
                    userProperties.Degrees = (new DegreeOp()).GetDegrees();
                    userProperties.Grades = (new GradeOp()).GetGrades();
                    userProperties.StudyFields = (new StudyFieldOp()).GetStudyFields();
                    userProperties.ParentStatuses = (new ParentStatusOp()).GetParentStatuss();
                    userProperties.Users = (new UserOp()).GetUsers(user.UserId);                    
                    ViewData["returnUrl"] = returnUrl;
                    return View("Degree", userProperties);
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
        public async Task<IActionResult> Degree(int degree, int grade, int studyField, int id, string returnUrl = "")
        {
            try
            {
                UserOp userOp = new UserOp();
                User user = userOp.GetUser(id);
                if (user != null)
                {
                    if(degree > 0)
                    {
                        user.DegreeId = degree;
                    }
                    else
                    {
                        user.DegreeId = null;
                    }
                    if (grade > 0)
                    {
                        user.GradeId = grade;
                    }
                    else
                    {
                        user.GradeId = null;
                    }
                    if (studyField > 0)
                    {
                        user.StudyFieldId = studyField;
                    }
                    else
                    {
                        user.StudyFieldId = null;
                    }
                    await userOp.UpdateUser(user);
                    returnUrl = returnUrl.IndexOf("?") >= 0 ? returnUrl + "&msg=" + Need.Codes.GetResource("ChangeSuccess") : returnUrl + "?msg=" + Need.Codes.GetResource("ChangeSuccess");
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


        public IActionResult ExtraInfo(int id, string returnUrl = "")
        {
            try
            {
                User user = (new UserOp()).GetUser(id);
                if (user != null)
                {
                    ViewData["returnUrl"] = returnUrl;
                    return View("ExtraInfo", user);
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
        public async Task<IActionResult> ExtraInfo(string Nationality, string Insurance, int FamilyNumber, int SeveralChildren, bool RightHanded, bool PersianLanguage, bool IsRelativesParents, bool IsStudentInsurance, bool PreschoolEducation, bool IsCityPlace, bool ReferralReasonNew, int id, string returnUrl = "")
        {
            try
            {
                UserOp userOp = new UserOp();
                User user = userOp.GetUser(id);
                if (user != null)
                {
                    user.Nationality = Nationality;
                    user.Insurance = Insurance;
                    user.FamilyNumber = FamilyNumber;
                    user.SeveralChildren = SeveralChildren;
                    user.RightHanded = RightHanded;
                    user.PersianLanguage = PersianLanguage;
                    user.IsRelativesParents = IsRelativesParents;
                    user.IsStudentInsurance = IsStudentInsurance;
                    user.PreschoolEducation = PreschoolEducation;
                    user.IsCityPlace = IsCityPlace;
                    user.ReferralReasonNew = ReferralReasonNew;                    
                    await userOp.UpdateUser(user);
                    returnUrl = returnUrl.IndexOf("?") >= 0 ? returnUrl + "&msg=" + Need.Codes.GetResource("ChangeSuccess") : returnUrl + "?msg=" + Need.Codes.GetResource("ChangeSuccess");
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

        public IActionResult Parent(int id, string returnUrl = "")
        {
            try
            {
                User user = (new UserOp()).GetUserWithParent(id);
                if (user != null)
                {
                    ViewData["returnUrl"] = returnUrl;
                    return View("Parent", user);
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
        public async Task<IActionResult> Parent(string IrnationalFather, string IrnationalMother, int id, string returnUrl = "")
        {
            try
            {
                UserOp userOp = new UserOp();
                User user = userOp.GetUserWithParent(id);
                if (user != null)
                {
                    var newUserFather = userOp.GetUser(IrnationalFather);
                    if(newUserFather != null)
                    {
                        user.UserIdfather = newUserFather.UserId;
                    }
                    else
                    {
                        string msg = Need.Codes.GetResource("InvalidIrNational");
                        msg = msg.Replace("N", "پدر");
                        returnUrl = returnUrl.IndexOf("?") >= 0 ? returnUrl + "&msg=" + msg : returnUrl + "?msg=" + msg;
                        returnUrl = returnUrl + "&m=Error";
                        return Redirect(returnUrl);
                    }

                    var newUserMother = userOp.GetUser(IrnationalMother);
                    if (newUserMother != null)
                    {
                        user.UserIdmother = newUserMother.UserId;
                    }
                    else
                    {
                        string msg = Need.Codes.GetResource("InvalidIrNational");
                        msg = msg.Replace("N", "مادر");
                        returnUrl = returnUrl.IndexOf("?") >= 0 ? returnUrl + "&msg=" + msg : returnUrl + "?msg=" + msg;
                        returnUrl = returnUrl + "&m=Error";
                        return Redirect(returnUrl);
                    }

                    await userOp.UpdateUser(user);
                    returnUrl = returnUrl.IndexOf("?") >= 0 ? returnUrl + "&msg=" + Need.Codes.GetResource("ChangeSuccess") : returnUrl + "?msg=" + Need.Codes.GetResource("ChangeSuccess");
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


        public IActionResult New(int? id, string returnUrl = "")
        {
            UserOp userOp = new UserOp();
            ViewBag.IdcardSeriesLetters = Need.SelectList.getIdcardSeriesLetters();            
            ViewBag.LivePlaces = Need.SelectList.getLivePlaces();

            try
            {
                if (id == null)
                {
                    User user = new User();
                    ViewBag.Days = Need.SelectList.getDays();
                    ViewBag.Months = Need.SelectList.getMonths();
                    ViewData["returnUrl"] = returnUrl;
                    return View("StudentNew", user);
                }
                else
                {
                    User user = userOp.GetUser(id.Value);
                    if (user != null)
                    {
                        string Day = "01";
                        string Month = "01";
                        string Year = "";
                        if (user.BirthDate != null)
                        {
                            string[] persiandate = Need.Codes.getPersianDate(user.BirthDate).Split("/");
                            Day = persiandate[2];
                            Month = persiandate[1];
                            Year = persiandate[0];
                        }
                        ViewBag.Days = Need.SelectList.getDays(Day);
                        ViewBag.Months = Need.SelectList.getMonths(Month);
                        ViewData["BirthYear"] = Year;
                        ViewData["returnUrl"] = returnUrl;
                        return View("StudentNew", user);
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
        public async Task<IActionResult> Newed(User user, string BirthDay, string BirthMonth, string BirthYear, IFormFile file, string returnUrl = "")
        {
            if (user == null)
            {
                return Content(Need.Codes.GetResource("NoPersonFound"));
            }

            if (!Checks.CheckCodelMeli(user.Irnational))
            {
                return Content(Need.Codes.GetResource("InvalidIrNational").Replace("N", "دانش آموز"));
            }
            if (!Checks.CheckMobile(user.MobileNumber))
            {
                return Content(Need.Codes.GetResource("InvalidMobileNumber").Replace("N", "دانش آموز"));
            }
            if (!Checks.CheckPersianDate(BirthYear, BirthMonth, BirthDay))
            {
                return Content(Need.Codes.GetResource("InvalidBirthDate").Replace("N", "دانش آموز"));
            }

            /*byte[] pic = null;
            if (file != null)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    byte[] fileBytes = ms.ToArray();
                    pic = fileBytes;
                }
            }*/

            string picName = null;
            if (file != null)
            {
                var fileNameNew = Codes.GetFileNameNew(file.FileName);


                using (var output = new FileStream(Codes.GetPathStoreProfileImage(fileNameNew,false), FileMode.Create))
                {
                    await file.CopyToAsync(output);
                }

                Codes.ResizeImage(file, Codes.GetPathStoreProfileImage(fileNameNew, true));


                picName = fileNameNew;

                
            }

            UserOp userOp = new UserOp();            

            try
            {
                if (user.UserId == 0)
                {
                    User ExistUser = userOp.GetUser(user.Irnational);
                    if (ExistUser != null)
                    {
                        string m = Need.Codes.GetResource("CorrectIRNationalHasPerson").Replace("N", ExistUser.FirstName + " " + ExistUser.LastName);
                        return Content(m);
                    }

                    /*user.Pic = pic == null ? user.Pic : pic;
                    user.PicThumb = pic == null ? user.PicThumb : pic;*/
                    user.PicName = picName == null ? user.PicName : picName;
                    user.BirthDate = Need.Codes.PersianDateToDate(BirthYear, BirthMonth, BirthDay).Value;
                    user.UserTypeId = 1;
                    user.Password = "";
                    string count = await userOp.InsertUser(user);
                    returnUrl = returnUrl.IndexOf("?") >= 0 ? returnUrl + "&msg=" + Need.Codes.GetResource("SuccessRegister") : returnUrl + "?msg=" + Need.Codes.GetResource("SuccessRegister");
                    returnUrl = returnUrl + "&m=Success";
                    return Content("ok," + returnUrl);
                }
                else
                {
                    User CurrentUser = userOp.GetUser(user.UserId);
                    if(CurrentUser != null)
                    {
                        if (picName != null)
                        {
                            if (System.IO.File.Exists(Codes.GetPathStoreProfileImage(CurrentUser.PicName)))
                            {
                                System.IO.File.Delete(Codes.GetPathStoreProfileImage(CurrentUser.PicName));
                            }

                            if (System.IO.File.Exists(Codes.GetPathStoreProfileImage(CurrentUser.PicName, false)))
                            {
                                System.IO.File.Delete(Codes.GetPathStoreProfileImage(CurrentUser.PicName, false));
                            }
                        }
                        /*CurrentUser.Pic = pic == null ? CurrentUser.Pic : pic;
                        CurrentUser.PicThumb = pic == null ? CurrentUser.PicThumb : pic;*/
                        CurrentUser.PicName = picName == null ? CurrentUser.PicName : picName;
                        CurrentUser.BirthDate = Need.Codes.PersianDateToDate(BirthYear, BirthMonth, BirthDay).Value;
                        CurrentUser.Irnational = user.Irnational;
                        CurrentUser.FirstName = user.FirstName;
                        CurrentUser.LastName = user.LastName;
                        CurrentUser.IdcardSerial = user.IdcardSerial;
                        CurrentUser.IdcardSeriesLetter = user.IdcardSeriesLetter;
                        CurrentUser.IdcardSeriesNumber = user.IdcardSeriesNumber;
                        CurrentUser.IdcardPlace = user.IdcardPlace;
                        CurrentUser.BirthPlace = user.BirthPlace;
                        CurrentUser.LivePlace = user.LivePlace;
                        CurrentUser.LivePlaceOther = user.LivePlaceOther;
                        CurrentUser.Telephone = user.Telephone;
                        CurrentUser.MobileNumber = user.MobileNumber;
                        CurrentUser.Address = user.Address;

                        string count = await userOp.UpdateUser(CurrentUser);
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
        public async Task<IActionResult> StudentFinancial(int id, string returnUrl = "")
        {
            try
            {
                var financialShows = (List<Es.DataLayerCore.DTOs.Finacial.FinancialShowResult>)await _financialService.FinancialShow(id);

                if (financialShows != null)
                {
                    ViewData["returnUrl"] = returnUrl;


                    ViewData["UserID"] = id;
                    //ViewData["NickName"] = user.FirstName + " " + user.LastName;


                    ViewBag.FinancialSendType = (List<Es.DataLayerCore.Model.FinancialSendType>)await _financialService.FinancialSendTypeShow();


                    return View("StudentFinancialView", financialShows);
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
        public async Task<IActionResult> StudentFinancial(int financialSendTypeId, string financialText, int id, string returnUrl = "")
        {
            try
            {


                Es.DataLayerCore.Model.Financial financial = new Es.DataLayerCore.Model.Financial()
                {
                    UserId = id,
                    FinancialDate = DateTime.Now,
                    FinancialSendTypeId = financialSendTypeId,
                    FinancialText = financialText

                };



                string url = "/Panel/Student/StudentFinancial?id=" + id + "&returnUrl=" + returnUrl;



                bool r = await _financialService.FinancialInsert(financial);
                if (r)
                {

                    url = url + "&msg=" + Need.Codes.GetResource("SuccessAdd");
                    url = url + "&m=Success";
                }
                else
                {
                    url = url + "&msg=" + Need.Codes.GetResource("Error");
                    url = url + "&m=Error";
                }

                return Redirect(url);


            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }

        }

        [HttpPost]
        public async Task<IActionResult> StudentFinancialDelete(string ids, string returnUrl = "")
        {

            try
            {

                if (string.IsNullOrEmpty(ids))
                {
                    return Content(Need.Codes.GetResource("NoPersonFound"));
                }

                ViewData["ids"] = ids;
                ViewData["returnUrl"] = returnUrl;
                return View("StudentFinancialDelete");

            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }


        [HttpPost]
        public async Task<IActionResult> StudentFinancialDeleted(string ids, string returnUrl = "")
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
                    await _financialService.StudentFinancialDelete(ids2);
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


        public async Task<IActionResult> StudentDiscipline(int id, string returnUrl = "")
        {
            try
            {
                var disciplineShows = (List<Es.DataLayerCore.DTOs.Discipline.DisciplineShowResult>)await _disciplineService.DisciplineShow(id);

                if (disciplineShows != null)
                {
                    ViewData["returnUrl"] = returnUrl;


                    ViewData["UserID"] = id;
                    //ViewData["NickName"] = user.FirstName + " " + user.LastName;


                    ViewBag.DisciplineType = (List<Es.DataLayerCore.Model.DisciplineType>)await _disciplineService.DisciplineTypeShow();


                    return View("StudentDisciplineView", disciplineShows);
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
        public async Task<IActionResult> StudentDiscipline(int disciplineTypeId, string disciplineText, int id, string returnUrl = "")
        {
            try
            {


                Es.DataLayerCore.Model.Discipline discipline = new Es.DataLayerCore.Model.Discipline()
                {
                    UserId = id,
                    DisciplineDate = DateTime.Now,
                    DisciplineTypeId = disciplineTypeId,
                    DisciplineText = disciplineText

                };



                string url = "/Panel/Student/StudentDiscipline?id=" + id + "&returnUrl=" + returnUrl;



                bool r = await _disciplineService.DisciplineInsert(discipline);
                if (r)
                {

                    url = url + "&msg=" + Need.Codes.GetResource("SuccessAdd");
                    url = url + "&m=Success";
                }
                else
                {
                    url = url + "&msg=" + Need.Codes.GetResource("Error");
                    url = url + "&m=Error";
                }

                return Redirect(url);


            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }

        }

        [HttpPost]
        public async Task<IActionResult> StudentDisciplineDelete(string ids, string returnUrl = "")
        {

            try
            {

                if (string.IsNullOrEmpty(ids))
                {
                    return Content(Need.Codes.GetResource("NoPersonFound"));
                }

                ViewData["ids"] = ids;
                ViewData["returnUrl"] = returnUrl;
                return View("StudentDisciplineDelete");

            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }


        [HttpPost]
        public async Task<IActionResult> StudentDisciplineDeleted(string ids, string returnUrl = "")
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
                    await _disciplineService.StudentDisciplineDelete(ids2);
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
