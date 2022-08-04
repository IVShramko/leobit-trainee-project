using Dating.Logic.DTO;
using Dating.Logic.Models;
using Dating.Logic.Repositories.UserAlbumRepository;
using Dating.Logic.Repositories.UserPhotoRepository;
using Dating.Logic.Managers.PhotoManager;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

namespace Dating.Logic.Facades.PhotoFacade
{
    public class PhotoFacade : IPhotoFacade
    {
        private readonly IUserPhotoRepository _photoRepository;
        private readonly IPhotoManager _photoManager;
        private readonly IUserAlbumRepository _albumRepository;

        public PhotoFacade(IUserPhotoRepository photoRepository,
            IPhotoManager photoManager,
            IUserAlbumRepository albumRepository)
        {
            _photoRepository = photoRepository;
            _photoManager = photoManager;
            _albumRepository = albumRepository;
        }

        public async Task<bool> CreatePhotoAsync(Guid profileId, PhotoCreateDTO photo)
        {
            bool isCreated;

            AlbumFullDTO album = await _albumRepository.GetAlbumByIdAsync(photo.AlbumId);

            try
            {
                await _photoManager.CreatePhotoAsync(profileId, album.Name, photo);

                isCreated = await _photoRepository.Create(album.Id, photo);
            }
            catch (Exception)
            {
                isCreated = false;
            }

            return isCreated;
        }

        public async Task<bool> DeletePhotoAsync(Guid photoId, Guid profileId)
        {
            bool isDeleted;

            PhotoMainDTO photo = await _photoRepository.GetPhotoById(photoId);

            AlbumFullDTO album = 
                await _albumRepository.GetAlbumByIdAsync(photo.AlbumId);

            try
            {
                _photoManager.DeletePhoto(profileId, album.Name, photo.Name);

                isDeleted = await _photoRepository.Delete(photoId);
            }
            catch (Exception)
            {
                isDeleted = false;
            }

            return isDeleted;
        }

        public async Task<ICollection<PhotoMainDTO>> GetAllPhotosAsync(
            Guid profileId, Guid albumId)
        {
            AlbumFullDTO album = await _albumRepository.GetAlbumByIdAsync(albumId);

            var photos = await _photoRepository.GetAllAsync(albumId);

            foreach (var photo in photos)
            {
                try
                {
                    string base64 = await _photoManager
                        .GetPhotoBase64StringAsync(profileId, album.Name, photo.Name);

                    photo.Data = base64;
                }
                catch (Exception)
                {
                    photo.Data = null;
                }
            }

            return photos;
        }

        public async Task<PhotoMainDTO> GetPhotoByIdAsync(Guid profileId, Guid photoId)
        {
            PhotoMainDTO photo = await _photoRepository.GetPhotoById(photoId);

            if(photo != null)
            {
                AlbumFullDTO album =
                    await _albumRepository.GetAlbumByIdAsync(photo.AlbumId);

                string base64 =
                    await _photoManager.GetPhotoBase64StringAsync(
                        profileId, album.Name, photo.Name);

                photo.Data = base64;
            };

            return photo;
        }

        public async Task<bool> IsValidName(Guid albumId, string name)
        {
            bool isExist = await _photoRepository.Exists(albumId, name);

            return !isExist;
        }

        public async Task<bool> UpdatePhotoAsync(Guid profileId, PhotoMainDTO newPhoto)
        {
            bool isUpadted;

            try
            {
                PhotoMainDTO oldPhoto = await _photoRepository.GetPhotoById(newPhoto.Id);

                AlbumFullDTO album = 
                    await _albumRepository.GetAlbumByIdAsync(newPhoto.AlbumId);

                _photoManager.Rename(
                    profileId, album.Name, oldPhoto.Name, newPhoto.Name);

                isUpadted = await _photoRepository.Update(newPhoto);
            }
            catch (Exception)
            {
                isUpadted = false;
            }

            return isUpadted;
        }

        public async Task<bool> UpdatePhotoDataUrlAsync(Guid profileId, PhotoMainDTO photo)
        {
            bool isUpdated;

            AlbumFullDTO album = await _albumRepository.GetAlbumByIdAsync(photo.AlbumId);

            try
            {
                await _photoManager.ChangeDataUrlAsync(profileId, album.Name, photo);

                isUpdated = true;
            }
            catch (Exception)
            {
                isUpdated = false;
            }

            return isUpdated;
        }
    }
}
