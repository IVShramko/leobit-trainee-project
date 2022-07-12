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

        public async Task<UserAlbum> GetAlbumByIdAsync(Guid id)
        {
            return await _context.UserAlbums
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
        }

        public bool Exists(Guid userId, string name)
        {
            return _context.UserAlbums
                .Where(a => a.UserProfileId == userId && a.Name == name)
                .Any();
        }

        public async Task<ICollection<AlbumMainDTO>> GetAllAsync(Guid userId)
        {
            return await _context.UserAlbums
                .Where(a => a.UserProfileId == userId)
                .OrderBy(a => a.Name)
                .Select(a => new AlbumMainDTO
                {
                    Id = a.Id,
                    Name = a.Name
                })
                .ToListAsync();
        }

        public bool Create(UserAlbum album)
        {
            _context.UserAlbums.Add(album);
            int result = _context.SaveChanges();

            if (result != 0)
            {
                return true;
            }

            return false;
        }

        public bool Update(UserAlbum album)
        {
            _context.UserAlbums.Update(album);
            int result = _context.SaveChanges();

            if (result != 0)
            {
                return true;
            }

            return false;
        }

        public bool Delete(UserAlbum album)
        {
            _context.Remove(album);
            int result = _context.SaveChanges();

            if (result != 0)
            {
                return true;
            }

            return false;
        }
    }
}
