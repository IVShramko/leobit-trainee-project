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

        bool Exists(Guid userId, string name);

        bool Create(Guid userId, AlbumCreateDTO album);

        bool Update(AlbumFullDTO album);

        bool Delete(AlbumFullDTO album);
    }
}
