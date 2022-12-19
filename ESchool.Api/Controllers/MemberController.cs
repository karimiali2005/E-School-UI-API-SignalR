using ElmahCore;
using Es.DataLayerCore.DTOs.RoomChat;
using Es.DataLayerCore.Model;
using ESchool.Api.Helpers;
using ESchool.Api.Need;

using EsServiceCore.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ESchool.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api{api:apiVersion}/Member")]
    [ApiController]
    [Authorize]
    public class MemberController : ControllerBase
    {
        private readonly IRoomChatService _roomChatService;


        public MemberController(IRoomChatService roomChatService)
        {
            _roomChatService = roomChatService;

        }

        [HttpGet("RoomChatRight")]
        public async Task<IActionResult> RoomChatRight()
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId, token);
                var userTypeId = UserContext.GetClaimValueInteger(ClaimName.UserTypeId, token);
                var roomChatRight = await _roomChatService.RoomChatRightShow(userId, userTypeId);
                var roomLive = await _roomChatService.RoomLiveShow(userId);

                /*var room = roomChatRight.FirstOrDefault();

                var roomChatLeft =
                    await _roomChatService.RoomChatLeftShow(room.RoomID, room.TeacherID, (int) room.CourseID);*/

                var roomChatLeftViewModel = new RoomChatLeftViewModel()
                {
                    RoomChatLeftShowResult = null,
                    RoomChatLeftPropertyResult = null
                };
                var roomChatRightViewModel = new RoomChatRightViewModel
                {
                    RoomChatRightShowResult = roomChatRight,
                    RoomLiveShows = roomLive
                };

                var roomChatViewModel = new RoomChatViewModel()
                {
                    RoomChatLeftViewModel = roomChatLeftViewModel,
                    RoomChatRightViewModel = roomChatRightViewModel
                };



                var result = new ObjectResult(roomChatViewModel)
                {
                    StatusCode = (int)HttpStatusCode.OK
                };

                return Ok(result);

            }
            catch (Exception ex)
            {

                // ex.ToSaveElmah();
                HttpContext.RiseError(ex);
                return BadRequest(new { message = ex.Message });
            }

        }


        [HttpGet("RoomChatLeft")]
        public async Task<IActionResult> RoomChatLeft(int roomChatGroupID, int roomChatGroupType, bool tagLearn, int newChatCount, int? roomId, int teacherId, int? courseId, int pageNumber, int pageSize, string picName, string roomChatGroupTitle, string searchText)
        {
            try
            {

                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId, token);
                var userTypeId = UserContext.GetClaimValueInteger(ClaimName.UserTypeId, token);




                RoomChatLeftPropertyResult roomChatLeftPropertyResult;
                if (roomId != null && roomId != 0)
                {
                    roomChatLeftPropertyResult = await _roomChatService.RoomChatLeftPropertyShow((int)roomId, roomChatGroupID);
                }
                else
                    roomChatLeftPropertyResult = new RoomChatLeftPropertyResult();

                roomChatLeftPropertyResult.RoomID = roomId == 0 ? null : roomId;
                roomChatLeftPropertyResult.CourseID = courseId == 0 ? null : courseId;
                roomChatLeftPropertyResult.TeacherID = teacherId;
                roomChatLeftPropertyResult.RoomChatGroupID = roomChatGroupID;
                roomChatLeftPropertyResult.PicName = picName ?? "";
                roomChatLeftPropertyResult.RoomChatGroupTitle = roomChatGroupTitle;
                roomChatLeftPropertyResult.RoomChatGroupType = roomChatGroupType;


                if (userTypeId != 4)
                {
                    roomChatLeftPropertyResult.PermissionStudentChatEdit = false;
                    roomChatLeftPropertyResult.PermissionStudentChatDelete = false;
                }

                var tuple = await _roomChatService.RoomChatLeftShow(roomChatGroupID, tagLearn, userId, newChatCount, pageNumber, pageSize, searchText);

                roomChatLeftPropertyResult.RoomChatViewLast = tuple.Item2;

                var roomChatLeftViewModel = new RoomChatLeftViewModel()
                {
                    RoomChatLeftPropertyResult = roomChatLeftPropertyResult,
                    RoomChatLeftShowResult = tuple.Item1
                };
                var result = new ObjectResult(roomChatLeftViewModel)
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

        [HttpGet("RoomChatLeft2")]
        public async Task<IActionResult> RoomChatLeft2(int roomChatGroupID, bool tagLearn, int pageNumber, int pageSize, bool permissionStudentChatEdit, bool permissionStudentChatDelete, string searchText)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId, token);


                var roomChatLeftPropertyResult = new RoomChatLeftPropertyResult()
                {
                    PermissionStudentChatEdit = permissionStudentChatEdit,
                    PermissionStudentChatDelete = permissionStudentChatDelete
                };

                var tuple = await _roomChatService.RoomChatLeftShow(roomChatGroupID, tagLearn, userId, 0, pageNumber, pageSize, searchText);

                var roomChatLeftViewModel = new RoomChatLeftViewModel()
                {
                    RoomChatLeftPropertyResult = roomChatLeftPropertyResult,
                    RoomChatLeftShowResult = tuple.Item1
                };




                var result = new ObjectResult(roomChatLeftViewModel)
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

        [HttpPost("RoomChatDelete")]
        public async Task<IActionResult> RoomChatDelete(int roomChatId)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId, token);
                var userTypeId = UserContext.GetClaimValueInteger(ClaimName.UserTypeId, token);


                var roomChat = await _roomChatService.RoomChatDelete(roomChatId, userTypeId != 4 ? true : false, userTypeId != 4 ? Convert.ToInt32(SettingContext.RoomChatSatus.Instance.ChatDeleteTime) : 0);

                if (roomChat != null)
                {
                    var roomChatReturn = new ChatMessage
                    {
                        chatType = "D",
                        groupId = (int)roomChat.RoomChatGroupId,
                        senderId = userId,
                        attachMsg = roomChat.AttachMsg,
                        filename = "",
                        mimeType = "",

                        roomChatId = roomChat.RoomChatId,
                        senderName = "",
                        tagLearn = roomChat.TagLearn,
                        textChat = "",
                        roomChatParentId = null,
                        parentTextChat = "",
                        parentSenderName = "",
                        mainAddress = "",
                        roomChatViewNumber = 0,
                        roomChatDateString = "",

                    };
                    var result = new ObjectResult(roomChatReturn)
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        //Value=true
                    };

                    return Ok(result);
                    //return Json(new { roomChatModel = roomChatReturn, allowDelete = true });
                }
                else
                {
                    var roomChatReturn = new ChatMessage
                    {
                        chatType = "DM",
                        groupId = (int)roomChat.RoomChatGroupId,
                        senderId = userId,
                        attachMsg = roomChat.AttachMsg,
                        filename = "",
                        mimeType = "",

                        roomChatId = roomChat.RoomChatId,
                        senderName = "",
                        tagLearn = roomChat.TagLearn,
                        textChat = "",
                        roomChatParentId = null,
                        parentTextChat = "",
                        parentSenderName = "",
                        mainAddress = "",
                        roomChatViewNumber = 0,
                        roomChatDateString = "",

                    };
                    var result = new ObjectResult(roomChatReturn)
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        //   Value = false
                    };

                    return Ok(result);
                }

            }
            catch (Exception ex)
            {

                HttpContext.RiseError(ex);
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpPost("RoomChatInsert")]
        public async Task<IActionResult> RoomChatInsert(int roomChatGroupId, string textChat, string fileName, bool tagLearn, int? roomChatParentId, int? roomId, int? teacherId, int? courseId, string parentTextChat, string parentSenderName, string roomChatGroupTitle, int? roomChatGroupType)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId, token);
                var senderName = UserContext.GetClaimValueString(ClaimName.UserType, token) + " : " +
                                 UserContext.GetClaimValueString(ClaimName.Nickname, token);
                var userTypeId = UserContext.GetClaimValueInteger(ClaimName.UserTypeId, token);

                var roomChat = new RoomChat()
                {
                    RoomChatDate = DateTime.Now,
                    SenderId = userId,
                    SenderName = senderName,
                    RecieverId = null,
                    RecieverName = "",
                    RoomId = roomId == 0 ? null : roomId,
                    TeacherId = teacherId == 0 ? null : teacherId,
                    CourseId = courseId == 0 ? null : courseId,
                    TextChat = textChat,
                    Filename = fileName ?? "",
                    MimeType = Codes.GetMimeType(fileName),
                    TagLearn = tagLearn,
                    RoomChatDelete = false,
                    RoomChatUpdate = null,
                    RoomChatParentId = roomChatParentId == 0 ? null : roomChatParentId,
                    AttachMsg = false,
                    RoomChatGroupId = roomChatGroupId
                };
                await _roomChatService.RoomChatInsert(roomChat);
                await _roomChatService.RoomChatViewInsert(roomChat.RoomChatId, roomChatGroupId, userId);

                if (roomChatGroupType > 1)
                {
                    await _roomChatService.RoomChatGroupUpdate(roomChatGroupId);
                }

                var roomChatReturn = new ChatMessage
                {
                    chatType = "C",
                    groupId = roomChatGroupId,
                    senderId = userId,
                    attachMsg = roomChat.AttachMsg,
                    filename = roomChat.Filename ?? "",
                    mimeType = roomChat.MimeType ?? "",

                    roomChatId = roomChat.RoomChatId,
                    senderName = roomChat.SenderName,
                    tagLearn = roomChat.TagLearn,
                    textChat = roomChat.TextChat ?? "",
                    roomChatParentId = roomChat.RoomChatParentId==0?null: roomChat.RoomChatParentId,
                    parentTextChat = parentTextChat ?? "",
                    parentSenderName = parentSenderName ?? "",
                    mainAddress = "",
                    roomChatViewNumber = 0,
                    roomChatDateString = roomChat.RoomChatDate.ToString("HH:mm"),
                    roomChatDate = roomChat.RoomChatDate,
                    roomChatGroupTitle= roomChatGroupType == 1 ? roomChatGroupTitle : roomChat.SenderName,
                    roomChatGroupType = roomChatGroupType


                };
               

                var result = new ObjectResult(roomChatReturn)
                {
                    StatusCode = (int)HttpStatusCode.OK
                    //  Value = userTypeId
                };

                return Ok(result);

                // return Json(new { roomChatModel = roomChatReturn, userTypeId = userTypeId });
            }
            catch (Exception ex)
            {

                HttpContext.RiseError(ex);
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpPost("RoomChatEdit")]
        public async Task<IActionResult> RoomChatEdit(int roomChatId, string textChat, bool tagLearn, string parentTextChat, string parentSenderName)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId, token);

                var tuple = await _roomChatService.RoomChatUpdate(roomChatId, textChat, tagLearn);
                var roomChat = tuple.Item1;
                if (tuple.Item2 == true && !string.IsNullOrEmpty(roomChat.Filename))
                {
                    var address = SettingContext.PathStoreFiles.Instance.Address;
                    var fullAddressFtpCut = address;
                    var fullAddressFtpPaste = address;

                    if (tagLearn)
                    {
                        fullAddressFtpCut += SettingContext.PathStoreFiles.Instance.NormalFile + roomChat.RoomChatFolder + "/";
                        fullAddressFtpPaste += SettingContext.PathStoreFiles.Instance.LearnFile + roomChat.RoomChatFolder + "/";
                    }
                    else
                    {
                        fullAddressFtpCut += SettingContext.PathStoreFiles.Instance.LearnFile + roomChat.RoomChatFolder + "/";
                        fullAddressFtpPaste += SettingContext.PathStoreFiles.Instance.NormalFile + roomChat.RoomChatFolder + "/";
                    }
                    Codes.CopyFile(fullAddressFtpCut, fullAddressFtpPaste, roomChat.Filename, roomChat.Filename, SettingContext.PathStoreFiles.Instance.UserId, SettingContext.PathStoreFiles.Instance.Password);
                    Codes.DeleteFile(fullAddressFtpCut, roomChat.Filename, SettingContext.PathStoreFiles.Instance.UserId, SettingContext.PathStoreFiles.Instance.Password);
                }

                var roomChatReturn = new ChatMessage
                {
                    chatType = "E",
                    groupId = (int)roomChat.RoomChatGroupId,
                    senderId = userId,
                    attachMsg = roomChat.AttachMsg,
                    filename = roomChat.Filename ?? "",
                    mimeType = roomChat.MimeType ?? "",

                    roomChatId = roomChat.RoomChatId,
                    senderName = roomChat.SenderName,
                    tagLearn = roomChat.TagLearn,
                    textChat = roomChat.TextChat ?? "",
                    roomChatParentId = roomChat.RoomChatParentId==0?null: roomChat.RoomChatParentId,
                    parentTextChat = parentTextChat ?? "",
                    parentSenderName = parentSenderName ?? "",
                    mainAddress = "",
                    roomChatViewNumber = 0,
                    roomChatDateString = roomChat.RoomChatDate.ToString("HH:mm"),

                };

                var result = new ObjectResult(roomChatReturn)
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

        [HttpPost("RoomChatPin")]
        public async Task<IActionResult> RoomChatPin(int roomChatGroupId, int roomChatId, bool isPin)
        {
            try
            {

                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId, token);
                var userTypeId = UserContext.GetClaimValueInteger(ClaimName.UserTypeId, token);

                var roomChat = await _roomChatService.RoomChatPin(roomChatGroupId, roomChatId, isPin);

                var roomChatReturn = new ChatMessage
                {
                    chatType = isPin ? "P" : "UP",
                    groupId = (int)roomChat.RoomChatGroupId,
                    senderId = userId,
                    attachMsg = roomChat.AttachMsg,
                    filename = "",
                    mimeType = "",

                    roomChatId = roomChat.RoomChatId,
                    senderName = "",
                    tagLearn = roomChat.TagLearn,
                    textChat = roomChat.TextChat,
                    roomChatParentId = null,
                    parentTextChat = "",
                    parentSenderName = "",
                    mainAddress = "",
                    roomChatViewNumber = 0,
                    roomChatDateString = "",

                };
                var result = new ObjectResult(roomChatReturn)
                {
                    StatusCode = (int)HttpStatusCode.OK,
                   // Value = userTypeId
                };

                return Ok(result);

                // return Json(new { roomChatModel = roomChatReturn, userTypeId = UserContext.GetClaimValueInteger(ClaimName.UserTypeId) });
            }
            catch (Exception ex)
            {

                HttpContext.RiseError(ex);
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpPost("RoomChatLock")]
        public async Task<IActionResult> RoomChatLock(int roomChatGroupId, bool isLock)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId, token);
                var userTypeId = UserContext.GetClaimValueInteger(ClaimName.UserTypeId, token);

                var result = await _roomChatService.RoomChatLock(roomChatGroupId, isLock);

                var roomChatReturn = new ChatMessage
                {
                    chatType = isLock ? "L" : "UL",
                    groupId = roomChatGroupId,
                    senderId = userId,
                    attachMsg = false,
                    filename = "",
                    mimeType = "",

                    roomChatId = 0,
                    senderName = "",
                    tagLearn = false,
                    textChat = "",
                    roomChatParentId = null,
                    parentTextChat = "",
                    parentSenderName = "",
                    mainAddress = "",
                    roomChatViewNumber = 0,
                    roomChatDateString = "",

                };
                var result2 = new ObjectResult(roomChatReturn)
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Value = userTypeId
                };

                return Ok(result2);

                // return Json(new { roomChatModel = roomChatReturn });
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

        [HttpPost("SendRecordAudio")]
        [RequestFormLimits(MultipartBodyLengthLimit = 2147483648)]
        [RequestSizeLimit(2147483648)]
        public async Task<IActionResult> SendRecordAudio([FromForm] IFormFile file, string tagLearn, int roomChatGroupId, string textChat, int? roomChatParentId, int? roomId, int? teacherId, int? courseId, string parentTextChat, string parentSenderName,string roomChatGroupTitle, int? roomChatGroupType)
        {
            try
            {
                var dateTimeNow = DateTime.Now;
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId, token);
                var senderName = UserContext.GetClaimValueString(ClaimName.UserType, token) + " : " +
                                 UserContext.GetClaimValueString(ClaimName.Nickname, token);
                var userTypeId = UserContext.GetClaimValueInteger(ClaimName.UserTypeId, token);

                var address = SettingContext.PathStoreFiles.Instance.Address;
                var fullAddressFtp = address;

                if (tagLearn.Equals("true"))
                {
                    fullAddressFtp += SettingContext.PathStoreFiles.Instance.LearnFile;
                }
                else
                {
                    fullAddressFtp += SettingContext.PathStoreFiles.Instance.NormalFile;
                }



                string filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                filename = this.EnsureCorrectFilename(filename);

                string savefilename = Codes.GetRandomFileName(filename);
                var requestUri = fullAddressFtp + "Month" + dateTimeNow.ToString("yyyyMMdd") + "/" + savefilename;



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

                var roomChat = new RoomChat()
                {
                    RoomChatDate = dateTimeNow,
                    SenderId = userId,
                    SenderName = senderName,
                    RecieverId = null,
                    RecieverName = "",
                    RoomId = roomId == 0 ? null : roomId,
                    TeacherId = teacherId == 0 ? null : teacherId,
                    CourseId = courseId == 0 ? null : courseId,
                    TextChat = textChat ?? "",
                    Filename = savefilename ?? "",
                    MimeType = Codes.GetMimeType(savefilename),
                    TagLearn = Convert.ToBoolean(tagLearn),
                    RoomChatDelete = false,
                    RoomChatUpdate = null,
                    RoomChatParentId = roomChatParentId == 0 ? null : roomChatParentId,
                    AttachMsg = false,
                    RoomChatGroupId = roomChatGroupId,
                    RoomChatFolder = "Month" + dateTimeNow.ToString("yyyyMMdd")
                };

                await _roomChatService.RoomChatInsert(roomChat);
                await _roomChatService.RoomChatViewInsert(roomChat.RoomChatId, roomChatGroupId, userId);

                if (roomChatGroupType > 1)
                {
                    await _roomChatService.RoomChatGroupUpdate(roomChatGroupId);
                }


                var roomChatReturn = new ChatMessage
                {
                    chatType = "C",
                    groupId = roomChatGroupId,
                    senderId = userId,
                    attachMsg = roomChat.AttachMsg,
                    filename = roomChat.Filename ?? "",
                    mimeType = roomChat.MimeType ?? "",

                    roomChatId = roomChat.RoomChatId,
                    senderName = roomChat.SenderName,
                    tagLearn = roomChat.TagLearn,
                    textChat = roomChat.TextChat ?? "",
                    roomChatParentId = roomChat.RoomChatParentId==0?null: roomChat.RoomChatParentId,
                    parentTextChat = parentTextChat ?? "",
                    parentSenderName = parentSenderName ?? "",
                    mainAddress = SettingContext.PathStoreFiles.Instance.Path1 + (roomChat.TagLearn ? SettingContext.PathStoreFiles.Instance.LearnFile : SettingContext.PathStoreFiles.Instance.NormalFile) + "Month" + dateTimeNow.ToString("yyyyMMdd") + "/" + savefilename,
                    roomChatViewNumber = 0,
                    roomChatDateString = roomChat.RoomChatDate.ToString("HH:mm"),
                    roomChatGroupTitle= roomChatGroupType == 1 ? roomChatGroupTitle : roomChat.SenderName,
                    roomChatGroupType = roomChatGroupType

                };

                var result = new ObjectResult(roomChatReturn)
                {
                    StatusCode = (int)HttpStatusCode.OK,

                };

                return Ok(result);

                //  return Json(new { roomChatModel = roomChatReturn, userTypeId = UserContext.GetClaimValueInteger(ClaimName.UserTypeId) });
            }
            catch (Exception ex)
            {
                HttpContext.RiseError(ex);
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPost("StoreFile")]
        [RequestFormLimits(MultipartBodyLengthLimit = 2147483648)]
        [RequestSizeLimit(2147483648)]

        public async Task<IActionResult> StoreFile([FromForm] IFormFile file, string tagLearn, int roomChatGroupId, string textChat, int? roomChatParentId, int? roomId, int? teacherId, int? courseId, string parentTextChat, string parentSenderName, string roomChatGroupTitle, int? roomChatGroupType)
        {
            try
            {
                var dateTimeNow = DateTime.Now;

                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId, token);
                var senderName = UserContext.GetClaimValueString(ClaimName.UserType, token) + " : " +
                                 UserContext.GetClaimValueString(ClaimName.Nickname, token);
                var userTypeId = UserContext.GetClaimValueInteger(ClaimName.UserTypeId, token);

                var address = SettingContext.PathStoreFiles.Instance.Address;
                var fullAddressFtp = address;

                if (tagLearn.Equals("true"))
                {
                    fullAddressFtp += SettingContext.PathStoreFiles.Instance.LearnFile;
                }
                else
                {
                    fullAddressFtp += SettingContext.PathStoreFiles.Instance.NormalFile;
                }



                string filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                filename = this.EnsureCorrectFilename(filename);

                string savefilename = Codes.GetRandomFileName(filename);
                var requestUri = fullAddressFtp + "Month" + dateTimeNow.ToString("yyyyMMdd") + "/" + savefilename;



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



                var roomChat = new RoomChat()
                {
                    RoomChatDate = dateTimeNow,
                    SenderId = userId,
                    SenderName = senderName,
                    RecieverId = null,
                    RecieverName = "",
                    RoomId = roomId == 0 ? null : roomId,
                    TeacherId = teacherId == 0 ? null : teacherId,
                    CourseId = courseId == 0 ? null : courseId,
                    TextChat = textChat ?? "",
                    Filename = savefilename ?? "",
                    MimeType = Codes.GetMimeType(savefilename),
                    TagLearn = Convert.ToBoolean(tagLearn),
                    RoomChatDelete = false,
                    RoomChatUpdate = null,
                    RoomChatParentId = roomChatParentId == 0 ? null : roomChatParentId,
                    AttachMsg = false,
                    RoomChatGroupId = roomChatGroupId,
                    RoomChatFolder = "Month" + dateTimeNow.ToString("yyyyMMdd")
                };
                await _roomChatService.RoomChatInsert(roomChat);
                await _roomChatService.RoomChatViewInsert(roomChat.RoomChatId, roomChatGroupId, userId);

                if (roomChatGroupType > 1)
                {
                    await _roomChatService.RoomChatGroupUpdate(roomChatGroupId);
                }



                var roomChatReturn = new ChatMessage
                {
                    chatType = "C",
                    groupId = roomChatGroupId,
                    senderId = userId,
                    attachMsg = roomChat.AttachMsg,
                    filename = roomChat.Filename ?? "",
                    mimeType = roomChat.MimeType ?? "",

                    roomChatId = roomChat.RoomChatId,
                    senderName = roomChat.SenderName,
                    tagLearn = roomChat.TagLearn,
                    textChat = roomChat.TextChat ?? "",
                    roomChatParentId = roomChat.RoomChatParentId,
                    parentTextChat = parentTextChat ?? "",
                    parentSenderName = parentSenderName ?? "",
                    mainAddress = SettingContext.PathStoreFiles.Instance.Path1 + (roomChat.TagLearn ? SettingContext.PathStoreFiles.Instance.LearnFile : SettingContext.PathStoreFiles.Instance.NormalFile) + "Month" + dateTimeNow.ToString("yyyyMMdd") + "/" + savefilename,
                    roomChatViewNumber = 0,
                    roomChatDateString = roomChat.RoomChatDate.ToString("HH:mm"),
                    roomChatGroupTitle= roomChatGroupType == 1 ? roomChatGroupTitle : roomChat.SenderName,
                    roomChatGroupType = roomChatGroupType

                };
                var result = new ObjectResult(roomChatReturn)
                {
                    StatusCode = (int)HttpStatusCode.OK
                    //  Value = userTypeId
                };

                return Ok(result);
                // return Json(new { roomChatModel = roomChatReturn, userTypeId = UserContext.GetClaimValueInteger(ClaimName.UserTypeId) });
            }
            catch (Exception ex)
            {
                HttpContext.RiseError(ex);
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("RoomChatGroupOnlineShow")]
        public async Task<IActionResult> RoomChatGroupOnlineShow(int roomChatGroupId,string userListOnline)
        {
            try
            {
                List<int> listonline = new List<int>();

                foreach (var item in userListOnline.Split(","))
                {
                    if (!String.IsNullOrEmpty(item))
                    {
                        listonline.Add((int)Math.Round(Convert.ToDouble(item)));
                    }
                }

                // Dictionary<string, int> userListOnline = ChatGroupHub._userList;
               // List<int> userListOnline2 = userListOnline;
                var memberGroup = await _roomChatService.RoomChatGroupMemberShow(roomChatGroupId);
                var result = (from m in memberGroup.ToList()
                              join list in listonline on m.UserID equals list into ps
                              from listNew in ps.DefaultIfEmpty()
                              select new RoomChatGroupOnlineViewModel()
                              {
                                  UserID = m.UserID,
                                  FullName = m.FullName,
                                  IsOnline = (listNew!=0) ? true : false,
                                  // PicName = Convert.ToBase64String(Codes.ConvertImageToArray(m.PicName))
                              }
                            ).ToList();

                var result2 = new ObjectResult(result)
                {
                    StatusCode = (int)HttpStatusCode.OK
                };

                return Ok(result2);

                //  return Json(new { roomChatGroupOnline = result });
            }
            catch (Exception ex)
            {

                HttpContext.RiseError(ex);
                return BadRequest(new { message = ex.Message });
            }


        }
        [HttpGet("RoomChatContactShow")]
        public async Task<IActionResult> RoomChatContactShow()
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId, token);
                var userTypeId = UserContext.GetClaimValueInteger(ClaimName.UserTypeId, token);

                var roomChatContacts = await _roomChatService.RoomChatContactShow(userId, userTypeId);

                var result = (from c in roomChatContacts
                              select new RoomChatContactResult
                              {
                                  UserID = c.UserID,
                                  FullName = c.FullName,
                                  // PicName = Convert.ToBase64String(Codes.ConvertImageToArray(c.PicName)),
                                  PicNameShort = c.PicName
                              });
                var result2 = new ObjectResult(result)
                {
                    StatusCode = (int)HttpStatusCode.OK
                };

                return Ok(result2);
                // return Json(new { roomChatContact = result });
            }
            catch (Exception ex)
            {

                HttpContext.RiseError(ex);
                return BadRequest(new { message = ex.Message });
            }


        }

        [HttpPost("RoomChatGroupInsert")]
        public async Task<IActionResult> RoomChatGroupInsert(int teacherId, string teacherTitle, string picName, string picNameShort)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId, token);
                var userTypeId = UserContext.GetClaimValueInteger(ClaimName.UserTypeId, token);

                var roomChatGroup = new RoomChatGroup()
                {
                    RoomChatGroupCreateDateTime = DateTime.Now,
                    RoomChatGroupDelete = false,
                    RoomChatGroupVisible = true,
                    RoomChatGroupTitle = teacherTitle,
                    TeacherId = (userTypeId == 1 ? teacherId : userId),
                    StudentId = (userTypeId == 1 ? userId : teacherId),
                    RoomChatGroupType = 2

                };

               var result2= await _roomChatService.RoomChatGroupInsert(roomChatGroup, userTypeId);



                var result = new ObjectResult(result2)
                {
                    StatusCode = (int)HttpStatusCode.OK
                };

                return Ok(result);


                // return Json(new { roomChatGroupModel = roomChatGroup });
            }
            catch (Exception ex)
            {

                HttpContext.RiseError(ex);
                return BadRequest(new { message = ex.Message });
            }

        }
        [HttpPost("RoomChatGroupRemove")]
        public async Task<IActionResult> RoomChatGroupRemove(int roomChatGroupId)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId, token);
                var userTypeId = UserContext.GetClaimValueInteger(ClaimName.UserTypeId, token);
                _roomChatService.RoomChatDeleteAll(roomChatGroupId, userId);
                var returnValue = await _roomChatService.RoomChatGroupRemove(roomChatGroupId, userTypeId);
                var result = new ObjectResult(true)
                {
                    StatusCode = (int)HttpStatusCode.OK,

                };

                return Ok(result);
            }
            catch (Exception ex)
            {

                HttpContext.RiseError(ex);
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("RoomChatViewInsert")]
        public async Task<IActionResult> RoomChatViewInsert(int roomChatId, int roomChatGroupId)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId, token);

                await _roomChatService.RoomChatViewInsert(roomChatId, roomChatGroupId, userId);
                var result = new ObjectResult(true)
                {
                    StatusCode = (int)HttpStatusCode.OK,

                };

                return Ok(result);

                //return Json(new { returnVlaue = true });
            }
            catch (Exception ex)
            {

                HttpContext.RiseError(ex);
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpPost("RoomChatViewDelete")]
        public async Task<IActionResult> RoomChatViewDelete(int roomChatId)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId, token);
                var roomChat = await _roomChatService.RoomChatViewDelete(roomChatId, userId);
                var roomChatReturn = new ChatMessage
                {
                    chatType = "D",
                    groupId = (int)roomChat.RoomChatGroupId,
                    senderId = userId,
                    attachMsg = roomChat.AttachMsg,
                    filename = "",
                    mimeType = "",

                    roomChatId = roomChat.RoomChatId,
                    senderName = "",
                    tagLearn = roomChat.TagLearn,
                    textChat = "",
                    roomChatParentId = null,
                    parentTextChat = "",
                    parentSenderName = "",
                    mainAddress = "",
                    roomChatViewNumber = 0,
                    roomChatDateString = "",

                };
                var result = new ObjectResult(roomChatReturn)
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


        [HttpPost("RoomChatDeleteAll")]
        public IActionResult RoomChatDeleteAll(int roomChatGroupId)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId, token);
                var returnValue = _roomChatService.RoomChatDeleteAll(roomChatGroupId, userId);
                var roomChatReturn = new ChatMessage
                {
                    chatType = "DA",
                    groupId = (int)roomChatGroupId,
                    senderId = userId,
                    attachMsg = false,
                    filename = "",
                    mimeType = "",

                    roomChatId = 0,
                    senderName = "",
                    tagLearn = false,
                    textChat = "",
                    roomChatParentId = null,
                    parentTextChat = "",
                    parentSenderName = "",
                    mainAddress = "",
                    roomChatViewNumber = 0,
                    roomChatDateString = "",

                };
                var result = new ObjectResult(roomChatReturn)
                {
                    StatusCode = (int)HttpStatusCode.OK,

                };

                return Ok(result);
                // return Json(new { returnValue = returnValue });
            }
            catch (Exception ex)
            {

                HttpContext.RiseError(ex);
                return BadRequest(new { message = ex.Message });
            }

        }


        [HttpPost("RoomChatForwardUserShow")]
        public async Task<IActionResult> RoomChatForwardUserShow(int roomChatId, int roomChatGroupId)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId, token);
                var userTypeId = UserContext.GetClaimValueInteger(ClaimName.UserTypeId, token);

                var roomChatForwardUser = await _roomChatService.RoomChatRightShow(userId, userTypeId);
                var result = (from r in roomChatForwardUser.ToList()
                              where r.RoomChatGroupID != roomChatGroupId
                              select new RoomChatForwardUser()
                              {
                                  GroupID = r.RoomChatGroupID,
                                  GroupTitle = r.RoomChatTitle,
                                  UserID=r.UserIDPic
                                 // PicName = r.PicName
                              }
                            ).ToList();


                var result2 = new ObjectResult(result)
                {
                    StatusCode = (int)HttpStatusCode.OK

                };




                return Ok(result2);

                //  return Json(new { roomChatGroupOnline = result });
            }
            catch (Exception ex)
            {

                HttpContext.RiseError(ex);
                return BadRequest(new { message = ex.Message });
            }


        }

        [HttpGet("RoomChatForwardSend")]
        public async Task<IActionResult> RoomChatForwardSend(string listId, int roomChatId)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId, token);
                var senderName = UserContext.GetClaimValueString(ClaimName.UserType, token) + " : " +
                                 UserContext.GetClaimValueString(ClaimName.Nickname, token);
                var userTypeId = UserContext.GetClaimValueInteger(ClaimName.UserTypeId, token);
                var listRoomChatReturn = await _roomChatService.RoomChatForwardInsert(listId, roomChatId, userId, senderName);

                var result = new ObjectResult(listRoomChatReturn)
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Value = userTypeId
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