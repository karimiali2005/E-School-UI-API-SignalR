using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Es.DataLayerCore.DTOs.HomeWork;
using Es.DataLayerCore.DTOs;
using Es.DataLayerCore.Model;
using ESchool.Need;
using EsServiceCore.DTOs.HomeWork;
using EsServiceCore.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ESchool.Controllers
{
    [Authorize(AuthenticationSchemes = "AccountAuth")]
    public class HomeWorkStudenController : Controller
    {
        IHomeWorkService _homework;
        public HomeWorkStudenController(IHomeWorkService homeWorkService)
        {
            _homework = homeWorkService;
        }
        public async Task<IActionResult> Index(int idroom,int idcours,string returnUrl="/", int pagenumber=1, int pagesize = 10)
        {
            try
            {
                int user = UserContext.GetClaimValueInteger(ClaimName.UserId);

                   var x = await _homework.getListHomeworkForStudents(user, idroom, idcours, pagenumber, pagesize);
              //  var x = await _homework.getListHomeworkForStudents(725, 16, 163,pagenumber,pagesize);
                ViewBag.countpage = x!=null&&x.Count!=0?(x.FirstOrDefault().RecordCount/pagesize)+1:0;
                ViewBag.pagenumber = pagenumber;
                ViewBag.idroom = idroom;
                ViewBag.idcourse = idcours;
                ViewBag.returnUrl = returnUrl;
                return View(x);
            }
            catch (Exception ex)
            {

                throw;
            }

           
        }

        public async Task<IActionResult> sendHomeworkStudent(int id,string returnUrl="/")
        {
            try
            {
                   int uid = UserContext.GetClaimValueInteger(ClaimName.UserId);

                   var x =await _homework.getHomeworkByID(id, uid);
                //    var x =await _homework.getHomeworkByID(id, 725);   
                ViewBag.returnUrl = returnUrl;


                return View(x);
              
            }
            catch (Exception ex)
            {

                throw;
            }
        
        }

        [HttpPost]
        public async Task<IActionResult> sendHomeworkStudent(vm_AnsverhomeWork homeworkAnswer)
        {
            try
            {
                
                int uid = UserContext.GetClaimValueInteger(ClaimName.UserId);

                if (ModelState.IsValid)
                {
                    switch (homeworkAnswer.HomeworkTypeID)
                    {
                        case 1://فایل متنی
                            if (String.IsNullOrEmpty(homeworkAnswer.HomeworkResponse))
                            {
                                ModelState.AddModelError("Eror", "جواب ارسالی نباید خالی باشد");
                                return View(homeworkAnswer);
                            }
                            break;
                        //default://صوت و فایل
                        //    /*  var f1=homeworkAnswer..FirstOrDefault();
                        //       var f2 = homeworkAnswer.HomeworkResponseFile.FirstOrDefault();
                        //       if (f1 == null && f2 == null)
                        //       {
                        //           ModelState.AddModelError("Eror", "هیچ فایلی ارسال نشده است");
                        //           return View(homeworkAnswer);
                        //       }*/
                           
                        //    ModelState.AddModelError("Eror", "هیچ فایلی ارسال نشده است");
                        //    return View(homeworkAnswer);
                            
                      

                    }

                    if (await _homework.updateAnswerHomeWork(homeworkAnswer, uid))
                    {
                        return RedirectToAction("Index", new { idroom = homeworkAnswer.RoomId, idcours = homeworkAnswer.CourseId });
                    }
                    else
                    {
                        ModelState.AddModelError("Eror", "خطا در برقراری ارتباط");
                    }


                }

                
                //   var x =await _homework.getHomeworkByID(id, uid);
                // var x = await _homework.getHomeworkByID(id, 725);
                return View(homeworkAnswer);

            }
            catch (Exception ex)
            {

                throw;
            }

        }

      
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 2147483648)]
        [RequestSizeLimit(2147483648)]
        public async Task<IActionResult> StoreFile(IFormFile file)
        {
            try
            {
              

                var address = SettingContext.PathStoreFiles.Instance.Address;
                var fullAddressFtp = address;              
                    fullAddressFtp += SettingContext.PathStoreFiles.Instance.HomeworkFile;
               


                string filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                filename = EnsureCorrectFilename(filename);

                string savefilename = Codes.GetRandomFileName(filename);
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
                    await file.CopyToAsync(requestStream);
                }

                
              //  return Json(new { Filename = savefilename, MimeType = file.ContentType });
             
                var result = new { status = "ok", path = savefilename, mimtype=file.ContentType };

                return Ok(Json(result));
            }
            catch (Exception ex)
            {
              //  return Content("Error: " + ex.Message);
                var result = new { status = "eror", message = ex.Message };
                return BadRequest(Json(result));
            }
        }

        [HttpPost]
        public async Task<IActionResult> addresponsefileHomeworh(int homeworkanswerId,string filenam)
        {
            try
            {
                int fid = await _homework.addAnsverFile(filenam, homeworkanswerId);

                var result = new { status = "ok", id = fid };

                return Ok(Json(result));
            }
            catch (Exception ex)
            {
                //  return Content("Error: " + ex.Message);
                var result = new { status = "eror", message = ex.Message };
                return BadRequest(Json(result));
            }
        }

        [HttpPost]
        public async Task<IActionResult> deletefileHomeworh(int idanswerfile,int idhomeworkAnswer)
        {
            try
            {
                string filename = await _homework.removeAnsverFile(idanswerfile,idhomeworkAnswer);
                if (filename != null)
                {
                    var address = SettingContext.PathStoreFiles.Instance.Address;
                    var fullAddressFtp = address;
                    fullAddressFtp += SettingContext.PathStoreFiles.Instance.HomeworkFile;
                    var requestUri = fullAddressFtp + filename;
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(requestUri);
                    request.Method = WebRequestMethods.Ftp.DeleteFile;
                    request.Credentials = new System.Net.NetworkCredential(SettingContext.PathStoreFiles.Instance.UserId, SettingContext.PathStoreFiles.Instance.Password);
                    request.GetResponse().Close();
                    var result1 = new { status = "ok"};
                return Ok(Json(result1));
                }

                var result = new { status = "eror", message = "NotFoundFile" };
                return BadRequest(Json(result));
            }
            catch (Exception ex)
            {
                //  return Content("Error: " + ex.Message);
                var result = new { status = "eror", message = ex.Message };
                return BadRequest(Json(result));
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
    }
}