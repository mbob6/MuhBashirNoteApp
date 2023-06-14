using NoteApp.Models.Auth;
using NoteApp.Models.User;
using NoteApp.Models;

namespace NoteApp.Services.Interfaces
{
    public interface IUserService
    {
        UserResponseModel GetUser(string userId);
        BaseResponseModel Register(SignUpViewModel request, string roleName = null);
        UserResponseModel Login(LoginViewModel request);
    }
}
