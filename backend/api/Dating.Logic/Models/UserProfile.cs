using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Dating.Logic.Models
{
    public class UserProfile
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(450)]
        public string AspNetUserId { get; set; }

        public IdentityUser AspNetUser { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public bool Gender { get; set; }

        public string PhoneNumber { get; set; }

        public string Region { get; set; }

        public string Town { get; set; }
    }
}
