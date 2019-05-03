using CustomTokenBasedAuthentication.Business.Contracts.Repositories.Auth;

namespace CustomTokenBasedAuthentication.Business.Contracts.Repositories
{
    public interface IUnitOfWork
    {
        IAuthRepository AuthRepository { get; }

        void Dispose();
        void SaveChanges();
    }
}