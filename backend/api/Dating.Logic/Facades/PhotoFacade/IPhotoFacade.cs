using Dating.Logic.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dating.Logic.Facades.PhotoFacade
{
    public interface IPhotoFacade
    {
        public Task<ICollection<PhotoMainDTO>> GetAllPhotosAsync(Guid albumId);

    }
}
