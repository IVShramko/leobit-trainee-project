using Dating.Logic.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dating.Logic.Repositories.ChatMessageRepository
{
    public interface IChatMessageRepository
    {
        Task<ICollection<ChatMessageDTO>> GetAllChatMessagesAsync(Guid chatId);

        Task<bool> CreateMessageAsync(ChatMessageCreateDTO newMessage);
    }
}
