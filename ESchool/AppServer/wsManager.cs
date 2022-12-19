using DatabaseLayer.Access;
using DatabaseLayer.Models;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using ESchool.Need;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ESchool.AppServer
{
    public class wsManager
    {
        public async Task ListenAcceptAsync(HttpContext context)
        {
            WebSocket ws = await context.WebSockets.AcceptWebSocketAsync();
            string UId = context.Request.Query["id"];                       
            string TId = context.Request.Query["tid"];            
            string CId = context.Request.Query["cid"];            
            UserWebSocket userWebSocket = new UserWebSocket()
            {
                UserId = UId,                
                TeacherId = TId,
                CourseId =CId,
                Websocket = ws
            };

            var skts = UserList.sockets.Where(p => p.Key == UId && p.Value.TeacherId == TId&&p.Value.CourseId==CId);
            foreach(var s in skts)
            {
                UserWebSocket userWebSocket1;
                UserList.sockets.TryRemove(s.Key, out userWebSocket1);
            }
            UserList.sockets.TryAdd(UId, userWebSocket);
            await RecieveAsync(userWebSocket);
        }

        public async Task RecieveAsync(UserWebSocket uws)
        {
          

            var addressDownload = AppSettingClass.GetPathStoreFiles();

            UserOp userOp = new UserOp();
            RoomChatOp roomChatOp = new RoomChatOp();
            RoomUserOp roomUserOp = new RoomUserOp();

            string UserId = uws.UserId;            
            string TeacherId = uws.TeacherId;
            string CourseId = uws.CourseId;
            WebSocket ws = uws.Websocket;

            byte[] buff;

            while(ws.State == WebSocketState.Open)
            {
                buff = new byte[wsConfig.BufferSize];
                ArraySegment<byte> AS = new ArraySegment<byte>(buff, 0, buff.Length);
                try
                {
                    WebSocketReceiveResult result = await ws.ReceiveAsync(AS, CancellationToken.None);

                    if (result != null)
                    {
                        if (result.MessageType == WebSocketMessageType.Text)
                        {
                            string message = UTF8Encoding.UTF8.GetString(buff);
                            MessageRecieve messageRecieve = Newtonsoft.Json.JsonConvert.DeserializeObject<MessageRecieve>(message);

                            if (messageRecieve.Reciever == "All")
                            {
                                User user = userOp.GetUser(Convert.ToInt32(UserId));
                                string SenderName = user.UserType.UserTypeTitle + " : " + user.FirstName + " " + user.LastName;

                                var sockets = UserList.sockets.Where(p => /*p.Key != UserId && p.Value.RoomId == messageRecieve.RoomId && */ p.Value.TeacherId == TeacherId&& p.Value.CourseId==CourseId);
                                var dt = Need.Codes.GetLocalDateTime();
                                RoomChat roomChat = null;

                                switch (messageRecieve.CRUD)
                                {
                                    case "L":
                                        bool Lock = await LockRoom(Convert.ToInt32(messageRecieve.RoomId), true);
                                        if (Lock)
                                        {
                                            string NewId = await SaveToDB(Convert.ToInt32(UserId), SenderName, Convert.ToInt32(messageRecieve.RoomId), Convert.ToInt32(TeacherId), Convert.ToInt32(CourseId), messageRecieve.Message, "", null, false, null);
                                            foreach (var socket in sockets)
                                            {
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
                                                    CourseId=messageRecieve.Course,
                                                    Tag = messageRecieve.Tag,
                                                    ParentId = roomChat == null ? "" : messageRecieve.ParentId,
                                                    MessageParent = roomChat == null ? "" : roomChat.TextChat,
                                                    SenderNameParent = roomChat == null ? "" : roomChat.SenderName,
                                                    CRUD = messageRecieve.CRUD
                                                };
                                                await SendAsync(socket.Value.Websocket, messageSend);
                                            }// end of foreach sockets
                                            
                                        }
                                        break;
                                    case "UL":
                                        bool UnLock = await LockRoom(Convert.ToInt32(messageRecieve.RoomId), false);
                                        if (UnLock)
                                        {
                                            string NewId = await SaveToDB(Convert.ToInt32(UserId), SenderName, Convert.ToInt32(messageRecieve.RoomId), Convert.ToInt32(TeacherId), Convert.ToInt32(CourseId), messageRecieve.Message, "", null, false, null);
                                            foreach (var socket in sockets)
                                            {
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
                                                await SendAsync(socket.Value.Websocket, messageSend);
                                            }
                                            
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

                                        string Id = await SaveToDB(Convert.ToInt32(UserId), SenderName, Convert.ToInt32(messageRecieve.RoomId), Convert.ToInt32(TeacherId), Convert.ToInt32(CourseId), messageRecieve.Message, "", null, messageRecieve.Tag, ParentId, false, messageRecieve.Filename);
                                        int ChatId = Convert.ToInt32(Id);
                                        if (ChatId > 0)
                                        {
                                            foreach (var socket in sockets)
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
                                                await SendAsync(socket.Value.Websocket, messageSend);
                                            }
                                            
                                        }
                                        break;
                                    case "D":
                                        int Delete = await DeleteToDB(Convert.ToInt32(messageRecieve.Id));
                                        if (Delete > 0)
                                        {
                                            foreach (var socket in sockets)
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
                                                await SendAsync(socket.Value.Websocket, messageSend);
                                            }
                                           
                                        }
                                        break;
                                    case "E":
                                        roomChat = roomChatOp.GetRoomChat(Convert.ToInt32(messageRecieve.Id));
                                        if (roomChat != null)
                                        {
                                            var socket = sockets.Where(p => p.Value.TeacherId == TeacherId && p.Value.UserId == Convert.ToString(roomChat.SenderId)&&p.Value.CourseId==CourseId);
                                            if(socket.Count() > 0)
                                            {
                                                var skt = socket.First();
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
                                                await SendAsync(skt.Value.Websocket, messageSend);
                                            }

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
                                            foreach (var socket in sockets)
                                            {
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
                                                await SendAsync(socket.Value.Websocket, messageSend);
                                            }
                                            
                                        }
                                        break;
                                    case "A":
                                        var roomUsers = roomUserOp.GetRoomUsers(Convert.ToInt32(messageRecieve.RoomId)).ToList();
                                        roomUsers.Add(new RoomUser { UserId = Convert.ToInt32(TeacherId), RoomId = Convert.ToInt32(messageRecieve.RoomId) });
                                        var roomChat1 = roomChatOp.GetRoomChat(Convert.ToInt32(messageRecieve.Id));
                                        foreach (var roomUser in roomUsers)
                                        {
                                            string NewId = await SaveToDB(Convert.ToInt32(UserId), SenderName, Convert.ToInt32(messageRecieve.RoomId), Convert.ToInt32(TeacherId), Convert.ToInt32(CourseId), roomChat1.TextChat, "", roomUser.UserId, false, null, true);
                                            var socket = sockets.Where(p => p.Value.TeacherId == TeacherId && p.Value.UserId == Convert.ToString(roomUser.UserId));
                                            if(socket.Count() > 0)
                                            {
                                                var skt = socket.First();
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
                                                await SendAsync(skt.Value.Websocket, messageSend);
                                            }
                                        }                                        
                                        break;
                                    case "LOG":
                                        string Id00 = await SaveToDB(Convert.ToInt32(UserId), SenderName, Convert.ToInt32(messageRecieve.RoomId), Convert.ToInt32(TeacherId), Convert.ToInt32(CourseId), messageRecieve.Message, "", null, messageRecieve.Tag, null, false, messageRecieve.Filename);
                                        break;
                                }
                                

                                
                                
                            }
                            else
                            {
                                //UserWebSocket userWebSocket = UserList.sockets[messageRecieve.Reciever];
                                //UserOp userOp = new UserOp();
                                //User user = userOp.GetUser(Convert.ToInt32(userWebSocket.UserId));

                                //var dt = Need.Codes.GetLocalDateTime();

                                //MessageSend messageSend = new MessageSend()
                                //{
                                //    Message = messageRecieve.Message,
                                //    Sender = userWebSocket.UserId,
                                //    SenderName = user.FirstName + " " + user.LastName,
                                //    SenderTime = dt.ToString("HH:mm:ss"),
                                //    RoomId = messageRecieve.RoomId,
                                //    Tag = messageRecieve.Tag
                                //};
                                
                                //await SendAsync(userWebSocket.Websocket, messageSend);
                            }

                        }
                        else
                        {
                            var skts = UserList.sockets.Where(p => p.Key == UserId && p.Value.TeacherId == TeacherId&&p.Value.CourseId==CourseId);
                            foreach (var s in skts)
                            {
                                UserWebSocket userWebSocket1;
                                UserList.sockets.TryRemove(s.Key, out userWebSocket1);
                            }
                        }
                    }//end of if result
                }
                catch(Exception ex)
                {
                    string msg = ex.Message;
                }

            }//end of while
        }

        public async Task SendAsync(WebSocket ws, MessageSend message)
        {
            string StrMsg = Newtonsoft.Json.JsonConvert.SerializeObject(message);
            byte[] buff = UTF8Encoding.UTF8.GetBytes(StrMsg);
            ArraySegment<byte> AS = new ArraySegment<byte>(buff, 0, buff.Length);
            await ws.SendAsync(AS, WebSocketMessageType.Text, true, CancellationToken.None);
        }


        private async Task<string> SaveToDB(int SenderId, string SenderName, int RoomId, int TeacherId,int CourseId, string Text, string RecieverName, int? RecieverId, bool TagLearn = false, int? ParentId = null, bool AttachMsg = false, string Filename = "")
        {
            RoomChatOp roomChatOp = new RoomChatOp();
            var Id = await roomChatOp.InsertRoomChat(SenderId, SenderName, RoomId, TeacherId,CourseId,null, Text, RecieverName, RecieverId, TagLearn, ParentId, AttachMsg, Filename);
            return Id;
        }        

        private async Task<bool> LockRoom(int RoomId, bool Lock)
        {
            RoomOp roomOp = new RoomOp();
            var room = roomOp.GetRoom(RoomId);
            if(room != null)
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
