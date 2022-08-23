
using System;

namespace Dating.Logic.DTO
{
    public class ChatMessageDTO
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public Guid ChatId { get; set; }

        public string SenderId { get; set; }

        public string ReceiverId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
