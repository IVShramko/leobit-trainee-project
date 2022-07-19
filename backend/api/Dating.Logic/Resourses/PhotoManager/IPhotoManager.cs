using Dating.Logic.DTO;
using System;

namespace Dating.Logic.Resourses.PhotoManager
{
    public interface IPhotoManager
    {
        void CreatePhoto(Guid userId, string albumName, PhotoCreateDTO photo);

        string GetPhotoBase64String(Guid userId, string albumName, string fileName);

        void DeletePhoto(Guid userId, string albumName, string fileName);
    }
}
