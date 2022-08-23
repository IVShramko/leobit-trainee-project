using System;
using System.Collections.Generic;

namespace Dating.Logic.DTO
{
    public class ChatFullDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ChatMemberShortDTO Sender { get; set; }

        public ICollection<ChatMemberShortDTO> Receivers { get; set; }
    }
}
