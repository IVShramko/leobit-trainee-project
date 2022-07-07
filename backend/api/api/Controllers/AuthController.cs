using Dating.Logic.DTO;
using Dating.Logic.Facades.AccountFacade;
using Dating.Logic.Facades.UserProfileFacade;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dating.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserProfileFacade _profileFacade;
        private readonly IAccountFacade _accountFacade;

        public AuthController(IUserProfileFacade profileFacade, 
            IAccountFacade accountFacade)
        {
            _profileFacade = profileFacade;
            _accountFacade = accountFacade;
        }

        [HttpGet]
        public async Task<IActionResult> Login(string userName, string password)
        {
            string token = await _accountFacade.LogIn(userName, password);

            if (token != null)
            {
                return Ok(new { access_token = token });
            }

            return Unauthorized();
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _accountFacade.LogOut();
            
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserProfileRegisterDTO registerData)
        {
            bool res = await _profileFacade.RegisterAsync(registerData);

            if (res)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
