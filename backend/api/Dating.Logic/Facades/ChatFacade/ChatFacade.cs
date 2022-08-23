using Dating.Logic.DTO;
using Dating.Logic.Repositories.ChatRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dating.Logic.Facades.ChatFacade
{
    public class ChatFacade : IChatFacade
    {
        private readonly IChatRepository _chatRepository;

        public ChatFacade(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task<ICollection<ChatShortDTO>> GetAllUserChatsAsync(string aspNetUserId)
        {
            var chats = await _chatRepository.GetAllUserChatsAsync(aspNetUserId);

            return chats;
        }

        public async Task<ChatFullDTO> GetChatByIdAsync(Guid chatId, string senderId)
        {
            ChatFullDTO chat = await _chatRepository.GetChatByIdAsync(chatId, senderId);

            return chat;
        }
    }
}
