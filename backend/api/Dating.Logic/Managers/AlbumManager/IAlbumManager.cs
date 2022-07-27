using System;

namespace Dating.Logic.Managers.AlbumManager
{
    public interface IAlbumManager
    {
        void CreateAlbum(Guid profileId, string name);

        void UpdateAlbum(Guid profileId, string oldName, string newName);

        void DeleteAlbum(Guid profileId, string name);
    }
}
