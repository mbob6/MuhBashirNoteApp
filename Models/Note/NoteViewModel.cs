using Microsoft.AspNetCore.Mvc;

namespace NoteApp.Models.Note;

public class NoteViewModel
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
        
    [BindProperty(SupportsGet = true)]
    public string SearchString { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
}
