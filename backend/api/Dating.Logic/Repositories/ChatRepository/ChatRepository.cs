using Dating.Logic.DB;
using Dating.Logic.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating.Logic.Repositories.ChatRepository
{
    public class ChatRepository : IChatRepository
    {
        private readonly AppDbContext _context;

        public ChatRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<ChatShortDTO>> GetAllUserChatsAsync(string aspNetUserId)
        {
            ICollection<ChatShortDTO> chats = await _context.ChatMemberChats
                .Where(m => m.ChatMember.AspNetUserId == aspNetUserId)
                .Include(cmc => cmc.ChatMember)
                .Include(cmc => cmc.Chat)
                .Select(m => new ChatShortDTO()
                {
                    Id = m.ChatId,
                    Name = m.Chat.Name
                })
                .ToListAsync();

            return chats;
        }

        public async Task<ChatFullDTO> GetChatByIdAsync(Guid chatId, string senderId)
        {
            var chat = await _context.Chats
                .Where(c => c.Id == chatId)
                .Include(c => c.ChatMemberChats)
                .ThenInclude(cmc => cmc.ChatMember)
                .Select(c => new ChatFullDTO()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Sender = c.ChatMemberChats
                        .Where(cmc => cmc.ChatMember.AspNetUserId == senderId)
                        .Select(cmc => new ChatMemberShortDTO()
                        {
                            Id = cmc.ChatMemberId,
                            AspNetUserId = cmc.ChatMember.AspNetUserId
                        })
                        .SingleOrDefault(),
                    Receivers = c.ChatMemberChats
                        .Where(cmc => cmc.ChatMember.AspNetUserId != senderId)
                        .Select(cmc => new ChatMemberShortDTO()
                        {
                            Id = cmc.ChatMemberId,
                            AspNetUserId = cmc.ChatMember.AspNetUserId
                        })
                        .ToList()
                })
                .SingleOrDefaultAsync();

            return chat;
        }
    }
}
