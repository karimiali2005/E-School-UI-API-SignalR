using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Es.DataLayerCore.DTOs.RoomChat;
using Es.DataLayerCore.Model;

namespace EsServiceCore.Interface
{
    public interface IRoomChatService
    {
        Task<List<RoomChatRightShowResult>> RoomChatRightShow(int? userId, int? userTypeId);
        Task<List<RoomChatRightShowResult>> RoomChatRightShowNew(int? userId, int? userTypeId, int? pageNumber, int? pageSize);
        Task<Tuple<List<RoomChatLeftShowResult>, int?>> RoomChatLeftShow(int roomChatGroupID, bool tagLearn, int userId,int newChatCount, int pageNumber, int pageSize, string searchText);
        Task<RoomChatLeftPropertyResult> RoomChatLeftPropertyShow(int roomId, int roomChatGroupId);
        Task<IEnumerable<RoomChatGroupByIDResult>> GetChatGroupByUser(int userId, int userTypeId);
        Task<bool> RoomChatInsert(RoomChat roomChat);
        Task<bool> RoomChatViewInsert(int roomChatId, int roomChatGroupId, int userId);
        Task<RoomChat> RoomChatDelete(int roomChatId, bool checkForDelete = false, int chatDeleteTime=0);
        Task<Tuple<RoomChat, bool>> RoomChatUpdate(int roomChatId, string textChat, bool tagLearn);
        Task<RoomChat> RoomChatPin(int roomChatGroupId, int roomChatId, bool isPin);
        Task<bool> RoomChatLock(int roomChatGroupId,bool isLock);
        Task<List<RoomChatGroupMemberResult>> RoomChatGroupMemberShow(int roomChatGroupID);
        Task<List<RoomLiveViewModel>> RoomLiveShow(int userId);
        Task<List<RoomChatContactResult>> RoomChatContactShow(int userId, int userTypeId);
        Task<RoomChatGroup> RoomChatGroupInsert(RoomChatGroup roomChatGroup, int userTypeId);
        Task<RoomChat> RoomChatViewDelete(int roomChatId, int userId);
        bool RoomChatDeleteAll(int roomChatGroupId, int userId);
        Task<List<ChatMessage>> RoomChatForwardInsert(string listId, int roomChatId, int userId, string senderName);
        Task<bool> RoomChatGroupRemove(int roomChatGroupId, int userTypeId);
        Task<bool> RoomChatGroupUpdate(int roomChatGroupId);
        Task<bool> RoomChatPlayVoiceUpdate(int roomChatId, int userId);
    }
}
