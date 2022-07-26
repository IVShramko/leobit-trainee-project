using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Dating.Logic.Managers.TokenManager
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
