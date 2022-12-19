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

namespace ESchool.Areas.Panel.Controllers
{
    [Area("Panel")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string loginUsername, string loginPassword, bool remeberme, string returnUrl = "")
        {
            ViewData["returnUrl"] = returnUrl;
            ViewData["loginUsername"] = loginUsername;            

            bool result = Need.AppSettingClass.Login(loginUsername, loginPassword);
            if(result)
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim("Nickname", "مدیر سایت"));                
                var identity = new ClaimsIdentity(claims, "AdminAuth");
                await HttpContext.SignInAsync("AdminAuth", new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = remeberme });
                
                if (string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect("/Panel/Student");
                }
                else
                {
                    return Redirect(returnUrl);
                }
            }

            MessageModel messageModel = new MessageModel();
            messageModel.MessageType = "Error";
            messageModel.MessageText = Need.Codes.GetResource("InvalidUsernamePassword");
            return View("Index", messageModel);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }


        

    }
}