using AutoMapper;
using CustomTokenBasedAuthentication.Business.Contracts.Repositories;
using CustomTokenBasedAuthentication.Business.Contracts.Services.Auth;
using CustomTokenBasedAuthentication.Business.Helpers;
using CustomTokenBasedAuthentication.Business.Models;
using CustomTokenBasedAuthentication.Database.Models;

namespace CustomTokenBasedAuthentication.Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public UserViewModel Login(string email, string password)
        {
            User user = _unitOfWork.AuthRepository.Login(email);

            if (user == null)
                return null;

            if (!StaticHelpers.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return _mapper.Map<UserViewModel>(user);
        }

        public UserViewModel Register(UserViewModel user, string password)
        {
            StaticHelpers.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            User model = _mapper.Map<User>(user);

            model.PasswordHash = passwordHash;
            model.PasswordSalt = passwordSalt;

            model = _unitOfWork.AuthRepository.Register(model);
            _unitOfWork.SaveChanges();

            return _mapper.Map<UserViewModel>(model);
        }
    }
}
