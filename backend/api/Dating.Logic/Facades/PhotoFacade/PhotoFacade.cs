using Dating.Logic.DTO;
using Dating.Logic.Repositories.UserPhotoRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dating.Logic.Facades.PhotoFacade
{
    public class PhotoFacade : IPhotoFacade
    {
        private readonly IUserPhotoRepository _photoRepository;

        public PhotoFacade(IUserPhotoRepository photoRepository)
        {
            _photoRepository = photoRepository;
        }

        public async Task<ICollection<PhotoMainDTO>> GetAllPhotosAsync(Guid albumId)
        {
            return await _photoRepository.GetAllAsync(albumId);
        }
    }
}
