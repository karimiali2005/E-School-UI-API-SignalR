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
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _articleService.ArticleShowAll();
            return View(result);
        }

        public IActionResult New(string returnUrl = "")
        {


            try
            {
                Article article = new Article();
                ViewData["returnUrl"] = returnUrl;
                return View("ArticleNew", article);

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
        public async Task<IActionResult> New(IFormFile filePic, string ArticleTitle, string ArticleBody,int ArticleStudyTime, string returnUrl = "")
        {


            try
            {
                string savefilename = "";
                if (filePic != null)
                {
                    var address = SettingContext.PathStoreFiles.Instance.Address;
                    var fullAddressFtp = address;


                    fullAddressFtp += SettingContext.PathStoreFiles.Instance.Article;




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


                Article article = new Article()
                {
                    ArticleTitle=ArticleTitle,
                    ArticleBody=ArticleBody,
                    ArticleCountor=0,
                    ArticleCreateDate=DateTime.Now,
                    ArticlePic=savefilename,
                    ArticleStudyTime=ArticleStudyTime,
                    CreateUser="Admin"

                };

                await _articleService.ArticleInsert(article);

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

                Article article = await _articleService.ArticleGetById(id.Value);
                if (article != null)
                {
                    ViewData["returnUrl"] = returnUrl;
                    return View("ArticleEdit", article);
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
        public async Task<IActionResult> Edit(IFormFile filePic,int id, string ArticleTitle, string ArticleBody, int ArticleStudyTime, string returnUrl = "")
        {
            if (ArticleTitle == null)
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


                    fullAddressFtp += SettingContext.PathStoreFiles.Instance.Article;




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


                Article CurrentArticle = await _articleService.ArticleGetById(id);
                if (CurrentArticle != null)
                {
                    CurrentArticle.ArticleTitle = ArticleTitle;
                    CurrentArticle.ArticleBody =ArticleBody;
                    CurrentArticle.ArticleStudyTime = ArticleStudyTime;
                   
                    if(savefilename!="")
                    {
                        CurrentArticle.ArticlePic = savefilename;
                    }

                    await _articleService.ArticleUpdate(CurrentArticle);
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
                var article = await _articleService.ArticleGetById(id);
                if (article != null)
                {
                    ViewData["returnUrl"] = returnUrl;
                    return View(article);
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
                var result = await _articleService.ArticleDelete(id);
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