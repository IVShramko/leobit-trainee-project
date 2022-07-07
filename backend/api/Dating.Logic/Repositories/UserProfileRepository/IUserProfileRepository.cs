using Dating.Logic.DTO;
using Dating.Logic.Models;
using System;
using System.Threading.Tasks;

namespace Dating.Logic.Repositories
{
    public interface IUserProfileRepository
    {
        public Task<UserProfile> GetFullUserDataAsync(Guid id);

        public Task<UserProfileMainDTO> GetMainUserDataAsync(Guid id);

        public Task<SearchResultDTO> GetProfilesOnCriteriaAsync(CriteriaDTO criteria);

        public void SaveUserData(UserProfile profile);

        public void UpdateUserData(UserProfile profile);

        public Guid GetProfileIdByAspNetId(string aspNetId);
    }
}
