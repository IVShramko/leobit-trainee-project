using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Dating.Logic.Services
{
    public interface ITokenManager
    {
        Task<bool> IsCurentActiveToken();

        Task DeactivateCurrentAsync();

        Task<bool> IsActiveAsync(string token);

        Task DeactivateAsync(string token);

        string GenerateToken(IdentityUser user);

        Guid ReadProfileId();
    }
}
