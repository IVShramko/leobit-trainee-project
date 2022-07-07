using Dating.Logic.DTO;
using System.Threading.Tasks;

namespace Dating.Logic.Facades.AccountFacade
{
    public interface IAccountFacade
    {
        public Task<string> LogIn(string userName, string password);

        public Task LogOut();

        public Task Register(UserProfileRegisterDTO registerData);
    }
}
