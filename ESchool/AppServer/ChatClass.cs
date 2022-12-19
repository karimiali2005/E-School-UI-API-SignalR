using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseLayer.Access;
using DatabaseLayer.Models;
using ESchool.Need;

namespace ESchool.AppServer
{
    public class ChatClass
    {
        public async Task<ESchool.AppServer.MessageSend> Insert(MessageRecieve messageRecieve,string UserId,string TeacherId,string CourseId)
        {
            var addressDownload = AppSettingClass.GetPathStoreFiles();

            UserOp userOp = new UserOp();
            RoomChatOp roomChatOp = new RoomChatOp();
            RoomUserOp roomUserOp = new RoomUserOp();

            if (messageRecieve.Reciever == "All")
            {
                User user = userOp.GetUser(Convert.ToInt32(UserId));
                string SenderName = user.UserType.UserTypeTitle + " : " + user.FirstName + " " + user.LastName;

                /*
                var sockets = UserList.sockets.Where(p => /*p.Key != UserId && p.Value.RoomId == messageRecieve.RoomId && #1# p.Value.TeacherId == TeacherId && p.Value.CourseId == CourseId);*/
                var dt = Need.Codes.GetLocalDateTime();
                RoomChat roomChat = null;

                switch (messageRecieve.CRUD)
                {
                    case "L":
                        bool Lock = await LockRoom(Convert.ToInt32(messageRecieve.RoomId), true);
                        if (Lock)
                        {
                            string NewId = await SaveToDB(Convert.ToInt32(UserId), SenderName, Convert.ToInt32(messageRecieve.RoomId), Convert.ToInt32(TeacherId), Convert.ToInt32(CourseId), messageRecieve.Message, "", null, false, null);
                            MessageSend messageSend = new MessageSend()
                            {
                                Message = messageRecieve.Message,
                                Filename = messageRecieve.Filename,
                                
                                AddressDownload = addressDownload,
                                MimeType = Codes.GetMimeType(messageRecieve.Filename),
                                Id = NewId,
                                Sender = UserId,
                                SenderName = SenderName,
                                SenderTime = dt.ToString("HH:mm:ss"),
                                RoomId = messageRecieve.RoomId,
                                CourseId = messageRecieve.Course,
                                Tag = messageRecieve.Tag,
                                ParentId = roomChat == null ? "" : messageRecieve.ParentId,
                                MessageParent = roomChat == null ? "" : roomChat.TextChat,
                                SenderNameParent = roomChat == null ? "" : roomChat.SenderName,
                                CRUD = messageRecieve.CRUD
                            };
                            return messageSend;
                        }
                        break;
                    case "UL":
                        bool UnLock = await LockRoom(Convert.ToInt32(messageRecieve.RoomId), false);
                        if (UnLock)
                        {
                            string NewId = await SaveToDB(Convert.ToInt32(UserId), SenderName, Convert.ToInt32(messageRecieve.RoomId), Convert.ToInt32(TeacherId), Convert.ToInt32(CourseId), messageRecieve.Message, "", null, false, null);
                            MessageSend messageSend = new MessageSend()
                            {
                                Message = messageRecieve.Message,
                                Filename = messageRecieve.Filename,
                                AddressDownload = addressDownload,
                                MimeType = Codes.GetMimeType(messageRecieve.Filename),
                                Id = NewId,
                                Sender = UserId,
                                SenderName = SenderName,
                                SenderTime = dt.ToString("HH:mm:ss"),
                                RoomId = messageRecieve.RoomId,
                                CourseId = messageRecieve.Course,
                                Tag = messageRecieve.Tag,
                                ParentId = roomChat == null ? "" : messageRecieve.ParentId,
                                MessageParent = roomChat == null ? "" : roomChat.TextChat,
                                SenderNameParent = roomChat == null ? "" : roomChat.SenderName,
                                CRUD = messageRecieve.CRUD
                            };
                            return messageSend;

                        }
                        break;

                    case "C":
                        int? ParentId = null;
                        if (messageRecieve.ParentId != "0" && !string.IsNullOrEmpty(messageRecieve.ParentId))
                        {
                            roomChat = roomChatOp.GetRoomChat(Convert.ToInt32(messageRecieve.ParentId));
                            if (roomChat != null)
                            {
                                ParentId = roomChat.RoomChatId;
                            }
                        }

                        string Id = await SaveToDB(Convert.ToInt32(UserId), SenderName, Convert.ToInt32(messageRecieve.RoomId), Convert.ToInt32(TeacherId), Convert.ToInt32(CourseId), messageRecieve.Message, "", null, messageRecieve.Tag, ParentId, false, messageRecieve.Filename, Codes.GetMimeType(messageRecieve.Filename));
                        int ChatId = Convert.ToInt32(Id);
                        if (ChatId > 0)
                        {
                          MessageSend messageSend = new MessageSend()
                                {
                                    Message = messageRecieve.Message,
                                    Filename = messageRecieve.Filename,
                                    AddressDownload = addressDownload,
                                    MimeType = Codes.GetMimeType(messageRecieve.Filename),
                                    Id = Id,
                                    Sender = UserId,
                                    SenderName = SenderName,
                                    SenderTime = dt.ToString("HH:mm:ss"),
                                    RoomId = messageRecieve.RoomId,
                                    CourseId = messageRecieve.Course,
                                    Tag = messageRecieve.Tag,
                                    ParentId = roomChat == null ? "" : messageRecieve.ParentId,
                                    MessageParent = roomChat == null ? "" : roomChat.TextChat,
                                    SenderNameParent = roomChat == null ? "" : roomChat.SenderName,
                                    CRUD = messageRecieve.CRUD
                                };
                          return messageSend;

                        }
                        break;
                    case "D":
                        int Delete = await DeleteToDB(Convert.ToInt32(messageRecieve.Id));
                        if (Delete > 0)
                        {
                            MessageSend messageSend = new MessageSend()
                                {
                                    Message = messageRecieve.Message,
                                    Filename = messageRecieve.Filename,
                                    AddressDownload = addressDownload,
                                    MimeType = Codes.GetMimeType(messageRecieve.Filename),
                                    Id = messageRecieve.Id,
                                    Sender = UserId,
                                    SenderName = SenderName,
                                    SenderTime = dt.ToString("HH:mm:ss"),
                                    RoomId = messageRecieve.RoomId,
                                    CourseId = messageRecieve.Course,
                                    Tag = messageRecieve.Tag,
                                    ParentId = roomChat == null ? "" : messageRecieve.ParentId,
                                    MessageParent = roomChat == null ? "" : roomChat.TextChat,
                                    SenderNameParent = roomChat == null ? "" : roomChat.SenderName,
                                    CRUD = messageRecieve.CRUD
                                };
                            return messageSend;
                        }
                        break;
                    case "E":
                        roomChat = roomChatOp.GetRoomChat(Convert.ToInt32(messageRecieve.Id));
                        if (roomChat != null)
                        {
                            /*var socket = sockets.Where(p => p.Value.TeacherId == TeacherId && p.Value.UserId == Convert.ToString(roomChat.SenderId) && p.Value.CourseId == CourseId);
                            if (socket.Count() > 0)
                            {
                                var skt = socket.First();*/
                                MessageSend messageSend = new MessageSend()
                                {
                                    Message = roomChat.TextChat,
                                    Filename = roomChat.Filename,
                                    AddressDownload = addressDownload,
                                    MimeType = Codes.GetMimeType(roomChat.Filename),
                                    Id = messageRecieve.Id,
                                    Sender = roomChat.SenderId.ToString(),
                                    SenderName = roomChat.SenderName,
                                    SenderTime = dt.ToString("HH:mm:ss"),
                                    RoomId = Convert.ToString(roomChat.RoomId),
                                    CourseId = messageRecieve.Course,
                                    Tag = roomChat.TagLearn,
                                    ParentId = "",
                                    MessageParent = "",
                                    SenderNameParent = "",
                                    CRUD = messageRecieve.CRUD
                               };
                            /*  await SendAsync(skt.Value.Websocket, messageSend);*/
                            return messageSend;
                        }

     
                        break;
                    case "U":
                        int Update = await UpdateToDB(Convert.ToInt32(messageRecieve.Id), messageRecieve.Message);
                        if (Update > 0)
                        {
                            RoomChat roomChatParent = null;
                            roomChat = roomChatOp.GetRoomChat(Convert.ToInt32(messageRecieve.Id));
                            if (roomChat != null)
                            {
                                if (roomChat.RoomChatParentId != null)
                                {
                                    roomChatParent = roomChatOp.GetRoomChat(roomChat.RoomChatParentId.Value);
                                }
                            }
                            MessageSend messageSend = new MessageSend()
                                {
                                    Message = messageRecieve.Message,
                                    Filename = roomChat.Filename,
                                    AddressDownload = addressDownload,
                                    MimeType = Codes.GetMimeType(roomChat.Filename),
                                    Id = messageRecieve.Id,
                                    Sender = roomChat.SenderId.ToString(),
                                    SenderName = roomChat.SenderName,
                                    SenderTime = dt.ToString("HH:mm:ss"),
                                    RoomId = messageRecieve.RoomId,
                                    CourseId = messageRecieve.Course,
                                    Tag = messageRecieve.Tag,
                                    ParentId = roomChatParent == null ? "" : Convert.ToString(roomChatParent.RoomChatId),
                                    MessageParent = roomChatParent == null ? "" : roomChatParent.TextChat,
                                    SenderNameParent = roomChatParent == null ? "" : roomChatParent.SenderName,
                                    CRUD = messageRecieve.CRUD
                                };
                            return messageSend;

                        }
                        break;
                    case "A":
                        var roomUsers = roomUserOp.GetRoomUsers(Convert.ToInt32(messageRecieve.RoomId)).ToList();
                        roomUsers.Add(new RoomUser { UserId = Convert.ToInt32(TeacherId), RoomId = Convert.ToInt32(messageRecieve.RoomId) });
                        var roomChat1 = roomChatOp.GetRoomChat(Convert.ToInt32(messageRecieve.Id));
                        foreach (var roomUser in roomUsers)
                        {
                            string NewId = await SaveToDB(Convert.ToInt32(UserId), SenderName, Convert.ToInt32(messageRecieve.RoomId), Convert.ToInt32(TeacherId), Convert.ToInt32(CourseId), roomChat1.TextChat, "", roomUser.UserId, false, null, true);
                            /*var socket = sockets.Where(p => p.Value.TeacherId == TeacherId && p.Value.UserId == Convert.ToString(roomUser.UserId));
                            if (socket.Count() > 0)
                            {
                                var skt = socket.First();*/
                                MessageSend messageSend = new MessageSend()
                                {
                                    Message = roomChat1.TextChat,
                                    Filename = roomChat1.Filename,
                                    AddressDownload = addressDownload,
                                    MimeType = "",
                                    Id = NewId,
                                    Sender = UserId,
                                    SenderName = SenderName,
                                    SenderTime = dt.ToString("HH:mm:ss"),
                                    RoomId = messageRecieve.RoomId,
                                    CourseId = messageRecieve.Course,
                                    Tag = false,
                                    ParentId = "",
                                    MessageParent = "",
                                    SenderNameParent = "",
                                    CRUD = messageRecieve.CRUD
                                };
                                /*await SendAsync(skt.Value.Websocket, messageSend);*/
                            /*}*/
                            return messageSend;
                        }
                        break;
                    case "LOG":
                        string Id00 = await SaveToDB(Convert.ToInt32(UserId), SenderName, Convert.ToInt32(messageRecieve.RoomId), Convert.ToInt32(TeacherId), Convert.ToInt32(CourseId), messageRecieve.Message, "", null, messageRecieve.Tag, null, false, messageRecieve.Filename);
                        break;
                }




            }

            return null;
        }
        private async Task<string> SaveToDB(int SenderId, string SenderName, int RoomId, int TeacherId, int CourseId, string Text, string RecieverName, int? RecieverId, bool TagLearn = false, int? ParentId = null, bool AttachMsg = false, string Filename = "",string mimeType="")
        {
            RoomChatOp roomChatOp = new RoomChatOp();
            var roomChatGroup = await roomChatOp.GetRoomChatGroup(RoomId, TeacherId, CourseId);
            int? roomChatGroupId = null;
            if (roomChatGroup != null)
                roomChatGroupId = roomChatGroup.RoomChatGroupId;

         
            var Id = await roomChatOp.InsertRoomChat(SenderId, SenderName, RoomId, TeacherId, CourseId, roomChatGroupId, Text, RecieverName, RecieverId, TagLearn, ParentId, AttachMsg, Filename,mimeType);
            return Id;
        }

        private async Task<bool> LockRoom(int RoomId, bool Lock)
        {
            RoomOp roomOp = new RoomOp();
            var room = roomOp.GetRoom(RoomId);
            if (room != null)
            {
                room.CloseChat = Lock;
                int count = await roomOp.UpdateRoom(room);
                return count > 0;
            }
            else
            {
                return false;
            }
        }

        private async Task<int> UpdateToDB(int RoomChatId, string Text)
        {
            RoomChatOp roomChatOp = new RoomChatOp();
            var Id = await roomChatOp.UpdateRecord(RoomChatId, Text);
            return Id;
        }

        private async Task<int> DeleteToDB(int RoomChatId)
        {
            RoomChatOp roomChatOp = new RoomChatOp();
            var Id = await roomChatOp.DeleteRecord(RoomChatId, true);
            return Id;
        }

        private async Task<int> AttachToDB(int RoomChatId, bool AttachMsg)
        {
            RoomChatOp roomChatOp = new RoomChatOp();
            var Id = await roomChatOp.AttachRecord(RoomChatId, AttachMsg);
            return Id;
        }
    }
}
