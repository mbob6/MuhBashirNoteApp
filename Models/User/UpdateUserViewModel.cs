namespace NoteApp.Models.User
{
    public class UpdateUserViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string password { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public Guid RoleId { get; set; }
        public string PhoneNumber { get; set; }
    }
}
