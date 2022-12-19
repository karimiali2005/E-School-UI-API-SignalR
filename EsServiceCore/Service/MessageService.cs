using Es.DataLayerCore.Context;
using Es.DataLayerCore.Model;
using EsServiceCore.Interface;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsServiceCore.Service
{
    public class MessageService: IMessageService
    {
        private readonly ESchoolContext _context;
        public MessageService(ESchoolContext context)
        {
            _context = context;
        }
        public async Task<bool> SendMessageHub(Es.DataLayerCore.DTOs.RoomChat.ChatMessage roomChatReturn)
        {
            HubConnection connection = new HubConnectionBuilder()
                    .WithUrl("http://localhost:9116/ChatGroupHub")
                    .Build();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };

            try
            {
                await connection.StartAsync();
                await connection.InvokeAsync("SendMessage",
                   roomChatReturn);
            }
            catch (Exception ex)
            {

            }
            return true;
        }

        public async Task<bool> SendMessage(List<int> userIdList,string roomChatGroupTitle,int roomChatGroupType,string textChat,int id)
        {
            for (int i = 0; i < userIdList.Count; i++)
            {
                //اضافه کردن گروه
                var roomChatGroup = new RoomChatGroup()
                {
                    RoomChatGroupCreateDateTime = DateTime.Now,
                    RoomChatGroupDelete = false,
                    RoomChatGroupVisible = true,
                    RoomChatGroupTitle = roomChatGroupTitle,
                    TeacherId = null,
                    StudentId = userIdList[i],
                    RoomChatGroupType = roomChatGroupType

                };

                var query = _context.RoomChatGroup.Where(r => r.StudentId == roomChatGroup.StudentId && r.RoomChatGroupType == roomChatGroupType).FirstOrDefault();
                if (query == null)
                {
                    await _context.RoomChatGroup.AddAsync(roomChatGroup);
                    await _context.SaveChangesAsync();

                }
                else
                    roomChatGroup = query;

                //signalR

                var roomChat = new RoomChat()
                {
                    RoomChatDate = DateTime.Now,
                    SenderId = 788,
                    SenderName = roomChatGroupTitle,
                    RecieverId = null,
                    RecieverName = "",
                    RoomId = null,
                    TeacherId = 788,
                    CourseId = null,
                    TextChat =textChat,
                    Filename = "",
                    MimeType = "",
                    TagLearn = false,
                    RoomChatDelete = false,
                    RoomChatUpdate = null,
                    RoomChatParentId = null,
                    AttachMsg = false,
                    RoomChatGroupId = roomChatGroup.RoomChatGroupId
                };
                await _context.RoomChat.AddAsync(roomChat);
                await _context.SaveChangesAsync();



                //financial Update
                switch(roomChatGroupType)
                {
                    case 3: await FinancialUpdate(i, id, roomChat.RoomChatId);
                        break;
                    case 4: await DisciplineUpdate(i, id, roomChat.RoomChatId);
                        break;
                }

            

                


                var roomChatReturn = new Es.DataLayerCore.DTOs.RoomChat.ChatMessage
                {
                    chatType = "C",
                    groupId = roomChatGroup.RoomChatGroupId,
                    senderId = 788,
                    attachMsg = false,
                    filename = "",
                    mimeType = "",

                    roomChatId = roomChat.RoomChatId,
                    senderName = roomChat.SenderName,
                    tagLearn = roomChat.TagLearn,
                    textChat = roomChat.TextChat ?? "",
                    roomChatParentId = roomChat.RoomChatParentId,
                    parentTextChat = "",
                    parentSenderName = "",
                    mainAddress = "",
                    roomChatViewNumber = 0,
                    roomChatDateString = roomChat.RoomChatDate.ToString("HH:mm"),
                    roomChatGroupTitle = roomChatGroupTitle,
                    roomChatGroupType = roomChatGroupType

                };

                await SendMessageHub(roomChatReturn);

            }
            return true;
        }

        public async Task<bool> FinancialUpdate(int index, int financialId, int roomChatId)
        {
            var financialUpdate = _context.Financial.Where(f => f.FinancialId == financialId).FirstOrDefault();
            if (index == 0)
                financialUpdate.RoomChatId = roomChatId;
            else
                financialUpdate.RoomChatId2 = roomChatId;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DisciplineUpdate(int index,int disciplineId,int roomChatId)
        {
            var disciplineUpdate = _context.Discipline.Where(f => f.DisciplineId == disciplineId).FirstOrDefault();
            if (index == 0)
                disciplineUpdate.RoomChatId = roomChatId;
            else
                disciplineUpdate.RoomChatId2 = roomChatId;
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<int> SendMessageCeremony(int roomChatGroupType, string textChat)
        {


          

            var roomChat = new RoomChat()
            {
                RoomChatDate = DateTime.Now,
                SenderId = 788,
                SenderName = "بنر",
                RecieverId = null,
                RecieverName = "",
                RoomId = null,
                TeacherId = 788,
                CourseId = null,
                TextChat = textChat,
                Filename = "",
                MimeType = "",
                TagLearn = false,
                RoomChatDelete = false,
                RoomChatUpdate = null,
                RoomChatParentId = null,
                AttachMsg = false,
                RoomChatGroupId = roomChatGroupType==6?0:-1
            };
            await _context.RoomChat.AddAsync(roomChat);
            await _context.SaveChangesAsync();







            var roomChatReturn = new Es.DataLayerCore.DTOs.RoomChat.ChatMessage
            {
                chatType = "C",
                groupId = roomChatGroupType==6?0:-1,
                senderId = 788,
                attachMsg = false,
                filename = "",
                mimeType = "",

                roomChatId = roomChat.RoomChatId,
                senderName = roomChat.SenderName,
                tagLearn = roomChat.TagLearn,
                textChat = roomChat.TextChat ?? "",
                roomChatParentId = roomChat.RoomChatParentId,
                parentTextChat = "",
                parentSenderName = "",
                mainAddress = "",
                roomChatViewNumber = 0,
                roomChatDateString = roomChat.RoomChatDate.ToString("HH:mm"),
                roomChatGroupTitle = "بنر",
                roomChatGroupType = roomChatGroupType

            };

            await SendMessageHub(roomChatReturn);


            return roomChat.RoomChatId;
        }
    }
}
