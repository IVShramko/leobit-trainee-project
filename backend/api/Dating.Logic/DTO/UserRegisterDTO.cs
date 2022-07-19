using System;

namespace Dating.Logic.DTO
{
    public class UserRegisterDTO
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public ProfileRegisterDTO Profile { get; set; }
    }
}
