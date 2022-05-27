using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dating.Logic.DTO
{
    public class UserProfileMainData
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public DateTime BirthDate { get; set; }

        public bool Gender { get; set; }

        public string Email { get; set; }
    }
}
