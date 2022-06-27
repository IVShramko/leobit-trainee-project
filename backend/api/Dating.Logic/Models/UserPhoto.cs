using System;
using System.ComponentModel.DataAnnotations;

namespace Dating.Logic.Models
{
    public class UserPhoto
    {
        public Guid Id { get; set; }

        public Guid AlbumId { get; set; }

        public UserAlbum Album { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

    }
}
