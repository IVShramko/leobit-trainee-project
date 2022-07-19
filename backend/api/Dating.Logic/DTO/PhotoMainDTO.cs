using System;

namespace Dating.Logic.DTO
{
    public class PhotoMainDTO
    {
        public Guid Id { get; set; }

        public Guid AlbumId { get; set; }

        public string Name { get; set; }

        public string Data { get; set; }
    }
}
