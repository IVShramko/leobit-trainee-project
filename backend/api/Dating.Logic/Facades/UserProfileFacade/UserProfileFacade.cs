using AutoMapper;
using Dating.Logic.DB;
using Dating.Logic.DTO;
using Dating.Logic.Models;
using Dating.Logic.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

                UserProfile profile = _mapper.Map<UserProfile>(fullData);

                profile.AspNetUserId = aspUser.Id;
                profile.AspNetUser = aspUser;
                _repository.UpdateUserData(profile);
            
        }
    }
}
