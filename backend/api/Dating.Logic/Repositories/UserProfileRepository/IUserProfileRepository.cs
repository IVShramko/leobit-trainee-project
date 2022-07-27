using Dating.Logic.DTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Dating.Logic.Repositories
{
    public interface IUserProfileRepository
    {
        Task<UserProfileFullDTO> GetFullProfileAsync(Guid id);

        Task<UserProfileMainDTO> GetMainProfileAsync(Guid id);

        Task<ProfileSearchresult> GetProfilesOnCriteriaAsync(SearchCriteria criteria);

        Task<bool> CreateProfileAsync(IdentityUser aspUser, ProfileRegisterDTO profile);

        Task<bool> UpdateProfileAsync(UserProfileFullDTO profile);

        Task<Guid> GetProfileIdByAspNetIdAsync(string aspNetId);
    }
}
