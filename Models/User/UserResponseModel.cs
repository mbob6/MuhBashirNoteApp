namespace NoteApp.Models.User
{
    public class UserResponseModel : BaseResponseModel
    {
        public UserViewModel Data { get; set; }
    }
    public class UsersViewModel : BaseResponseModel 
    {
        public List<UserViewModel> Data { get; set; }
    }
}
