using Microsoft.EntityFrameworkCore;
using NoteApp.Context;
using NoteApp.Repository.Interfaces;

namespace NoteApp.Repository.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NoteDbContext _context;
        public bool _disposed = false;
        public IUserRepository User { get; } 

        public IRoleRepository Role { get; }

        public INoteRepository Note { get; }

        public UnitOfWork
            (NoteDbContext context
            , IUserRepository user
            , IRoleRepository role
            , INoteRepository note
            )
        {
            _context = context;
            User = user;
            Role = role;
            Note = note;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
