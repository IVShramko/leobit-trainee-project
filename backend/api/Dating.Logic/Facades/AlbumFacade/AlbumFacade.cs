using Dating.Logic.DTO;
using Dating.Logic.Models;
using Dating.Logic.Repositories.UserAlbumRepository;
using Dating.Logic.Resourses.AlbumManager;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dating.Logic.Facades.AlbumFacade
{
    public class AlbumFacade : IAlbumFacade
    {
        private readonly IUserAlbumRepository _albumRepository;
        private readonly IAlbumManager _albumManager;

        public AlbumFacade(IUserAlbumRepository albumRepository,
            IAlbumManager albumManager)
        {
            _albumRepository = albumRepository;
            _albumManager = albumManager;
        }

        public bool CreateAlbum(Guid userId, AlbumCreateDTO album)
        {
            try
            {
                _albumManager.CreateAlbum(userId, album.Name);

                UserAlbum newAlbum = new UserAlbum()
                {
                    UserProfileId = userId,
                    Name = album.Name,
                    Description = album.Description
                };

                return _albumRepository.Create(newAlbum);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAlbumAsync(Guid userId, Guid albumId)
        {
            var album = await _albumRepository.GetAlbumByIdAsync(albumId);

            try
            {
                _albumManager.DeleteAlbum(userId, album.Name);

                UserAlbum delAlbum = new UserAlbum()
                {
                    Id = album.Id,
                    Name = album.Name,
                    Description = album.Description
                };

                return _albumRepository.Delete(delAlbum);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<AlbumFullDTO> GetAlbumByIdAsync(Guid id)
        {
            UserAlbum album = await _albumRepository.GetAlbumByIdAsync(id);
            return new AlbumFullDTO
            {
                Id = album.Id,
                Name = album.Name,
                Description = album.Description
            };
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

        public async Task<bool> UpdateAlbumAsync(Guid userId, AlbumFullDTO album)
        {
            string oldName = (await _albumRepository.GetAlbumByIdAsync(album.Id)).Name;

            try
            {
                _albumManager.UpdateAlbum(userId, oldName, album.Name);

                UserAlbum newAlbum = new UserAlbum()
                {
                    Id = album.Id,
                    UserProfileId = userId,
                    Name = album.Name,
                    Description = album.Description
                };

                return _albumRepository.Update(newAlbum);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
