using Dating.Logic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dating.Logic.Facades.UserProfileFacade
{
    public interface IUserProfileFacade
    {
        public Task<UserProfileFullDTO> GetUserProfileFullDataAsync(string aspNetUserId);

        public Task<UserProfileMainDTO> GetUserProfileMainDataAsync(string aspNetUserId);

        public Task RegisterAsync(UserProfileRegisterDTO registerData);

        public Task ChangeProfileAsync(UserProfileFullDTO fullData);
    }
}
