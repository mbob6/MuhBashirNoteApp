namespace NoteApp.Entities
{
    public class Note
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
