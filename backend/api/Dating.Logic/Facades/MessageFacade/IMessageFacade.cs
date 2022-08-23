using Dating.Logic.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dating.Logic.Facades.MessageFacade
{
    public interface IMessageFacade
    {
        Task<ICollection<ChatMessageDTO>> GetAllChatMessagesAsync(Guid chatId);

        Task<bool> CreateMessageAsync(ChatMessageCreateDTO newMessage);
    }
}
