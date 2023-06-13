namespace NoteApp.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public Role Role { get; set; }
        public Guid RoleId { get; set; }
        public string Email { get; set; }
        public ICollection<Note> Notes { get; set; } = new HashSet<Note>();
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public DateTime DateJoined { get; set; }
    }
}
