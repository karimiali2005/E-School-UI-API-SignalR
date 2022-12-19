using Es.Server;
using ESchool.SignalR.fireBase;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESchool.SignalR.Server
{


    public class ChatGroupHub : Hub
    {

        public readonly static Dictionary<string, int> _userList = new Dictionary<string, int>();
        //private RoomChatGroup MapToValue(SqlDataReader reader)
        //{
        //    return new RoomChatGroup()
        //    {
        //        RoomChatGroupID = (int)reader["RoomChatGroupID"],
        //        //Value1 = (int)reader["Value1"],
        //        //Value2 = reader["Value2"].ToString()
        //    };
        //}
        //public async Task<List<RoomChatGroup>> GetChatGroupByUser(int userID, int userTypeId)
        //{
        //    using (SqlConnection sql = new SqlConnection(Startup._connection))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("RoomChatGroupByID", sql))
        //        {
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            cmd.Parameters.Add(new SqlParameter("@UserID", userID));
        //            cmd.Parameters.Add(new SqlParameter("@UserTypeID", userTypeId));
        //            List<RoomChatGroup> response = null;
        //            await sql.OpenAsync();

        //            using (var reader = await cmd.ExecuteReaderAsync())
        //            {
        //                while (await reader.ReadAsync())
        //                {
        //                    response.Add (MapToValue(reader));
        //                }
        //            }

        //            return response;
        //        }
        //    }
        //}


        //L====>Lock
        //U====>UnLock
        //C====>Chat
        //D====>Delete
        //U====>Update
        //A====>attach
        //public override async Task OnConnectedAsync()
        //{
        //    try
        //    {
        //        await base.OnConnectedAsync();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                _userList.Remove(Context.ConnectionId, out int userId);
                await LeaveRoom(userId);
                await base.OnDisconnectedAsync(exception);
            }
            catch (Exception ex)
            {

            }

        }

        //Key=chatType+groupId+userId
        //filenameMimeType=Filename+MimeType
        //Status=tagLearn+attachMsg
        //roomChatParent=RoomChatParentID+ParentTextChat+ParentSenderName
        //private readonly IRoomChatService _roomChatService;


        //public ChatGroupHub(IRoomChatService roomChatService)
        //{
        //    _roomChatService = roomChatService;
        //}
        //public async Task SendMessage(string key,string roomChatId, string senderName, string textChat,string filenameMimeType ,string roomChatParent,string status)
        public async Task SendMessage(ChatMessage chatMessage)
        {


            //Add message DB
            /*await _chatRoomService.AddMessage(roomId, message);*/



            //var groupId = key.Split("||")[1];

            //await Clients.Group("Group_" + groupId).SendAsync("ReceiveMessage",k,key , DateTimeOffset.Now.ToString("HH:mm"), roomChatId, senderName, textChat, filenameMimeType, roomChatParent, status);

            if (chatMessage.chatType == "C")
            {
                await SendFireBase.SendNotificationAsync(chatMessage);
            }


            await Clients.Group("Group_" + chatMessage.groupId).SendAsync("ReceiveMessage", chatMessage, Context.ConnectionId);
        }
        public async Task JoinRoom(int userId)
        {
            _userList.TryAdd(Context.ConnectionId, userId);
            using (SqlConnection sql = new SqlConnection(Startup._connection))
            {
                using (SqlCommand cmd = new SqlCommand("RoomChatGroupByID", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@UserID", userId));


                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            await Groups.AddToGroupAsync(Context.ConnectionId, "Group_" + (int)reader["RoomChatGroupID"]);
                        }
                    }


                }
            }


            //var query = await GetChatGroupByUser(userId, userTypeId);
            //foreach (var group in query)
            //{
            //    await Groups.AddToGroupAsync(Context.ConnectionId, "Group_" + group.RoomChatGroupID);
            //}
            /*await Groups.AddToGroupAsync(Context.ConnectionId, "Group_3");
            await Groups.AddToGroupAsync(Context.ConnectionId, "Group_36");*/
        }


        public async Task LeaveRoom(int userId)
        {
            using (SqlConnection sql = new SqlConnection(Startup._connection))
            {
                using (SqlCommand cmd = new SqlCommand("RoomChatGroupByID", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@UserID", userId));


                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Group_" + (int)reader["RoomChatGroupID"]);
                        }
                    }


                }
            }
            //var query = await _roomChatService.GetChatGroupByUser(userId, userTypeId);
            //foreach (var group in query)
            //{
            //    await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Group_" + group.RoomChatGroupID);
            //}
        }
        public async void GetUserOnlineHub()
        {
            await Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessageAdmin", _userList);
        }

    }
}
