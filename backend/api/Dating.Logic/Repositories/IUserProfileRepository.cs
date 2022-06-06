using Dating.Logic.DTO;
using Dating.Logic.Models;
using System.Threading.Tasks;

namespace Dating.Logic.Repositories
{
    public interface IUserProfileRepository
    {
        public Task<UserProfileFullDTO> GetFullUserDataAsync(string aspNetUserId);

        public Task<UserProfileMainDTO> GetMainUserDataAsync(string aspNetUserId);

        public void SaveUserData(UserProfile profile);

        public void UpdateUserData(UserProfile profile);
    }
}
