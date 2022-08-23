using Dating.Logic.DB;
using Dating.Logic.DTO;
using Dating.Logic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating.Logic.Repositories.ChatMessageRepository
{
    public class ChatMessageRepository : IChatMessageRepository
    {
        private readonly AppDbContext _context;

        public ChatMessageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateMessageAsync(ChatMessageCreateDTO newMessage)
        {
            ChatMessage message = new ChatMessage()
            {
                Text = newMessage.Text,
                ChatId = newMessage.ChatId,
                SenderId = newMessage.SenderId,
                ReceiverId = newMessage.ReceiverId,
                CreatedAt = newMessage.CreatedAt
            };

            await _context.AddAsync(message);
            int result = await _context.SaveChangesAsync();

            return result != 0;
        }

        public async Task<ICollection<ChatMessageDTO>> GetAllChatMessagesAsync(Guid chatId)
        {
            ICollection<ChatMessageDTO> messages = await _context.ChatMessages
                .Where(ms => ms.ChatId == chatId)
                .Select(ms => new ChatMessageDTO()
                {
                    Id = ms.Id,
                    Text = ms.Text,
                    SenderId = ms.Sender.AspNetUserId,
                    ReceiverId = ms.Receiver.AspNetUserId,
                    ChatId = ms.ChatId,
                    CreatedAt = ms.CreatedAt
                })
                .OrderBy(ms => ms.CreatedAt)
                .ToListAsync();

            return messages;
        }
    }
}
