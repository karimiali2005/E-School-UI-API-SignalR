using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Es.DataLayerCore.DTOs;
using ESchool.Need;
using EsServiceCore.DTOs.HomeWork;
using EsServiceCore.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.Controllers
{
    [Authorize(AuthenticationSchemes = "AccountAuth")]
    public class HomeWorkController : Controller
    {
        private readonly IHomeWorkService _homework;
        public HomeWorkController(IHomeWorkService homeWork)
        {
            _homework = homeWork;
        }

        public IActionResult Index(string retwrnurl="/")
        {
            ViewBag.back = Request.Path.Value;
            return View();
        }

        public async Task<IActionResult> manageHomWork(string nameDars, int idroom, int courseid, string returnUrl = "/", int pagenumber = 1, int pagesize = 10,int roomChatGroupID = 0, string retwrnurl = "/")
        {
            try
            {
                ViewBag.back =retwrnurl;
                int uid = UserContext.GetClaimValueInteger(ClaimName.UserId);
                ViewBag.nameDars = nameDars;
                var listhomeWork = await _homework.getListHomeWorkByRoomid(uid, idroom, courseid, pagenumber, pagesize);
                ViewBag.idroom = idroom;
                ViewBag.idcourse = courseid;
                ViewBag.countpage = listhomeWork!=null&&listhomeWork.Count!=0? (listhomeWork.FirstOrDefault().RecordCount/pagesize)+1:0;
                ViewBag.pagenumber = pagenumber;
                ViewBag.returnUrl = returnUrl;
                ViewBag.roomChatGroupID = roomChatGroupID;
                return View(listhomeWork);

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<IActionResult> manageHomWorkDetails(int idhomework, string returnUrl = "/")
        {
            try
            {
               
                int uid = UserContext.GetClaimValueInteger(ClaimName.UserId);
                var x = await _homework.getDetailsHomework(idhomework, uid);

                ViewBag.returnUrl = returnUrl;
                return View(x);

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<IActionResult> manageAnswerHomWorkDetails(int idhomework, int idstudents, string thisurl, string returnUrl = "/")
        {
            try
            {
                
                var x = await _homework.getDetailsAnswerHomework(idhomework, idstudents);
                ViewBag.idhomework = idhomework;
                ViewBag.returnUrl = returnUrl;
                ViewBag.thisurl = thisurl;
                return View(x);

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpPost]
        public async Task<IActionResult> manageAnswerHomWorkDetails(vm_HomeworkDetailsShowByIDResult vm, int id, bool isnumberic, int homeworkid, string dateend, string returnUrl = "/",string thisurl="/")
        {
          
            try
            {
                if (id == vm.HomeworkAnswerID)
                {
                    try
                    {
                        DateTime edndate = dateend.ToGeorgianDateTimeByMinutes();
                        bool x = await _homework.updateAnswerHomeWork_fromTeacher(vm, isnumberic, edndate);
                        if (x)
                        {
                            return RedirectToAction("manageHomWorkDetails", new { idhomework = homeworkid, returnUrl= thisurl });
                        }
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("DateEror", "تاریخ وارد شده صحیح نیست");
                    }
                }
                ViewBag.returnUrl = thisurl;
                var xx = await _homework.getDetailsAnswerHomework(homeworkid, vm.UserID);
                ViewBag.idhomework = homeworkid;
                return View(xx);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IActionResult> CreateHomeWork(int idroom, int courseid, string nameDars,int roomChatGroupID = 0, string returnUrl = "/")
        {
            ViewBag.returnUrl = returnUrl;
            ViewBag.nameDars = nameDars;
            ViewBag.idroom = idroom;
            ViewBag.idcourse = courseid;
            ViewBag.nomreh = await _homework.getnomretype();
            ViewBag.roomChatGroupID = roomChatGroupID;
            ViewBag.listTypeHomework = await _homework.getTypeHomeWork();
            return View();
        }
        [HttpPost]        
        public async Task<IActionResult> CreateHomeWork([FromForm]vm_homework vm_Homework,int roomChatGroupID, string returnUrl = "/")
        {
            ViewBag.returnUrl = returnUrl;
            try
            {
                if (ModelState.IsValid)
                {
                    int uid = UserContext.GetClaimValueInteger(ClaimName.UserId);
                    if (vm_Homework.HomeWorkFileName_file != null)
                    {
                    vm_Homework.HomeWorkFileName=await  StoreFile(vm_Homework.HomeWorkFileName_file);
                    }
                    try
                    {
                        DateTime satartdate = vm_Homework.start.ToGeorgianDateTimeByMinutes();
                        DateTime enddate = vm_Homework.end.ToGeorgianDateTimeByMinutes();
                        await _homework.addHomework(vm_Homework, uid, satartdate, enddate, roomChatGroupID);
                        return RedirectToAction("manageHomWork", new { nameDars = vm_Homework.namedars, idroom = vm_Homework.RoomId, courseid = vm_Homework.CourseId,roomChatGroupID = roomChatGroupID,returnUrl = "/Member/RoomChat" });
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("dateEror", "تاریخ وارد شده صحیح نیست ");
                    }
                }
                ViewBag.idroom = vm_Homework.RoomId;
                ViewBag.idcourse = vm_Homework.CourseId;
                ViewBag.nomreh = await _homework.getnomretype();
                ViewBag.nameDars = vm_Homework.namedars;
                ViewBag.listTypeHomework = await _homework.getTypeHomeWork();
                ViewBag.roomChatGroupID = roomChatGroupID;
                return View();
            }
            catch (Exception ex)
            {
                // throw;
                return BadRequest();
            }

        }

        //[HttpPost]
        //[RequestFormLimits(MultipartBodyLengthLimit = 2147483648)]
        //[RequestSizeLimit(2147483648)]
        public async Task<string> StoreFile(IFormFile file)
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
              //  var result = new { status = "ok", path = savefilename, mimtype = file.ContentType };
               // return Ok(Json(result));
                return savefilename;
            }
            catch (Exception ex)
            {
                //  return Content("Error: " + ex.Message);
              //  var result = new { status = "eror", message = ex.Message };
                return null;
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
        public async Task<IActionResult> UpdateHomeWork(int idroom, int courseid, int homeworkid, string nameDars,int roomChatGroupID=0,string returnUrl = "/")
        {           
            int uid = UserContext.GetClaimValueInteger(ClaimName.UserId);
            ViewBag.idroom = idroom;
            ViewBag.returnUrl = returnUrl;           
            ViewBag.idcourse = courseid;
            ViewBag.nameDars = nameDars;
            ViewBag.nomreh = await _homework.getnomretype();
            ViewBag.listTypeHomework = await _homework.getTypeHomeWork();
            ViewBag.roomChatGroupID = roomChatGroupID;
            vm_homework vm = await _homework.GetHomeworkByid(uid, homeworkid);
            ViewBag.returnUrl = returnUrl;
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateHomeWork([FromForm]vm_homework vm_Homework,int roomChatGroupID=0, string returnUrl = "/")
        {
            ViewBag.returnUrl = returnUrl;
            ViewBag.roomChatGroupID = roomChatGroupID;
            try
            {
                if (ModelState.IsValid)
                {
                    int uid = UserContext.GetClaimValueInteger(ClaimName.UserId);
                    if (vm_Homework.HomeWorkFileName_file != null)
                    {
                        vm_Homework.HomeWorkFileName = await StoreFile(vm_Homework.HomeWorkFileName_file);
                    }
                    try
                    {
                        DateTime satartdate = vm_Homework.start.ToGeorgianDateTimeByMinutes();
                        DateTime enddate = vm_Homework.end.ToGeorgianDateTimeByMinutes();
                        await _homework.updatehomework(uid, vm_Homework, satartdate, enddate);
                        return RedirectToAction("manageHomWork", new { nameDars = vm_Homework.namedars, idroom = vm_Homework.RoomId, courseid = vm_Homework.CourseId,roomChatGroupID = roomChatGroupID ,returnUrl = "/Member/RoomChat" });
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("dateEror", "تاریخ وارد شده صحیح نیست ");
                    }
                }
                ViewBag.roomChatGroupID = roomChatGroupID;
                ViewBag.idroom = vm_Homework.RoomId;
                ViewBag.idcourse = vm_Homework.CourseId;
                ViewBag.nomreh = await _homework.getnomretype();
                ViewBag.nameDars = vm_Homework.namedars;
                ViewBag.listTypeHomework = await _homework.getTypeHomeWork();
                return View();
            }
            catch (Exception ex)
            {
                // throw;
                return BadRequest();
            }

        }


        public async Task<IActionResult> DeleteHomeWork(int idroom, int courseid, int homeworkid, string nameDars, int roomChatGroupID=0 ,string returnUrl = "/")
        {           
            int uid = UserContext.GetClaimValueInteger(ClaimName.UserId);
            ViewBag.roomChatGroupID = roomChatGroupID;
            ViewBag.idroom = idroom;
            ViewBag.idcourse = courseid;
            ViewBag.nameDars = nameDars;
            ViewBag.nomreh = await _homework.getnomretype();
            ViewBag.listTypeHomework = await _homework.getTypeHomeWork();
            vm_homework vm = await _homework.GetHomeworkByid(uid, homeworkid);
            ViewBag.returnUrl = returnUrl;
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteHomeWork(vm_homework vm_Homework,int roomChatGroupID=0 ,string returnUrl = "/")
        {
            ViewBag.returnUrl = returnUrl;
            try
            {               
                    int uid = UserContext.GetClaimValueInteger(ClaimName.UserId);
                    try
                    {                      
                        await _homework.Deletehomework(uid,vm_Homework.HomeworkId);
                        return RedirectToAction("manageHomWork", new { nameDars = vm_Homework.namedars, idroom = vm_Homework.RoomId, courseid = vm_Homework.CourseId, roomChatGroupID = roomChatGroupID,returnUrl = "/Member/RoomChat" });
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("dateEror", "تاریخ وارد شده صحیح نیست ");
                    }
                ViewBag.roomChatGroupID = roomChatGroupID;
                ViewBag.idroom = vm_Homework.RoomId;
                ViewBag.idcourse = vm_Homework.CourseId;
                ViewBag.nomreh = await _homework.getnomretype();
                ViewBag.nameDars = vm_Homework.namedars;
                ViewBag.listTypeHomework = await _homework.getTypeHomeWork();
                return View();
            }
            catch (Exception ex)
            {
                // throw;
                return BadRequest();
            }

        }
    }
}