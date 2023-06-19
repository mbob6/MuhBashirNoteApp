using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Models.Note;
using NoteApp.Services.Interfaces;

namespace NoteApp.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        private readonly INoteService _noteService;
        private readonly INotyfService _notyf;
        public NoteController(INoteService noteService, INotyfService notyf)
        {
            _noteService = noteService;
            _notyf = notyf;
        }
        [Authorize]
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
                _notyf.Error(response.Message);
                return View(request);
            }

            _notyf.Success(response.Message);
            return RedirectToAction("Index", "Note");
        }

        public IActionResult GetNoteDetail(string id)
        {
            var response = _noteService.GetNote(id);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return RedirectToAction("Index", "Note");
            }

            _notyf.Success(response.Message);
            return View(response.Data);
        }

        public IActionResult Update(string id)
        {
            var response = _noteService.GetNote(id);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
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
        public IActionResult Update(string id, UpdateNoteViewModel request)
        {
            var response = _noteService.UpdateNote(id, request);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return View(request);
            }
            _notyf.Success(response.Message);

            return RedirectToAction("Index", "Note");
        }

        [HttpPost]
        public IActionResult DeleteNote([FromRoute] string id)
        {
            var response = _noteService.DeleteNote(id);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return RedirectToAction("Index", "Note"); ;
            }

            _notyf.Success(response.Message);
            return RedirectToAction("Index", "Note");
        }
    }
}
