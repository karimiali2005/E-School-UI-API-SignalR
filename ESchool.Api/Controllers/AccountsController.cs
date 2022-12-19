using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ElmahCore;
using Es.DataLayerCore.DTOs.User;
using ESchool.Api.Helpers;
using ESchool.Api.Models;
using ESchool.Api.Need;
using EsServiceCore.Interface;
using EsServiceCore.Utility.Convertor;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api{api:apiVersion}/Accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountsController(IUserService userService)
        {
            _userService = userService;           
        }

        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate(LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("The Model Is Not Valid");
                }

                var user = await _userService.LoginUser(model);



                if (user != null)
                {

                    if (user.UserActive == 0 || user.UserActive == 2)
                    {
                        string ReasonInactive = string.IsNullOrEmpty(user.ReasonInactive) ? "" : Convert.ToString(user.ReasonInactive);

                        //  await HttpContext.SignInAsync("AccountAuth", new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = login.RememberMe });
                        if (user.UserActive == 2)
                        {
                            if (string.IsNullOrEmpty(user.Password) && user.BirthDate.getPersianDate().Substring(0, 4) != model.UsersPass)
                            {
                                return BadRequest(new { message = "کاربری با مشخصات وارد شده یافت نشد" });
                            }
                            // authentication successful so generate jwt token
                            user.Token = TokenGenerator.GenerateJwtToken(user);


                            /*if (string.IsNullOrEmpty(userModel.Password))
                            {
                                return Redirect("/Account/ChangePass/force");
                            }*/
                            //if (string.IsNullOrEmpty(returnUrl))
                            //{
                            //    return Redirect("/Member/RoomChat");
                            //}
                            //else
                            //{
                            //    return Redirect(returnUrl);
                            //}
                        }
                        else
                        {
                            /*messageModel.MessageType = "Warning";
                            messageModel.MessageText = UserType + " گرامی، " + Nickname + " کاربری شما معلق شده است. به علت " + "<br/>";
                            messageModel.MessageText += ReasonInactive;
                            TempData["MessageModel"] = JsonConvert.SerializeObject(messageModel);
                            ViewBag.MessageModel = messageModel;
                            return Redirect("/Account/MsgShow");*/
                            // return BadRequest(new { message = "کاربری شما به علت " + ReasonInactive + " معلق شده است" });
                            return BadRequest(new { message = "کاربری شما به علت " + ReasonInactive + " معلق شده است" });
                        }

                    }
                    if (user.UserActive == 1)
                    {
                        return BadRequest(new { message = "کاربری شما غیرفعال است" });

                    }
                }
                else
                {
                    return BadRequest(new { message = "کاربری با مشخصات وارد شده یافت نشد" });
                }


                user.Password = "";

                await _userService.UserPlatformAdd(user.UserID, model.PlatfornName, model.mobileName, model.TokenFireBase);

                return Ok(user);
            }
            catch (Exception ex)
            {

                HttpContext.RiseError(ex);
                return BadRequest();
            }
         
        }

        [HttpPost("Test")]
        public async Task<IActionResult> Test([FromBody] LoginViewModel model)
        {
           
            return Ok();
        }

        [HttpGet("Test2")]
        public async Task<IActionResult> Test2()
        {

            return Ok(new LoginViewModel
            {
                UsersName = "ali",
                UsersPass = "123",
                RememberMe = true
            });
        }

        [HttpGet("SettingGet")]
        [Authorize]
        public IActionResult SettingGet()
        {
            try
            {
                var result = new SettingContextViewModel()
                {
                    ConnectionStrings = SettingContext.ConnectionStrings.Instance,
                    AdminPanel = SettingContext.AdminPanel.Instance,
                    PathStoreFiles = SettingContext.PathStoreFiles.Instance,
                    PathStoreProfileImage = SettingContext.PathStoreProfileImage.Instance,
                    RoomChatSatus = SettingContext.RoomChatSatus.Instance,
                    UploadSize = SettingContext.UploadSize.Instance
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                HttpContext.RiseError(ex);
                return BadRequest(new { message = ex.Message });
            }
        }

 /*       [HttpGet("GetUserPic")]
       // [Authorize]
        public HttpResponseMessage GetUserPic(string picName)
        {
            try
            {

                byte[] imgData = Codes.ConvertImageToArray(picName);
                MemoryStream ms = new MemoryStream(imgData);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StreamContent(ms);
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
            
                return response;
            }
            catch (Exception ex)
            {
                HttpContext.RiseError(ex);
                // return BadRequest(new { message = ex.Message });
                return null;
            }

        }
*/

        [HttpGet("GetUserPic")]
         [Authorize]
        public string GetUserPic(string picName)
        {
            try
            {

                byte[] imgData = Codes.ConvertImageToArray(picName);
                string response = Convert.ToBase64String(imgData);
                return response;
            }
            catch (Exception ex)
            {
                HttpContext.RiseError(ex);
                // return BadRequest(new { message = ex.Message });
                return null;
            }

        }

        [HttpGet("GetUserPicName")]
        [Authorize]
        //  [HttpRequestMessage]
        public async Task<IActionResult> GetUserPicName(int userId)
        {
            try
            {
                var user =await _userService.GetGetById(userId);

                var userViewModel=new UserPicViewModel()
                {
                    UserID = user.UserId,
                    UserPicName = user.PicName
                };
                return Ok(userViewModel);
            }
            catch (Exception ex)
            {
                HttpContext.RiseError(ex);
                // return BadRequest(new { message = ex.Message });
                return null;
            }

        }


        [HttpPost("UserSetTicket")]
        [Authorize]
        public async Task<IActionResult> UserSetTicket()
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId, token);
                var returnString = await _userService.UserSetTicket(userId);
                var result = new ObjectResult(returnString)
                {
                    StatusCode = (int)HttpStatusCode.OK
                };

                return Ok(result);
                //return Json(new { roomChatModel = roomChatReturn });
            }
            catch (Exception ex)
            {

                HttpContext.RiseError(ex);
                return BadRequest(new { message = ex.Message });
            }

        }
    }
}