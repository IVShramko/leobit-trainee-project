using Dating.Logic.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dating.Logic.Repositories.UserPhotoRepository
{
    public interface IUserPhotoRepository
    {
        public Task<ICollection<UserPhotoDTO>> GetAllPhotosAsync(Guid albumId);
    }
}
