using Dating.Logic.DTO;
using Dating.Logic.Facades.UserProfileFacade;
using Dating.Logic.Managers.TokenManager;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Dating.WebAPI.Controllers
{
    [Route("api/[controller]")]
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
        [Route("main")]
        public async Task<IActionResult> Main()
        {
            Guid profileId = _tokenManager.ReadProfileId();

            UserProfileMainDTO mainProfile =
                await _profileFacade.GetMainProfileAsync(profileId);

            return Ok(mainProfile);
        }

        [HttpGet]
        [Route("full")]
        public async Task<IActionResult> Full()
        {
            Guid profileId = _tokenManager.ReadProfileId();

            UserProfileFullDTO fullProfile =
                await _profileFacade.GetFullProfileAsync(profileId);

            return Ok(fullProfile);
        }

        [HttpGet]
        [Route("chat/{aspNetUserId}")]
        public async Task<IActionResult> GetChatProfile(string aspNetUserId)
        {
            ProfileChatDTO profile = await 
                _profileFacade.GetChatProfileAsync(aspNetUserId);

            return Ok(profile);
        }

        [HttpPost]
        [Route("change")]
        public async Task<IActionResult> Change(UserProfileFullDTO fullProfile)
        {
            bool isChanged;

            isChanged = await _profileFacade.ChangeProfileAsync(fullProfile);

            if (isChanged)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("avatar")]
        public async Task<IActionResult> SetProfileAvatarAsync(Guid photoId)
        {
            bool isSet;

            Guid profileId = _tokenManager.ReadProfileId();

            isSet = await _profileFacade.SetProfileAvatarAsync(profileId, photoId);

            if (isSet)
            {
                return Ok();
            }

            return BadRequest();        
        }
    }
}
