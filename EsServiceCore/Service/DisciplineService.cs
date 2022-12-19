using Es.DataLayerCore.Context;
using Es.DataLayerCore.DTOs.Discipline;
using Es.DataLayerCore.Model;
using EsServiceCore.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsServiceCore.Service
{
    public class DisciplineService: IDisciplineService
    {
        private readonly ESchoolContext _context;
        private readonly IMessageService _messageService;
        public DisciplineService(ESchoolContext context, IMessageService messageService)
        {
            _context = context;
            _messageService = messageService;
        }
        public async Task<List<DisciplineShowResult>> DisciplineShow(int userId)
        {
            try
            {
                var disciplineShows = await _context.DisciplineShowAsync(userId);
                return disciplineShows;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<DisciplineType>> DisciplineTypeShow()
        {
            try
            {
                var disciplineTypes = await _context.DisciplineType.ToListAsync();
                return disciplineTypes;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> DisciplineInsert(Discipline discipline)
        {
            try
            {
                discipline.RoomChatId = null;

                await _context.Discipline.AddAsync(discipline);
                await _context.SaveChangesAsync();

                List<int> userIdList = new List<int> { };

                if (discipline.DisciplineTypeId == 2)
                {
                    var user = await _context.User.FindAsync(discipline.UserId);
                    if (user.UserIdfather != null)
                        userIdList.Add((int)user.UserIdfather);

                    if (user.UserIdmother != null)
                        userIdList.Add((int)user.UserIdmother);
                }
                else
                {
                    userIdList.Add(discipline.UserId);
                }

                await _messageService.SendMessage(userIdList, "پیام انضباطی", 4, discipline.DisciplineText, discipline.DisciplineId);

               




                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> StudentDisciplineDelete(List<int> StudentDisciplineIds)
        {
            try
            {
                foreach (var id in StudentDisciplineIds)
                {
                    var discipline = await _context.Discipline.FindAsync(id);
                    var roomChat = await _context.RoomChat.FindAsync(discipline.RoomChatId);
                    if (discipline.RoomChatId2 != null)
                    {
                        var roomChat2 = await _context.RoomChat.FindAsync(discipline.RoomChatId2);
                        roomChat2.RoomChatDelete = true;
                    }
                    roomChat.RoomChatDelete = true;
                    _context.Discipline.Remove(discipline);


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
