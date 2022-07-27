using Dating.Logic.DTO;
using Dating.Logic.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Dating.Logic.Facades.UserProfileFacade
{
    public class UserProfileFacade : IUserProfileFacade
    {
        private readonly IUserProfileRepository _profileRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public UserProfileFacade(IUserProfileRepository profileRepository,
            UserManager<IdentityUser> userManager)
        {
            _profileRepository = profileRepository;
            _userManager = userManager;
        }

        public async Task<UserProfileFullDTO> GetFullProfileAsync(Guid id)
        {
            UserProfileFullDTO profile = 
                await _profileRepository.GetFullProfileAsync(id);

            return profile;
        }

        public async Task<UserProfileMainDTO> GetMainProfileAsync(Guid id)
        {
            UserProfileMainDTO mainProfile =
                await _profileRepository.GetMainProfileAsync(id);

            return mainProfile;
        }

        public async Task<bool> RegisterAsync(UserRegisterDTO registerData)
        {
            bool isRegistered = false;

            IdentityUser user = new IdentityUser()
            {
                UserName = registerData.UserName,
                Email = registerData.Email
            };

            IdentityResult result = 
                await _userManager.CreateAsync(user, registerData.Password);

            if (result.Succeeded)
            {
                var aspUser = await _userManager.FindByNameAsync(registerData.UserName);

                isRegistered = 
                    await _profileRepository.CreateProfileAsync(aspUser, registerData.Profile);
            }

            return isRegistered;
        }

        public async Task<bool> ChangeProfileAsync(UserProfileFullDTO profile)
        {
            bool isChanged;

            var aspUser = await _userManager.FindByNameAsync(profile.UserName);

            isChanged = await _profileRepository.UpdateProfileAsync(profile);

            return isChanged;
        }

        public async Task<bool> SetProfileAvatarAsync(Guid profileId, Guid photoId)
        {
            bool isSet;

            var profile = await _profileRepository.GetFullProfileAsync(profileId);

            profile.Avatar = photoId;

            isSet = await _profileRepository.UpdateProfileAsync(profile);

            return isSet;
        }
    }
}
