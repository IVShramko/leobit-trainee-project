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

        public async Task<UserProfile> GetFullUserDataAsync(Guid id)
        {
            UserProfile userData = await _context.UserProfiles
                .Where(p => p.Id == id)
                .Select(p => new UserProfile
                {
                    Id = p.Id,
                    AspNetUser = p.AspNetUser,
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

            return userData;
        }

        public async Task<UserProfileMainDTO> GetMainUserDataAsync(Guid id)
        {
            UserProfileMainDTO userData = await _context.UserProfiles
                .Where(p => p.Id == id)
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

        public async Task<ProfileSearchresult> GetProfilesOnCriteriaAsync(SearchCriteria criteria)
        {
            ProfileSearchresult result = new ProfileSearchresult();

            IQueryable<UserProfile> query = PrepareQuery(criteria.Profile);
            result.ResultsTotal = query.Count();

            query = OrderQuery(query, (Filters)criteria.Filter);

            result.Profiles = await query
                .Skip(criteria.PageSize * (criteria.PageIndex - 1))
                .Take(criteria.PageSize)
                .Select(u => new ProfileListDTO
                {
                    UserName = u.AspNetUser.UserName,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Age = DateTime.Now.Year - u.BirthDate.Year
                })
                .ToListAsync();

            return result;
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

        private IQueryable<UserProfile> OrderQuery(IQueryable<UserProfile> query, Filters filter)
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

        public bool SaveUserData(IdentityUser aspUser, ProfileRegisterDTO registerData)
        {
            UserProfile profile = new UserProfile
            {
                AspNetUserId = aspUser.Id,
                BirthDate = registerData.BirthDate,
                Gender = registerData.Gender
            };

            _context.UserProfiles.Add(profile);
            int result = _context.SaveChanges();

            return result != 0;
        }

        public void UpdateUserData(UserProfile userProfile)
        {
            _context.UserProfiles.Update(userProfile);
            _context.SaveChanges();
        }

        public Guid GetProfileIdByAspNetId(string aspNetId)
        {
            return _context.UserProfiles
                .Where(p => p.AspNetUserId == aspNetId)
                .Select(p => p.Id)
                .FirstOrDefault();
        }
    }
}
