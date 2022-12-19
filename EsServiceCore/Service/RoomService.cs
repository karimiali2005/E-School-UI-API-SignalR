using Es.DataLayerCore.Context;
using Es.DataLayerCore.Model;
using EsServiceCore.Interface;
using System;
using System.Threading.Tasks;

namespace EsServiceCore.Service
{
    public class RoomService: IRoomService
    {
        private readonly ESchoolContext _context;
        public RoomService(ESchoolContext context)
        {
            _context = context;
        }
        public bool RoomChatGroupUpdate()
        {
            try
            {

               _context.RoomChatGroupInsert();
              
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
