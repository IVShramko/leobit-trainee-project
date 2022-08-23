using System;

namespace Dating.Logic.Models
{
    public class ChatMemberChat
    {
        public Guid ChatId { get; set; }

        public Chat Chat { get; set; }

        public Guid ChatMemberId { get; set; }

        public ChatMember ChatMember { get; set; }
    }
}
