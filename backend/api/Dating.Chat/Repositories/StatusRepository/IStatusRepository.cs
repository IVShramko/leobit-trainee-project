using System;
using System.Threading.Tasks;

namespace Dating.Chat.Repositories.StatusRepository
{
    public interface IStatusRepository
    {
        Task<bool> UpdateStatus(string userId, Guid chatId);

        Task<bool> ExistsAsync(string userId);

        Task<bool> DeleteStatus(string userId);

        Task<bool> CreateStatus(string userId);

        Task<string> GetStatus(string userId);
    }
}
