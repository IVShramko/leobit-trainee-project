using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Threading.Tasks;

namespace Dating.Chat.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        public async Task SendToUser(string message, string receiverId)
        {
            string adresseeName = Context.User.Claims
                .Where(c => c.Type == "UserName")
                .Select(c => c.Value)
                .FirstOrDefault();

            string response = adresseeName + " says: " + message;

            var receiver = Clients.User(receiverId);

            await receiver.SendAsync("ReceiveMessage", response);
        }
    }
}
