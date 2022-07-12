using System;

namespace Dating.Logic.Resourses.AlbumManager
{
    public interface IAlbumManager
    {
        public void CreateAlbum(Guid userId, string name);

        public void UpdateAlbum(Guid userId, string oldName, string newName);

        public void DeleteAlbum(Guid userId, string name);

    }
}
