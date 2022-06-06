using System;

namespace Dating.Logic.DTO
{
    public class UserProfileRegisterDTO
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public RegisterData Data { get; set; }
    }

    public class RegisterData
    {
        public DateTime BirthDate { get; set; }

        public bool Gender { get; set; }
    }
}
