using Es.DataLayerCore.Context;
using Es.DataLayerCore.DTOs.Finacial;
using Es.DataLayerCore.Model;
using EsServiceCore.Interface;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsServiceCore.Service
{
    public class FinancialService: IFinancialService
    {
        private readonly ESchoolContext _context;
        private readonly IMessageService _messageService;
        public FinancialService(ESchoolContext context, IMessageService messageService)
        {
            _context = context;
            _messageService = messageService;
        }
        public async Task<List<FinancialShowResult>> FinancialShow(int userId)
        {
            try
            {
                var financialShows = await _context.FinancialShowAsync(userId);
                return financialShows;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<FinancialSendType>> FinancialSendTypeShow()
        {
            try
            {
                var financialSendType = await _context.FinancialSendType.ToListAsync();
                return financialSendType;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> FinancialInsert(Financial financial)
        {
            try
            {
                financial.RoomChatId = null;

                await _context.Financial.AddAsync(financial);
                await _context.SaveChangesAsync();

                List<int> userIdList = new List<int> { };

                if(financial.FinancialSendTypeId==2)
                {
                    var user = await _context.User.FindAsync(financial.UserId);
                    if(user.UserIdfather!=null)
                        userIdList.Add((int)user.UserIdfather);

                    if (user.UserIdmother != null)
                        userIdList.Add((int)user.UserIdmother);
                }
                else
                {
                    userIdList.Add(financial.UserId);
                }
                await _messageService.SendMessage(userIdList, "پیام مالی", 3, financial.FinancialText, financial.FinancialId);

                //for(int i=0;i< userIdList.Count;i++)
                //{
                //    //اضافه کردن گروه
                //    var roomChatGroup = new RoomChatGroup()
                //    {
                //        RoomChatGroupCreateDateTime = DateTime.Now,
                //        RoomChatGroupDelete = false,
                //        RoomChatGroupVisible = true,
                //        RoomChatGroupTitle = "پیام مالی",
                //        TeacherId = null,
                //        StudentId = userIdList[i],
                //        RoomChatGroupType = 3

                //    };

                //    var query = _context.RoomChatGroup.Where(r => r.StudentId == roomChatGroup.StudentId && r.RoomChatGroupType == 3).FirstOrDefault();
                //    if (query == null)
                //    {
                //        await _context.RoomChatGroup.AddAsync(roomChatGroup);
                //        await _context.SaveChangesAsync();

                //    }
                //    else
                //        roomChatGroup = query;

                //    //signalR

                //    var roomChat = new RoomChat()
                //    {
                //        RoomChatDate = DateTime.Now,
                //        SenderId = 788,
                //        SenderName = "پیام مالی",
                //        RecieverId = null,
                //        RecieverName = "",
                //        RoomId = null,
                //        TeacherId = 788,
                //        CourseId = null,
                //        TextChat = financial.FinancialText,
                //        Filename = "",
                //        MimeType = "",
                //        TagLearn = false,
                //        RoomChatDelete = false,
                //        RoomChatUpdate = null,
                //        RoomChatParentId = null,
                //        AttachMsg = false,
                //        RoomChatGroupId = roomChatGroup.RoomChatGroupId
                //    };
                //    await _context.RoomChat.AddAsync(roomChat);
                //    await _context.SaveChangesAsync();



                //    //financial Update

                //    var financialUpdate = _context.Financial.Where(f => f.FinancialId == financial.FinancialId).FirstOrDefault();
                //    if(i==0)
                //        financial.RoomChatId = roomChat.RoomChatId;
                //    else
                //        financial.RoomChatId2 = roomChat.RoomChatId;
                //    await _context.SaveChangesAsync();


                //    var roomChatReturn = new Es.DataLayerCore.DTOs.RoomChat.ChatMessage
                //    {
                //        chatType = "C",
                //        groupId = roomChatGroup.RoomChatGroupId,
                //        senderId = 788,
                //        attachMsg = false,
                //        filename = "",
                //        mimeType = "",

                //        roomChatId = roomChat.RoomChatId,
                //        senderName = roomChat.SenderName,
                //        tagLearn = roomChat.TagLearn,
                //        textChat = roomChat.TextChat ?? "",
                //        roomChatParentId = roomChat.RoomChatParentId,
                //        parentTextChat = "",
                //        parentSenderName = "",
                //        mainAddress = "",
                //        roomChatViewNumber = 0,
                //        roomChatDateString = roomChat.RoomChatDate.ToString("HH:mm"),
                //        roomChatGroupTitle = "پیام مالی",
                //        roomChatGroupType = 3

                //    };

                //    HubConnection connection = new HubConnectionBuilder()
                //    .WithUrl("http://localhost:9116/ChatGroupHub")
                //    .Build();

                //    connection.Closed += async (error) =>
                //    {
                //        await Task.Delay(new Random().Next(0, 5) * 1000);
                //        await connection.StartAsync();
                //    };

                //    try
                //    {
                //        await connection.StartAsync();
                //        await connection.InvokeAsync("SendMessage",
                //           roomChatReturn);
                //    }
                //    catch (Exception ex)
                //    {

                //    }

                //}




                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> StudentFinancialDelete(List<int> StudentFinancialIds)
        {
            try
            {
                foreach (var id in StudentFinancialIds)
                {
                    var financial = await _context.Financial.FindAsync(id);
                    var roomChat = await _context.RoomChat.FindAsync(financial.RoomChatId);
                    if (financial.RoomChatId2 != null)
                    {
                        var roomChat2 = await _context.RoomChat.FindAsync(financial.RoomChatId2);
                        roomChat2.RoomChatDelete = true;
                    }
                    roomChat.RoomChatDelete = true;
                    _context.Financial.Remove(financial);

                    
                    await _context.SaveChangesAsync();



                }

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
