using Microsoft.AspNetCore.Mvc;
using NoteApp.Entities;
using NoteApp.Models;
using NoteApp.Models.Note;
using NoteApp.Repository.Interfaces;
using NoteApp.Services.Interfaces;
using System.Security.Claims;

namespace NoteApp.Services.Implementation
{
    public class NoteService : INoteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;

        public NoteService(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;
        }

        public BaseResponseModel CreateNote(CreateNoteViewModel model)
        {
            var response = new BaseResponseModel();
            var createdBy = _contextAccessor.HttpContext.User.Identity.Name;
            var userIdClaim = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var noteExist = _unitOfWork.Note.Exists(n => (n.Title == model.Title)
                                                        && (n.CreatedBy == createdBy));
            var user = _unitOfWork.User.Get(userIdClaim);

            if (noteExist)
            {
                response.Message = $"Note with Title {model.Title} already exist ";
                return response;
            }

            var note = new Note
            {
                Title = model.Title,
                Content = model.Content,
                DateCreated = DateTime.Now,
                UserId = user.Id,
                CreatedBy = createdBy
            };

            try
            {
                _unitOfWork.Note.Create(note);
                _unitOfWork.SaveChanges();
                response.Message = "Note Successfully Created ";
                response.Status = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"Unable to Create Note : {ex.Message}";
                return response;
            }
        }

        public BaseResponseModel DeleteNote(string id)
        {
            var response = new BaseResponseModel();
            var note = _unitOfWork.Note.Get(id);

            var noteExist = _unitOfWork.Note.Exists(n => (n.Id == id)
                                                && (n.IsDeleted == false));
            if (!noteExist)
            {
                response.Message = "Note Does not exist";
                return response;
            }
            note.IsDeleted = true;

            try
            {
                _unitOfWork.Note.Update(note);
                _unitOfWork.SaveChanges();
                response.Message = "Note Deleted Successfully ";
                response.Status = true;

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"Unable to delete Note : {ex.Message}";
                return response;
            }

        }

        public NotesResponseModel GetAllNotes(string searchString)
        {
            var response = new NotesResponseModel();
            var createdBy = _contextAccessor.HttpContext.User.Identity.Name;
            var notes = _unitOfWork.Note.GetAll(n => (n.IsDeleted == false)
                                            && (n.CreatedBy == createdBy));

            if (!string.IsNullOrEmpty(searchString))
            {
                notes = _unitOfWork.Note.GetAll(n => n.Title.Contains(searchString));
            }

            if (notes.Count == 0)
            {
                response.Message = "No Records Found ";
                return response;
            }
            try
            {
                response.Data = notes.Select(n => new NoteViewModel
                {
                    Id = n.Id,
                    Title = n.Title,
                    Content = n.Content,
                    DateCreated = n.DateCreated,
                    DateUpdated = n.DateUpdated
                }).ToList();
                response.Message = "Notes Successfully retrieved";
                response.Status = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"An error occured : {ex.Message}";
                return response;
            }
        }

        public NoteResponseModel GetNote(string id)
        {
            var response = new NoteResponseModel();
            try
            {
                var noteExist = _unitOfWork.Note.Exists(n => (n.Id == id)
                                                && (n.IsDeleted == false));
                if (!noteExist)
                {
                    response.Message = "Note does not exist";
                    return response;
                }
                var note = _unitOfWork.Note.Get(id);

                response.Data = new NoteViewModel
                {
                    Title = note.Title,
                    Content = note.Content,
                    DateCreated = note.DateCreated,
                    DateUpdated = note.DateUpdated
                };
                response.Message = "Note Successfully retrieved";
                response.Status = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"An error occured : {ex.Message}";
                return response;
            }
        }

        public BaseResponseModel UpdateNote(string id, UpdateNoteViewModel model)
        {
            var response = new BaseResponseModel();
            var noteExist = _unitOfWork.Note.Exists(n => (n.Id == id) && (n.IsDeleted == false));
            if (!noteExist)
            {
                response.Message = "Note does not exist";
                return response;
            }

            var role = _unitOfWork.Note.Get(id);
            role.Title = model.Title;
            role.Content = model.Content;
            role.DateUpdated = DateTime.Now;
            try
            {
                _unitOfWork.Note.Update(role);
                _unitOfWork.SaveChanges();
                response.Message = "Note Updated Successfully ";
                response.Status = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"An error occured : {ex.Message}";
                return response;
            }
        }
    }
}
