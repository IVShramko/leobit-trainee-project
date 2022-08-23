using System;

namespace Dating.Logic.DTO
{
    public class ChatMessageCreateDTO
    {
        public Guid ChatId { get; set; }

        public Guid SenderId { get; set; }

        public Guid ReceiverId { get; set; }

        public string Text { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
