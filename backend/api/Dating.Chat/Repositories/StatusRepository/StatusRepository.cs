using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace Dating.Chat.Repositories.StatusRepository
{
    public class StatusRepository : IStatusRepository
    {
        private readonly IDatabase _db;

        public StatusRepository(IConnectionMultiplexer redis)
        {
            _db = redis.GetDatabase();
        }

        public async Task<bool> CreateStatus(string userId)
        {
            bool isSet = await _db.StringSetAsync(userId, string.Empty);

            return isSet;
        }

        public async Task<bool> DeleteStatus(string userId)
        {
            bool isDeleted = await _db.KeyDeleteAsync(userId);

            return isDeleted;
        }

        public async Task<bool> ExistsAsync(string userId)
        {
            string status = await _db.StringGetAsync(userId);

            return status != null;
        }

        public async Task<string> GetStatus(string userId)
        {
            string status = await _db.StringGetAsync(userId);

            return status;
        }

        public async Task<bool> UpdateStatus(string userId, Guid chatId) 
        {
            bool isUpdated = await _db.StringSetAsync(userId, chatId.ToString());

            return isUpdated;
        }
    }
}
