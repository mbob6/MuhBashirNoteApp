using System.ComponentModel.DataAnnotations;

namespace NoteApp.Models.Question;

public class UpdateNoteViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
}
