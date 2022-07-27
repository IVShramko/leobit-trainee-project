using Dating.Logic.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Dating.Logic.Managers.TokenManager
{
    public class TokenManager : ITokenManager
    {
        private readonly IDistributedCache _cache;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserProfileRepository _profileRepository;
        private readonly IConfiguration _confing;

        public TokenManager(IDistributedCache cache,
                IHttpContextAccessor httpContextAccessor,
                IUserProfileRepository profileRepository, IConfiguration confing)
        {
            _cache = cache;
            _httpContextAccessor = httpContextAccessor;
            _profileRepository = profileRepository;
            _confing = confing;
        }

        public async Task<bool> IsActiveAsync(string token)
        {
            bool isActive = await _cache.GetStringAsync(GetKey(token)) == null;

            return isActive;
        }

        public async Task DeactivateAsync(string token)
        {
            await _cache.SetStringAsync(
                GetKey(token), 
                String.Empty, 
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
                });
        }

        public async Task<bool> IsCurentActiveToken()
        {
            bool isCurrentActive = await IsActiveAsync(GetCurrent());

            return isCurrentActive;
        }

        public async Task DeactivateCurrentAsync()
        {
            await DeactivateAsync(GetCurrent());
        }

        private string GetCurrent()
        {
            var authorizationHeader = _httpContextAccessor
                .HttpContext.Request.Headers["authorization"];

            string currentToken = authorizationHeader == StringValues.Empty
                ? String.Empty
                : authorizationHeader.Single().Split(" ").Last();

            return currentToken;
        }

        private static string GetKey(string token)
        {
            string key = $"tokens:{token}:deactivated";

            return key;
        }

        public async Task<string> GenerateTokenAsync(IdentityUser user)
        {
            Guid profileId = await GetProfileIdAsync(user.Id);

            Claim[] claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim("UserName", user.UserName),
                new Claim("ProfileId", profileId.ToString())
            };
            
            byte[] secretBytes = Encoding.UTF8.GetBytes(
                _confing.GetSection("Constants")["Secret"]);

            SymmetricSecurityKey key = new SymmetricSecurityKey(secretBytes);

            SigningCredentials signinCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                _confing.GetSection("Constants")["Issuer"], 
                _confing.GetSection("Constants")["Audiance"], 
                claims, DateTime.Now, DateTime.Now.AddHours(1),
                signinCredentials);

            string tokenJson = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenJson;
        }

        private async Task<Guid> GetProfileIdAsync(string aspNetId)
        {
            Guid profileId = 
                await _profileRepository.GetProfileIdByAspNetIdAsync(aspNetId);

            return profileId;
        }

        public Guid ReadProfileId()
        {
            Guid profileId = Guid.Empty;

            string jsontoken = GetCurrent();
            var token = new JwtSecurityTokenHandler().ReadJwtToken(jsontoken);

            string id = token.Claims
                .Where(c => c.Type == "ProfileId")
                .Select(c => c.Value)
                .FirstOrDefault();

            if (id != null)
            {
                profileId = Guid.Parse(id);
            }

            return profileId;
        }
    }
}
