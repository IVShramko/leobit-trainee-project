using Dating.Logic.DTO;
using System;
using System.Threading.Tasks;

namespace Dating.Logic.Facades.UserProfileFacade
{
    public interface IUserProfileFacade
    {
        public Task<UserProfileFullDTO> GetUserProfileFullDataAsync(Guid id);

        public Task<UserProfileMainDTO> GetUserProfileMainDataAsync(Guid id);

        public Task<bool> RegisterAsync(UserProfileRegisterDTO registerData);

        public Task ChangeProfileAsync(UserProfileFullDTO fullData);
    }
}
