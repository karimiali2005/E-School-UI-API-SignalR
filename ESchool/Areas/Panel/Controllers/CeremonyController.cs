using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Es.DataLayerCore.Model;
using EsServiceCore.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.Areas.Panel.Controllers
{
    [Area("Panel")]
    [Authorize(AuthenticationSchemes = "AdminAuth")]
    public class CeremonyController : Controller
    {
        private readonly ICeremonyService _ceremonyService;

        public CeremonyController(ICeremonyService ceremonyService)
        {
            _ceremonyService = ceremonyService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _ceremonyService.CeremonyShow();
            return View(result);
        }

        public async Task<IActionResult> New(string returnUrl = "")
        {


            try
            {
                Ceremony article = new Ceremony();
                ViewData["returnUrl"] = returnUrl;
                ViewBag.CeremonyType = (List<CeremonyType>)await _ceremonyService.CeremonyTypeShow();
                return View("CeremonyNew", article);

            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Newed(Ceremony ceremony, string returnUrl = "")
        {
            if (ceremony == null)
            {
                return Content(Need.Codes.GetResource("NoPersonFound"));
            }



            try
            {
                ceremony.CeremonyDate = DateTime.Now;
                await _ceremonyService.CeremonyInsert(ceremony);
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

        public async Task<IActionResult> Delete(int id, string returnUrl = "")
        {

            try
            {
                var ceremony = await _ceremonyService.CeremonyGetById(id);
                if (ceremony != null)
                {
                    ViewData["returnUrl"] = returnUrl;
                    return View(ceremony);
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
        public async Task<IActionResult> Deleted(int id, string returnUrl = "")
        {


            try
            {
                var result = await _ceremonyService.CeremonyDelete(id);
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