using System;
using System.ComponentModel.DataAnnotations;

namespace Dating.Logic.Models
{
    public class UserAlbum
    {
        public Guid Id { get; set; }

        public Guid UserProfileId { get; set; }

        public UserProfile UserProfile { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
