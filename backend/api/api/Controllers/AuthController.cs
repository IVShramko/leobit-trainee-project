﻿using Dating.Logic.DTO;
using Dating.Logic.Facades.UserProfileFacade;
using Dating.WebAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Identity = Microsoft.AspNetCore.Identity;

namespace Dating.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly ITokenManager _tokenManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserProfileFacade _profileFacade;

        public AuthController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            ITokenManager tokenManager, IUserProfileFacade profileFacade)
        {
            _tokenManager = tokenManager;
            _signInManager = signInManager;
            _userManager = userManager;
            _profileFacade = profileFacade;
        }

        [HttpGet]
        public async Task<IActionResult> Login(string userName, string password)
        {
            IdentityUser user = await _userManager.FindByNameAsync(userName);

            if (user != null)
            {
                Identity.SignInResult result =
                await _signInManager.CheckPasswordSignInAsync(user, password, false);

                if (result.Succeeded)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                        new Claim("UserName", userName)
                    };

                    var secretBytes = Encoding.UTF8.GetBytes(Constants.Secret);
                    var key = new SymmetricSecurityKey(secretBytes);

                    var signinCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        Constants.Issuer, Constants.Audiance, claims,
                        DateTime.Now, DateTime.Now.AddHours(1), signinCredentials);

                    var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);

                    return Ok(new { access_token = tokenJson });
                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _tokenManager.DeactivateCurrentAsync();
            await _signInManager.SignOutAsync();
            

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
