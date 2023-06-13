using NoteApp.Context;
using NoteApp.Entities;
using NoteApp.Repository.Implementations;
using NoteApp.Repository.Interfaces;

namespace NoteApp.Repository.Implementation
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(NoteDbContext context) : base(context) { }
    }
}
