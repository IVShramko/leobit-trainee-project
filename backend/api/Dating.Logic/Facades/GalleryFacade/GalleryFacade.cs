using Dating.Logic.DTO;
using Dating.Logic.Models;
using Dating.Logic.Repositories.UserAlbumRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dating.Logic.Facades.GalleryFacade
{
    public class GalleryFacade : IGalleryFacade
    {
        private readonly IUserAlbumRepository _albumRepository;

        public GalleryFacade(IUserAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        public async Task<ICollection<UserAlbumDTO>> GetAllAlbumsAsync(Guid userId)
        {
            return await _albumRepository.GetAllAlbumsAsync(userId);
        }
    }
}
