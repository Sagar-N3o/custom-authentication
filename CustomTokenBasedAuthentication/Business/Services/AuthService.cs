using AutoMapper;
using CustomTokenBasedAuthentication.Business.Contracts.Repositories;
using CustomTokenBasedAuthentication.Business.Contracts.Services.Auth;
using CustomTokenBasedAuthentication.Business.Models;
using CustomTokenBasedAuthentication.Business.Repositories;
using CustomTokenBasedAuthentication.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

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

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return _mapper.Map<UserViewModel>(user);
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for(int i = 0; i < computedHash.Length; i++)
                    if (computedHash[i] != passwordHash[i]) return false;
            }
            return true;
        }

        public UserViewModel Register(UserViewModel user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            User model = _mapper.Map<User>(user);

            model.PasswordHash = passwordHash;
            model.PasswordSalt = passwordSalt;

            model = _unitOfWork.AuthRepository.Register(model);
            _unitOfWork.SaveChanges();

            return _mapper.Map<UserViewModel>(model);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
