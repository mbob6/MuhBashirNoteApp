using NoteApp.Entities;

namespace NoteApp.Repository.Interfaces
{
    public interface INoteRepository : IRepository<Note>
    {
        // IEnumerable<Note> SearchNotes(string searchString);
    }
}
