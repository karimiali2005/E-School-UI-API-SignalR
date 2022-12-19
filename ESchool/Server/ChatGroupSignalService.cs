using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EsServiceCore.Interface;

namespace ESchool.Server
{
    public class ChatGroupSignalService:IChatGroupSignalService
    {
        /*private static readonly Dictionary<string,int> _roomInfo = new Dictionary<string, int>();*/
        /*private readonly IRoomChatService _roomChatService;

        public ChatGroupSignalService(IRoomChatService roomChatService)
        {
            _roomChatService = roomChatService;
        }*/
        /*public void CreateRooms(List<int> groupList)
        {
            foreach (var groupId in groupList)
            {
                var groupIdTemp = 0;
                if (_roomInfo.TryGetValue("Group_" + groupId,
                    out groupIdTemp) == false)
                {
                    _roomInfo.TryAdd("Group_" + groupId, groupId);
                }

            }
           
        }*/
        /*public async Task<IEnumerable<int?>> GetChatGroupByUser(int userId)
        {
            var roomChatRight = await _roomChatService.RoomChatRightShow(userId);
            var result = roomChatRight.Select(r => r.RoomChatGroupID);
            return await Task.FromResult(result);
        }
        */

    }
}
