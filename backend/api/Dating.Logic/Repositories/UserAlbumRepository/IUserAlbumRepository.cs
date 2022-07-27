using Dating.Logic.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dating.Logic.Repositories.UserAlbumRepository
{
    public interface IUserAlbumRepository
    {
        Task<ICollection<AlbumMainDTO>> GetAllAsync(Guid userId);

        Task<AlbumFullDTO> GetAlbumByIdAsync(Guid id);

        Task<bool> Exists(Guid userId, string name);

        Task<bool> Create(Guid userId, AlbumCreateDTO album);

        Task<bool> Update(AlbumFullDTO album);

        Task<bool> Delete(AlbumFullDTO album);
    }
}
