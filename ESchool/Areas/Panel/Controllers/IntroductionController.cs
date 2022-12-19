using Es.DataLayerCore.Model;
using ESchool.Need;
using EsServiceCore.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ESchool.Areas.Panel.Controllers
{
    [Area("Panel")]
    [Authorize(AuthenticationSchemes = "AdminAuth")]
    public class IntroductionController : Controller
    {
        private readonly IIntroductionService _introductionService;

        public IntroductionController(IIntroductionService introductionService)
        {
            _introductionService = introductionService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _introductionService.IntroductionShowAll();
            return View(result);
        }

        public async Task<IActionResult> New(string returnUrl = "")
        {


            try
            {
                ViewBag.IntroductionTypeId = await _introductionService.IntroductionTypeShowAll();
                Introduction introduction = new Introduction();
                ViewData["returnUrl"] = returnUrl;
                return View("IntroductionNew", introduction);

            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }

        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
            {
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);
            }
            return filename;
        }


        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 2147483648)]
        [RequestSizeLimit(2147483648)]
        public async Task<IActionResult> New(IFormFile filePic, string IntroductionTitle, string IntroductionPosition, int IntroductionTypeId, string returnUrl = "")
        {


            try
            {
                string savefilename = "";
                if (filePic != null)
                {
                    var address = SettingContext.PathStoreFiles.Instance.Address;
                    var fullAddressFtp = address;


                    fullAddressFtp += SettingContext.PathStoreFiles.Instance.Introduction;




                    string filename = ContentDispositionHeaderValue.Parse(filePic.ContentDisposition).FileName.Trim('"');
                    filename = this.EnsureCorrectFilename(filename);

                    savefilename = Codes.GetRandomFileName(filename);
                    var requestUri = fullAddressFtp + savefilename;



                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(requestUri);
                    request.Method = WebRequestMethods.Ftp.UploadFile;
                    request.Credentials = new NetworkCredential(SettingContext.PathStoreFiles.Instance.UserId, SettingContext.PathStoreFiles.Instance.Password);
                    // request.UsePassive is true by default.




                    using (Stream requestStream = request.GetRequestStream())
                    {
                        await filePic.CopyToAsync(requestStream);
                    }

                }


                Introduction introduction = new Introduction()
                {
                    IntroductionTitle= IntroductionTitle,
                    IntroductionPosition = IntroductionPosition,
                    IntroductionPic = savefilename,
                    IntroductionTypeId = IntroductionTypeId

                };

                await _introductionService.IntroductionInsert(introduction);

                //returnUrl = returnUrl.IndexOf("?") >= 0 ? returnUrl + "&msg=" + Need.Codes.GetResource("ChangeSuccess") : returnUrl + "?msg=" + Need.Codes.GetResource("ChangeSuccess");
                //returnUrl = returnUrl + "&m=Success";
                //return Redirect(returnUrl);
                return Content("ok," + returnUrl);

            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }

        public async Task<IActionResult> Edit(int? id, string returnUrl = "")
        {


            try
            {
                ViewBag.IntroductionTypeId = await _introductionService.IntroductionTypeShowAll();
                Introduction introduction = await _introductionService.IntroductionGetById(id.Value);
                if (introduction != null)
                {
                    ViewData["returnUrl"] = returnUrl;
                    return View("IntroductionEdit", introduction);
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
        [RequestFormLimits(MultipartBodyLengthLimit = 2147483648)]
        [RequestSizeLimit(2147483648)]
        public async Task<IActionResult> Edit(IFormFile filePic, int id, string IntroductionTitle, string IntroductionPosition, int IntroductionTypeId, string returnUrl = "")
        {
            if (IntroductionTitle == null)
            {
                return Content(Need.Codes.GetResource("NoPersonFound"));
            }



            try
            {
                string savefilename = "";
                if (filePic != null)
                {
                    var address = SettingContext.PathStoreFiles.Instance.Address;
                    var fullAddressFtp = address;


                    fullAddressFtp += SettingContext.PathStoreFiles.Instance.Introduction;




                    string filename = ContentDispositionHeaderValue.Parse(filePic.ContentDisposition).FileName.Trim('"');
                    filename = this.EnsureCorrectFilename(filename);

                    savefilename = Codes.GetRandomFileName(filename);
                    var requestUri = fullAddressFtp + savefilename;



                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(requestUri);
                    request.Method = WebRequestMethods.Ftp.UploadFile;
                    request.Credentials = new NetworkCredential(SettingContext.PathStoreFiles.Instance.UserId, SettingContext.PathStoreFiles.Instance.Password);
                    // request.UsePassive is true by default.




                    using (Stream requestStream = request.GetRequestStream())
                    {
                        await filePic.CopyToAsync(requestStream);
                    }

                }


                Introduction CurrentIntroduction = await _introductionService.IntroductionGetById(id);
                if (CurrentIntroduction != null)
                {
                    CurrentIntroduction.IntroductionTitle = IntroductionTitle;
                    CurrentIntroduction.IntroductionPosition = IntroductionPosition;
                    CurrentIntroduction.IntroductionTypeId = IntroductionTypeId;

                    if (savefilename != "")
                    {
                        CurrentIntroduction.IntroductionPic = savefilename;
                    }

                    await _introductionService.IntroductionUpdate(CurrentIntroduction);
                    returnUrl = returnUrl.IndexOf("?") >= 0 ? returnUrl + "&msg=" + Need.Codes.GetResource("ChangeSuccess") : returnUrl + "?msg=" + Need.Codes.GetResource("ChangeSuccess");
                    returnUrl = returnUrl + "&m=Success";
                    return Content("ok," + returnUrl);
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


        public async Task<IActionResult> Delete(int id, string returnUrl = "")
        {

            try
            {
                var introduction = await _introductionService.IntroductionGetById(id);
                if (introduction != null)
                {
                    ViewData["returnUrl"] = returnUrl;
                    return View(introduction);
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
                var result = await _introductionService.IntroductionDelete(id);
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