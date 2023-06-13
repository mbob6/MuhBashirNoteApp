using NoteApp.Entities;
using System.Linq.Expressions;

namespace NoteApp.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUser(Expression<Func<User, bool>> expression);
    }
}
