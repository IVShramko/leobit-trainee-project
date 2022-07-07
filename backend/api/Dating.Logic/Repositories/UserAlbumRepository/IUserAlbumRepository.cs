using Dating.Logic.DTO;
using Dating.Logic.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dating.Logic.Repositories.UserAlbumRepository
{
    public interface IUserAlbumRepository
    {
        public Task<ICollection<AlbumMainDTO>> GetAllAsync(Guid userId);

        public Task<AlbumFullDTO> GetAlbumByIdAsync(Guid id);

        public bool Exists(Guid userId, string name);

        public bool Create(UserAlbum album);

        public bool Update(UserAlbum album);
    }
}
