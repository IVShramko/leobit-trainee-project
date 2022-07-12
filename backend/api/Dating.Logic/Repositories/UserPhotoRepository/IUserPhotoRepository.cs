using Dating.Logic.DTO;
using Dating.Logic.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dating.Logic.Repositories.UserPhotoRepository
{
    public interface IUserPhotoRepository
    {
        public Task<ICollection<PhotoMainDTO>> GetAllAsync(Guid albumId);

        public bool Create(UserPhoto photo);

        public UserPhoto GetPhotoById(Guid id);

        public bool Delete(UserPhoto photo);
    }
}
