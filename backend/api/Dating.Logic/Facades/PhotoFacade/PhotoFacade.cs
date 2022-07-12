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
            UserAlbum album = await _albumRepository.GetAlbumByIdAsync(photo.AlbumId);

            try
            {
                _photoManager.CreatePhoto(userId, album.Name, photo);

                UserPhoto newPhoto = new UserPhoto()
                {
                    Id = Guid.NewGuid(),
                    Album = album,
                    AlbumId = album.Id,
                    Name = photo.Name
                };

                return _photoRepository.Create(newPhoto);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeletePhoto(Guid id, Guid userId)
        {
            UserPhoto photo = _photoRepository.GetPhotoById(id);

            try
            {
                _photoManager.DeletePhoto(userId, photo.Album.Name, photo.Name);

                return _photoRepository.Delete(photo);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ICollection<PhotoMainDTO>> GetAllPhotosAsync(
            Guid userId, Guid albumId)
        {
            string albumName = (await _albumRepository.GetAlbumByIdAsync(albumId)).Name;

            var photos = await _photoRepository.GetAllAsync(albumId);

            foreach (var photo in photos)
            {
                try
                {
                    string base64 = _photoManager
                        .GetPhotoBase64String(userId, albumName, photo.Name);

                    photo.base64 = base64;
                }
                catch (Exception)
                {
                    photo.base64 = null;
                }
            }

            return photos;
        }
    }
}
