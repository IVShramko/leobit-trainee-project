using Dating.Logic.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Dating.Logic.Resourses.PhotoManager
{
    public class PhotoManager : IPhotoManager
    { 
        public void CreatePhoto(Guid userId, string albumName, PhotoCreateDTO photo)
        {
            string userPath = GetUserDirectory(userId);

            string albumPath = Path.Combine(userPath, albumName);

            string base64 = Regex
                    .Match(photo.Base64, @"data:image/(?<type>.+?),(?<data>.+)")
                    .Groups["data"].Value;

            byte[] bytes = Convert.FromBase64String(base64);

            string photoPath = Path.Combine(albumPath, photo.Name);

            File.WriteAllBytes(photoPath, bytes);
        }

        public void DeletePhoto(Guid userId, string albumName, string fileName)
        {
            string userPath = GetUserDirectory(userId);
            string albumPath = Path.Combine(userPath, albumName);
            string filePath = Path.Combine(albumPath, fileName);

            File.Delete(filePath);
        }

        public string GetPhotoBase64String(Guid userId, string albumName, string fileName)
        {
            string userPath = GetUserDirectory(userId);
            string albumPath = Path.Combine(userPath, albumName);
            string filePath = Path.Combine(albumPath, fileName);

            byte[] bytes = File.ReadAllBytes(filePath);
            string base64 = Convert.ToBase64String(bytes);

            return base64;
        }

        private string GetUserDirectory(Guid userId)
        {
            string dir = Directory.GetCurrentDirectory();

            dir = Directory.GetParent(dir).FullName;
            dir = Directory.GetParent(dir).FullName;
            dir = Directory.GetParent(dir).FullName;

            string path = Path.Combine(dir, "resourses", userId.ToString());

            return path;
        }
    }
}
