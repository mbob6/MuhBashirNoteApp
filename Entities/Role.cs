namespace NoteApp.Entities
{
    public class Role : BaseEntity
    {
        public string RoleName { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastModified { get; set; }
        public ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}
