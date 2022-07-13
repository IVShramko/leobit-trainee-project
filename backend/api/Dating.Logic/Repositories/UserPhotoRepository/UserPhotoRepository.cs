using Dating.Logic.DB;
using Dating.Logic.DTO;
using Dating.Logic.Models;
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

        public bool Create(UserPhoto photo)
        {
            _context.UserPhotos.Add(photo);
            int result = _context.SaveChanges();

            if (result != 0)
            {
                return true;
            }

            return false;
        }

        public bool Delete(UserPhoto photo)
        {
            _context.UserPhotos.Remove(photo);
            int result = _context.SaveChanges();

            if (result != 0)
            {
                return true;
            }

            return false;
        }

        public async Task<ICollection<PhotoMainDTO>> GetAllAsync(Guid albumId)
        {
            return await _context.UserPhotos
                .Where(p => p.AlbumId == albumId)
                .Select(p => new PhotoMainDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    AlbumId = p.AlbumId
                })
                .ToListAsync();
        }

        public UserPhoto GetPhotoById(Guid id)
        {
            return _context.UserPhotos
                .Where(p => p.Id == id)
                .Include(p => p.Album)
                .FirstOrDefault();
        }
    }
}
