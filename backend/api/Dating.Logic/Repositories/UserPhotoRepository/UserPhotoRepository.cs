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

        public bool Create(Guid albumId, PhotoCreateDTO photo)
        {
            UserPhoto newPhoto = new UserPhoto()
            {
                Id = Guid.NewGuid(),
                AlbumId = albumId,
                Name = photo.Name
            };

            _context.UserPhotos.Add(newPhoto);
            int result = _context.SaveChanges();

            return result != 0;
        }

        public bool Delete(Guid id)
        {
            var entity = _context.UserPhotos
                .Where(a => a.Id == id)
                .SingleOrDefault();

            _context.Remove(entity);
            int result = _context.SaveChanges();

            return result != 0;
        }

        public async Task<ICollection<PhotoMainDTO>> GetAllAsync(Guid albumId)
        {
            var photos = await _context.UserPhotos
                .Where(p => p.AlbumId == albumId)
                .Select(p => new PhotoMainDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    AlbumId = p.AlbumId
                })
                .ToListAsync();

            return photos;
        }

        public UserPhoto GetPhotoById(Guid id)
        {
            UserPhoto photo = _context.UserPhotos
                .Include(p => p.Album)
                .Where(p => p.Id == id)
                .FirstOrDefault();

            return photo;
        }
    }
}
