using MassTransit;
namespace NoteApp.Entities
{
    public class BaseEntity
    {
        public string Id { get; set; } = NewId.Next().ToSequentialGuid().ToString();
        public bool IsDeleted { get; set; }
    }
}
