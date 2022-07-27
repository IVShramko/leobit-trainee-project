using Dating.Logic.DTO;
using Dating.Logic.Managers.TokenManager;
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
            string token = null;

            IdentityUser user = await _userManager.FindByNameAsync(userName);

            if (user != null)
            {
                Identity.SignInResult result =
                    await _signInManager.CheckPasswordSignInAsync(user, password, false);

                if (result.Succeeded)
                {
                   token = await _tokenManager.GenerateTokenAsync(user);
                }
            }

            return token;
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
            await _tokenManager.DeactivateCurrentAsync();
        }

        public Task Register(UserRegisterDTO registerData)
        {
            throw new System.NotImplementedException();
        }
    }
}
