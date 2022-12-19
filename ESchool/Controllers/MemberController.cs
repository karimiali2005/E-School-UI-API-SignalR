using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Es.DataLayerCore.DTOs.RoomChat;
using Es.DataLayerCore.Model;
using ESchool.Need;
using EsServiceCore.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.Controllers
{
    [Authorize(AuthenticationSchemes = "AccountAuth")]
    public class MemberController : Controller
    {
        private readonly IRoomChatService _roomChatService;

        public MemberController(IRoomChatService roomChatService)
        {
            _roomChatService = roomChatService;
        }

       

        

        [HttpPost]
        public async Task<IActionResult> GetUserLogin()
        {
            try
            {

                return Json(new { userId = UserContext.GetClaimValueInteger(ClaimName.UserId) , userTypeId = UserContext.GetClaimValueInteger(ClaimName.UserTypeId) });
            }
            catch (Exception ex)
            {

                ex.ToSaveElmah();
                return View("Error");
            }

        }

        public async Task<IActionResult> RoomChat()
        {
            try
            {
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId);
                var userTypeId = UserContext.GetClaimValueInteger(ClaimName.UserTypeId);
                var roomChatRight = await _roomChatService.RoomChatRightShow(userId, userTypeId);
              //  var roomChatRight = await _roomChatService.RoomChatRightShowNew(userId, userTypeId,1,15);
                var roomLive = await _roomChatService.RoomLiveShow(userId);

                /*var room = roomChatRight.FirstOrDefault();

                var roomChatLeft =
                    await _roomChatService.RoomChatLeftShow(room.RoomID, room.TeacherID, (int) room.CourseID);*/

                var roomChatLeftViewModel = new RoomChatLeftViewModel()
                {
                    RoomChatLeftShowResult = null,
                    RoomChatLeftPropertyResult=null
                };
                var roomChatRightViewModel = new RoomChatRightViewModel
                {
                    RoomChatRightShowResult=roomChatRight,
                    RoomLiveShows=roomLive
                };

                var roomChatViewModel = new RoomChatViewModel()
                {
                    RoomChatLeftViewModel = roomChatLeftViewModel,
                    RoomChatRightViewModel= roomChatRightViewModel
                };





                return View(roomChatViewModel);

            }
            catch (Exception ex)
            {

                ex.ToSaveElmah();
                return View("Error");
            }

        }
        public async Task<IActionResult> RoomChatRight2(int pageNumber, int pageSize)
        {
            try
            {

                var userTypeId = UserContext.GetClaimValueInteger(ClaimName.UserTypeId);
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId);


                var roomChatRight = await _roomChatService.RoomChatRightShowNew(userId, userTypeId, pageNumber, pageSize);


                return View("RoomChatRightDetail", roomChatRight);
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }
        public async Task<IActionResult> RoomChatLeft(int roomChatGroupID,int roomChatGroupType, bool tagLearn,int newChatCount, int? roomId,int teacherId, int? courseId, int pageNumber, int pageSize,string picName,string roomChatGroupTitle,string searchText="")
        {
            try
            {

                var userId = Convert.ToInt32(User.FindFirst("UserId").Value);



                /*ViewBag.RoomChatGroupIDCurrent = roomChatGroupID;*/
                RoomChatLeftPropertyResult roomChatLeftPropertyResult;
                if (roomId != null)
                {
                    roomChatLeftPropertyResult = await _roomChatService.RoomChatLeftPropertyShow((int)roomId, roomChatGroupID);
                }
                else
                    roomChatLeftPropertyResult = new RoomChatLeftPropertyResult();

                roomChatLeftPropertyResult.RoomID = roomId;
                roomChatLeftPropertyResult.CourseID = courseId;
                roomChatLeftPropertyResult.TeacherID = teacherId;
                roomChatLeftPropertyResult.RoomChatGroupID = roomChatGroupID;
                roomChatLeftPropertyResult.PicName = picName??"";
                roomChatLeftPropertyResult.RoomChatGroupTitle = roomChatGroupTitle;
                roomChatLeftPropertyResult.RoomChatGroupType = roomChatGroupType;

                var userTypeId = UserContext.GetClaimValueInteger(ClaimName.UserTypeId);
                if (userTypeId!=4)
                {
                    roomChatLeftPropertyResult.PermissionStudentChatEdit = false;
                    roomChatLeftPropertyResult.PermissionStudentChatDelete = false;
                }

                var tuple = await _roomChatService.RoomChatLeftShow(roomChatGroupID, tagLearn, userId, newChatCount, pageNumber, pageSize,searchText);

                roomChatLeftPropertyResult.RoomChatViewLast = tuple.Item2;

                var roomChatLeftViewModel = new RoomChatLeftViewModel()
                {
                    RoomChatLeftPropertyResult= roomChatLeftPropertyResult,
                    RoomChatLeftShowResult = tuple.Item1
                };
                return View("RoomChatView", roomChatLeftViewModel);
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }
        public async Task<IActionResult> RoomChatLeft2(int roomChatGroupID, bool tagLearn, int pageNumber, int pageSize, bool permissionStudentChatEdit,bool permissionStudentChatDelete, string searchText="")
        {
            try
            {

                var userId = Convert.ToInt32(User.FindFirst("UserId").Value);


                var roomChatLeftPropertyResult = new RoomChatLeftPropertyResult()
                {
                    PermissionStudentChatEdit = permissionStudentChatEdit,
                    PermissionStudentChatDelete = permissionStudentChatDelete
                };

                var tuple = await _roomChatService.RoomChatLeftShow(roomChatGroupID, tagLearn, userId, 0, pageNumber, pageSize,searchText);

                var roomChatLeftViewModel = new RoomChatLeftViewModel()
                {
                    RoomChatLeftPropertyResult = roomChatLeftPropertyResult,
                    RoomChatLeftShowResult= tuple.Item1
                };

                
                    
              

                return View("RoomChatViewDetail", roomChatLeftViewModel);
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> RoomChatDelete(int roomChatId)
        {
            try
            {
                var userTypeId = UserContext.GetClaimValueInteger(ClaimName.UserTypeId);
               

                var roomChat = await _roomChatService.RoomChatDelete(roomChatId, userTypeId != 4?true:false, userTypeId != 4 ? Convert.ToInt32(SettingContext.RoomChatSatus.Instance.ChatDeleteTime) : 0);

                if (roomChat != null)
                {
                    var roomChatReturn = new ChatMessage
                    {
                        chatType = "D",
                        groupId = (int)roomChat.RoomChatGroupId,
                        senderId = UserContext.GetClaimValueInteger(ClaimName.UserId),
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
                    return Json(new { roomChatModel = roomChatReturn, allowDelete = true });
                }
                else
                {
                    return Json(new { allowDelete=false });
                }
                
            }
            catch (Exception ex)
            {

                ex.ToSaveElmah();
                return View("Error");
            }

        }
        [HttpPost]
        public async Task<IActionResult> RoomChatInsert(int roomChatGroupId, string textChat,string fileName,bool tagLearn,int? roomChatParentId,int ? roomId,int? teacherId,int? courseId,string parentTextChat,string parentSenderName,string roomChatGroupTitle,int ? roomChatGroupType)
        {
            try
            {
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId);
                var roomChat = new RoomChat()
                {
                    RoomChatDate = DateTime.Now,
                    SenderId = userId,
                    SenderName = UserContext.GetClaimValueString(ClaimName.UserType) + " : " +
                                 UserContext.GetClaimValueString(ClaimName.Nickname),
                    RecieverId = null,
                    RecieverName = "",
                    RoomId = roomId,
                    TeacherId = teacherId,
                    CourseId = courseId,
                    TextChat = textChat,
                    Filename = fileName ?? "",
                    MimeType = Codes.GetMimeType(fileName),
                    TagLearn = tagLearn,
                    RoomChatDelete = false,
                    RoomChatUpdate = null,
                    RoomChatParentId = roomChatParentId,
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
                    senderId =userId ,
                    attachMsg = roomChat.AttachMsg,
                    filename = roomChat.Filename??"",
                    mimeType = roomChat.MimeType??"",

                    roomChatId = roomChat.RoomChatId,
                    senderName = roomChat.SenderName,
                    tagLearn = roomChat.TagLearn,
                    textChat = roomChat.TextChat??"",
                    roomChatParentId = roomChat.RoomChatParentId,
                    parentTextChat = parentTextChat??"",
                    parentSenderName = parentSenderName??"",
                    mainAddress = "",
                    roomChatViewNumber = 0,
                    roomChatDateString= roomChat.RoomChatDate.ToString("HH:mm"),
                    roomChatGroupTitle= roomChatGroupType == 1 ? roomChatGroupTitle : roomChat.SenderName,
                    roomChatGroupType = roomChatGroupType

                };



                return Json(new { roomChatModel = roomChatReturn , userTypeId=UserContext.GetClaimValueInteger(ClaimName.UserTypeId) });
            }
            catch (Exception ex)
            {

                ex.ToSaveElmah();
                return View("Error");
            }

        }

        [HttpPost]
        public async Task<IActionResult> RoomChatEdit(int roomChatId, string textChat, bool tagLearn,string parentTextChat,string parentSenderName)
        {
            try
            {
               
               var tuple = await _roomChatService.RoomChatUpdate(roomChatId,textChat,tagLearn);
                var roomChat = tuple.Item1;
                if (tuple.Item2 == true && !string.IsNullOrEmpty(roomChat.Filename))
                {
                    var address = SettingContext.PathStoreFiles.Instance.Address;
                    var fullAddressFtpCut = address;
                    var fullAddressFtpPaste = address;

                    if (tagLearn)
                    {
                        fullAddressFtpCut += SettingContext.PathStoreFiles.Instance.NormalFile+roomChat.RoomChatFolder + "/" ;
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
                    senderId = UserContext.GetClaimValueInteger(ClaimName.UserId),
                    attachMsg = roomChat.AttachMsg,
                    filename = roomChat.Filename??"",
                    mimeType = roomChat.MimeType??"",

                    roomChatId = roomChat.RoomChatId,
                    senderName = roomChat.SenderName,
                    tagLearn = roomChat.TagLearn,
                    textChat = roomChat.TextChat??"",
                    roomChatParentId = roomChat.RoomChatParentId,
                    parentTextChat = parentTextChat??"",
                    parentSenderName = parentSenderName??"",
                    mainAddress = "",
                    roomChatViewNumber = 0,
                    roomChatDateString = roomChat.RoomChatDate.ToString("HH:mm")
                };

                return Json(new { roomChatModel = roomChatReturn });
            }
            catch (Exception ex)
            {

                ex.ToSaveElmah();
                return View("Error");
            }

        }
        [HttpPost]
        public async Task<IActionResult> RoomChatPin(int roomChatGroupId, int roomChatId, bool isPin)
        {
            try
            {
                
                var roomChat = await _roomChatService.RoomChatPin(roomChatGroupId,roomChatId,isPin);

                var roomChatReturn = new ChatMessage
                {
                    chatType = isPin?"P":"UP",
                    groupId = (int)roomChat.RoomChatGroupId,
                    senderId = UserContext.GetClaimValueInteger(ClaimName.UserId),
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

                return Json(new { roomChatModel = roomChatReturn, userTypeId=UserContext.GetClaimValueInteger(ClaimName.UserTypeId) });
            }
            catch (Exception ex)
            {

                ex.ToSaveElmah();
                return View("Error");
            }

        }
        [HttpPost]
        public async Task<IActionResult> RoomChatLock(int roomChatGroupId,bool isLock)
        {
            try
            {

                var result = await _roomChatService.RoomChatLock(roomChatGroupId, isLock);

                var roomChatReturn = new ChatMessage
                {
                    chatType = isLock ? "L" : "UL",
                    groupId = roomChatGroupId,
                    senderId = UserContext.GetClaimValueInteger(ClaimName.UserId),
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

                return Json(new { roomChatModel = roomChatReturn });
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
        public async Task<IActionResult> SendRecordAudio(IFormFile file, string tagLearn, int roomChatGroupId, string textChat, int? roomChatParentId, int? roomId, int? teacherId, int? courseId, string parentTextChat, string parentSenderName, string roomChatGroupTitle, int? roomChatGroupType)
        {
            try
            {
                var dateTimeNow = DateTime.Now;

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
                var requestUri = fullAddressFtp+"Month"+ dateTimeNow.ToString("yyyyMMdd")+"/" + savefilename;



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
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId);
                var roomChat = new RoomChat()
                {
                    RoomChatDate = dateTimeNow,
                    SenderId = userId,
                    SenderName = UserContext.GetClaimValueString(ClaimName.UserType) + " : " +
                                 UserContext.GetClaimValueString(ClaimName.Nickname),
                    RecieverId = null,
                    RecieverName = "",
                    RoomId = roomId,
                    TeacherId = teacherId,
                    CourseId = courseId,
                    TextChat = textChat ?? "",
                    Filename = savefilename ?? "",
                    MimeType = Codes.GetMimeType(savefilename),
                    TagLearn = Convert.ToBoolean(tagLearn),
                    RoomChatDelete = false,
                    RoomChatUpdate = null,
                    RoomChatParentId = roomChatParentId==0?null: roomChatParentId,
                    AttachMsg = false,
                    RoomChatGroupId = roomChatGroupId,
                    RoomChatFolder= "Month" + dateTimeNow.ToString("yyyyMMdd")
                };

                await _roomChatService.RoomChatInsert(roomChat);
                await _roomChatService.RoomChatViewInsert(roomChat.RoomChatId,roomChatGroupId, userId);

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
                    mainAddress = SettingContext.PathStoreFiles.Instance.Path1 + (roomChat.TagLearn ? SettingContext.PathStoreFiles.Instance.LearnFile : SettingContext.PathStoreFiles.Instance.NormalFile)+"Month" + dateTimeNow.ToString("yyyyMMdd") + "/" + savefilename,
                    roomChatViewNumber = 0,
                    roomChatDateString = roomChat.RoomChatDate.ToString("HH:mm"),
                    roomChatGroupTitle= roomChatGroupType == 1 ? roomChatGroupTitle : roomChat.SenderName,
                    roomChatGroupType = roomChatGroupType

                };

                return Json(new { roomChatModel = roomChatReturn, userTypeId = UserContext.GetClaimValueInteger(ClaimName.UserTypeId) });
            }
            catch (Exception ex)
            {
                return Content("Error: " + ex.Message);
            }
        }


        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 2147483648)]
        [RequestSizeLimit(2147483648)]
        public async Task<IActionResult> StoreFile(IFormFile file, string tagLearn, int roomChatGroupId, string textChat, int? roomChatParentId, int? roomId, int? teacherId, int? courseId,string parentTextChat,string parentSenderName, string roomChatGroupTitle, int? roomChatGroupType)
        {
            try
            {
                var dateTimeNow = DateTime.Now;


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

                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId);

                var roomChat = new RoomChat()
                {
                    RoomChatDate = dateTimeNow,
                    SenderId = userId,
                    SenderName = UserContext.GetClaimValueString(ClaimName.UserType) + " : " +
                                 UserContext.GetClaimValueString(ClaimName.Nickname),
                    RecieverId = null,
                    RecieverName = "",
                    RoomId = roomId,
                    TeacherId = teacherId,
                    CourseId = courseId,
                    TextChat = textChat ?? "",
                    Filename = savefilename ?? "",
                    MimeType = Codes.GetMimeType(savefilename),
                    TagLearn = Convert.ToBoolean(tagLearn),
                    RoomChatDelete = false,
                    RoomChatUpdate = null,
                    RoomChatParentId = roomChatParentId==0?null: roomChatParentId,
                    AttachMsg = false,
                    RoomChatGroupId = roomChatGroupId,
                    RoomChatFolder= "Month" + dateTimeNow.ToString("yyyyMMdd")
                };
                await _roomChatService.RoomChatInsert(roomChat);
                await _roomChatService.RoomChatViewInsert(roomChat.RoomChatId,roomChatGroupId, userId);

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
                    mainAddress = SettingContext.PathStoreFiles.Instance.Path1 + (roomChat.TagLearn ? SettingContext.PathStoreFiles.Instance.LearnFile : SettingContext.PathStoreFiles.Instance.NormalFile) + "Month" + dateTimeNow.ToString("yyyyMMdd") + "/"+ savefilename,
                    roomChatViewNumber = 0,
                    roomChatDateString = roomChat.RoomChatDate.ToString("HH:mm"),
                    roomChatGroupTitle= roomChatGroupType==1?roomChatGroupTitle:roomChat.SenderName,
                    roomChatGroupType= roomChatGroupType

                };

                return Json(new { roomChatModel = roomChatReturn, userTypeId = UserContext.GetClaimValueInteger(ClaimName.UserTypeId)});
            }
            catch (Exception ex)
            {
                return Content("Error: " + ex.Message);
            }
        }

        public async Task<IActionResult> RoomChatGroupOnlineShow(int roomChatGroupId,Dictionary<string, int> userListOnline)
        {
            try
            {

                var memberGroup = await _roomChatService.RoomChatGroupMemberShow(roomChatGroupId);
                var result = (from m in memberGroup.ToList()
                              join list in userListOnline on m.UserID equals list.Value into ps
                              from listNew in ps.DefaultIfEmpty()
                              select new RoomChatGroupOnlineViewModel()
                              {
                                  UserID = m.UserID,
                                  FullName = m.FullName,
                                  IsOnline = listNew.Key != null ? true : false,
                                  PicName = Convert.ToBase64String(Codes.ConvertImageToArray(m.PicName))
                              }
                            ).ToList();
               

                
                return Json(new { roomChatGroupOnline = result });
            }
            catch (Exception ex)
            {

                ex.ToSaveElmah();
                return View("Error");
            }


        }

        public  IActionResult GetUserOnline()
        {
            return View("UserOnlineCount");
        }

        public async Task<IActionResult> GetUserOnlineCount(Dictionary<string, int> userListOnline)
        {


            return Json(new { count = userListOnline.Count() });
        }

        public async Task<IActionResult> RoomChatContactShow()
        {
            try
            {

                var roomChatContacts = await _roomChatService.RoomChatContactShow(UserContext.GetClaimValueInteger(ClaimName.UserId), UserContext.GetClaimValueInteger(ClaimName.UserTypeId));

                var result = (from c in roomChatContacts
                              select new RoomChatContactResult
                              {
                                  UserID = c.UserID,
                                  FullName = c.FullName,
                                  PicName = Convert.ToBase64String(Codes.ConvertImageToArray(c.PicName)),
                                  PicNameShort= c.PicName
                              });

                return Json(new { roomChatContact = result });
            }
            catch (Exception ex)
            {

                ex.ToSaveElmah();
                return View("Error");
            }


        }


        [HttpPost]
        public async Task<IActionResult> RoomChatGroupInsert(int teacherId,string teacherTitle,string picName,string picNameShort)
        {
            try
            {
                var userTypeId = UserContext.GetClaimValueInteger(ClaimName.UserTypeId);
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId);

                var roomChatGroup = new RoomChatGroup()
                {
                    RoomChatGroupCreateDateTime = DateTime.Now,
                    RoomChatGroupDelete = false,
                    RoomChatGroupVisible = true,
                    RoomChatGroupTitle = teacherTitle,
                    TeacherId = (userTypeId==1? teacherId:userId),
                    StudentId = (userTypeId == 1 ? userId : teacherId),
                    RoomChatGroupType =2
                    
                };

               var result= await _roomChatService.RoomChatGroupInsert(roomChatGroup, userTypeId);
               

            



                return Json(new { roomChatGroupModel = result });
            }
            catch (Exception ex)
            {

                ex.ToSaveElmah();
                return View("Error");
            }

        }

        [HttpPost]
        public async Task<IActionResult> RoomChatGroupRemove(int roomChatGroupId)
        {
            try
            {
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId);
                var userTypeId = UserContext.GetClaimValueInteger(ClaimName.UserTypeId);
                _roomChatService.RoomChatDeleteAll(roomChatGroupId, userId);
                var returnValue = await _roomChatService.RoomChatGroupRemove(roomChatGroupId, userTypeId);
                return Json(new { returnValue = returnValue });
            }
            catch (Exception ex)
            {

                ex.ToSaveElmah();
                return View("Error");
            }

        }


        [HttpPost]
        public async Task<IActionResult> RoomChatViewInsert(int roomChatId,int roomChatGroupId)
        {
            try
            {
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId);
               
                await _roomChatService.RoomChatViewInsert(roomChatId, roomChatGroupId, userId);

                return Json(new { returnVlaue = true});
            }
            catch (Exception ex)
            {

                ex.ToSaveElmah();
                return View("Error");
            }

        }
        [HttpPost]
        public async Task<IActionResult> RoomChatViewDelete(int roomChatId)
        {
            try
            {
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId);
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

                return Json(new { roomChatModel = roomChatReturn });
            }
            catch (Exception ex)
            {

                ex.ToSaveElmah();
                return View("Error");
            }

        }


        [HttpPost]
        public IActionResult RoomChatDeleteAll(int roomChatGroupId)
        {
            try
            {
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId);
                var returnValue = _roomChatService.RoomChatDeleteAll(roomChatGroupId, userId);
                return Json(new { returnValue = returnValue });
            }
            catch (Exception ex)
            {

                ex.ToSaveElmah();
                return View("Error");
            }

        }

        [HttpPost]
        public async Task<IActionResult> RoomChatForwardUserShow(int roomChatId,int roomChatGroupId)
        {
            try
            {
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId);
                var userTypeId = UserContext.GetClaimValueInteger(ClaimName.UserTypeId);
                var roomChatForwardUser = await _roomChatService.RoomChatRightShow(userId, userTypeId);
                var result = (from r in roomChatForwardUser.ToList()
                             where r.RoomChatGroupID!=roomChatGroupId
                              select new RoomChatForwardUser()
                              {
                                  GroupID = r.RoomChatGroupID,
                                  GroupTitle =r.RoomChatTitle,
                                  PicName = Convert.ToBase64String(Codes.ConvertImageToArray(r.PicName))
                              }
                            ).ToList();

              

                return Json(new { roomChatForwardUser = result });
            }
            catch (Exception ex)
            {

                ex.ToSaveElmah();
                return View("Error");
            }


        }

        public async Task<IActionResult> RoomChatForwardSend(string listId,int roomChatId)
        {
            try
            {
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId);
                var senderName = UserContext.GetClaimValueString(ClaimName.UserType) + " : " +
                                 UserContext.GetClaimValueString(ClaimName.Nickname);
                var listRoomChatReturn = await _roomChatService.RoomChatForwardInsert(listId, roomChatId, userId, senderName);



                return Json(new { listRoomChatReturn = listRoomChatReturn, userTypeId = UserContext.GetClaimValueInteger(ClaimName.UserTypeId) });
            }
            catch (Exception ex)
            {

                ex.ToSaveElmah();
                return View("Error");
            }

        }

        [HttpPost]
        public async Task<IActionResult> RoomChatPlayVoiceUpdate(int roomChatId)
        {
            try
            {
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId);

                await _roomChatService.RoomChatPlayVoiceUpdate(roomChatId, userId);

                return Json(new { returnVlaue = true });
            }
            catch (Exception ex)
            {

                ex.ToSaveElmah();
                return View("Error");
            }

        }

    }
}