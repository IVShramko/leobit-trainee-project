using Dating.Logic.Infrastructure;
using Dating.Logic.Managers.PhotoManager;
using System;
using System.IO;
using System.Linq;

namespace Dating.Logic.Managers.AlbumManager
{
    public class AlbumManager : IAlbumManager
    {
        private readonly IPhotoManager _photoManager;
        private readonly IDirectoryUtility _directoryUtility;

        public AlbumManager(IPhotoManager photoManager,
            IDirectoryUtility directoryUtility)
        {
            _photoManager = photoManager;
            _directoryUtility = directoryUtility;
        }

        public void CreateAlbum(Guid profileId, string name)
        {
            string userPath = _directoryUtility.GetUserDirectory(profileId);

            if (!Directory.Exists(userPath))
            {
                Directory.CreateDirectory(userPath);
            }

            string albumPath = Path.Combine(userPath, name);

            Directory.CreateDirectory(albumPath);
        }

        public void DeleteAlbum(Guid profileId, string name)
        {
            string userPath = _directoryUtility.GetUserDirectory(profileId);
            string albumPath = Path.Combine(userPath, name);

            string[] filePaths = Directory.GetFiles(albumPath);

            foreach (var filePath in filePaths)
            {
                string file = filePath.Split("\\").Last();
                _photoManager.DeletePhoto(profileId, name, file);
            }

            Directory.Delete(albumPath);
        }

        public void UpdateAlbum(Guid profileId, string oldName, string newName)
        {
            string userPath = _directoryUtility.GetUserDirectory(profileId);

            string oldPath = Path.Combine(userPath, oldName);
            string newPath = Path.Combine(userPath, newName);

            Directory.Move(oldPath, newPath);
        }
    }
}
