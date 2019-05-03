using CustomTokenBasedAuthentication.Database.Models;

namespace CustomTokenBasedAuthentication.Business.Contracts.Repositories.Auth
{
    public interface IAuthRepository
    {
        User Register(User user);
        User Login(string email);
        bool EmailExists(string email);
    }
}
