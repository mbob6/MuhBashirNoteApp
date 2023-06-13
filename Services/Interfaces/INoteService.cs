using NoteApp.Models;
using NoteApp.Models.Note;
using NoteApp.Models.Role;

namespace NoteApp.Services.Interfaces
{
    public interface INoteService
    {
        public BaseResponseModel CreateNote(CreateNoteViewModel model);
        public BaseResponseModel UpdateNote(Guid id, UpdateNoteViewModel model);
        public BaseResponseModel DeleteNote(Guid id);
        public NoteResponseModel GetNote(Guid id);
        public NotesResponseModel GetAllNotes();
    }
}
