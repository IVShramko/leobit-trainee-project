using Dating.Logic.DTO;
using Dating.Logic.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Dating.Logic.Repositories
{
    public interface IUserProfileRepository
    {
        Task<UserProfile> GetUserProfileAsync(Guid id);

        Task<UserProfileMainDTO> GetUserProfileMainAsync(Guid id);

        Task<ProfileSearchresult> GetProfilesOnCriteriaAsync(SearchCriteria criteria);

        bool SaveUserData(IdentityUser aspUser, ProfileRegisterDTO profile);

        void UpdateUserData(UserProfile profile);

        Guid GetProfileIdByAspNetId(string aspNetId);
    }
}
