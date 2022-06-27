using Dating.Logic.DTO;
using Dating.Logic.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dating.Logic.Repositories.UserAlbumRepository
{
    public interface IUserAlbumRepository
    {
        public Task<ICollection<UserAlbumDTO>> GetAllAlbumsAsync(Guid userId);
    }
}
