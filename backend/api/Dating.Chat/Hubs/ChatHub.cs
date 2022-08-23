using Dating.Logic.DTO;
using Dating.Logic.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace Dating.Chat.Hubs
{
    //[Authorize]
    public class ChatHub : Hub
    {
        public async Task SendToUser(ChatMessageDTO message)
        {
            var receiver = Clients.User(message.ReceiverId);
            var sender = Clients.User(message.SenderId);

            Task sendMessage = receiver.SendAsync(ChatMethods.ReceiveMessage, message);

            Task sendMessageStatus = sender
                    .SendAsync(ChatMethods.GetMessageDeliveryStatus, MessageDeliveryStatus.Sent);

            await sendMessage.ContinueWith( (sendMessageStatus) => { });
        }
    }
}
