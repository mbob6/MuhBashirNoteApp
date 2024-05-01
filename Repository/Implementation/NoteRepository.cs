using NoteApp.Context;
using NoteApp.Entities;
using NoteApp.Repository.Implementations;
using NoteApp.Repository.Interfaces;

namespace NoteApp.Repository.Implementation
{
    public class NoteRepository : BaseRepository<Note>, INoteRepository
    {
        public NoteRepository(NoteDbContext context) : base(context) { }
        // IEnumerable<Note> INoteRepository.SearchNotes(string searchString)
        // {
        //     return _context.Notes.Where(n => n.Title.Contains(searchString)).ToList();
        // }
    }
}
