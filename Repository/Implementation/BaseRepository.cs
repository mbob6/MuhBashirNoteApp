using NoteApp.Entities;
using NoteApp.Repository;
using NoteApp.Context;
using System.Linq.Expressions;

namespace NoteApp.Repository.Implementations
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity, new()
    {
        public readonly NoteDbContext _context;

        protected BaseRepository(NoteDbContext context)
        {
            _context = context;
        }
        public T Create(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }

        public bool Exists(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Any(expression);

        }

        public T Get(string id)
        {
            return _context.Set<T>().Find(id);
        }

        public T Get(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().SingleOrDefault(expression);
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public List<T> GetAll(Expression<Func<T, bool>> expression = null)
        {
            return _context.Set<T>().Where(expression).ToList();
        }

        public List<T> GetAllByIds(List<string> ids)
        {
            return _context.Set<T>().Where(t => ids.Contains(t.Id)).ToList();
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public IReadOnlyList<T> SelectAll()
        {
            return _context.Set<T>().ToList();
        }

        public IReadOnlyList<T> SelectAll(Expression<Func<T, bool>> expression = null) 
        {
            return _context.Set<T>().Where(expression).ToList();
        }

        public T Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return entity;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
