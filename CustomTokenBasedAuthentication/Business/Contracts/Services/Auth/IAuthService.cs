using CustomTokenBasedAuthentication.Business.Models;

namespace CustomTokenBasedAuthentication.Business.Contracts.Services.Auth
{
    public interface IAuthService
    {
        UserViewModel Register(UserViewModel user, string password);
        UserViewModel Login(string email, string password);
    }
}
