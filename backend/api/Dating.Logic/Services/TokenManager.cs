using Dating.Logic.DB;
using Dating.Logic.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Dating.Logic.Services
{
    public class TokenManager : ITokenManager
    {
        private readonly IDistributedCache _cache;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserProfileRepository _profileRepository;

        public TokenManager(IDistributedCache cache,
                IHttpContextAccessor httpContextAccessor,
                IUserProfileRepository profileRepository)
        {
            _cache = cache;
            _httpContextAccessor = httpContextAccessor;
            _profileRepository = profileRepository;
        }

        public async Task<bool> IsActiveAsync(string token)
        {
            return await _cache.GetStringAsync(GetKey(token)) == null;
        }

        public async Task DeactivateAsync(string token)
        {
            await _cache.SetStringAsync(GetKey(token),
                String.Empty, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
                });
        }

        public async Task<bool> IsCurentActiveToken()
        {
            return await IsActiveAsync(GetCurrentAsync());
        }

        public async Task DeactivateCurrentAsync()
        {
            await DeactivateAsync(GetCurrentAsync());
        }

        private string GetCurrentAsync()
        {
            var authorizationHeader = _httpContextAccessor
                .HttpContext.Request.Headers["authorization"];

            return authorizationHeader == StringValues.Empty
                ? String.Empty
                : authorizationHeader.Single().Split(" ").Last();
        }

        private static string GetKey(string token)
        {
            return $"tokens:{token}:deactivated";
        }

        public string GenerateToken(IdentityUser user)
        {
            Guid profileId = GetProfileId(user.Id);

            Claim[] claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim("UserName", user.UserName),
                new Claim("ProfileId", profileId.ToString())
            };
            
            byte[] secretBytes = Encoding.UTF8.GetBytes(Constants.Secret);
            SymmetricSecurityKey key = new SymmetricSecurityKey(secretBytes);

            SigningCredentials signinCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                Constants.Issuer, Constants.Audiance, claims,
                DateTime.Now, DateTime.Now.AddHours(1), signinCredentials);

            string tokenJson = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenJson;
        }

        private Guid GetProfileId(string aspNetId)
        {
            return _profileRepository.GetProfileIdByAspNetId(aspNetId);
        }

        public Guid ReadProfileId()
        {
            string jsontoken = GetCurrentAsync();

            var token = new JwtSecurityTokenHandler().ReadJwtToken(jsontoken);

            var id = token.Claims
                .Where(c => c.Type == "ProfileId")
                .Select(c => c.Value)
                .FirstOrDefault();

            if (id != null)
            {
                return Guid.Parse(id);
            }

            return Guid.Empty;
        }
    }
}
