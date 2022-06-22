using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace e_okul.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        void Remove();
        void Add(T entity);
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null);
        T GetFirstOrDefault(Expression<Func<T, bool>> filter);
    }
}
