using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dating.Logic.Models
{
    public class UserProfile
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(450)]
        public string AspNetUserId { get; set; }

        public IdentityUser AspNetUser { get; set; }

        [Required]
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public bool Gender { get; set; }

        [MaxLength(256)]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Region { get; set; }

        public string Town { get; set; }
    }
}
