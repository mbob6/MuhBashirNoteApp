using NoteApp.Models;
using NoteApp.Models.Role;

namespace NoteApp.Services.Interfaces
{
    public interface IRoleService
    {
        public BaseResponseModel CreateRole(CreateRoleViewModel model);
        public BaseResponseModel UpdateRole(string id, UpdateRoleViewModel model);
        public BaseResponseModel DeleteRole(string id);
        public RoleResponseModel GetRole(string id );
        public RolesResponseModel GetAllRoles();
    }
}
