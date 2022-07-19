using Dating.Logic.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dating.Logic.Facades.AlbumFacade
{
    public interface IAlbumFacade
    {
        Task<ICollection<AlbumMainDTO>> GetAllAlbumsAsync(Guid userId);

        Task<AlbumFullDTO> GetAlbumByIdAsync(Guid id);

        bool IsValidName(Guid userId, string name);

        bool CreateAlbum(Guid userId, AlbumCreateDTO album);

        Task<bool> UpdateAlbumAsync(Guid userId, AlbumFullDTO album);

        Task<bool> DeleteAlbumAsync(Guid userId, Guid albumId);
    }
}
