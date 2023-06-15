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

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
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

            return RedirectToAction("Index", "Home");
        }

        public IActionResult GetNoteDetail(string id)
        {
            var response = _noteService.GetNote(id);

            if (response.Status is false)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(response.Data);
        }

        public IActionResult Update(string id)
        {
            var response = _noteService.GetNote(id);

            if (response.Status is false)
            {
                return RedirectToAction("Index", "Home");
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
                return View(request);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult DeleteNote([FromRoute] string id)
        {
            var response = _noteService.GetNote(id);

            if (response.Status is false)
            {
                return RedirectToAction("Index", "Home"); ;
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
