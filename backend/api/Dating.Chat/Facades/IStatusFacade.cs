using System;
using System.Threading.Tasks;

namespace Dating.Chat.Facades
{
    public interface IStatusFacade
    {
        bool GetUserStatus(string id);

        Task<bool> IsOffline(string id);

        Task<bool> SetOnlineStatus(string id);

        Task<bool> SetOfflineStaus(string id);

        Task<bool> RegisterChatEnterance(string id, Guid chatId);

        Task<bool> IsUserAtChat(string userId, Guid chatId);
    }
}
