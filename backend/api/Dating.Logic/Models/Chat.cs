using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dating.Logic.Models
{
    public class Chat
    {
        public Guid Id { get; set; }

        public ICollection<ChatMessage> Messages { get; set; }

        public ICollection<ChatMemberChat> ChatMemberChats { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
