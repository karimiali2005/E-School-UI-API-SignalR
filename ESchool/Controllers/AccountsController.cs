using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using DatabaseLayer.Models;
using Es.DataLayerCore.DTOs.User;
using ESchool.Need;
using EsServiceCore.Interface;
using EsServiceCore.Utility.Convertor;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using UAParser;

namespace ESchool.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IUserService _userService;

        public AccountsController(IUserService userService)
        {
            _userService = userService;
        }
      
        public async Task<IActionResult> Login(string returnUrl = "")
        {

            if(returnUrl.Contains("ticket"))
            {
                var ticket = returnUrl.Split("ticket");
                if(ticket!=null && !string.IsNullOrEmpty(ticket[1]) )
                {
                    var ulogin = await _userService.TicketApprove(ticket[1].Substring(1));
                    if(ulogin!=null)
                    {
                        var user = await _userService.LoginUserTicket(ulogin.Irnational,ulogin.Password);
                        if (user != null)
                        {

                            if (user.UserActive == 0 || user.UserActive == 2)
                            {
                                string ReasonInactive = string.IsNullOrEmpty(user.ReasonInactive) ? "" : Convert.ToString(user.ReasonInactive);

                                IList<Claim> claims = new List<Claim>();
                                claims.Add(new Claim("UserId", user.UserID.ToString()));
                                claims.Add(new Claim("Nickname", user.FullName));
                                claims.Add(new Claim("IrNational", user.IRNational));
                                claims.Add(new Claim("BirthDate", Codes.getPersianDate(user.BirthDate)));
                                claims.Add(new Claim("UserTypeId", user.UserTypeID.ToString()));
                                claims.Add(new Claim("UserType", user.UserTypeTitle));
                                claims.Add(new Claim("UserActive", Convert.ToString(user.UserActive)));
                                claims.Add(new Claim("ReasonInactive", ReasonInactive));
                                claims.Add(new Claim("MobileNumber", user.MobileNumber));
                                claims.Add(new Claim("PicName", user.PicName ?? ""));
                                claims.Add(new Claim("ExamAddress", user.ExamAddress ?? ""));
                                var identity = new ClaimsIdentity(claims, "AccountAuth");

                                if (user.UserActive == 2)
                                {
                                    
                                    await HttpContext.SignInAsync("AccountAuth", new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = false });

                                    var userAgent = HttpContext.Request.Headers["User-Agent"];
                                    var uaParser = Parser.GetDefault();
                                    ClientInfo c = uaParser.Parse(userAgent);


                                    await _userService.LoginInsert(new Es.DataLayerCore.Model.Login()
                                    {
                                        UserId = user.UserID,
                                        LoginPlatformId = 1,
                                        BrowserName = c.UA.Family,
                                        DeviceName = c.Device.Family,
                                        DateLogin = DateTime.Now,
                                        Ips = HttpContext.Connection.RemoteIpAddress.ToString(),
                                        LoginSuccess = true,
                                        BrowserVersion = c.UA.Major + "." + c.UA.Minor,

                                    }, MyHttpContext.Current.Session.Id);

                                    if (string.IsNullOrEmpty(ticket[0]))
                                    {

                                        return Redirect("/Member/RoomChat");
                                    }
                                    else
                                    {
                                        return Redirect(ticket[0]);
                                    }
                                }
                                else
                                {
                                    /*messageModel.MessageType = "Warning";
                                    messageModel.MessageText = UserType + " گرامی، " + Nickname + " کاربری شما معلق شده است. به علت " + "<br/>";
                                    messageModel.MessageText += ReasonInactive;
                                    TempData["MessageModel"] = JsonConvert.SerializeObject(messageModel);
                                    ViewBag.MessageModel = messageModel;
                                    return Redirect("/Account/MsgShow");*/
                                    ModelState.AddModelError("Error", "کاربری شما به علت " + ReasonInactive + " معلق شده است");
                                }

                            }
                            if (user.UserActive == 1)
                            {
                                ModelState.AddModelError("Error", "کاربری شما غیرفعال است");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("Error", "کاربری با مشخصات وارد شده یافت نشد");
                        }

                    }
                }
            }

            ViewData["returnUrl"] = returnUrl;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login, string returnUrl = "")
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await  _userService.LoginUser(login);
                    if (user != null)
                    {
                        
                        if (user.UserActive == 0 || user.UserActive == 2)
                        {
                            string ReasonInactive = string.IsNullOrEmpty(user.ReasonInactive) ? "" : Convert.ToString(user.ReasonInactive);

                            IList<Claim> claims = new List<Claim>();
                            claims.Add(new Claim("UserId", user.UserID.ToString()));
                            claims.Add(new Claim("Nickname", user.FullName));
                            claims.Add(new Claim("IrNational", user.IRNational));
                            claims.Add(new Claim("BirthDate", Codes.getPersianDate(user.BirthDate)));
                            claims.Add(new Claim("UserTypeId", user.UserTypeID.ToString()));
                            claims.Add(new Claim("UserType", user.UserTypeTitle));
                            claims.Add(new Claim("UserActive", Convert.ToString(user.UserActive)));
                            claims.Add(new Claim("ReasonInactive", ReasonInactive));
                            claims.Add(new Claim("MobileNumber", user.MobileNumber));
                            claims.Add(new Claim("PicName", user.PicName??""));
                            claims.Add(new Claim("ExamAddress", user.ExamAddress ?? ""));
                            var identity = new ClaimsIdentity(claims, "AccountAuth");
                          
                            if (user.UserActive == 2)
                            {
                                if (string.IsNullOrEmpty(user.Password) && user.BirthDate.getPersianDate().Substring(0, 4) != login.UsersPass)
                                {
                                    ModelState.AddModelError("Error", "کاربری با مشخصات وارد شده یافت نشد");
                                    return View(login);
                                    // return Redirect("/Account/ChangePass/force");
                                }

                                await HttpContext.SignInAsync("AccountAuth", new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = login.RememberMe });

                                var userAgent = HttpContext.Request.Headers["User-Agent"];
                                var uaParser = Parser.GetDefault();
                                ClientInfo c = uaParser.Parse(userAgent);


                                await _userService.LoginInsert(new Es.DataLayerCore.Model.Login()
                                {
                                    UserId = user.UserID,
                                    LoginPlatformId = 1,
                                    BrowserName = c.UA.Family,
                                    DeviceName = c.Device.Family,
                                    DateLogin = DateTime.Now,
                                    Ips = HttpContext.Connection.RemoteIpAddress.ToString(),
                                    LoginSuccess = true,
                                    BrowserVersion = c.UA.Major + "." + c.UA.Minor,

                                }, MyHttpContext.Current.Session.Id);

                                if (string.IsNullOrEmpty(returnUrl))
                                {
                                    
                                    return Redirect("/Member/RoomChat");
                                }
                                else
                                {
                                    return Redirect(returnUrl);
                                }
                            }
                            else
                            {
                                /*messageModel.MessageType = "Warning";
                                messageModel.MessageText = UserType + " گرامی، " + Nickname + " کاربری شما معلق شده است. به علت " + "<br/>";
                                messageModel.MessageText += ReasonInactive;
                                TempData["MessageModel"] = JsonConvert.SerializeObject(messageModel);
                                ViewBag.MessageModel = messageModel;
                                return Redirect("/Account/MsgShow");*/
                                ModelState.AddModelError("Error", "کاربری شما به علت "+ ReasonInactive+" معلق شده است");
                            }

                        }
                        if (user.UserActive == 1)
                        {
                            ModelState.AddModelError("Error", "کاربری شما غیرفعال است");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "کاربری با مشخصات وارد شده یافت نشد");
                    }
                    
                }

                return View(login);
               
                
            }
            catch (Exception ex)
            {

                ex.ToSaveElmah();
                return View("Error");
            }

        }
        #region Logout
      
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            var userId = UserContext.GetClaimValueInteger(ClaimName.UserId);
            await _userService.LoginEnd(userId);
            return RedirectToAction("Login");
        }

        #endregion
    }
}