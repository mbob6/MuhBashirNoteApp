using Microsoft.EntityFrameworkCore;
using NoteApp.Context;
using NoteApp.Entities;
using NoteApp.Repository.Implementations;
using NoteApp.Repository.Interfaces;
using System.Linq.Expressions;

namespace NoteApp.Repository.Implementation
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(NoteDbContext context) : base(context) { }

        public User GetUser(Expression<Func<User, bool>> expression)
        { 
            return _context.Users
                .Include(x => x.Role)
                .SingleOrDefault(expression);
        }
    }
}
