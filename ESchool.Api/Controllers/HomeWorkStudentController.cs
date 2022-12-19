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
    [Route("api{api:apiVersion}/HomeWorkStudent")]
    [ApiController]
    [Authorize]
    public class HomeWorkStudentController : ControllerBase
    {
        private readonly IHomeWorkService _homeWorkService;

        public HomeWorkStudentController(IHomeWorkService homeWorkService)
        {
            _homeWorkService = homeWorkService;

        }

        [HttpGet("HomeWorkStudentGet")]
        public async Task<IActionResult> Index(int idroom, int idcours, int pagenumber = 1, int pagesize = 10)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId, token);

                var list = await _homeWorkService.getListHomeworkForStudents(userId, idroom, idcours, pagenumber, pagesize);

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


        [HttpGet("SendHomeworkStudentGet")]
        public async Task<IActionResult> SendHomeworkStudent(int id)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId, token);


                var list = await _homeWorkService.getHomeworkByID(id, userId);
                //    var x =await _homework.getHomeworkByID(id, 725);   
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


        [HttpPost("SendHomeworkStudent")]
        public async Task<IActionResult> SendHomeworkStudent(vm_AnsverhomeWork homeworkAnswer)
        {
            try
            {

                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId, token);


                await _homeWorkService.updateAnswerHomeWork(homeworkAnswer, userId);

             
                var result = new ObjectResult(homeworkAnswer)
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

                var result = new { status = "ok", path = savefilename, mimtype = file.ContentType };

              

                var result2 = new ObjectResult(result)
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




        [HttpPost("AddResponseFileHomeWork")]
        public async Task<IActionResult> AddResponseFileHomeWork(int homeworkanswerId, string filenam)
        {
            try
            {
                int fid = await _homeWorkService.addAnsverFile(filenam, homeworkanswerId);

                var result = new { status = "ok", id = fid };


                var result2 = new ObjectResult(result)
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

        [HttpPost("DeleteFileHomework")]
        public async Task<IActionResult> DeleteFileHomework(int idanswerfile, int idhomeworkAnswer)
        {
            try
            {
                string filename = await _homeWorkService.removeAnsverFile(idanswerfile, idhomeworkAnswer);
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
                    var result1 = new { status = "ok" };
                    var result2 = new ObjectResult(result1)
                    {
                        StatusCode = (int)HttpStatusCode.OK
                    };

                    return Ok(result2);
                }

                var result = new { status = "eror", message = "NotFoundFile" };
                var result3 = new ObjectResult(result)
                {
                    StatusCode = (int)HttpStatusCode.OK
                };

                return Ok(result3);
            }
            catch (Exception ex)
            {

                HttpContext.RiseError(ex);
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}