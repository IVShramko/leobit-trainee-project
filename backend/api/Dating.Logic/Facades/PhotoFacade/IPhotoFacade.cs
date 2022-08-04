using Dating.Logic.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dating.Logic.Facades.PhotoFacade
{
    public interface IPhotoFacade
    {
        Task<ICollection<PhotoMainDTO>> GetAllPhotosAsync(Guid profileId, Guid albumId);

        Task<bool> CreatePhotoAsync(Guid profileId, PhotoCreateDTO photo);

        Task<bool> DeletePhotoAsync(Guid photoId, Guid profileId);

        Task<bool> IsValidName(Guid albumId, string name);

        Task<bool> UpdatePhotoAsync(Guid profileId, PhotoMainDTO photo);

        Task<bool> UpdatePhotoDataUrlAsync(Guid profileId, PhotoMainDTO photo);

        Task<PhotoMainDTO> GetPhotoByIdAsync(Guid profileId, Guid photoId);
    }
}
