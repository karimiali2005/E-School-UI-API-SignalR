using EsServiceCore.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ESchool.Areas.Panel.Controllers
{
    [Area("Panel")]
    [Authorize(AuthenticationSchemes = "AdminAuth")]
    public class PreRegistrationController : Controller
    {
        private readonly IPreRegistrationService _preRegistrationService;

        public PreRegistrationController(IPreRegistrationService preRegistrationService)
        {
            _preRegistrationService = preRegistrationService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _preRegistrationService.PreRegistrationShow();
            return View(result);
        }

       
        public async Task<IActionResult> Archive(int id, string returnUrl = "")
        {
           
            try
            {
                var preRegistration =await _preRegistrationService.PreRegistrationByID(id);
                if (preRegistration!=null)
                {
                    ViewData["returnUrl"] = returnUrl;
                    return View( preRegistration);
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
        public async Task<IActionResult> ArchiveFinish(int id, string returnUrl = "")
        {


            try
            {
                var result = await _preRegistrationService.PreRegistrationArchive(id);
                if (result)
                {

                    string msg = Need.Codes.GetResource("ItemArchive");

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