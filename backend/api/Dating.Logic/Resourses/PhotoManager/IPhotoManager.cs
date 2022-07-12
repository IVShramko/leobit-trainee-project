using Dating.Logic.DTO;
using System;
using System.Collections.Generic;

namespace Dating.Logic.Resourses.PhotoManager
{
    public interface IPhotoManager
    {
        public void CreatePhoto(Guid userId, string albumName, PhotoCreateDTO photo);

        public string GetPhotoBase64String(Guid userId, string albumName, string fileName);

        public void DeletePhoto(Guid userId, string albumName, string fileName);
    }
}
