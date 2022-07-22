using Dating.Logic.DTO;
using Dating.Logic.Models;
using Dating.Logic.Repositories;
using Dating.Logic.Repositories.ImageRepository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Dating.Logic.Facades.UserProfileFacade
{
    public class UserProfileFacade : IUserProfileFacade
    {
        private readonly IUserProfileRepository _profileRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IImageRepository _imageRepository;

        public UserProfileFacade(IUserProfileRepository profileRepository,
            UserManager<IdentityUser> userManager, IImageRepository imageRepository)
        {
            _profileRepository = profileRepository;
            _userManager = userManager;
            _imageRepository = imageRepository;
        }

        public async Task<UserProfileFullDTO> GetUserProfileFullDataAsync(Guid id)
        {
            var profile = await _profileRepository.GetUserProfileAsync(id);

            string photo = _imageRepository.GetPhotoById(profile.Avatar, profile.Id);

            UserProfileFullDTO fullData = new UserProfileFullDTO
            {
                Id = profile.Id,
                UserName = profile.AspNetUser.UserName,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Email = profile.AspNetUser.Email,
                BirthDate = profile.BirthDate,
                Gender = profile.Gender,
                PhoneNumber = profile.PhoneNumber,
                Region = profile.Region,
                Town = profile.Town,
                Photo = photo
            };

            return fullData;
        }

        public async Task<UserProfileMainDTO> GetUserProfileMainDataAsync(Guid id)
        {
            UserProfileMainDTO mainProfile =
                await _profileRepository.GetUserProfileMainAsync(id);

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
                    _profileRepository.SaveUserData(aspUser, registerData.Profile);
            }

            return isRegistered;
        }

        public async Task ChangeProfileAsync(UserProfileFullDTO fullData)
        {
            var aspUser = await _userManager.FindByNameAsync(fullData.UserName);

            Guid photoId = _imageRepository.AddPhoto(fullData.Photo, fullData.Id);

            UserProfile profile = new UserProfile()
            {
                Id = fullData.Id,
                AspNetUser = aspUser,
                AspNetUserId = aspUser.Id,
                FirstName = fullData.FirstName,
                LastName = fullData.LastName,
                BirthDate = fullData.BirthDate,
                Gender = fullData.Gender,
                PhoneNumber = fullData.PhoneNumber,
                Region = fullData.Region,
                Town = fullData.Town,
                Avatar = photoId
            };

            _profileRepository.UpdateUserData(profile);
        }
    }
}
