using Dating.Logic.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dating.Logic.Facades.AlbumFacade
{
    public interface IAlbumFacade
    {
        public Task<ICollection<AlbumMainDTO>> GetAllAlbumsAsync(Guid userId);

        public Task<AlbumFullDTO> GetAlbumByIdAsync(Guid id);

        public bool IsValidName(Guid userId, string name);

        public bool CreateAlbum(Guid userId, AlbumCreateDTO album);

        public Task<bool> UpdateAlbumAsync(Guid userId, AlbumFullDTO album);

        public Task<bool> DeleteAlbumAsync(Guid userId, Guid albumId);

    }
}
