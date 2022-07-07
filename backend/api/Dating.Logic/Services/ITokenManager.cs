using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Dating.Logic.Services
{
    public interface ITokenManager
    {
        public Task<bool> IsCurentActiveToken();

        public Task DeactivateCurrentAsync();

        public Task<bool> IsActiveAsync(string token);

        public Task DeactivateAsync(string token);

        public string GenerateToken(IdentityUser user);

        public Guid ReadProfileId();
    }
}
