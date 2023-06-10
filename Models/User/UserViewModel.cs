namespace NoteApp.Models.User
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string  Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Dob { get; set; }
        public string RoleName { get; set; }
        public Guid RoleId { get; set; }
        public DateTime DateJoined { get; set; }
    }
}
