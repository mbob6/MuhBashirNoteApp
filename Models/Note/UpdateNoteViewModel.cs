using System.ComponentModel.DataAnnotations;

namespace NoteApp.Models.Note;

public class UpdateNoteViewModel
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}
