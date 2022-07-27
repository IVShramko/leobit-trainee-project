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

        Task<string> GenerateTokenAsync(IdentityUser user);

        Guid ReadProfileId();
    }
}
