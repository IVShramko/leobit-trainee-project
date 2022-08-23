using Dating.Logic.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dating.Logic.Repositories.ChatRepository
{
    public interface IChatRepository
    {
        Task<ICollection<ChatShortDTO>> GetAllUserChatsAsync(string aspNetUserId);

        Task<ChatFullDTO> GetChatByIdAsync(Guid chatId, string senderId);
    }
}
