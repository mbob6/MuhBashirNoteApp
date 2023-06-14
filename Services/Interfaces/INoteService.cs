using NoteApp.Models;
using NoteApp.Models.Note;
using NoteApp.Models.Role;

namespace NoteApp.Services.Interfaces
{
    public interface INoteService
    {
        public BaseResponseModel CreateNote(CreateNoteViewModel model);
        public BaseResponseModel UpdateNote(string id, UpdateNoteViewModel model);
        public BaseResponseModel DeleteNote(string id);
        public NoteResponseModel GetNote(string id);
        public NotesResponseModel GetAllNotes();
    }
}
