using System;

namespace Dating.Logic.DTO
{
    public class ProfileListDTO
    {
        public Guid Id { get; set; }
        public int Age { get; set; }

        public Guid Avatar { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }
    }
}
