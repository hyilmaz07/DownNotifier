using DownNotifier.Core.Entities;
using DownNotifier.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DownNotifier.Core.DataAccess
{
    public interface IRepositoryBase<T> where T : EntityBase
    {
        Task<T> GetAsync(GenericExpression<T> parameters = null);

        Task<IEnumerable<T>> GetAllAsync(GenericExpression<T> parameters = null);

        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task<bool> DeleteAsync(T entity);
    }
}
