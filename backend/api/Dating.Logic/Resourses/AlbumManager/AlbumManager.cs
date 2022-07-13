using Dating.Logic.Resourses.PhotoManager;
using System;
using System.IO;
using System.Linq;

namespace Dating.Logic.Resourses.AlbumManager
{
    public class AlbumManager : IAlbumManager
    {
        private readonly IPhotoManager _photoManager;

        public AlbumManager(IPhotoManager photoManager)
        {
            _photoManager = photoManager;
        }

        public void CreateAlbum(Guid userId, string name)
        {
            string userPath = GetUserDirectory(userId);

            if (!Directory.Exists(userPath))
            {
                Directory.CreateDirectory(userPath);
            }

            string albumPath = Path.Combine(userPath, name);

            Directory.CreateDirectory(albumPath);
        }

        public void DeleteAlbum(Guid userId, string name)
        {
            string userPath = GetUserDirectory(userId);
            string albumPath = Path.Combine(userPath, name);

            string[] filePaths = Directory.GetFiles(albumPath);

            foreach (var filePath in filePaths)
            {
                string file = filePath.Split("\\").Last();
                _photoManager.DeletePhoto(userId, name, file);
            }

            Directory.Delete(albumPath);
        }

        public void UpdateAlbum(Guid userId, string oldName, string newName)
        {
            string userPath = GetUserDirectory(userId);

            string oldPath = Path.Combine(userPath, oldName);
            string newPath = Path.Combine(userPath, newName);

            Directory.Move(oldPath, newPath);
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
