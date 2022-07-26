using Dating.Logic.DTO;
using Dating.Logic.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dating.Logic.Repositories.UserPhotoRepository
{
    public interface IUserPhotoRepository
    {
        Task<ICollection<PhotoMainDTO>> GetAllAsync(Guid albumId);

        bool Create(Guid albunId, PhotoCreateDTO photo);

        bool Exists(Guid albumId, string name);

        UserPhoto GetPhotoById(Guid id);

        bool Delete(Guid id);
    }
}
