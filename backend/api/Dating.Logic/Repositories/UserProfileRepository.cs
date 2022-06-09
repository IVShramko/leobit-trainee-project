using AutoMapper;
using Dating.Logic.DB;
using Dating.Logic.DTO;
using Dating.Logic.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
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

        public async Task<ICollection<SearchResultDTO>> GetProfilesOnCriteria(CriteriaDTO criteria)
        {
            IQueryable<UserProfile> query = _context.UserProfiles
                .Where(u => u.FirstName != null && u.LastName != null);

            query = query.Where(u => u.Gender == criteria.Gender);

            if (criteria.MinAge.HasValue)
            {
                query = query
                    .Where(u => DateTime.Compare(u.BirthDate.AddYears((int)criteria.MinAge), DateTime.Today) <= 0);
            }

            if (criteria.MaxAge.HasValue)
            {
                query = query
                    .Where(u => DateTime.Compare(u.BirthDate.AddYears((int)criteria.MaxAge), DateTime.Today) >= 0);
            }

            if (!String.IsNullOrWhiteSpace(criteria.Region))
            {
                query = query
                    .Where(u => u.Region.Contains(criteria.Region));
            }

            if (!String.IsNullOrWhiteSpace(criteria.Town))
            {
                query = query
                    .Where(u => u.Town.Contains(criteria.Town));
            }

            ICollection<SearchResultDTO> result = await query
                .Select(u => new SearchResultDTO
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Age = DateTime.Today.Year - u.BirthDate.Year,
                    UserName = u.AspNetUser.UserName
                })
                .OrderBy(u => u.FirstName)
                .ToListAsync();

            return result;
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
