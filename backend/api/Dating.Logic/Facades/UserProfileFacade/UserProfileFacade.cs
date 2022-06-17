using AutoMapper;
using Dating.Logic.DB;
using Dating.Logic.DTO;
using Dating.Logic.Models;
using Dating.Logic.Repositories;
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
        private readonly IUserProfileRepository _repository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public UserProfileFacade(IUserProfileRepository repository,
            UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _repository = repository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserProfileFullDTO> GetUserProfileFullDataAsync(string aspNetUserId)
        {
            return await _repository.GetFullUserDataAsync(aspNetUserId);
        }

        public async Task<UserProfileMainDTO> GetUserProfileMainDataAsync(string aspNetUserId)
        {
            return await _repository.GetMainUserDataAsync(aspNetUserId);
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

                _repository.SaveUserData(profile);

                return true;
            }

            return false;
        }

        public async Task ChangeProfileAsync(UserProfileFullDTO fullData)
        {
            var aspUser = await _userManager.FindByNameAsync(fullData.UserName);

            Guid photoid = AddPhoto(fullData.Photo, fullData.Id);

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
                Town = fullData.Town
            };

            _repository.UpdateUserData(profile);

        }

        private Guid AddPhoto(string DataUrlString, Guid UserId)
        {
            var base64Data = Regex.Match(DataUrlString, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;

            var photobytes = Convert.FromBase64String(base64Data);

            string root = Directory.GetParent(Directory.GetCurrentDirectory()).FullName + "/img";

            string path = Directory.CreateDirectory(root + "/" + UserId).FullName;

            Guid id = Guid.NewGuid();

            File.WriteAllBytes(path + "/" + id, photobytes);

            return id;
        }
    }
}
