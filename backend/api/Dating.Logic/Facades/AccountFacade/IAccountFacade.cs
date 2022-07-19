using Dating.Logic.DTO;
using System.Threading.Tasks;

namespace Dating.Logic.Facades.AccountFacade
{
    public interface IAccountFacade
    {
        Task<string> LogIn(string userName, string password);

        Task LogOut();

        Task Register(UserRegisterDTO registerData);
    }
}
