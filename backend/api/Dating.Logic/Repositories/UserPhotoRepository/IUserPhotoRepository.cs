using Dating.Logic.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dating.Logic.Repositories.UserPhotoRepository
{
    public interface IUserPhotoRepository
    {
        Task<ICollection<PhotoMainDTO>> GetAllAsync(Guid albumId);

        Task<bool> Create(Guid albunId, PhotoCreateDTO photo);

        Task<bool> Exists(Guid albumId, string name);

        Task<PhotoMainDTO> GetPhotoById(Guid id);

        Task<bool> Delete(Guid id);

        Task<bool> Update(PhotoMainDTO photo);
    }
}
