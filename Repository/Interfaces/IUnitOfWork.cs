namespace NoteApp.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository User { get; }
        IRoleRepository Role { get; }
        INoteRepository Note { get; }
        int SaveChanges();
    }
}
