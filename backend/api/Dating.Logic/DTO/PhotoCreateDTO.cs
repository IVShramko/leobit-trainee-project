﻿using System;

namespace Dating.Logic.DTO
{
    public class PhotoCreateDTO
    {
        public Guid AlbumId { get; set; }

        public string Name { get; set; }

        public string Base64 { get; set; }
    }
}
