using Dating.Logic.DTO;
using Dating.Logic.Facades.UserProfileFacade;
using Dating.Logic.Managers.TokenManager;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Dating.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IUserProfileFacade _profileFacade;
        private readonly ITokenManager _tokenManager;

        public ProfileController(IUserProfileFacade profileFacade,
            ITokenManager tokenManager)
        {
            _profileFacade = profileFacade;
            _tokenManager = tokenManager;
        }

        [HttpGet]
        public async Task<IActionResult> Main()
        {
            Guid profileId = _tokenManager.ReadProfileId();

            UserProfileMainDTO mainProfile = 
                await _profileFacade.GetUserProfileMainDataAsync(profileId);

            return Ok(mainProfile);
        }

        [HttpGet]
        public async Task<IActionResult> Full()
        {
            Guid profileId = _tokenManager.ReadProfileId();

            UserProfileFullDTO fullProfile = 
                await _profileFacade.GetUserProfileFullDataAsync(profileId);

            return Ok(fullProfile);
        }

        [HttpPost]
        public async Task<IActionResult> Change(UserProfileFullDTO fullProfile)
        {
            await _profileFacade.ChangeProfileAsync(fullProfile);

            return Ok();
        }
    }
}
