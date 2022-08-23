using Dating.Logic.DTO;
using Dating.Logic.Repositories.ChatMessageRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dating.Logic.Facades.MessageFacade
{
    public class MessageFacade : IMessageFacade
    {
        private readonly IChatMessageRepository _messageRepository;

        public MessageFacade(IChatMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<bool> CreateMessageAsync(ChatMessageCreateDTO newMessage)
        {
            bool result = await _messageRepository.CreateMessageAsync(newMessage);

            return result;
        }

        public async Task<ICollection<ChatMessageDTO>> GetAllChatMessagesAsync(Guid chatId)
        {
            ICollection<ChatMessageDTO> messages = 
                await _messageRepository.GetAllChatMessagesAsync(chatId);

            return messages;
        }
    }
}
