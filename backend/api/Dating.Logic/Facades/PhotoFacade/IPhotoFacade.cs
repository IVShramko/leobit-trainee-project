using Dating.Logic.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dating.Logic.Facades.PhotoFacade
{
    public interface IPhotoFacade
    {
        Task<ICollection<PhotoMainDTO>> GetAllPhotosAsync(Guid userId, Guid albumId);

        Task<bool> CreatePhotoAsync(Guid userId, PhotoCreateDTO photo);

        bool DeletePhoto(Guid id, Guid userId);

        bool IsValidName(Guid albumId, string name);
    }
}
