using Dating.Logic.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dating.Logic.Facades.AlbumFacade
{
    public interface IAlbumFacade
    {
        Task<ICollection<AlbumMainDTO>> GetAllAlbumsAsync(Guid profileId);

        Task<AlbumFullDTO> GetAlbumByIdAsync(Guid id);

        Task<bool> IsValidName(Guid profileId, string name);

        Task<bool> CreateAlbum(Guid profileId, AlbumCreateDTO album);

        Task<bool> UpdateAlbumAsync(Guid profileId, AlbumFullDTO album);

        Task<bool> DeleteAlbumAsync(Guid profileId, Guid albumId);
    }
}
