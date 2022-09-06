using Dating.Chat.Facades;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Dating.Chat.Hubs
{
    public class StatusHub : Hub
    {
        private readonly IStatusFacade _statusFacade;

        public StatusHub(IStatusFacade statusFacade)
        {
            _statusFacade = statusFacade;
        }

        public async Task<bool> UpdateStatus(bool status)
        {
            string id = Context.UserIdentifier;

            bool isUpdated;

            if (status)
            {
                isUpdated = await _statusFacade.SetOnlineStatus(id);
            }
            else 
            {
                isUpdated = await _statusFacade.SetOfflineStaus(id);
            }

            var awaiter = Clients
                .AllExcept(Context.ConnectionId)
                .SendAsync(ChatMethods.GetStatusUpdates, id, status)
                .GetAwaiter();

            bool result = awaiter.IsCompleted && isUpdated;

            return result;
        }

        public async Task<bool> RegisterChatEnterance(Guid chatId) 
        {
            string userId = Context.UserIdentifier;

            bool result = await _statusFacade.RegisterChatEnterance(userId, chatId);

            return result;
        }
    }
}
