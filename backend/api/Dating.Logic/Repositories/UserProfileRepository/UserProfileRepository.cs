using Dating.Logic.DB;
using Dating.Logic.DTO;
using Dating.Logic.Enums;
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

        public async Task<UserProfileFullDTO> GetFullProfileAsync(Guid id)
        {
            UserProfileFullDTO profile = await _context.UserProfiles
                .Where(p => p.Id == id)
                .Select(p => new UserProfileFullDTO
                {
                    Id = p.Id,
                    UserName = p.AspNetUser.UserName,
                    Email = p.AspNetUser.Email,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    BirthDate = p.BirthDate,
                    Gender = p.Gender,
                    PhoneNumber = p.PhoneNumber,
                    Region = p.Region,
                    Town = p.Town,
                    Avatar = p.Avatar
                })
                .SingleOrDefaultAsync();

            return profile;
        }

        public async Task<UserProfileMainDTO> GetMainProfileAsync(Guid id)
        {
            UserProfileMainDTO userData = await _context.UserProfiles
                .Where(p => p.Id == id)
                .Select(p => new UserProfileMainDTO
                {
                    Id = p.Id,
                    UserName = p.AspNetUser.UserName,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Avatar = p.Avatar
                })
                .SingleOrDefaultAsync();

            return userData;
        }

        public async Task<ProfileSearchresult> GetProfilesOnCriteriaAsync(
            SearchCriteria criteria)
        {
            ProfileSearchresult searchResult = new ProfileSearchresult();

            IQueryable<UserProfile> query = PrepareQuery(criteria.Profile);

            searchResult.ResultsTotal = query.Count();

            query = OrderQuery(query, (Filters)criteria.Filter);

            searchResult.Profiles = await query
                .Skip(criteria.PageSize * (criteria.PageIndex - 1))
                .Take(criteria.PageSize)
                .Select(u => new ProfileListDTO
                {
                    Id = u.Id,
                    UserName = u.AspNetUser.UserName,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Age = DateTime.Now.Year - u.BirthDate.Year,
                    Avatar = u.Avatar
                })
                .ToListAsync();

            return searchResult;
        }

        private IQueryable<UserProfile> PrepareQuery(ProfileCriteria criteria)
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

            return query;
        }

        private IQueryable<UserProfile> OrderQuery(
            IQueryable<UserProfile> query, Filters filter)
        {
            switch (filter)
            {
                case Filters.Age:
                    query = query.OrderByDescending(u => u.BirthDate);
                    break;

                case Filters.Name:
                    query = query.OrderBy(u => u.FirstName);
                    break;

                default:
                    throw new Exception("incorrect filter type");
            }

            return query;
        }

        public async Task<bool> CreateProfileAsync(
            IdentityUser aspUser, ProfileRegisterDTO registerProfile)
        {
            UserProfile profile = new UserProfile
            {
                AspNetUserId = aspUser.Id,
                BirthDate = registerProfile.BirthDate,
                Gender = registerProfile.Gender
            };

            await _context.UserProfiles.AddAsync(profile);
            int result = _context.SaveChanges();

            return result != 0;
        }

        public async Task<bool> UpdateProfileAsync(UserProfileFullDTO profile)
        {
            var entity = await _context.UserProfiles
                .Include(p => p.AspNetUser)
                .Where(p => p.Id == profile.Id)
                .SingleOrDefaultAsync();

            entity.FirstName = profile.FirstName;
            entity.LastName = profile.LastName;
            entity.AspNetUser.Email = profile.Email;
            entity.PhoneNumber = profile.PhoneNumber;
            entity.Region = profile.Region;
            entity.Town = profile.Town;
            entity.Avatar = profile.Avatar;
            entity.BirthDate = profile.BirthDate;
            entity.Gender = profile.Gender;

            int result = _context.SaveChanges();

            return result != 0;
        }

        public async Task<Guid> GetProfileIdByAspNetIdAsync(string aspNetId)
        {
            Guid profileId = await _context.UserProfiles
                .Where(p => p.AspNetUserId == aspNetId)
                .Select(p => p.Id)
                .SingleOrDefaultAsync();

            return profileId;
        }
    }
}
