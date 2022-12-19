using ElmahCore;
using ESchool.Api.Helpers;
using ESchool.Api.Need;
using EsServiceCore.DTOs.HomeWork;
using EsServiceCore.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ESchool.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api{api:apiVersion}/HomeWork")]
    [ApiController]
    [Authorize]
    public class HomeWorkController : ControllerBase
    {
        private readonly IHomeWorkService _homeWorkService;

        public HomeWorkController(IHomeWorkService homeWorkService)
        {
            _homeWorkService = homeWorkService;

        }

        [HttpGet("ManageHomWork")]
        public async Task<IActionResult> ManageHomWork( int idroom, int courseid, int pagenumber = 1, int pagesize = 10)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId, token);

                var listhomeWork = await _homeWorkService.getListHomeWorkByRoomid(userId, idroom, courseid, pagenumber, pagesize);

                var result = new ObjectResult(listhomeWork)
                {
                    StatusCode = (int)HttpStatusCode.OK
                };

                return Ok(result);


              
            }
            catch (Exception ex)
            {

                HttpContext.RiseError(ex);
                return BadRequest(new { message = ex.Message });
            }

        }


        [HttpGet("ManageHomWorkDetails")]
        public async Task<IActionResult> ManageHomWorkDetails(int idhomework)
        {
            try
            {

                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId, token);

                var list = await _homeWorkService.getDetailsHomework(idhomework, userId);

                var result = new ObjectResult(list)
                {
                    StatusCode = (int)HttpStatusCode.OK
                };

                return Ok(result);


            }
            catch (Exception ex)
            {

                HttpContext.RiseError(ex);
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpGet("ManageAnswerHomWorkDetails")]
        public async Task<IActionResult> ManageAnswerHomWorkDetails(int idhomework, int idstudents)
        {
            try
            {

                var list = await _homeWorkService.getDetailsAnswerHomework(idhomework, idstudents);

                var result = new ObjectResult(list)
                {
                    StatusCode = (int)HttpStatusCode.OK
                };

                return Ok(result);

              

            }
            catch (Exception ex)
            {

                HttpContext.RiseError(ex);
                return BadRequest(new { message = ex.Message });
            }

        }


        [HttpPost("ManageAnswerHomWorkDetails")]
        public async Task<IActionResult> ManageAnswerHomWorkDetails(vm_HomeworkDetailsShowByIDResult vm, int id, bool isnumberic, int homeworkid, string dateend, string returnUrl = "/", string thisurl = "/")
        {

            try
            {
                if (id == vm.HomeworkAnswerID)
                {
                   
                    DateTime edndate = dateend.ToGeorgianDateTimeByMinutes();
                    bool x = await _homeWorkService.updateAnswerHomeWork_fromTeacher(vm, isnumberic, edndate);
                    if (x)
                    {
                        var result = new ObjectResult(true)
                        {
                            StatusCode = (int)HttpStatusCode.OK
                        };

                        return Ok(result);
                    }
                   
                }
               
                var list = await _homeWorkService.getDetailsAnswerHomework(homeworkid, vm.UserID);
                var result2 = new ObjectResult(list)
                {
                    StatusCode = (int)HttpStatusCode.OK
                };

                return Ok(result2);
            }
            catch (Exception ex)
            {

                HttpContext.RiseError(ex);
                return BadRequest(new { message = ex.Message });
            }

        }

            //public async Task<IActionResult> CreateHomeWork(int idroom, int courseid, string nameDars, int roomChatGroupID = 0, string returnUrl = "/")
            //{
            //    ViewBag.returnUrl = returnUrl;
            //    ViewBag.nameDars = nameDars;
            //    ViewBag.idroom = idroom;
            //    ViewBag.idcourse = courseid;
            //    ViewBag.nomreh = await _homework.getnomretype();
            //    ViewBag.roomChatGroupID = roomChatGroupID;
            //    ViewBag.listTypeHomework = await _homework.getTypeHomeWork();
            //    return View();
            //}

        [HttpPost("CreateHomeWork")]
        public async Task<IActionResult> CreateHomeWork([FromForm]vm_homework vm_Homework, int roomChatGroupID)
        {
            
            try
            {

                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId, token);
                if (vm_Homework.HomeWorkFileName_file != null)
                {
                    vm_Homework.HomeWorkFileName = await StoreFile(vm_Homework.HomeWorkFileName_file);
                }
               
                DateTime satartdate = vm_Homework.start.ToGeorgianDateTimeByMinutes();
                DateTime enddate = vm_Homework.end.ToGeorgianDateTimeByMinutes();
                await _homeWorkService.addHomework(vm_Homework, userId, satartdate, enddate, roomChatGroupID);
                
                
                
                var result = new ObjectResult(vm_Homework)
                {
                    StatusCode = (int)HttpStatusCode.OK
                };

                return Ok(result);

            }
            catch (Exception ex)
            {

                HttpContext.RiseError(ex);
                return BadRequest(new { message = ex.Message });
            }


        }


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



        [HttpGet("UpdateHomeWorkGet")]
        public async Task<IActionResult> UpdateHomeWorkGet( int homeworkid)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId, token);

                vm_homework vm = await _homeWorkService.GetHomeworkByid(userId, homeworkid);

                var result = new ObjectResult(vm)
                {
                    StatusCode = (int)HttpStatusCode.OK
                };

                return Ok(result);
            }
            catch (Exception ex)
            {

                HttpContext.RiseError(ex);
                return BadRequest(new { message = ex.Message });
            }




        }


        [HttpPost("UpdateHomeWork")]
        public async Task<IActionResult> UpdateHomeWork([FromForm]vm_homework vm_Homework, int roomChatGroupID = 0, string returnUrl = "/")
        {
          
            try
            {

                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId, token);
                if (vm_Homework.HomeWorkFileName_file != null)
                {
                    vm_Homework.HomeWorkFileName = await StoreFile(vm_Homework.HomeWorkFileName_file);
                }
                  
                DateTime satartdate = vm_Homework.start.ToGeorgianDateTimeByMinutes();
                DateTime enddate = vm_Homework.end.ToGeorgianDateTimeByMinutes();
                await _homeWorkService.updatehomework(userId, vm_Homework, satartdate, enddate);

                var result = new ObjectResult(vm_Homework)
                {
                    StatusCode = (int)HttpStatusCode.OK
                };

                return Ok(result);

            }
            catch (Exception ex)
            {

                HttpContext.RiseError(ex);
                return BadRequest(new { message = ex.Message });
            }

        }


        [HttpGet("DeleteHomeWorkGet")]
        public async Task<IActionResult> DeleteHomeWork(int homeworkid)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId, token);

                vm_homework vm = await _homeWorkService.GetHomeworkByid(userId, homeworkid);
                var result = new ObjectResult(vm)
                {
                    StatusCode = (int)HttpStatusCode.OK
                };

                return Ok(result);

            }
            catch(Exception ex)
            {

                HttpContext.RiseError(ex);
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPost("DeleteHomeWork")]
        public async Task<IActionResult> DeleteHomeWork(vm_homework vm_Homework, int roomChatGroupID = 0, string returnUrl = "/")
        {

            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId, token);

                await _homeWorkService.Deletehomework(userId, vm_Homework.HomeworkId);
                var result = new ObjectResult(vm_Homework)
                {
                    StatusCode = (int)HttpStatusCode.OK
                };

                return Ok(result);
            }
            catch (Exception ex)
            {

                HttpContext.RiseError(ex);
                return BadRequest(new { message = ex.Message });
            }
        }
           
    }
}