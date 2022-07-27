using Dating.Logic.DTO;
using Dating.Logic.Infrastructure;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dating.Logic.Managers.PhotoManager
{
    public class PhotoManager : IPhotoManager
    {
        private readonly IDirectoryUtility _directoryUtility;

        public PhotoManager(IDirectoryUtility directoryUtility)
        {
            _directoryUtility = directoryUtility;
        }

        public async Task CreatePhotoAsync(Guid profileId, string albumName, PhotoCreateDTO photo)
        {
            string userPath = _directoryUtility.GetUserDirectory(profileId);
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

            await File.WriteAllBytesAsync(photoPath, bytes);
        }

        public void DeletePhoto(Guid profileId, string albumName, string fileName)
        {
            string userPath = _directoryUtility.GetUserDirectory(profileId);
            string filePath = Path.Combine(userPath, albumName, fileName);

            File.Delete(filePath);
        }

        public async Task<string> GetPhotoBase64StringAsync(
            Guid profileId, string albumName, string fileName)
        {
            string userPath = _directoryUtility.GetUserDirectory(profileId);
            string filePath = Path.Combine(userPath, albumName, fileName);

            byte[] bytes = await File.ReadAllBytesAsync(filePath);
            string base64 = Convert.ToBase64String(bytes);

            return base64;
        }

        public void Rename(
            Guid profileId, string albumName, string oldName, string newName)
        {
            string userPath = _directoryUtility.GetUserDirectory(profileId);
            string oldPath = Path.Combine(userPath, albumName, oldName);
            string newPath = Path.Combine(userPath, albumName, newName);

            if (!File.Exists(oldPath))
            {
                throw new FileNotFoundException();
            }

            File.Move(oldPath, newPath);
        }
    }
}
