using NoteApp.Entities;
using NoteApp.Models;
using NoteApp.Models.Role;
using NoteApp.Repository.Interfaces;
using NoteApp.Services.Interfaces;

namespace NoteApp.Services.Implementation
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public BaseResponseModel CreateRole(CreateRoleViewModel model)
        {
            var response = new BaseResponseModel();
            var roleExist = _unitOfWork.Role.Exists(r => model.RoleName == r.RoleName);

            if (roleExist)
            {
                response.Message = $"Role with Name {model.RoleName} already exist ";
                return response;
            }

            var role = new Role
            {
                RoleName = model.RoleName,
                Description = model.Description,
            };

            try
            {
                _unitOfWork.Role.Create(role);
                _unitOfWork.SaveChanges();
                response.Status = true;
                response.Message = "Role Created Succesfully ";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = $" Failed To Create Role {ex.Message}";
                return response;
            }
        }

        public BaseResponseModel DeleteRole(Guid id)
        {
            var response = new BaseResponseModel();
            var roleExist = _unitOfWork.Role.Exists(r => id == r.Id);
            var role = _unitOfWork.Role.Get(id);

            if (!roleExist)
            {
                response.Message = "Role Does not exist";
                return response;
            }

            if (role.RoleName == "Admin" || role.RoleName == "AppUser")
            {
                response.Message = "Role Cannot be Deleted";
                return response;
            }

            role.IsDeleted = true;

            try
            {
                _unitOfWork.Role.Update(role);
                _unitOfWork.SaveChanges();
                response.Message = "Role Deleted Successfully ";
                response.Status = true;

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"Cannot delete Role {ex.Message}";
                return response;
            }
        }

        public RolesResponseModel GetAllRoles()
        {
            var response = new RolesResponseModel();
            try
            {
                var roles = _unitOfWork.Role.GetAll(r => r.IsDeleted == false);

                if (roles.Count == 0)
                {
                    response.Message = "No Records found";
                    return response;
                }
                response.Data = roles.Select(r => new RoleViewModel
                {
                    Id = r.Id,
                    Description = r.Description,
                }).ToList();
                response.Status = true;
                response.Message = "Role Data Successfully Retrieved ";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"An error occured {ex.Message}";
                return response;
            }
        }

        public RoleResponseModel GetRole(Guid id)
        {
            var response = new RoleResponseModel();

            try
            {
                var roleExist = _unitOfWork.Role.Exists(r => (r.Id == id)
                                                    && (r.IsDeleted == false));
                if (!roleExist)
                {
                    response.Message = "Role does not exist ";
                    return response;
                }

                var role = _unitOfWork.Role.Get(id);
                response.Data = new RoleViewModel
                {
                    Id = id,
                    Description = role.Description,
                };
                response.Message = "Role Retrieved Successfully ";
                response.Status = true;

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"Unable To Retrieve Role : {ex.Message}";
                return response;
            }
        }

        public BaseResponseModel UpdateRole(Guid id, UpdateRoleViewModel model)
        {
            var response = new BaseResponseModel();
            var roleExist = _unitOfWork.Role.Exists(r => (r.Id == id) 
                                                && (r.IsDeleted == false));
            if (!roleExist)
            {
                response.Message = "Role does not exist ";
                return response;
            }

            var role = _unitOfWork.Role.Get(id);
            role.RoleName = model.RoleName;
            role.Description = model.Description;
            try
            {
                _unitOfWork.Role.Update(role);
                _unitOfWork.SaveChanges();
                response.Message = "Role Updated Successfully ";
                response.Status = true;

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"An error occured {ex.Message}";
                return response;
            }
        }
    }
}
