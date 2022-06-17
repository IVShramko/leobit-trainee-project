using System;

namespace Dating.Logic.Repositories.ImageRepository
{
    public interface IImageRepository
    {
        public Guid AddPhoto(string DataUrlString, Guid userId);

        public void DeletePhotoById(Guid id);

        public string GetPhotoById(Guid id, Guid userId);

        public string[] GetAllUserPhotos(Guid userId);
    }
}
