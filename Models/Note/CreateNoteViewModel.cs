using System.ComponentModel.DataAnnotations;

namespace NoteApp.Models.Note;

public class CreateNoteViewModel
{ 
    public string Title { get; set; }

    public string Content { get; set; }
}
