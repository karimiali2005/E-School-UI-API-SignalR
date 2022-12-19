using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseLayer.Access;
using ESchool.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace ESchool.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string controller = Convert.ToString(HttpContext.Request.RouteValues["Controller"]);
            string action = Convert.ToString(HttpContext.Request.RouteValues["Action"]);


            if (this.User.Identity.IsAuthenticated == true)
            {
                var UserActive = Convert.ToInt32(this.User.FindFirst("UserActive").Value);
                var UserTypeId = Convert.ToInt32(this.User.FindFirst("UserTypeId").Value);
                var Nickname = Convert.ToString(this.User.FindFirst("Nickname").Value);
                var UserType = Convert.ToString(this.User.FindFirst("UserType").Value);

                if (UserActive == 0)
                {
                    MessageModel messageModel = new MessageModel();
                    messageModel.MessageText = UserType + " گرامی، " + Nickname + " کاربری شما معلق شده است. به علت " + "<br/>";
                    messageModel.MessageText += this.User.FindFirst("ReasonInactive").Value;
                    messageModel.MessageType = "Warning";
                    ViewBag.MessageModel = messageModel;
                    TempData["MessageModel"] = JsonConvert.SerializeObject(messageModel);
                    filterContext.HttpContext.Response.Redirect("/Account/MsgShow");
                }
            }

            if (TempData["MessageModel"] != null)
            {
                string temp = TempData["MessageModel"] as string;
                MessageModel messageModel = JsonConvert.DeserializeObject<MessageModel>(temp);
                ViewBag.MessageModel = messageModel;
            }
        }

        

        public bool IsRoomForUser(int id)
        {
            var UserId = Convert.ToInt32(this.User.FindFirst("UserId").Value);            
            RoomUserOp roomUserOp = new RoomUserOp();
            var roomUser = roomUserOp.GetRoomsUsers(UserId).ToList();
            var ExistRoom = roomUser.Where(p => p.RoomId == id).ToList();
            if (ExistRoom.Count() == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool IsRoomForTeacher(int id)
        {
            var UserId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
            RoomUserOp roomUserOp = new RoomUserOp();
            var roomUser = roomUserOp.GetRoomsTeachers(UserId).ToList();
            var ExistRoom = roomUser.Where(p => p.RoomId == id).ToList();
            if (ExistRoom.Count() == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}