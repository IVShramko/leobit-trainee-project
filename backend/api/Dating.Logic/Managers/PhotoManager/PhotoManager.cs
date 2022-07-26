using Dating.Logic.DTO;
using Dating.Logic.Infrastructure;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Dating.Logic.Managers.PhotoManager
{
    public class PhotoManager : IPhotoManager
    {
        private readonly IDirectoryUtility _directoryUtility;

        public PhotoManager(IDirectoryUtility directoryUtility)
        {
            _directoryUtility = directoryUtility;
        }

        public void CreatePhoto(Guid userId, string albumName, PhotoCreateDTO photo)
        {
            string userPath = _directoryUtility.GetUserDirectory(userId);
            string albumPath = Path.Combine(userPath, albumName);

            string base64 = Regex
                    .Match(photo.Data, @"data:image/(?<type>.+?),(?<data>.+)")
                    .Groups["data"].Value;

            byte[] bytes = Convert.FromBase64String(base64);

            string photoPath = Path.Combine(albumPath, photo.Name);

            if (File.Exists(photoPath))
            {
                throw new IOException();
            }

            File.WriteAllBytes(photoPath, bytes);
        }

        public void DeletePhoto(Guid userId, string albumName, string fileName)
        {
            string userPath = _directoryUtility.GetUserDirectory(userId);
            string filePath = Path.Combine(userPath, albumName, fileName);

            File.Delete(filePath);
        }

        public string GetPhotoBase64String(Guid userId, string albumName, string fileName)
        {
            string userPath = _directoryUtility.GetUserDirectory(userId);
            string filePath = Path.Combine(userPath, albumName, fileName);

            byte[] bytes = File.ReadAllBytes(filePath);
            string base64 = Convert.ToBase64String(bytes);

            return base64;
        }
    }
}
