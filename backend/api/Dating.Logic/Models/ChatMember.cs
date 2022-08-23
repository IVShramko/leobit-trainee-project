using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dating.Logic.Models
{
    public class ChatMember
    {
        public Guid Id { get; set; }

        public Guid ChatId { get; set; }

        public ICollection<ChatMemberChat> ChatMemberChats { get; set; }

        [Required]
        [MaxLength(450)]
        public string AspNetUserId { get; set; }
    }
}
