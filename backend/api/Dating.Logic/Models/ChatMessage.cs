using System;
using System.ComponentModel.DataAnnotations;

namespace Dating.Logic.Models
{
    public class ChatMessage
    {
        public Guid Id { get; set; }

        public Guid ChatId { get; set; }

        public Chat Chat { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Text { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid SenderId { get; set; }

        public ChatMember Sender { get; set; }

        public Guid ReceiverId { get; set; }

        public ChatMember Receiver { get; set; }
    }
}
