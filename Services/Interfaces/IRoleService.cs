using NoteApp.Models;
using NoteApp.Models.Role;

namespace NoteApp.Services.Interfaces
{
    public interface IRoleService
    {
        public BaseResponseModel CreateRole(CreateRoleViewModel model);
        public BaseResponseModel UpdateRole(Guid id, UpdateRoleViewModel model);
        public BaseResponseModel DeleteRole(Guid id);
        public RoleResponseModel GetRole(Guid id );
        public RolesResponseModel GetAllRoles();
    }
}
