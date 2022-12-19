using Es.DataLayerCore.Context;
using Es.DataLayerCore.DTOs.Ceremony;
using Es.DataLayerCore.Model;
using EsServiceCore.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EsServiceCore.Service
{
    public class CeremonyService: ICeremonyService
    {
        private readonly ESchoolContext _context;
        private readonly IMessageService _messageService;
        public CeremonyService(ESchoolContext context, IMessageService messageService)
        {
            _context = context;
            _messageService = messageService;
        }
        public async Task<List<CeremonyShowResult>> CeremonyShow()
        {
            try
            {
                var ceremonyShows = await _context.CeremonyShowAsync();
                return ceremonyShows;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<CeremonyType>> CeremonyTypeShow()
        {
            try
            {
                var ceremonyTypes = await _context.CeremonyType.ToListAsync();
                return ceremonyTypes;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> CeremonyInsert(Ceremony ceremony)
        {
            try
            {

                await _context.Ceremony.AddAsync(ceremony);
                

                var roomChatId= await _messageService.SendMessageCeremony(ceremony.CeremonyTypeId==1?6:7,ceremony.CeremonyText);

                ceremony.RoomChatId = roomChatId;

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Ceremony> CeremonyGetById(int ceremonyId)
        {
            try
            {
                var ceremony = await _context.Ceremony.FindAsync(ceremonyId);
                return ceremony;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> CeremonyDelete(int id)
        {
            try
            {


                var ceremony = await _context.Ceremony.FindAsync(id);
                _context.Ceremony.Remove(ceremony);


                var roomChat = await _context.RoomChat.FindAsync(ceremony.RoomChatId);
                roomChat.RoomChatDelete = true;
                await _context.SaveChangesAsync();


                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
