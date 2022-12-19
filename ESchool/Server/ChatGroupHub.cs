using System.Threading.Tasks;
using Es.DataLayerCore.DTOs.RoomChat;
using EsServiceCore.Interface;
using Microsoft.AspNetCore.SignalR;

namespace ESchool.Server
{
    public class ChatGroupHub:Hub
    {
        //L====>Lock
        //U====>UnLock
        //C====>Chat
        //D====>Delete
        //U====>Update
        //A====>attach


        //Key=chatType+groupId+userId
        //filenameMimeType=Filename+MimeType
        //Status=tagLearn+attachMsg
        //roomChatParent=RoomChatParentID+ParentTextChat+ParentSenderName
        private readonly IRoomChatService _roomChatService;

        public ChatGroupHub(IRoomChatService roomChatService)
        {
            _roomChatService = roomChatService;
        }
        //public async Task SendMessage(string key,string roomChatId, string senderName, string textChat,string filenameMimeType ,string roomChatParent,string status)
        public async Task SendMessage(ChatMessage chatMessage)
        {


            //Add message DB
            /*await _chatRoomService.AddMessage(roomId, message);*/



            //var groupId = key.Split("||")[1];

            //await Clients.Group("Group_" + groupId).SendAsync("ReceiveMessage",k,key , DateTimeOffset.Now.ToString("HH:mm"), roomChatId, senderName, textChat, filenameMimeType, roomChatParent, status);
            await Clients.Group("Group_" + chatMessage.groupId).SendAsync("ReceiveMessage", chatMessage, Context.ConnectionId);
        }
        public async Task JoinRoom(int userId,int userTypeId)
        {
            var query = await _roomChatService.GetChatGroupByUser(userId, userTypeId);
            foreach (var groupId in query)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "Group_" + groupId);
            }
            /*await Groups.AddToGroupAsync(Context.ConnectionId, "Group_3");
            await Groups.AddToGroupAsync(Context.ConnectionId, "Group_36");*/
        }

      
        public async Task LeaveRoom(int userId, int userTypeId)
        {
            var query = await _roomChatService.GetChatGroupByUser(userId, userTypeId);
            foreach (var groupId in query)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Group_" + groupId);
            }
        }
    }
}
