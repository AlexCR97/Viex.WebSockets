using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Viex.WebSockets.Core.Repositories
{
    public interface IRepository<T>
    {
        Task<int> Add(T model);
        Task Edit(int id, T model);
        Task<T> GetFirst(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate);
        Task Remove(int id);
    }
}
