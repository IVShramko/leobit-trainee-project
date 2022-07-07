using Dating.Logic.DTO;
using Dating.Logic.Services;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

using Identity = Microsoft.AspNetCore.Identity;

namespace Dating.Logic.Facades.AccountFacade
{
    public class AccountFacade : IAccountFacade
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenManager _tokenManager;

        public AccountFacade(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager, ITokenManager tokenManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenManager = tokenManager;
        }

        public async Task<string> LogIn(string userName, string password)
        {
            IdentityUser user = await _userManager.FindByNameAsync(userName);

            if (user != null)
            {
                Identity.SignInResult result =
                await _signInManager.CheckPasswordSignInAsync(user, password, false);

                if (result.Succeeded)
                {
                   string token = _tokenManager.GenerateToken(user);

                   return token;
                }
            }

            return null;
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
            await _tokenManager.DeactivateCurrentAsync();
        }

        public Task Register(UserProfileRegisterDTO registerData)
        {
            throw new System.NotImplementedException();
        }
    }
}
