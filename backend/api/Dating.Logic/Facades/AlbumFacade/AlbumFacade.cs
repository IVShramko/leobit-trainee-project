using Dating.Logic.DTO;
using Dating.Logic.Models;
using Dating.Logic.Repositories.UserAlbumRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dating.Logic.Facades.AlbumFacade
{
    public class AlbumFacade : IAlbumFacade
    {
        private readonly IUserAlbumRepository _albumRepository;

        public AlbumFacade(IUserAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        public bool CreateAlbum(Guid userId, AlbumCreateDTO album)
        {
            UserAlbum newAlbum = new UserAlbum()
            {
                UserProfileId = userId,
                Name = album.Name,
                Description = album.Description
            };

            bool result = _albumRepository.Create(newAlbum);

            return result;
        }

        public async Task<AlbumFullDTO> GetAlbumByIdAsync(Guid id)
        {
            return await _albumRepository.GetAlbumByIdAsync(id);
        }

        public async Task<ICollection<AlbumMainDTO>> GetAllAlbumsAsync(Guid userId)
        {
            return await _albumRepository.GetAllAsync(userId);
        }

        public bool IsValidName(Guid userId, string name)
        {
            bool result = _albumRepository.Exists(userId, name);

            return !result;
        }

        public bool UpdateAlbum(Guid userId, AlbumFullDTO album)
        {
            UserAlbum newAlbum = new UserAlbum()
            {
                Id = album.Id,
                UserProfileId = userId,
                Name = album.Name,
                Description = album.Description
            };

            bool result = _albumRepository.Update(newAlbum);

            return result;
        }
    }
}
