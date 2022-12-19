using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Es.DataLayerCore.Context;
using Es.DataLayerCore.DTOs.RoomChat;
using Es.DataLayerCore.Model;
using EsServiceCore.Interface;
using Microsoft.EntityFrameworkCore;

namespace EsServiceCore.Service
{
    public class RoomChatService:IRoomChatService
    {
        private readonly ESchoolContext _context;
        public RoomChatService(ESchoolContext context)
        {
            _context = context;
        }

        public async Task<List<RoomChatRightShowResult>> RoomChatRightShow(int? userId,int? userTypeId)
        {

            try
            {
                var roomChatRight = await _context.RoomChatRightShowAsync(userId, userTypeId);
                return roomChatRight;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public async Task<List<RoomChatRightShowResult>> RoomChatRightShowNew(int? userId, int? userTypeId,int? pageNumber,int? pageSize)
        {

            try
            {
                var roomChatRight = await _context.RoomChatRightShowNewAsync(userId, userTypeId,pageNumber,pageSize);

                var roomChatRightNew = (from r in roomChatRight
                                        select new RoomChatRightShowResult()
                                        {
                                            CourseID = r.CourseID,
                                            HomeWorkNewNumber = r.HomeWorkNewNumber,
                                            MessageNewNumber = r.MessageNewNumber,
                                            Mimetype = r.Mimetype,
                                            PicName = r.PicName,
                                            RoomChatDate = r.RoomChatDate,
                                            RoomChatGroupID = r.RoomChatGroupID,
                                            RoomChatGroupType = r.RoomChatGroupType,
                                            RoomChatTitle = r.RoomChatTitle,
                                            RoomID = r.RoomID,
                                            TeacherID = r.TeacherID,
                                            TextChat = r.TextChat,
                                            UserIDPic = r.UserIDPic,

                                        }).ToList();

                return roomChatRightNew;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public async Task<Tuple<List<RoomChatLeftShowResult>,int?>> RoomChatLeftShow(int roomChatGroupID, bool tagLearn,int userId,int newChatCount, int pageNumber,int pageSize,string searchText)
        {

            try
            {
              //  _context.RoomChatViewInsertAll(userId, roomChatGroupID);
                var roomChatRight = await _context.RoomChatLeftShowAsync(roomChatGroupID,tagLearn,userId, newChatCount, pageNumber,pageSize,searchText);
                return roomChatRight;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public async Task<RoomChatLeftPropertyResult> RoomChatLeftPropertyShow(int roomId, int roomChatGroupId)
        {

            try
            {
                var roomChatLeftProperty = await _context.RoomChatLeftPropertyAsync(roomId, roomChatGroupId);
                return roomChatLeftProperty.FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<IEnumerable<RoomChatGroupByIDResult>> GetChatGroupByUser(int userId,int userTypeId)
        {
            var roomChatRight = await _context.RoomChatGroupByIDAsync(userId,userTypeId);
         
            return await Task.FromResult(roomChatRight);
        }

        public async Task<bool> RoomChatInsert(RoomChat roomChat)
        {
            try
            {
               
                await _context.RoomChat.AddAsync(roomChat);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> RoomChatViewInsert(int roomChatId,int roomChatGroupId,int userId)
        {
            try
            {
                _context.RoomChatViewInsert(roomChatId,roomChatGroupId, userId);
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<Tuple<RoomChat,bool>> RoomChatUpdate(int roomChatId, string textChat, bool tagLearn)
        {
            try
            {
                var changeTagLearn = false;
                var roomChat = await _context.RoomChat.FindAsync(roomChatId);
                if(roomChat.TagLearn!= tagLearn)
                {
                    changeTagLearn = true;
                }
                roomChat.TextChat = textChat;
                roomChat.TagLearn = tagLearn;
              
                _context.Entry(roomChat).State = EntityState.Modified;
             
                await _context.SaveChangesAsync();


                return new Tuple<RoomChat, bool>(roomChat, changeTagLearn) ;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<RoomChat> RoomChatDelete(int roomChatId,bool checkForDelete=false,int chatDeleteTime=0)
        {
            try
            {
                var roomChat = await _context.RoomChat.FindAsync(roomChatId);
                if (checkForDelete && (DateTime.Now - roomChat.RoomChatDate).TotalMinutes < chatDeleteTime)
                {
                    roomChat.RoomChatDelete = true;
                    _context.Entry(roomChat).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return roomChat;
                }
                else if(checkForDelete==false)
                {
                    roomChat.RoomChatDelete = true;
                    _context.Entry(roomChat).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return roomChat;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<RoomChat> RoomChatPin(int roomChatGroupId,int roomChatId,bool isPin)
        {
            try
            {
                if (isPin)
                {
                    var roomChatBefore = await _context.RoomChat.Where(r=>r.RoomChatGroupId==roomChatGroupId&&r.AttachMsg==true).FirstOrDefaultAsync();
                    if (roomChatBefore != null)
                    {
                        roomChatBefore.AttachMsg = false;
                        _context.Entry(roomChatBefore).State = EntityState.Modified;

                        await _context.SaveChangesAsync();
                    }

                }
                var roomChat = await _context.RoomChat.FindAsync(roomChatId);
                roomChat.AttachMsg = isPin;

                _context.Entry(roomChat).State = EntityState.Modified;

                await _context.SaveChangesAsync();


                return roomChat;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> RoomChatLock(int roomChatGroupId, bool isLock)
        {
            try
            {
                
                var roomChatGroup = await _context.RoomChatGroup.FindAsync(roomChatGroupId);
                roomChatGroup.CloseChat = isLock;

                _context.Entry(roomChatGroup).State = EntityState.Modified;

                await _context.SaveChangesAsync();


                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<RoomChatGroupMemberResult>> RoomChatGroupMemberShow(int roomChatGroupID)
        {

            try
            {
                var memberGroup = await _context.RoomChatGroupMemberAsync(roomChatGroupID);
               
                return memberGroup;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public async Task<List<RoomLiveViewModel>> RoomLiveShow(int userId)
        {

            try
            {
                var roomLives = await _context.RoomLiveShowAsync(userId);
                List<RoomLiveViewModel> roomLiveViewModel = new List<RoomLiveViewModel>();
                foreach (var roomLive in roomLives )
                {
                    if(!string.IsNullOrEmpty(roomLive.AdobeLiveAddress))
                    {
                        roomLiveViewModel.Add(new RoomLiveViewModel
                        {
                            LiveType = 1,
                            LiveAddress = roomLive.AdobeLiveAddress,
                            LivePassword = roomLive.AdobeLivePass,
                            LiveUsername = roomLive.AdobeLiveUsername,
                            RoomChatGroupTitle = roomLive.RoomChatGroupTitle
                        });
                    }
                    if (!string.IsNullOrEmpty(roomLive.JitsiLiveAddress))
                    {
                        roomLiveViewModel.Add(new RoomLiveViewModel
                        {
                            LiveType = 2,
                            LiveAddress = roomLive.JitsiLiveAddress,
                            LivePassword = roomLive.JitsiLivePassword,
                            RoomChatGroupTitle=roomLive.RoomChatGroupTitle
                        });
                    }
                    if (!string.IsNullOrEmpty(roomLive.ZoomAddress))
                    {
                        roomLiveViewModel.Add(new RoomLiveViewModel
                        {
                            LiveType = 3,
                            LiveAddress = roomLive.ZoomAddress,
                            RoomChatGroupTitle = roomLive.RoomChatGroupTitle
                        });
                    }

                }

                return roomLiveViewModel;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public async Task<List<RoomChatContactResult>> RoomChatContactShow(int userId,int userTypeId)
        {

            try
            {
                var roomChatContacts = await _context.RoomChatContactAsync(userId, userTypeId);
                
                return roomChatContacts;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<RoomChatGroup> RoomChatGroupInsert(RoomChatGroup roomChatGroup,int userTypeId)
        {
            try
            {
                var query =  _context.RoomChatGroup.Where(r => r.TeacherId == roomChatGroup.TeacherId && r.StudentId == roomChatGroup.StudentId&&r.RoomChatGroupType==2).FirstOrDefault();
                if (query == null)
                {
                    await _context.RoomChatGroup.AddAsync(roomChatGroup);
                    await _context.SaveChangesAsync();
                    return roomChatGroup;
                }
                else
                {
                    if (userTypeId == 1)
                    {
                        query.RoomChatGroupDeleteStudent = false;

                    }
                    else if (userTypeId == 4)
                    {
                        query.RoomChatGroupDeleteTeacher = false;
                    }
                    await _context.SaveChangesAsync();
                    return query;
                }
                  
                
              
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<RoomChat> RoomChatViewDelete(int roomChatId,int userId)
        {
            try
            {
                var roomChatView = await _context.RoomChatView.Where(r => r.RoomChatId == roomChatId && r.UserId == userId).FirstOrDefaultAsync();
                roomChatView.RoomChatViewDelete = true;
                _context.Entry(roomChatView).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                var roomChat = await _context.RoomChat.FindAsync(roomChatId);



                return roomChat;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool RoomChatDeleteAll(int roomChatGroupId, int userId )
        {
            try
            {
                _context.RoomChatDeleteAll(roomChatGroupId,userId);
               
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<ChatMessage>> RoomChatForwardInsert(string listId,int roomChatId,int userId,string senderName)
        {
            try
            {
                var listChatMessage = new List<ChatMessage>();

                var roomChat = await _context.RoomChat.FindAsync(roomChatId);
                foreach (var roomChatGroupId in listId.Split(","))
                {
                    if (roomChatGroupId != "")
                    {
                        var roomChatGroup = await _context.RoomChatGroup.FindAsync(Convert.ToInt32( roomChatGroupId));
                        var roomchatNew = new RoomChat()
                        {
                            RoomChatDate = DateTime.Now,
                            SenderId = userId,
                            SenderName = senderName,
                            RecieverId = null,
                            RecieverName = "",
                            RoomId = roomChatGroup.RoomId,
                            TeacherId = roomChatGroup.TeacherId,
                            CourseId = roomChatGroup.CourseId,
                            TextChat = roomChat.TextChat,
                            Filename = roomChat.Filename,
                            MimeType = roomChat.MimeType,
                            TagLearn = roomChat.TagLearn,
                            RoomChatDelete = false,
                            RoomChatUpdate = null,
                            RoomChatParentId = null,
                            AttachMsg = false,
                            RoomChatGroupId = roomChatGroup.RoomChatGroupId,
                            RoomChatFolder=roomChat.RoomChatFolder
                        };
                        await _context.RoomChat.AddAsync(roomchatNew);
                        await _context.SaveChangesAsync();

                        var roomChatReturn = new ChatMessage
                        {
                            chatType = "C",
                            groupId = roomChatGroup.RoomChatGroupId,
                            senderId = userId,
                            attachMsg = roomchatNew.AttachMsg,
                            filename = roomchatNew.Filename ?? "",
                            mimeType = roomchatNew.MimeType ?? "",

                            roomChatId = roomchatNew.RoomChatId,
                            senderName = roomchatNew.SenderName,
                            tagLearn = roomchatNew.TagLearn,
                            textChat = roomchatNew.TextChat ?? "",
                            roomChatParentId = null,
                            parentTextChat = "",
                            parentSenderName ="",
                            mainAddress = "",
                            roomChatViewNumber = 0,
                            roomChatDateString = roomchatNew.RoomChatDate.ToString("HH:mm"),

                        };
                        listChatMessage.Add(roomChatReturn);
                    }
                }
                
                

                return listChatMessage;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> RoomChatGroupRemove(int roomChatGroupId,int userTypeId)
        {
            try
            {
                var roomChatGroup = await _context.RoomChatGroup.Where(r => r.RoomChatGroupId== roomChatGroupId).FirstOrDefaultAsync();
               
              
                if (userTypeId==4)
                {
                    roomChatGroup.RoomChatGroupDeleteTeacher = true;
                }
                else
                {
                    roomChatGroup.RoomChatGroupDeleteStudent = true;
                }
              
                _context.Entry(roomChatGroup).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> RoomChatGroupUpdate(int roomChatGroupId)
        {
            try
            {
                var roomChatGroup = await _context.RoomChatGroup.Where(r => r.RoomChatGroupId == roomChatGroupId).FirstOrDefaultAsync();


                if (roomChatGroup.RoomChatGroupDeleteStudent == true || roomChatGroup.RoomChatGroupDeleteTeacher == true)
                {
                    roomChatGroup.RoomChatGroupDeleteTeacher = false;
                    roomChatGroup.RoomChatGroupDeleteStudent = false;


                    _context.Entry(roomChatGroup).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> RoomChatPlayVoiceUpdate(int roomChatId,int userId)
        {
            try
            {
                var roomChatView = await _context.RoomChatView.Where(r => r.RoomChatId == roomChatId && r.UserId== userId).FirstOrDefaultAsync();

                if(roomChatView!=null)
                {
                    roomChatView.RoomChatPlayVoice = true;
                    _context.Entry(roomChatView).State = EntityState.Modified;
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
