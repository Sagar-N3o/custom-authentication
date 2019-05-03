using CustomTokenBasedAuthentication.Business.Contracts.Repositories.Auth;
using CustomTokenBasedAuthentication.Database.Context;
using CustomTokenBasedAuthentication.Database.Models;
using System.Linq;

namespace CustomTokenBasedAuthentication.Business.Repositories.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserDbContext _context;

        public AuthRepository(UserDbContext context)
        {
            _context = context;
        }

        public User Login(string email)
        {
            return _context.Users.FirstOrDefault(user => user.Email == email);
        }

        public User Register(User user)
        {
            _context.Users.Add(user);

            return user;
        }
    }
}
