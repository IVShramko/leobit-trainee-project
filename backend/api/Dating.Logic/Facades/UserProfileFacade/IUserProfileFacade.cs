using Dating.Logic.DTO;
using System;
using System.Threading.Tasks;

namespace Dating.Logic.Facades.UserProfileFacade
{
    public interface IUserProfileFacade
    {
        Task<UserProfileFullDTO> GetUserProfileFullDataAsync(Guid id);

        Task<UserProfileMainDTO> GetUserProfileMainDataAsync(Guid id);

        Task<bool> RegisterAsync(UserRegisterDTO registerData);

        Task ChangeProfileAsync(UserProfileFullDTO fullData);
    }
}
