using NoteApp.Entities;
using NoteApp.Models;
using NoteApp.Models.Auth;
using NoteApp.Models.User;
using NoteApp.Repository.Interfaces;
using NoteApp.Services.Interfaces;

namespace NoteApp.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserService(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;
        }

        public UserResponseModel GetUser(Guid userId)
        {
            var response = new UserResponseModel();
            var user = _unitOfWork.User.GetUser(u => u.Id == userId);

            if (user == null)
            {
                response.Message = "User does not exist ";
                return response;
            }

            response.Data = new UserViewModel
            {
                Id = userId,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                DateJoined = user.DateJoined,
            };
            response.Message = "User Successfully retrieved ";
            response.Status = true;
            return response;
        }

        public UserResponseModel Login(LoginViewModel request)
        {
            var response = new UserResponseModel();

            try
            {
                var user = _unitOfWork.User.GetUser(u => (u.UserName.ToLower() == request.UserName.ToLower())
                                                    || (u.Password.ToLower() == request.Password.ToLower()));
                if (user == null)
                {
                    response.Message = "Account does not exist";
                    return response;
                }

                if (request.Password != user.Password)
                {
                    response.Message = "Incorrect Username or Password";
                    return response;
                }

                response.Data = new UserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    DateJoined = user.DateJoined,
                };
                response.Message = "Login Successful";
                response.Status = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"An error occured : {ex.Message}";
                return response;
            }
        }

        public BaseResponseModel Register(SignUpViewModel request, string roleName = null)
        {
            var response = new BaseResponseModel();
            var userExist = _unitOfWork.User.Exists(u => (request.UserName == u.UserName) 
                                                ||(u.Email == request.Email));
            var createdBy = _contextAccessor.HttpContext.User.Identity.Name;
            if (userExist)
            {
                response.Message = $"User with UserName {request.UserName} already exist";
                return response;
            }
            roleName ??= "AppUser";
            var role = _unitOfWork.Role.Get(r => r.RoleName == roleName);

            if (role is null)
            {
                response.Message = "Role does not exist";
                return response;
            }

            var user = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                Password = request.Password,
                PhoneNumber = request.PhoneNumber,
            };
            try
            {
                _unitOfWork.User.Create(user);
                _unitOfWork.SaveChanges();
                response.Message = "You have Sign Up Successfully";
                response.Status = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"Unable to SignUp, An error occured : {ex.Message}";
                return response;
            }
        }
    }
}
