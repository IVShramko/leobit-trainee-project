using Dating.Logic.DTO;
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
            bool isCreated;

            try
            {
                _albumManager.CreateAlbum(userId, album.Name);

                isCreated =  _albumRepository.Create(userId, album);
            }
            catch (Exception)
            {
                isCreated = false;
            }

            return isCreated;
        }

        public async Task<bool> DeleteAlbumAsync(Guid userId, Guid albumId)
        {
            bool isDeleted;

            var album = await _albumRepository.GetAlbumByIdAsync(albumId);

            try
            {
                _albumManager.DeleteAlbum(userId, album.Name);

                isDeleted = _albumRepository.Delete(album);
            }
            catch (Exception)
            {
                isDeleted = false;
            }

            return isDeleted;
        }

        public async Task<AlbumFullDTO> GetAlbumByIdAsync(Guid id)
        {
            AlbumFullDTO album = await _albumRepository.GetAlbumByIdAsync(id);

            return album;
        }

        public async Task<ICollection<AlbumMainDTO>> GetAllAlbumsAsync(Guid userId)
        {
            ICollection<AlbumMainDTO> albums = 
                await _albumRepository.GetAllAsync(userId);

            return albums;
        }

        public bool IsValidName(Guid userId, string name)
        {
            bool isExist = _albumRepository.Exists(userId, name);
            bool isValid = !isExist;

            return isValid;
        }

        public async Task<bool> UpdateAlbumAsync(Guid userId, AlbumFullDTO album)
        {
            bool isUpdated;

            string oldName = (await _albumRepository.GetAlbumByIdAsync(album.Id)).Name;

            try
            {
                _albumManager.UpdateAlbum(userId, oldName, album.Name);

                isUpdated = _albumRepository.Update(album);
            }
            catch (Exception)
            {
                isUpdated = false;
            }

            return isUpdated;
        }
    }
}
