using AutoMapper;
using Dating.Logic.DB;
using Dating.Logic.DTO;
using Dating.Logic.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dating.Logic.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly AppDbContext _context;

        public UserProfileRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserProfileFullDTO> GetFullUserDataAsync(string aspNetUserId)
        {
            UserProfileFullDTO userData = await _context.UserProfiles
                .Where(p => p.AspNetUserId == aspNetUserId)
                .Select(p => new UserProfileFullDTO
                {
                    Id = p.Id,
                    UserName = p.AspNetUser.UserName,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Email = p.AspNetUser.Email,
                    BirthDate = p.BirthDate,
                    Gender = p.Gender,
                    PhoneNumber = p.PhoneNumber,
                    Region = p.Region,
                    Town = p.Town
                })
                .SingleOrDefaultAsync();

            return userData;
        }

        public async Task<UserProfileMainDTO> GetMainUserDataAsync(string aspNetUserId)
        {
            UserProfileMainDTO userData = await _context.UserProfiles
                .Where(p => p.AspNetUserId == aspNetUserId)
                .Select(p => new UserProfileMainDTO
                {
                    Id = p.Id,
                    UserName = p.AspNetUser.UserName,
                    FirstName = p.FirstName,
                    LastName = p.LastName
                })
                .SingleOrDefaultAsync();

            return userData;
        }

        public void SaveUserData(UserProfile userProfile)
        {
            _context.UserProfiles.Add(userProfile);
            _context.SaveChanges();
        }

        public void UpdateUserData(UserProfile userProfile)
        {
            _context.UserProfiles.Update(userProfile);
            _context.SaveChanges();
        }
    }
}
