using Dating.Logic.DTO;
using Dating.Logic.Repositories.UserAlbumRepository;
using Dating.Logic.Managers.AlbumManager;
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

        public async Task<bool> CreateAlbum(Guid profileId, AlbumCreateDTO album)
        {
            bool isCreated;

            try
            {
                _albumManager.CreateAlbum(profileId, album.Name);

                isCreated = await _albumRepository.Create(profileId, album);
            }
            catch (Exception)
            {
                isCreated = false;
            }

            return isCreated;
        }

        public async Task<bool> DeleteAlbumAsync(Guid profileId, Guid albumId)
        {
            bool isDeleted;

            var album = await _albumRepository.GetAlbumByIdAsync(albumId);

            try
            {
                _albumManager.DeleteAlbum(profileId, album.Name);

                isDeleted = await _albumRepository.Delete(album);
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

        public async Task<ICollection<AlbumMainDTO>> GetAllAlbumsAsync(Guid profileId)
        {
            ICollection<AlbumMainDTO> albums = 
                await _albumRepository.GetAllAsync(profileId);

            return albums;
        }

        public async Task<bool> IsValidName(Guid profileId, string name)
        {
            bool isExist = await _albumRepository.Exists(profileId, name);
            bool isValid = !isExist;

            return isValid;
        }

        public async Task<bool> UpdateAlbumAsync(Guid profileId, AlbumFullDTO album)
        {
            bool isUpdated;

            string oldName = (await _albumRepository.GetAlbumByIdAsync(album.Id)).Name;

            try
            {
                _albumManager.UpdateAlbum(profileId, oldName, album.Name);

                isUpdated = await _albumRepository.Update(album);
            }
            catch (Exception)
            {
                isUpdated = false;
            }

            return isUpdated;
        }
    }
}
