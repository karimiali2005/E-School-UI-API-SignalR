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
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Authorization;
using System.Drawing;
using System.Drawing.Imaging;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ESchool.Controllers
{
    public class AccountController : Controller        
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (TempData["MessageModel"] != null)
            {
                string temp = TempData["MessageModel"] as string;
                MessageModel messageModel = JsonConvert.DeserializeObject<MessageModel>(temp);
                ViewBag.MessageModel = messageModel;
            }
        }

        public IActionResult Index(string returnUrl = "")
        {
            return RedirectToAction("Register");
        }
        public IActionResult Login(string returnUrl = "")
        {
           // ViewData["returnUrl"] = returnUrl;
            return Redirect("/Accounts/Login");
            //return View();
        }

        //public IActionResult Login(string returnUrl = "")
        //{
        //    ViewData["returnUrl"] = returnUrl;

        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Login(string loginUsername, string loginPassword, bool remeberme, string returnUrl = "")
        //{
        //    ViewData["returnUrl"] = returnUrl;
        //    ViewData["loginUsername"] = loginUsername;

        //    MessageModel messageModel = new MessageModel();

        //    UserOp userOp = new UserOp();
        //    UserTypeOp userTypeOp = new UserTypeOp();
        //    DatabaseLayer.Models.User userModel = userOp.Login(loginUsername, loginPassword);
        //    if(userModel != null)
        //    {
        //        if(userModel.UserActive == 0 || userModel.UserActive == 2)
        //        {
        //            string ReasonInactive = string.IsNullOrEmpty(userModel.ReasonInactive) ? "" : Convert.ToString(userModel.ReasonInactive);

        //            IList<Claim> claims = new List<Claim>();
        //            claims.Add(new Claim("UserId", userModel.UserId.ToString()));
        //            var Nickname = userModel.FirstName + " " + userModel.LastName;
        //            claims.Add(new Claim("Nickname", Nickname));
        //            claims.Add(new Claim("IrNational", userModel.Irnational));
        //            claims.Add(new Claim("BirthDate", Codes.getPersianDate(userModel.BirthDate)));
        //            claims.Add(new Claim("UserTypeId", userModel.UserTypeId.ToString()));
        //            var UserType = userTypeOp.GetUserTypeTitleWithId(userModel.UserTypeId);
        //            claims.Add(new Claim("UserType", UserType));
        //            claims.Add(new Claim("UserActive", Convert.ToString(userModel.UserActive)));
        //            claims.Add(new Claim("ReasonInactive", ReasonInactive));
        //            claims.Add(new Claim("MobileNumber", userModel.MobileNumber));
        //            claims.Add(new Claim("PicName", userModel.PicName??""));
        //            var identity = new ClaimsIdentity(claims, "AccountAuth");
        //            await HttpContext.SignInAsync("AccountAuth", new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = remeberme });
        //            if(userModel.UserActive == 2)
        //            {
        //                if (string.IsNullOrEmpty(userModel.Password))
        //                {
        //                    return Redirect("/Account/ChangePass/force");
        //                }
        //                if (string.IsNullOrEmpty(returnUrl))
        //                {
        //                    //return Redirect("/Account/Message/LoginSuccess");
        //                    messageModel.MessageType = "Success";
        //                    messageModel.MessageText = UserType + " گرامی، " + Nickname + " به پنل کاربری تان خوش آمدید ";
        //                    TempData["MessageModel"] = JsonConvert.SerializeObject(messageModel);
        //                    ViewBag.MessageModel = messageModel;
        //                    if(userModel.UserTypeId == 4)
        //                    {
        //                        return Redirect("/Class/Teacher");
        //                    }
        //                    else
        //                    {
        //                        return Redirect("/Class");
        //                    }
        //                }
        //                else
        //                {
        //                    return Redirect(returnUrl);
        //                }
        //            }
        //            else
        //            {
        //                messageModel.MessageType = "Warning";
        //                messageModel.MessageText = UserType + " گرامی، " + Nickname + " کاربری شما معلق شده است. به علت "  + "<br/>";
        //                messageModel.MessageText += ReasonInactive;
        //                TempData["MessageModel"] = JsonConvert.SerializeObject(messageModel);
        //                ViewBag.MessageModel = messageModel;
        //                return Redirect("/Account/MsgShow");
        //            }
        //        }
        //        else if(userModel.UserActive == 1)
        //        {
        //            return Redirect("/Account/Message/UserDeactive");
        //        }
        //    }

        //    return Redirect("/Account/Message/LoginInvalid");
        //}

        //public async Task<IActionResult> Logout()
        //{
        //    await HttpContext.SignOutAsync();
        //    return RedirectToAction("Login");
        //}

        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(string forgetPwdIRNational, string forgetPwdMobileNumber)
        {
            ViewData["irnational"] = forgetPwdIRNational;
            ViewData["mobilenumber"] = forgetPwdMobileNumber;

            MessageModel messageModel = new MessageModel();
            messageModel.MessageType = "Error";            

            try
            {
                if (!Checks.CheckCodelMeli(forgetPwdIRNational))
                {
                    messageModel.MessageText = Need.Codes.GetResource("InvalidIrNational").Replace("N", "");
                    ViewBag.MessageModel = messageModel;
                    TempData["MessageModel"] = JsonConvert.SerializeObject(messageModel);
                    return View("ForgetPassword");
                }
                if (!Checks.CheckMobile(forgetPwdMobileNumber))
                {
                    messageModel.MessageText = Need.Codes.GetResource("InvalidMobileNumber").Replace("N", "");
                    TempData["MessageModel"] = JsonConvert.SerializeObject(messageModel);
                    ViewBag.MessageModel = messageModel;
                    return View("ForgetPassword");
                }

                UserOp userOp = new UserOp();
                User user = userOp.GetUser(forgetPwdIRNational, forgetPwdMobileNumber);
                if (user == null)
                {
                    messageModel.MessageText = Need.Codes.GetResource("NoPersonFound").Replace("N", "");
                    ViewBag.MessageModel = messageModel;
                    TempData["MessageModel"] = JsonConvert.SerializeObject(messageModel);
                    return View("ForgetPassword");                    
                }

                APISms.SmsClass smsClass = new APISms.SmsClass();
                string code = Codes.CreateCode();
                HttpContext.Session.SetString("codeforgetpwd", code);
                string text = Codes.GetResource("SmsVerify");
                text = text.Replace("NL", System.Environment.NewLine);
                text = text.Replace("C", code);
                string[] mobiles = { forgetPwdMobileNumber };
                int result = await smsClass.SendAsync(text, mobiles);
                if(result == 1)
                {
                    ViewData["id"] = forgetPwdIRNational;
                    ViewData["Title"] = Codes.GetResource("SaveVerifyPassword");
                    TempData["MessageModel"] = null;
                    ViewBag.MessageModel = null;
                    return View("Verify");
                }
                else
                {
                    messageModel.MessageText = Need.Codes.GetResource("ImpossibleChangePassword");
                    ViewBag.MessageModel = messageModel;
                    TempData["MessageModel"] = JsonConvert.SerializeObject(messageModel);
                    return View("ForgetPassword");
                }
                
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Verify(string id, string verifyCode)
        {
            ViewData["id"] = id;
            ViewData["Title"] = Codes.GetResource("SaveVerifyPassword");

            MessageModel messageModel = new MessageModel();
            messageModel.MessageType = "Error";

            try
            {
                if (!Checks.CheckCodelMeli(id))
                {
                    messageModel.MessageText = Need.Codes.GetResource("InvalidIrNational").Replace("N", "");
                    TempData["MessageModel"] = JsonConvert.SerializeObject(messageModel);
                    ViewBag.MessageModel = messageModel;
                    return View("Verify");                    
                }

                var code = HttpContext.Session.GetString("codeforgetpwd");
                if (code != verifyCode)
                {
                    messageModel.MessageText = Need.Codes.GetResource("InvalidSmsVerify");
                    TempData["MessageModel"] = JsonConvert.SerializeObject(messageModel);
                    ViewBag.MessageModel = messageModel;
                    return View("Verify");                    
                }

                string token = Need.Codes.CreateCode(false, 11);
                UserOp userOp = new UserOp();
                bool r = await userOp.SetToken(id, token);
                if (r)
                {
                    ViewData["Title"] = "بازنشانی کلمه عبور";
                    ViewData["token"] = token;
                    ViewData["id"] = id;                    
                    TempData["MessageModel"] = null;
                    ViewBag.MessageModel = null;
                    return View("ChangePassword");
                }
                else
                {
                    messageModel.MessageText = Need.Codes.GetResource("ImpossibleSetToken");
                    TempData["MessageModel"] = JsonConvert.SerializeObject(messageModel);
                    return View("Verify");                    
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> ChangePassword(string chPwdPassword, string chPwdPassword2, string id, string token)
        {
            ViewData["Title"] = "بازنشانی کلمه عبور";

            MessageModel messageModel = new MessageModel();
            messageModel.MessageType = "Error";
            ViewData["token"] = token;
            ViewData["id"] = id;

            try
            {
                if (!Checks.CheckCodelMeli(id))
                {
                    string msg = Need.Codes.GetResource("InvalidIrNational").Replace("N", "");
                    messageModel.MessageText = msg;
                    TempData["MessageModel"] = JsonConvert.SerializeObject(messageModel);
                    ViewBag.MessageModel = messageModel;
                    return View("ChangePassword");                    
                }

                UserOp userOp = new UserOp();
                var token1 = userOp.GetToken(id);
                if (token != token1)
                {
                    messageModel.MessageText = Need.Codes.GetResource("InvalidToken");
                    TempData["MessageModel"] = JsonConvert.SerializeObject(messageModel);
                    ViewBag.MessageModel = messageModel;
                    return View("ChangePassword");
                }

                if (chPwdPassword != chPwdPassword2)
                {
                    messageModel.MessageText = Need.Codes.GetResource("NotMatchPassword");
                    TempData["MessageModel"] = JsonConvert.SerializeObject(messageModel);
                    ViewBag.MessageModel = messageModel;
                    return View("ChangePassword");
                }

                bool r = await userOp.ChangePassword(id, token, chPwdPassword);
                if(r)
                {
                    ViewData["Title"] = "اتمام بازنشانی کلمه عبور";
                    messageModel.MessageType = "Success";
                    messageModel.MessageText = Codes.GetResource("ChangePasswordSuccess");
                    TempData["MessageModel"] = JsonConvert.SerializeObject(messageModel);
                    ViewBag.MessageModel = messageModel;
                    return Redirect("/Account/Message");
                }
                else
                {
                    messageModel.MessageText = Need.Codes.GetResource("ChangePasswordFail");
                    TempData["MessageModel"] = JsonConvert.SerializeObject(messageModel);
                    ViewBag.MessageModel = messageModel;
                    return View("ChangePassword");
                }
                
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        public IActionResult MsgShow()
        {
            if (this.User.Identity.IsAuthenticated == true)
            {
                var UserActive = Convert.ToInt32(this.User.FindFirst("UserActive").Value);
                var UserTypeId = Convert.ToInt32(this.User.FindFirst("UserTypeId").Value);
                var Nickname = Convert.ToString(this.User.FindFirst("Nickname").Value);
                var UserType = Convert.ToString(this.User.FindFirst("UserType").Value);

                if (UserActive == 0)
                {
                    MessageModel messageModel = new MessageModel();
                    messageModel.MessageText = UserType + " گرامی، " + Nickname + " کاربری شما معلق شده است. به علت " + "<br/>";
                    messageModel.MessageText += this.User.FindFirst("ReasonInactive").Value;
                    messageModel.MessageType = "Warning";
                    ViewBag.MessageModel = messageModel;
                    TempData["MessageModel"] = JsonConvert.SerializeObject(messageModel);                    
                }
            }

            return View();
        }

        public IActionResult Message(string id)
        {
            MessageModel messageModel = new MessageModel();
            if (id == "RegisterSuccess")
            {
                ViewData["Title"] = "اتمام فرایند ثبت نام";
                
                messageModel.MessageType = "Success";
                messageModel.MessageText = Codes.GetResource("SuccessRegister");
                ViewBag.MessageModel = messageModel;
                TempData["MessageModel"] = JsonConvert.SerializeObject(messageModel);
                return View("RegisterMessage");
            }            
            else if (id == "UserDeactive")
            {
                ViewData["Title"] = "ورود ناموفق به پنل";

                messageModel.MessageType = "Error";
                messageModel.MessageText = Need.Codes.GetResource("UserIsDeactive");
                ViewBag.MessageModel = messageModel;
                TempData["MessageModel"] = JsonConvert.SerializeObject(messageModel);
                return View("LoginMessage");
            }            
            else if (id == "LoginInvalid")
            {
                ViewData["Title"] = "ورود ناموفق به پنل";
                messageModel.MessageType = "Error";
                messageModel.MessageText = Need.Codes.GetResource("InvalidUsernamePassword");
                ViewBag.MessageModel = messageModel;
                TempData["MessageModel"] = JsonConvert.SerializeObject(messageModel);
                return View("LoginMessage");
            }

            return View("Message");
        }

        public IActionResult Register()
        {
            UserProperties userProperties = new UserProperties();
            userProperties.Degrees = (new DegreeOp()).GetDegrees();
            userProperties.Grades= (new GradeOp()).GetGrades();
            userProperties.StudyFields = (new StudyFieldOp()).GetStudyFields();
            userProperties.ParentStatuses = (new ParentStatusOp()).GetParentStatuss();


            return View(userProperties);
        }

        [HttpPost]
        public IActionResult GetUserIrNational(string irnational)
        {
            if(!Checks.CheckCodelMeli(irnational))
            {
                return Content(Need.Codes.GetResource("InvalidIrNational").Replace("N", "وارد شده"));
            }

            DatabaseLayer.Access.UserOp userOp = new UserOp();
            User user = userOp.GetUser(irnational);
            if(user == null)
            {
                return Content(Codes.GetResource("CorrectIRNationalNoPerson"));
            }
            else
            {
                return Content("ok," + Codes.GetResource("CorrectIRNationalHasPerson").Replace("N", user.FirstName + " " + user.LastName));
            }
        }

        [HttpPost]        
        public async Task<IActionResult> Reg(UserRegister register)
        {
            try
            {
                if(!Checks.CheckCodelMeli(register.studentIRNational))
                {
                    return Content(Need.Codes.GetResource("InvalidIrNational").Replace("N", "دانش آموز"));
                }
                if (!Checks.CheckCodelMeli(register.fatherIRNational))
                {
                    return Content(Need.Codes.GetResource("InvalidIrNational").Replace("N", "پدر"));
                }
                if (!Checks.CheckCodelMeli(register.motherIRNational))
                {
                    return Content(Need.Codes.GetResource("InvalidIrNational").Replace("N", "مادر"));
                }
                if(!Checks.CheckMobile(register.studentMobileNumber))
                {
                    return Content(Need.Codes.GetResource("InvalidMobileNumber").Replace("N", "دانش آموز"));
                }
                if (!Checks.CheckMobile(register.fatherMobileNumber))
                {
                    return Content(Need.Codes.GetResource("InvalidMobileNumber").Replace("N", "پدر"));
                }
                if (!Checks.CheckMobile(register.motherMobileNumber))
                {
                    return Content(Need.Codes.GetResource("InvalidMobileNumber").Replace("N", "مادر"));
                }

                if (!Checks.CheckPersianDate(register.studentBirthYear, register.studentBirthMonth, register.studentBirthDay))
                {
                    return Content(Codes.GetResource("InvalidBirthDate").Replace("N", "دانش آموز"));
                }
                
                DatabaseLayer.Access.UserOp userOp = new UserOp();
                int FatherId = 0;
                int MotherId = 0;

                User existStudent = userOp.GetUser(register.studentIRNational);
                if (existStudent != null)
                {
                    return Content(Codes.GetResource("StudenIsExist"));
                }

                User existFather = userOp.GetUser(register.fatherIRNational);
                if (existFather != null)
                {
                    FatherId = existFather.UserId;
                }

                User existMother = userOp.GetUser(register.motherIRNational);
                if (existMother != null)
                {
                    MotherId = existMother.UserId;
                }
                
                if(FatherId == 0)
                {
                    if (!Checks.CheckPersianDate(register.fatherBirthYear, register.fatherBirthMonth, register.fatherBirthDay))
                    {
                        return Content(Codes.GetResource("InvalidBirthDate").Replace("N", "پدر"));
                    }
                }
                if(MotherId == 0)
                {
                    if (!Checks.CheckPersianDate(register.motherBirthYear, register.motherBirthMonth, register.motherBirthDay))
                    {
                        return Content(Codes.GetResource("InvalidBirthDate").Replace("N", "مادر"));
                    }
                }

                

                if(FatherId == 0)
                {
                    DatabaseLayer.Models.User userFather = new User();
                    userFather.UserTypeId = 2;
                    userFather.DegreeId = null;
                    userFather.GradeId = null;
                    userFather.StudyFieldId = null;

                    userFather.Irnational = register.fatherIRNational;
                    userFather.Password = "";
                    userFather.FirstName = register.fatherName;
                    userFather.LastName = register.fatherFamily;
                    userFather.IdcardSerial = register.fatherCertificateSerial;
                    userFather.IdcardSeriesNumber = register.fatherCertificateNum;
                    userFather.IdcardSeriesLetter = register.fatherCertificateChar;
                    userFather.IdcardPlace = register.fatherLocationCertificate;
                    userFather.BirthPlace = register.fatherLocationBirth;
                    userFather.BirthDate = Codes.PersianDateToDate(register.fatherBirthYear, register.fatherBirthMonth, register.fatherBirthDay).Value;
                    userFather.LivePlace = "";
                    userFather.LivePlaceOther = "";
                    userFather.Telephone = register.fatherNumber;
                    userFather.MobileNumber = register.fatherMobileNumber;
                    userFather.Address = register.fatherJobAddress;
                    userFather.ParentDegree = register.fatherDegree;
                    userFather.ParentJob = register.fatherjob;
                    userFather.ParentStatusId = register.fatherStatus;

                    userFather.Nationality = null;
                    userFather.Insurance = null;
                    userFather.FamilyNumber = null;
                    userFather.SeveralChildren = null;
                    userFather.IsCityPlace = null;
                    userFather.IsRelativesParents = null;
                    userFather.IsStudentInsurance = null;
                    userFather.RightHanded = null;
                    userFather.PreschoolEducation = null;
                    userFather.PersianLanguage = null;
                    userFather.ReferralReasonNew = null;
                    userFather.PicName = null;

                    string result = await userOp.InsertUser(userFather);
                    try
                    {
                        FatherId = int.Parse(result);
                    }
                    catch (Exception ex)
                    {
                        return Content("Error 1: " + ex.Message);
                    }
                }


                if (MotherId == 0)
                {
                    DatabaseLayer.Models.User userMother = new User();
                    userMother.UserTypeId = 3;
                    userMother.DegreeId = null;
                    userMother.GradeId = null;
                    userMother.StudyFieldId = null;

                    userMother.Irnational = register.motherIRNational;
                    userMother.Password = "";
                    userMother.FirstName = register.motherName;
                    userMother.LastName = register.motherFamily;
                    userMother.IdcardSerial = register.motherCertificateSerial;
                    userMother.IdcardSeriesNumber = register.motherCertificateNum;
                    userMother.IdcardSeriesLetter = register.motherCertificateChar;
                    userMother.IdcardPlace = register.motherLocationCertificate;
                    userMother.BirthPlace = register.motherLocationBirth;
                    userMother.BirthDate = Codes.PersianDateToDate(register.motherBirthYear, register.motherBirthMonth, register.motherBirthDay).Value;
                    userMother.LivePlace = "";
                    userMother.LivePlaceOther = "";
                    userMother.Telephone = register.motherNumber;
                    userMother.MobileNumber = register.motherMobileNumber;
                    userMother.Address = register.motherJobAddress;
                    userMother.ParentDegree = register.motherDegree;
                    userMother.ParentJob = register.motherjob;
                   
                    userMother.ParentStatusId = register.motherStatus;

                    userMother.Nationality = null;
                    userMother.Insurance = null;
                    userMother.FamilyNumber = null;
                    userMother.SeveralChildren = null;
                    userMother.IsCityPlace = null;
                    userMother.IsRelativesParents = null;
                    userMother.IsStudentInsurance = null;
                    userMother.RightHanded = null;
                    userMother.PreschoolEducation = null;
                    userMother.PersianLanguage = null;
                    userMother.ReferralReasonNew = null;
                    userMother.PicName = null;

                    string result = await userOp.InsertUser(userMother);
                    try
                    {
                        MotherId = int.Parse(result);
                    }
                    catch (Exception ex)
                    {
                        return Content("Error 2: " + ex.Message);
                    }
                }

                if(FatherId <= 0 || MotherId <= 0)
                {
                    return Content("پدر و مادر دانش آموز مشخص نیستند");
                }                

                DatabaseLayer.Models.User userStudent = new User();
                userStudent.UserTypeId = 1;
                userStudent.UserIdfather = FatherId;
                userStudent.UserIdmother = MotherId;

                userStudent.DegreeId = register.Degree;
                userStudent.GradeId = register.Grade;
                userStudent.StudyFieldId = register.StudyField;

                userStudent.Irnational = register.studentIRNational;
                userStudent.Password = "";
                userStudent.FirstName = register.studentName;
                userStudent.LastName = register.studentFamily;
                userStudent.IdcardSerial = register.studentCertificateSerial;
                userStudent.IdcardSeriesNumber = register.studentCertificateNum;
                userStudent.IdcardSeriesLetter = register.studentCertificateChar;
                userStudent.IdcardPlace = register.studentLocationCertificate;
                userStudent.BirthPlace = register.studentLocationBirth;
                userStudent.BirthDate = Codes.PersianDateToDate(register.studentBirthYear, register.studentBirthMonth, register.studentBirthDay).Value;
                userStudent.LivePlace = register.studentLivePerson;
                userStudent.LivePlaceOther = register.studentLivePersonOther;
                userStudent.Telephone = register.studentNumber;
                userStudent.MobileNumber = register.studentMobileNumber;
                userStudent.Address = register.studentHomeAddress;
                userStudent.ParentDegree = "";
                userStudent.ParentJob = "";                

                userStudent.Nationality = register.Nationality;
                userStudent.Insurance = register.Insurance;
                userStudent.FamilyNumber = register.FamilyNumber;
                userStudent.SeveralChildren = register.SeveralChildren;
                userStudent.IsCityPlace = register.IsCityPlace;
                userStudent.IsRelativesParents = register.IsRelativesParents;
                userStudent.IsStudentInsurance = register.IsStudentInsurance;
                userStudent.RightHanded = register.RightHanded;
                userStudent.PreschoolEducation = register.PreschoolEducation;
                userStudent.PersianLanguage = register.PersianLanguage;
                userStudent.ReferralReasonNew = register.ReferralReasonNew;

                if (register.file != null)
                {

                    var fileNameNew = Codes.GetFileNameNew(register.file.FileName);

                   
                    using (var output = new FileStream(Codes.GetPathStoreProfileImage(fileNameNew,false), FileMode.Create))
                    {
                        await register.file.CopyToAsync(output);
                    }

                    Codes.ResizeImage(register.file, Codes.GetPathStoreProfileImage(fileNameNew, true));


                    userStudent.PicName = fileNameNew;
                }
                else
                {
                    userStudent.PicName = null;
                }

                string result2 = await userOp.InsertUser(userStudent);
                try
                {
                    int StudentId = int.Parse(result2);
                    return Content("ok");
                }
                catch (Exception ex)
                {
                    return Content("Error 3: " + ex.Message);
                }

            }
            catch (Exception ex)
            {
                return Content("Error 4: " + ex.Message);
            }
        }

        [Authorize(AuthenticationSchemes = "AccountAuth")]
        public IActionResult ChangePass(string id)
        {
            ViewData["Title"] = "تغيير کلمه عبور";

            MessageModel messageModel = new MessageModel();
            if(id == "force" && this.User.Identity.IsAuthenticated)
            {
                messageModel.MessageType = "Error";
                messageModel.MessageText = Codes.GetResource("SuccessLoginChangePassword").Replace("T", this.User.FindFirst("UserType").Value).Replace("N", this.User.FindFirst("Nickname").Value);
            }
            ViewData["showCurrentPass"] = true;
            TempData["MessageModel"] = JsonConvert.SerializeObject(messageModel);
            return View("ChangePassword");
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "AccountAuth")]
        public async Task<IActionResult> ChangePass(string chPwdPasswordCurrent, string chPwdPassword, string chPwdPassword2)
        {
            ViewData["Title"] = "تغيير کلمه عبور";

            MessageModel messageModel = new MessageModel();
            messageModel.MessageType = "Error";
            ViewData["showCurrentPass"] = true;

            UserOp userOp = new UserOp();

            try
            {
                if (chPwdPassword != chPwdPassword2)
                {
                    messageModel.MessageText = Need.Codes.GetResource("NotMatchPassword");
                    TempData["MessageModel"] = JsonConvert.SerializeObject(messageModel);
                    ViewBag.MessageModel = messageModel;
                    return View("ChangePassword");
                }

                var UserId = Convert.ToInt32(User.FindFirst("UserId").Value);
                bool r = await userOp.ChangePassword(UserId, chPwdPasswordCurrent, chPwdPassword);
                if (r)
                {
                    ViewData["Title"] = "اتمام تغییر کلمه عبور";
                    messageModel.MessageType = "Success";
                    messageModel.MessageText = Codes.GetResource("ChangePassSuccess");
                    TempData["MessageModel"] = JsonConvert.SerializeObject(messageModel);
                    ViewBag.MessageModel = messageModel;
                    return Redirect("/Account/MsgShow");
                }
                else
                {
                    messageModel.MessageText = Need.Codes.GetResource("ChangePasswordFail");
                    TempData["MessageModel"] = JsonConvert.SerializeObject(messageModel);
                    ViewBag.MessageModel = messageModel;
                    return View("ChangePassword");
                }

            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        [Authorize(AuthenticationSchemes = "AccountAuth")]
        public IActionResult ExtraInfo()
        {
            try
            {
                var UserId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
                UserOp userOp = new UserOp();
                User user = userOp.GetUser(UserId);
                return View("ExtraInfo", user);
            }
            catch(Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "AccountAuth")]
        public async Task<IActionResult> ExtraInfo(string Nationality, string Insurance, int FamilyNumber, int SeveralChildren, bool RightHanded, bool PersianLanguage, bool IsRelativesParents, bool IsStudentInsurance, bool PreschoolEducation, bool IsCityPlace, bool ReferralReasonNew, int id)
        {
            MessageModel messageModel = new MessageModel();

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
                    messageModel.MessageType = "Success";
                    messageModel.MessageText = Need.Codes.GetResource("ChangeSuccess");                    
                    TempData["MessageModel"] = JsonConvert.SerializeObject(messageModel);
                    return Redirect("/Account/MsgShow");
                }
                else
                {
                    messageModel.MessageType = "Error";
                    messageModel.MessageText = Need.Codes.GetResource("NoPersonFound");
                    TempData["MessageModel"] = JsonConvert.SerializeObject(messageModel);
                    return Redirect("/Account/MsgShow");
                }

            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }

        }

        [Authorize(AuthenticationSchemes = "AccountAuth")]
        public IActionResult Profile()
        {
            ViewBag.IdcardSeriesLetters = Need.SelectList.getIdcardSeriesLetters();
            ViewBag.LivePlaces = Need.SelectList.getLivePlaces();
            ViewBag.ParentStatuses = Need.SelectList.getParentStatuses();
            ViewBag.Educations = Need.SelectList.getEducations();
            ViewBag.UserTypes = Need.SelectList.getUserTypes();

            try
            {
                var UserId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
                UserOp userOp = new UserOp();
                User user = userOp.GetUser(UserId);
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
                if (user.UserTypeId == 1)
                {
                    return View("ProfileStudent", user);
                }
                else
                {
                    return View("ProfileOther", user);
                }
                
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "AccountAuth")]
        public async Task<IActionResult> Profile(User user, string BirthDay, string BirthMonth, string BirthYear, IFormFile file, string returnUrl = "")
        {
            MessageModel messageModel = new MessageModel();

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
            }
            */
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
                User CurrentUser = userOp.GetUser(user.UserId);
                if (CurrentUser != null)
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
                    CurrentUser.PicThumb= pic == null ? CurrentUser.PicThumb : pic;*/

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
                    CurrentUser.Telephone = user.Telephone;
                    CurrentUser.MobileNumber = user.MobileNumber;
                    CurrentUser.Address = user.Address;
                    //CurrentUser.UserTypeId = user.UserTypeId;
                    if(CurrentUser.UserTypeId == 1)
                    {
                        CurrentUser.LivePlace = user.LivePlace;
                        CurrentUser.LivePlaceOther = user.LivePlaceOther;
                    }
                    else
                    {
                        CurrentUser.ParentJob = user.ParentJob;
                        CurrentUser.ParentStatusId = user.ParentStatusId;
                        CurrentUser.ParentDegree = user.ParentDegree;
                    }

                    await userOp.UpdateUser(CurrentUser);
                    ViewData["Title"] = "موفقیت";
                    messageModel.MessageType = "Success";
                    messageModel.MessageText = Need.Codes.GetResource("ChangeSuccess");
                    TempData["MessageModel"] = JsonConvert.SerializeObject(messageModel);
                    ViewBag.MessageModel = messageModel;
                    return Content("ok,/Account/MsgShow");
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

        [Authorize(AuthenticationSchemes = "AccountAuth")]
        public FileResult PictureUser(int? s, int id = 0)
        {
            try
            {
                UserOp userOp = new UserOp();
                var UserId = id == 0 ? Convert.ToInt32(this.User.FindFirst("UserId").Value) : Convert.ToInt32(id);
                var user = userOp.GetUser(UserId);
                if (user != null&& user.PicName!=null)
                {
                    byte[] picture = Codes.ConvertImageToArray(user.PicName);

                    if (picture != null)
                    {
                        string mimeType = Need.Codes.GeMimeTypeFromImageByteArray(picture);
                        /*byte[] pic = s != null ? Need.Codes.ResizeImage(picture, s.Value, s.Value) : picture;*/
                        return new FileStreamResult(new MemoryStream(picture), mimeType);
                    }
                }

                string path = Path.GetFullPath("wwwroot/imagepanel/icons/profile.png");
                var imageFileStream = System.IO.File.OpenRead(path);
                return File(imageFileStream, "image/png");
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return null;
            }
        }

        public static string GeMimeTypeFromImageByteArray(byte[] byteArray)
        {
            using (MemoryStream stream = new MemoryStream(byteArray))
            using (Image image = Image.FromStream(stream))
            {
                return ImageCodecInfo.GetImageEncoders().First(codec => codec.FormatID == image.RawFormat.Guid).MimeType;
            }
        }
    }
}