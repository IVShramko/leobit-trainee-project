using System;

namespace Dating.Logic.DTO
{
    public class UserProfileFullDTO
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public bool Gender { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Region { get; set; }

        public string Town { get; set; }

        public string Photo { get; set; }
    }
}

