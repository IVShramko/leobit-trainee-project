using Dating.Logic.DTO;
using Dating.Logic.Models;
using Dating.Logic.Repositories.UserAlbumRepository;
using Dating.Logic.Repositories.UserPhotoRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dating.Logic.Facades.GalleryFacade
{
    public class GalleryFacade : IGalleryFacade
    {
        private readonly IUserAlbumRepository _albumRepository;
        private readonly IUserPhotoRepository _photoRepository;

        public GalleryFacade(IUserAlbumRepository albumRepository,
            IUserPhotoRepository photoRepository)
        {
            _albumRepository = albumRepository;
            _photoRepository = photoRepository;
        }

        public async Task<ICollection<UserAlbumDTO>> GetAllAlbumsAsync(Guid userId)
        {
            return await _albumRepository.GetAllAlbumsAsync(userId);
        }

        public async Task<ICollection<UserPhotoDTO>> GetAllPhotosAsync(Guid albumId)
        {
            return await _photoRepository.GetAllPhotosAsync(albumId);
        }
    }
}
