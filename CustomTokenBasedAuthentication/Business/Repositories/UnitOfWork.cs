using CustomTokenBasedAuthentication.Business.Contracts.Repositories;
using CustomTokenBasedAuthentication.Business.Contracts.Repositories.Auth;
using CustomTokenBasedAuthentication.Business.Repositories.Auth;
using CustomTokenBasedAuthentication.Database.Context;
using System;

namespace CustomTokenBasedAuthentication.Business.Repositories
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly UserDbContext _context;
        public IAuthRepository AuthRepository { get; }

        public UnitOfWork(UserDbContext context)
        {
            _context = context;
            AuthRepository = new AuthRepository(_context);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        #region Dispose Implementation
        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                _context.Dispose();
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
