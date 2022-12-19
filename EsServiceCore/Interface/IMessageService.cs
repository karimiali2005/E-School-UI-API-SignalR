using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsServiceCore.Interface
{
    public interface IMessageService
    {
        Task<bool> SendMessage(List<int> userIdList, string roomChatGroupTitle, int roomChatGroupType, string textChat, int id);
        Task<int> SendMessageCeremony(int roomChatGroupType, string textChat);
    }
}
