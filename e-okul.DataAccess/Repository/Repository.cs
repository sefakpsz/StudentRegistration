using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using e_okul.DataAccess.Repository.IRepository;
using e_okul.DataAccess.Data;

namespace e_okul.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
        }
        public void Add(T entity)
        {
            _db.Add(entity);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.FirstOrDefault();
        }

        public void Remove()
        {
            throw new NotImplementedException();
        }
    }
}