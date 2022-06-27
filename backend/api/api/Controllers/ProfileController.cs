using Dating.Logic.DTO;
using Dating.Logic.Facades.UserProfileFacade;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Dating.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IUserProfileFacade _profileFacade;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileController(IUserProfileFacade profileFacade,
            IHttpContextAccessor httpContextAccessor)
        {
            _profileFacade = profileFacade;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> Main()
        {
            var authHeader = _httpContextAccessor
                .HttpContext.Request.Headers["authorization"];

            string jsontoken = authHeader.Single().Split(" ").Last();
            var token = new JwtSecurityTokenHandler().ReadJwtToken(jsontoken);

            var aspnetid = token.Claims
                .Where(c => c.Type == "sub")
                .Select(c => c.Value)
                .FirstOrDefault();

            var mainProfile = await _profileFacade.GetUserProfileMainDataAsync(aspnetid);

            return Ok(mainProfile);
        }

        [HttpGet]
        public async Task<IActionResult> Full()
        {
            var authHeader = _httpContextAccessor
                .HttpContext.Request.Headers["authorization"];
            var jsontoken = authHeader.Single().Split(" ").Last();
            var token = new JwtSecurityTokenHandler().ReadJwtToken(jsontoken);
            var aspnetid = token.Claims
                .Where(c => c.Type == "sub")
                .Select(c => c.Value)
                .FirstOrDefault();
            var fullProfile = await _profileFacade.GetUserProfileFullDataAsync(aspnetid);
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
