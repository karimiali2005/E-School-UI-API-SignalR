using System.Collections.Generic;
using System.Threading.Tasks;
using Es.DataLayerCore.DTOs.User;
using Es.DataLayerCore.Model;

namespace EsServiceCore.Interface
{
    public interface IUserService
    {
        Task<LoginUserResult> LoginUser(LoginViewModel login);
        Task<LoginUserResult> LoginUserTicket(string userName, string hashPassword);
        Task<bool> UserPlatformAdd(int userId, string platfornName, string mobileName, string tokenFireBase);
        Task<User> GetGetById(int userId);
        Task<bool> LoginInsert(Login login, string ticket);
        Task<bool> LoginEnd(int userId);
        Task<User> TicketApprove(string ticket);
        Task<string> UserSetTicket(int userId);
    }
}
