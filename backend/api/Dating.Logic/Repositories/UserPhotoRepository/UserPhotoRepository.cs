using Dating.Logic.DB;
using Dating.Logic.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating.Logic.Repositories.UserPhotoRepository
{
    public class UserPhotoRepository : IUserPhotoRepository
    {
        private readonly AppDbContext _context;

        public UserPhotoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<PhotoMainDTO>> GetAllAsync(Guid albumId)
        {
            return await _context.UserPhotos
                .Where(p => p.AlbumId == albumId)
                .Select(p => new PhotoMainDTO
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToListAsync();
        }
    }
}
