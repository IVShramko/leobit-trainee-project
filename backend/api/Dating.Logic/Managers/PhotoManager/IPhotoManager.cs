using Dating.Logic.DTO;
using System;
using System.Threading.Tasks;

namespace Dating.Logic.Managers.PhotoManager
{
    public interface IPhotoManager
    {
        Task CreatePhotoAsync(Guid profileId, string albumName, PhotoCreateDTO photo);

        Task<string> GetPhotoBase64StringAsync(Guid profileId, string albumName, string fileName);

        void DeletePhoto(Guid profileId, string albumName, string fileName);

        void Rename(Guid profileId, string albumName, string oldName, string newName);
    }
}
