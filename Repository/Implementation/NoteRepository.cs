using NoteApp.Context;
using NoteApp.Entities;
using NoteApp.Repository.Implementations;
using NoteApp.Repository.Interfaces;

namespace NoteApp.Repository.Implementation
{
    public class NoteRepository : BaseRepository<Note>, INoteRepository
    {
        public NoteRepository(NoteDbContext context) : base(context) { }
    }
}
