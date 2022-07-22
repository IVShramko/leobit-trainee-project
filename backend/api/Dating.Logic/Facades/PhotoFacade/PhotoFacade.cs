using Dating.Logic.DTO;
using Dating.Logic.Models;
using Dating.Logic.Repositories.UserAlbumRepository;
using Dating.Logic.Repositories.UserPhotoRepository;
using Dating.Logic.Resourses.PhotoManager;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<bool> CreatePhotoAsync(Guid userId, PhotoCreateDTO photo)
        {
            bool isCreated;

            AlbumFullDTO album = await _albumRepository.GetAlbumByIdAsync(photo.AlbumId);

            try
            {
                _photoManager.CreatePhoto(userId, album.Name, photo);

                isCreated = _photoRepository.Create(album.Id, photo);
            }
            catch (Exception)
            {
                isCreated = false;
            }

            return isCreated;
        }

        public bool DeletePhoto(Guid id, Guid userId)
        {
            bool isDeleted;

            UserPhoto photo = _photoRepository.GetPhotoById(id);

            try
            {
                _photoManager.DeletePhoto(userId, photo.Album.Name, photo.Name);

                isDeleted = _photoRepository.Delete(id);
            }
            catch (Exception)
            {
                isDeleted = false;
            }

            return isDeleted;
        }

        public async Task<ICollection<PhotoMainDTO>> GetAllPhotosAsync(
            Guid userId, Guid albumId)
        {
            AlbumFullDTO album = await _albumRepository.GetAlbumByIdAsync(albumId);

            var photos = await _photoRepository.GetAllAsync(albumId);

            foreach (var photo in photos)
            {
                try
                {
                    string base64 = _photoManager
                        .GetPhotoBase64String(userId, album.Name, photo.Name);

                    photo.Data = base64;
                }
                catch (Exception)
                {
                    photo.Data = null;
                }
            }

            return photos;
        }
    }
}
