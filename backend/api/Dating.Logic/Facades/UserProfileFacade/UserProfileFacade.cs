using AutoMapper;
using Dating.Logic.DB;
using Dating.Logic.DTO;
using Dating.Logic.Models;
using Dating.Logic.Repositories;
using Dating.Logic.Repositories.ImageRepository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public async Task<UserProfileFullDTO> GetUserProfileFullDataAsync(string aspNetUserId)
        {
            var profile = await _profileRepository.GetFullUserDataAsync(aspNetUserId);

            string photo = _imageRepository.GetPhotoById(profile.Avatar, profile.Id);

            UserProfileFullDTO fullData = new UserProfileFullDTO()
            {
                Id = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                BirthDate = profile.BirthDate,
                Gender = profile.Gender,
                PhoneNumber = profile.PhoneNumber,
                Region = profile.Region,
                Town = profile.Town,
                Photo = photo
            };

            return fullData;
        }

        public async Task<UserProfileMainDTO> GetUserProfileMainDataAsync(string aspNetUserId)
        {
            return await _profileRepository.GetMainUserDataAsync(aspNetUserId);
        }

        public async Task<bool> RegisterAsync(UserProfileRegisterDTO registerData)
        {
            IdentityUser user = new IdentityUser()
            {
                UserName = registerData.UserName,
                Email = registerData.Email
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerData.Password);

            if (result.Succeeded)
            {
                var aspUser = await _userManager.FindByNameAsync(registerData.UserName);
                UserProfile profile = new UserProfile
                {
                    AspNetUserId = aspUser.Id,
                    BirthDate = registerData.Data.BirthDate,
                    Gender = registerData.Data.Gender
                };

                _profileRepository.SaveUserData(profile);

                return true;
            }

            return false;
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
