using Microsoft.AspNetCore.Mvc;
using NoteApp.Models.Note;
using NoteApp.Services.Interfaces;

namespace NoteApp.Controllers
{
    public class NoteController : Controller
    {
        private readonly INoteService _noteService;

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        public IActionResult Index()
        {
            var notes = _noteService.GetAllNotes();

            ViewData["Message"] = notes.Message;
            ViewData["Status"] = notes.Status;

            return View(notes.Data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateNoteViewModel request)
        {
            var response = _noteService.CreateNote(request);

            if (response.Status is false)
            {
                return View(request);
            }

            return RedirectToAction("Index", "Note");
        }

        public IActionResult GetNoteDetail(Guid id)
        {
            var response = _noteService.GetNote(id);

            if (response.Status is false)
            {
                return RedirectToAction("Index", "Note");
            }

            return View(response.Data);
        }

        public IActionResult Update(Guid id)
        {
            var response = _noteService.GetNote(id);

            if (response.Status is false)
            {
                return RedirectToAction("Index", "Note");
            }

            var viewModel = new UpdateNoteViewModel
            {
                Id = response.Data.Id,
                Title = response.Data.Title,
                Content = response.Data.Content
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Update(Guid id, UpdateNoteViewModel request)
        {
            var response = _noteService.UpdateNote(id, request);

            if (response.Status is false)
            {
                return View(request);
            }

            return RedirectToAction("Index", "Note");
        }

        [HttpPost]
        public IActionResult DeleteNote([FromRoute] Guid id)
        {
            var response = _noteService.GetNote(id);

            if (response.Status is false)
            {
                return RedirectToAction("Index", "Note"); ;
            }

            return RedirectToAction("Index", "Note");
        }
    }
}
