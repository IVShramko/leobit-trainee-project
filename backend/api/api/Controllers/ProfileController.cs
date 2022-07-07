using Dating.Logic.DTO;
using Dating.Logic.Facades.UserProfileFacade;
using Dating.Logic.Services;
using Microsoft.AspNetCore.Http;
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
            Guid id = _tokenManager.ReadProfileId();

            var mainProfile = await _profileFacade.GetUserProfileMainDataAsync(id);

            return Ok(mainProfile);
        }

        [HttpGet]
        public async Task<IActionResult> Full()
        {
            Guid id = _tokenManager.ReadProfileId();

            var fullProfile = await _profileFacade.GetUserProfileFullDataAsync(id);

            return Ok(fullProfile);
        }

        [HttpPost]
        public async Task<IActionResult> Change(UserProfileFullDTO fullData)
        {
            await _profileFacade.ChangeProfileAsync(fullData);

            return Ok();
        }
    }
}
