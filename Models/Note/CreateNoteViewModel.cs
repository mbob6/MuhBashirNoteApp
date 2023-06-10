using System.ComponentModel.DataAnnotations;

namespace NoteApp.Models.Note;

public class CreateNoteViewModel
{

    [Required(ErrorMessage = "Question text required")]
    [MinLength(20, ErrorMessage = "Minimum of 20 character required")]
    [MaxLength(150, ErrorMessage = "Maximum of 150 character required")]
    public string Title { get; set; }

    public string Content { get; set; }
}
