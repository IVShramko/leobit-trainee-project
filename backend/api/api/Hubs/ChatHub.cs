using Dating.Logic.Managers.TokenManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dating.WebAPI.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly ITokenManager _tokenManager;

        public ChatHub(ITokenManager tokenManager)
        {
            _tokenManager = tokenManager;
        }
        public async Task SendToAllAsync(string message, string receiverId)
        {
            string adresseeName = Context.User.Claims
                .Where(c => c.Type == "UserName")
                .Select(c => c.Value)
                .FirstOrDefault();

            string response = adresseeName+ " says: " + message;

            var receiver = Clients.User(receiverId);
            
            await receiver.SendAsync("ReceiveMessage", response);
        }
    }
}
