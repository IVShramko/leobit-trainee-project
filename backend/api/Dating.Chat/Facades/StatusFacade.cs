using Dating.Chat.Repositories.StatusRepository;
using System;
using System.Threading.Tasks;

namespace Dating.Chat.Facades
{
    public class StatusFacade : IStatusFacade
    {
        private readonly IStatusRepository _statusRepository;

        public StatusFacade(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public bool GetUserStatus(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> IsOffline(string id)
        {
            bool exists = await _statusRepository.ExistsAsync(id);

            return !exists;
        }

        public async Task<bool> IsUserAtChat(string userId, Guid chatId)
        {
            string status = await _statusRepository.GetStatus(userId);

            bool result = chatId == Guid.Parse(status);

            return result;
        }

        public async Task<bool> RegisterChatEnterance(string id, Guid chatId)
        {
            bool isRegistered = await _statusRepository.UpdateStatus(id, chatId);

            return isRegistered;
        }

        public async Task<bool> SetOfflineStaus(string id)
        {
            bool result = await _statusRepository.DeleteStatus(id);

            return result;
        }

        public async Task<bool> SetOnlineStatus(string id)
        {
            bool result = await _statusRepository.CreateStatus(id);

            return result;
        }
    }
}
