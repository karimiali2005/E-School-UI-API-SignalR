using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESchool.Models;
using Microsoft.AspNetCore.Mvc;
using DatabaseLayer.Models;
using DatabaseLayer.Access;
using System.IO;
using Microsoft.AspNetCore.Http;
using ESchool.Need;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Authorization;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using ESchool.AppServer;

namespace ESchool.Controllers
{
    [Authorize(AuthenticationSchemes = "AccountAuth")]
    public class ChatController : BaseController
    {

        [HttpPost]
        public async Task<IActionResult> ChatInsert(string Id, string Reciever, string RoomId, string Teacher, string Course,string Message,string Filename,string MimeType,bool Tag,string ParentId,string CRUD)
        {
            try
            {
                ChatClass chatClass=new ChatClass();
                MessageRecieve messageRecieve=new MessageRecieve()
                {
                     RoomId = RoomId,
                     Course = Course,
                     MimeType = MimeType,
                     Filename = Filename,
                     CRUD = CRUD,
                     Id = Id,
                     Message = Message??"",
                     ParentId = ParentId,
                     Reciever = Reciever,
                     Tag = Tag,
                     Teacher = Teacher
                };
                ESchool.AppServer.MessageSend messageSend =await chatClass.Insert(messageRecieve, UserContext.GetClaimValueString(ClaimName.UserId), Teacher,Course);
                //change it
                //messageSend.Filename ??= "";
                //messageSend.Filename ??= "";
                return Json(new { messageSend = messageSend});
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult Room(int id, int teacherid, int rdid, string returnUrl = "",int courseid=0)
        {
            var UserId = Convert.ToInt32(this.User.FindFirst("UserId").Value);

            RoomDetailOp roomDetailOp = new RoomDetailOp();
            RoomOp roomOp = new RoomOp();
            RoomUserOp roomUserOp = new RoomUserOp();
            RoomTeacherOp roomTeacherOp = new RoomTeacherOp();
            RoomChatOp roomChatOp = new RoomChatOp();
            UserOp userOp = new UserOp();

            try
            {
                var teacher = userOp.GetUser(teacherid);
                if (teacher == null)
                {
                    return Content(Need.Codes.GetResource("NoTeacherFound"));
                }

                var room = roomOp.GetRoom(id);
                //if (room == null)
                //{
                //    return Content(Need.Codes.GetResource("NoRoomFound"));
                //}

                DateTime? dtRoomDetail = null;
                var roomDetail = roomDetailOp.GetRoomDetail(rdid);
                if (roomDetail != null)
                {
                    dtRoomDetail = roomDetail.RoomDetailDate;
                }

                RoomProperties roomProperties = new RoomProperties();
                roomProperties.Rooms = room == null
                    ? roomDetailOp.GetRoomsTeacher(teacherid)
                    : new List<Room>() { room };
                roomProperties.RoomChats = room == null
                    ? roomChatOp.GetRoomChats(teacherid,courseid).Where(p => p.RoomChatDelete == false).ToList()
                    : roomChatOp.GetRoomChats(id, teacherid, dtRoomDetail).Where(p => p.RoomChatDelete == false).ToList();
                roomProperties.RoomUsers = room == null
                    ? roomUserOp.GetRoomIdsTeachers(teacherid).OrderBy(p => p.Room.RoomTitle)
                    : roomUserOp.GetRoomUsers(id).OrderBy(p => p.User.LastName).ThenBy(p => p.User.FirstName);
                roomProperties.RoomTeachers = roomTeacherOp.GetRoomTeachers(teacherid, id);

                ViewData["Title"] = room == null ? "اتاق گفتگو " : room.RoomTitle + " - ";
                ViewData["Title"] += teacher.FirstName + " " + teacher.LastName;

                ViewData["RoomId"] = id;
                ViewData["RoomDetailId"] = rdid;
                ViewData["RoomTitle"] = room == null ? "" : room.RoomTitle;
                ViewData["RoomTypeTitle"] = room == null ? "" : room.RoomType.RoomTypeTitle;
                ViewData["AllowChat"] = room == null ? false : room.PermissionCloseChat;
                ViewData["TeacherId"] = teacherid;
                ViewData["CourseId"] = courseid;
                ViewData["returnUrl"] = returnUrl;

                return View("Room", roomProperties);
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }


        [HttpPost]
        public string getUserOnlines(int teacherid)
        {
            var query = AppServer.UserList.sockets.Where(p => p.Value.TeacherId == Convert.ToString(teacherid)).ToList();
            List<string> ids = new List<string>();
            foreach (var user in query)
            {
                ids.Add(user.Value.UserId);
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(ids);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveAttach(int id)
        {
            RoomChatOp roomChatOp = new RoomChatOp();
            var query = roomChatOp.GetRoomChat(id);
            if (query != null)
            {
                query.AttachMsg = false;
                await roomChatOp.AttachRecord(query.RoomChatId, false);
            }
            return Content("ok");
        }

        public async Task<IActionResult> FS(string fn)
        {
            try
            {
                var fullpath = Codes.GetPathAndFilename(fn);
                var memory = new MemoryStream();
                using (var stream = new FileStream(fullpath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 65536, FileOptions.Asynchronous | FileOptions.SequentialScan))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                //return File(memory, "application/octet-stream", Path.GetFileName(fullpath), true); //enableRangeProcessing = true

                //string mimetype = MimeMapping.MimeUtility.GetMimeMapping(fullpath);
                string mimetype = Codes.GetMimeType(fn);
                return File(memory, mimetype, Path.GetFileName(fullpath), true); //enableRangeProcessing = true
            }
            catch (Exception ex)
            {
                return Content("Error");
            }
        }

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 2147483648)]
        [RequestSizeLimit(2147483648)]
        public async Task<IActionResult> StoreFile(IFormFile file, string tag,bool isHomework=false)
        {
            try
            {
                

                var address = SettingContext.PathStoreFiles.Instance.Address;
                var fullAddressFtp = address;
                if (!isHomework)
                {
                    if (tag.Equals("true"))
                    {
                        fullAddressFtp += SettingContext.PathStoreFiles.Instance.LearnFile;
                    }
                    else
                    {
                        fullAddressFtp += SettingContext.PathStoreFiles.Instance.NormalFile;
                    }
                }
                else
                {
                    fullAddressFtp += SettingContext.PathStoreFiles.Instance.HomeworkFile;
                }
               

                string filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                filename = this.EnsureCorrectFilename(filename);

                string savefilename = Codes.GetRandomFileName(filename);
                var requestUri = fullAddressFtp+ savefilename;



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

                return Json(new {Filename = savefilename, MimeType = file.ContentType});
            }
            catch (Exception ex)
            {
                return Content("Error: " + ex.Message);
            }
        }

        /*[HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 2147483648)]
        [RequestSizeLimit(2147483648)]
        public async Task<IActionResult> StoreFile(IFormFile file)
        {
            try
            {
                string filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                filename = this.EnsureCorrectFilename(filename);
                //using (FileStream output = System.IO.File.Create(Codes.GetPathAndFilename(filename)))   
                string savefilename = Codes.GetFileName(filename);
                using (var output = new FileStream(Codes.GetPathAndFilename(savefilename), FileMode.Create))
                {
                    await file.CopyToAsync(output);
                }
                return Json(new { Filename = savefilename, MimeType = file.ContentType });
            }
            catch (Exception ex)
            {
                return Content("Error: " + ex.Message);
            }
        }
        */


        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
            {
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);
            }
            return filename;
        }

        /*[HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 2147483648)]
        [RequestSizeLimit(2147483648)]
        public async Task<IActionResult> SendRecordAudio(byte[] base64Str)
        {
            try
            {
                var stream = new MemoryStream(base64Str);
                //   IFormFile file = new FormFile(stream, 2147483648, 2147483648,"asdf","sd");
                //await documentRepository.UploadFile(stream);
                /*IFormFile file=  file.OpenReadStream();#1#
                string filename = Guid.NewGuid().ToString() + ".wav";

                /*string filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');#1#
                filename = this.EnsureCorrectFilename(filename);
                //using (FileStream output = System.IO.File.Create(Codes.GetPathAndFilename(filename)))   
                string savefilename = Codes.GetFileName(filename);
                using (var output = new FileStream(Codes.GetPathAndFilename(savefilename), FileMode.Create))
                {
                    /*await file.CopyToAsync(output);#1#
                    await stream.CopyToAsync(output);
                    /*stream.WriteTo(output);#1#
                }
                return Json(new { Filename = savefilename, MimeType = ".wav" });
                /*return null;#1#
            }
            catch (Exception ex)
            {
                return Content("Error: " + ex.Message);
            }
        }*/

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 2147483648)]
        [RequestSizeLimit(2147483648)]
        public async Task<IActionResult> SendRecordAudio(IFormFile file, string tag,bool isHomework=false)
        {
            try
            {
              
                var address = SettingContext.PathStoreFiles.Instance.Address;
                var fullAddressFtp = address;
                if (!isHomework)
                {
                    if (tag.Equals("true"))
                    {
                        fullAddressFtp += SettingContext.PathStoreFiles.Instance.LearnFile;
                    }
                    else
                    {
                        fullAddressFtp += SettingContext.PathStoreFiles.Instance.NormalFile;
                    }
                }
                else
                {
                    fullAddressFtp += SettingContext.PathStoreFiles.Instance.HomeworkFile;
                }
              

                string filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                filename = this.EnsureCorrectFilename(filename);

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

                return Json(new { Filename = savefilename, MimeType = file.ContentType });
            }
            catch (Exception ex)
            {
                return Content("Error: " + ex.Message);
            }
        }


    }
}