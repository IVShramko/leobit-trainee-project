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

        public async Task<bool> Create(Guid albumId, PhotoCreateDTO photo)
        {
            UserPhoto newPhoto = new UserPhoto()
            {
                Id = Guid.NewGuid(),
                AlbumId = albumId,
                Name = photo.Name
            };

            await _context.UserPhotos.AddAsync(newPhoto);
            int result = await _context.SaveChangesAsync();

            return result != 0;
        }

        public async Task<bool> Delete(Guid id)
        {
            var entity = await _context.UserPhotos
                .Where(a => a.Id == id)
                .SingleOrDefaultAsync();

            _context.Remove(entity);
            int result = await _context.SaveChangesAsync();

            return result != 0;
        }

        public async Task<bool> Exists(Guid albumId, string name) 
        {
            bool isExist = await _context.UserPhotos
                .Where(p => p.AlbumId == albumId && p.Name == name)
                .AnyAsync();

            return isExist;
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

        public async Task<PhotoMainDTO> GetPhotoById(Guid id)
        {
            PhotoMainDTO photo = await _context.UserPhotos
                .Where(p => p.Id == id)
                .Select(p => new PhotoMainDTO
                { 
                    Id = p.Id,
                    AlbumId = p.AlbumId,
                    Name = p.Name
                })
                .FirstOrDefaultAsync();

            return photo;
        }

        public async Task<bool> Update(PhotoMainDTO photo)
        {
            var entity = await _context.UserPhotos
                .Where(p => p.Id == photo.Id)
                .SingleOrDefaultAsync();

            entity.Name = photo.Name;

            _context.UserPhotos.Update(entity);
            int result = await _context.SaveChangesAsync();

            return result != 0;
        }
    }
}
