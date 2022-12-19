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
using DocumentFormat.OpenXml.Spreadsheet;
using ESchool.Models;
using ESchool.Need;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Codes = ESchool.Need.Codes;

namespace ESchool.Areas.Panel.Controllers
{
    [Area("Panel")]
    [Authorize(AuthenticationSchemes = "AdminAuth")]
    public class ParentController : Controller
    {
        public IActionResult Index(string q, int CountRow = 10, int pagenumber = 1)
        {
            try
            {
                ViewData["Title"] = "والدین";
                List<User> users = (new UserOp()).GetUsers(q, new List<int> { 2, 3 });
                ViewData["MaxRows"] = users.Count();
                ViewData["CountRow"] = CountRow;
                int skip = (pagenumber - 1) * CountRow;                
                return View(users.Skip(skip).Take(CountRow).ToList());
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
            ViewBag.ParentStatuses = Need.SelectList.getParentStatuses();
            ViewBag.Educations = Need.SelectList.getEducations();
            ViewBag.UserTypes = Need.SelectList.getUserTypes();

            try
            {
                if (id == null)
                {
                    User user = new User();
                    ViewBag.Days = Need.SelectList.getDays();
                    ViewBag.Months = Need.SelectList.getMonths();
                    ViewData["returnUrl"] = returnUrl;
                    return View("ParentNew", user);
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
                        return View("ParentNew", user);
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
                return Content(Need.Codes.GetResource("InvalidIrNational").Replace("N", "ولی"));
            }
            if (!Checks.CheckMobile(user.MobileNumber))
            {
                return Content(Need.Codes.GetResource("InvalidMobileNumber").Replace("N", "ولی"));
            }
            if (!Checks.CheckPersianDate(BirthYear, BirthMonth, BirthDay))
            {
                return Content(Need.Codes.GetResource("InvalidBirthDate").Replace("N", "ولی"));
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
                        //CurrentUser.LivePlace = user.LivePlace;
                        //CurrentUser.LivePlaceOther = user.LivePlaceOther;
                        CurrentUser.Telephone = user.Telephone;
                        CurrentUser.MobileNumber = user.MobileNumber;
                        CurrentUser.Address = user.Address;
                        CurrentUser.UserTypeId = user.UserTypeId;
                        CurrentUser.ParentJob = user.ParentJob;
                        CurrentUser.ParentStatusId = user.ParentStatusId;
                        CurrentUser.ParentDegree = user.ParentDegree;

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


    }
}