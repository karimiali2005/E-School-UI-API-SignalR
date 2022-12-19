using Es.DataLayerCore.Model;
using ESchool.Need;
using EsServiceCore.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ESchool.Areas.Panel.Controllers
{
    [Area("Panel")]
    [Authorize(AuthenticationSchemes = "AdminAuth")]
    public class GalleryController : Controller
    {
        private readonly IGalleryService _galleryService;

        public GalleryController(IGalleryService galleryService)
        {
            _galleryService = galleryService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _galleryService.GalleryShowAll();
            return View(result);
        }
        public IActionResult New( string returnUrl = "")
        {
           

            try
            {
                Gallery gallery = new Gallery();
                ViewData["returnUrl"] = returnUrl;
                return View("GalleryNew", gallery);
               
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Newed(Gallery gallery, string returnUrl = "")
        {
            if (gallery == null)
            {
                return Content(Need.Codes.GetResource("NoPersonFound"));
            }

         

            try
            {
                gallery.GalleryDateCreate = DateTime.Now; 
                     await _galleryService.GalleryInsert(gallery);
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



        public async Task<IActionResult> Edit(int? id, string returnUrl = "")
        {
           

            try
            {
             
                    Gallery gallery = await _galleryService.GalleryGetById(id.Value);
                    if (gallery != null)
                    {
                        ViewData["returnUrl"] = returnUrl;
                        return View("GalleryEdit", gallery);
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
        public async Task<IActionResult> Edited(Gallery gallery, string returnUrl = "")
        {
            if (gallery == null)
            {
                return Content(Need.Codes.GetResource("NoPersonFound"));
            }

         

            try
            {
                
                    Gallery CurrentGallery = await _galleryService.GalleryGetById(gallery.GalleryId);
                    if (CurrentGallery != null)
                    {
                        CurrentGallery.GalleryTitle = gallery.GalleryTitle;
                        CurrentGallery.GalleryDescription = gallery.GalleryDescription;

                       await _galleryService.GalleryUpdate(CurrentGallery);
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
                var gallery = await _galleryService.GalleryGetById(id);
                if (gallery != null)
                {
                    ViewData["returnUrl"] = returnUrl;
                    return View(gallery);
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
                var result = await _galleryService.GalleryDelete(id);
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


        public async Task<IActionResult> GalleryDetailPicShow(int id,string returnUrl = "")
        {


            try
            {
                var galleryDetail = await _galleryService.GalleryDetailPicShowAll(id);
                ViewData["returnUrl"] = returnUrl;
                ViewData["GalleryId"] = id;
                return View("GalleryDetailPicShow", galleryDetail);

            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }

        public IActionResult NewDetailPic(int id,string returnUrl = "")
        {


            try
            {
                GalleryDetail galleryDetail = new GalleryDetail();
                galleryDetail.GalleryId = id;
                ViewData["returnUrl"] = returnUrl;
                return View("GalleryNewDetailPic", galleryDetail);

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
        public async Task<IActionResult> NewDetailPic(IFormFile filePic, bool galleryIsDefault, int id, string returnUrl = "")
        {


            try
            {
                string savefilename = "";
                if (filePic != null)
                {
                    var address = SettingContext.PathStoreFiles.Instance.Address;
                    var fullAddressFtp = address;


                    fullAddressFtp += SettingContext.PathStoreFiles.Instance.Gallery;




                    string filename = ContentDispositionHeaderValue.Parse(filePic.ContentDisposition).FileName.Trim('"');
                    filename = this.EnsureCorrectFilename(filename);

                    savefilename = Codes.GetRandomFileName(filename);
                    var requestUri = fullAddressFtp + savefilename;



                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(requestUri);
                    request.Method = WebRequestMethods.Ftp.UploadFile;
                    request.Credentials = new NetworkCredential(SettingContext.PathStoreFiles.Instance.UserId, SettingContext.PathStoreFiles.Instance.Password);
                    // request.UsePassive is true by default.




                    /*string savefilename = Codes.GetFileName(filename);
                    using (var output = new FileStream(Codes.GetPathAndFilename(savefilename), FileMode.Create))
                    {
                        await file.CopyToAsync(output);
                    }*/
                    using (Stream requestStream = request.GetRequestStream())
                    {
                        await filePic.CopyToAsync(requestStream);
                    }
                    //var fileNew = System.IO.Path.GetFileName(savefilename) + "_Temp" + System.IO.Path.GetExtension(savefilename);
                    //Codes.ResizeImage(filePic, fileNew, 263, 147);

                    //FtpWebRequest request2 = (FtpWebRequest)WebRequest.Create(requestUri);
                    //request2.Method = WebRequestMethods.Ftp.UploadFile;
                    //request2.Credentials = new NetworkCredential(SettingContext.PathStoreFiles.Instance.UserId, SettingContext.PathStoreFiles.Instance.Password);

                    //using (Stream requestStream = request2.GetRequestStream())
                    //{
                    //    await filePic.CopyToAsync(requestStream);
                    //}
                }


                GalleryDetail galleryDetail = new GalleryDetail()
                {
                    GalleryId= id,
                    GalleryDetailName= savefilename,
                    GalleryDetailType=1,
                    GalleryIsDefault= galleryIsDefault

                };

                await _galleryService.GalleryDetailInsert(galleryDetail);

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



        public async Task<IActionResult> GalleryDetailDelete(int id, string returnUrl = "")
        {

            try
            {
                var gallerydetail = await _galleryService.GalleryDetailGetById(id);
                if (gallerydetail != null)
                {
                    ViewData["returnUrl"] = returnUrl;
                    return View(gallerydetail);
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
        public async Task<IActionResult> GalleryDetailDeleted(int id, string returnUrl = "")
        {


            try
            {
                var result = await _galleryService.GalleryDetailDelete(id);
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


        public async Task<IActionResult> GalleryDetailVideoShow(int id, string returnUrl = "")
        {


            try
            {
                var galleryDetail = await _galleryService.GalleryDetailVideoShowAll(id);
                ViewData["returnUrl"] = returnUrl;
                ViewData["GalleryId"] = id;
                return View("GalleryDetailVideoShow", galleryDetail);

            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }



        public IActionResult NewDetailVideo(int id, string returnUrl = "")
        {


            try
            {
                GalleryDetail galleryDetail = new GalleryDetail();
                galleryDetail.GalleryId = id;
                ViewData["returnUrl"] = returnUrl;
                return View("GalleryNewDetailVideo", galleryDetail);

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
        public async Task<IActionResult> NewDetailVideo(IFormFile filePic, bool galleryIsDefault, int id, string returnUrl = "")
        {


            try
            {
                string savefilename = "";
                if (filePic != null)
                {
                    var address = SettingContext.PathStoreFiles.Instance.Address;
                    var fullAddressFtp = address;


                    fullAddressFtp += SettingContext.PathStoreFiles.Instance.Gallery;




                    string filename = ContentDispositionHeaderValue.Parse(filePic.ContentDisposition).FileName.Trim('"');
                    filename = this.EnsureCorrectFilename(filename);

                    savefilename = Codes.GetRandomFileName(filename);
                    var requestUri = fullAddressFtp + savefilename;



                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(requestUri);
                    request.Method = WebRequestMethods.Ftp.UploadFile;
                    request.Credentials = new NetworkCredential(SettingContext.PathStoreFiles.Instance.UserId, SettingContext.PathStoreFiles.Instance.Password);
                    // request.UsePassive is true by default.




                    /*string savefilename = Codes.GetFileName(filename);
                    using (var output = new FileStream(Codes.GetPathAndFilename(savefilename), FileMode.Create))
                    {
                        await file.CopyToAsync(output);
                    }*/
                    using (Stream requestStream = request.GetRequestStream())
                    {
                        await filePic.CopyToAsync(requestStream);
                    }
                  
                }


                GalleryDetail galleryDetail = new GalleryDetail()
                {
                    GalleryId = id,
                    GalleryDetailName = savefilename,
                    GalleryDetailType = 2,
                    GalleryIsDefault = galleryIsDefault

                };

                await _galleryService.GalleryDetailInsert(galleryDetail);

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



       

    }
}