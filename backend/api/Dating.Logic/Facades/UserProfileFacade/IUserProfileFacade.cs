using Dating.Logic.DTO;
using System;
using System.Threading.Tasks;

namespace Dating.Logic.Facades.UserProfileFacade
{
    public interface IUserProfileFacade
    {
        Task<UserProfileFullDTO> GetFullProfileAsync(Guid id);

        Task<UserProfileMainDTO> GetMainProfileAsync(Guid id);

        Task<bool> RegisterAsync(UserRegisterDTO registerData);

        Task<bool> ChangeProfileAsync(UserProfileFullDTO fullData);

        Task<bool> SetProfileAvatarAsync(Guid profileId, Guid photoId);
    }
}
