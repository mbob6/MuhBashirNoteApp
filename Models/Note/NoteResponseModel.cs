using Microsoft.AspNetCore.Mvc;

namespace NoteApp.Models.Note;

public class NotesResponseModel : BaseResponseModel
{
    public List<NoteViewModel> Data { get; set; }
}

public class NoteResponseModel : BaseResponseModel
{
    public NoteViewModel Data { get; set; }
}