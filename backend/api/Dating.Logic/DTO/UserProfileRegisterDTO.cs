using System;

namespace Dating.Logic.DTO
{
    public class UserProfileRegisterDTO
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public DateTime BirthDate { get; set; }

        public bool Gender { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
