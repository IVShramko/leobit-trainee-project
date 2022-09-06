using Dating.Chat.Facades;
using Dating.Logic.DTO;
using Dating.Logic.Enums;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Dating.Chat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IStatusFacade _statusFacade;

        public ChatHub(IStatusFacade statusFacade)
        {
            _statusFacade = statusFacade;
        }

        public async Task<MessageStatuses> SendToUser(ChatMessageDTO message)
        {
            var receiver = Clients.User(message.ReceiverId);

            bool isOffline = await _statusFacade.IsOffline(message.ReceiverId);

            if (!isOffline)
            {
                bool isAtChat = await _statusFacade
                    .IsUserAtChat(message.ReceiverId, message.ChatId);

                if (isAtChat)
                {
                    var awaiter = receiver
                        .SendAsync(ChatMethods.ReceiveMessage, message)
                        .GetAwaiter();

                    MessageStatuses result = awaiter.IsCompleted
                        ? MessageStatuses.Sent
                        : MessageStatuses.Error;

                    return result;
                }
                
                //redirect to notification hub
            }

            //add message to db
            return MessageStatuses.Sent;
        }
    }
}
