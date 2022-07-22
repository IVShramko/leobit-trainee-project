using Dating.Logic.DB;
using Dating.Logic.DTO;
using Dating.Logic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating.Logic.Repositories.UserAlbumRepository
{
    public class UserAlbumRepository : IUserAlbumRepository
    {
        private readonly AppDbContext _context;

        public UserAlbumRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AlbumFullDTO> GetAlbumByIdAsync(Guid id)
        {
            AlbumFullDTO album = await _context.UserAlbums
                .Where(a => a.Id == id)
                .Select(a => new AlbumFullDTO()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description
                })
                .FirstOrDefaultAsync();

            return album;
        }

        public bool Exists(Guid userId, string name)
        {
            bool result = _context.UserAlbums
                .Where(a => a.UserProfileId == userId && a.Name == name)
                .Any();

            return result;
        }

        public async Task<ICollection<AlbumMainDTO>> GetAllAsync(Guid userId)
        {
            var albums =  await _context.UserAlbums
                .Where(a => a.UserProfileId == userId)
                .OrderBy(a => a.Name)
                .Select(a => new AlbumMainDTO
                {
                    Id = a.Id,
                    Name = a.Name
                })
                .ToListAsync();

            return albums;
        }

        public bool Create(Guid userId, AlbumCreateDTO album)
        {
            UserAlbum newAlbum = new()
            {
                Id = Guid.NewGuid(),
                UserProfileId = userId,
                Name = album.Name,
                Description = album.Description
            };

            _context.UserAlbums.Add(newAlbum);
            int result = _context.SaveChanges();

            return result != 0;
        }

        public bool Update(AlbumFullDTO album)
        {
            var entity = _context.UserAlbums
                .Where(a => a.Id == album.Id)
                .SingleOrDefault();

            entity.Name = album.Name;
            entity.Description = album.Description;

            _context.Update(entity);
            int result = _context.SaveChanges();

            return result != 0;
        }

        public bool Delete(AlbumFullDTO album)
        {
            var entity = _context.UserAlbums
                .Where(a => a.Id == album.Id)
                .SingleOrDefault();

            _context.Remove(entity);
            int result = _context.SaveChanges();

            return result != 0;
        }
    }
}
