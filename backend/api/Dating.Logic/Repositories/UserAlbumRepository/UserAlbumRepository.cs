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
        public async Task<ICollection<UserAlbumDTO>> GetAllAlbumsAsync(Guid userId)
        {
            return await _context.UserAlbums
                .Where(a => a.UserProfileId == userId)
                .Select(a => new UserAlbumDTO
                {
                    Id = a.Id,
                    Name = a.Name
                })
                .ToListAsync();
        }
    }
}
