using DownNotifier.Core.Entities;
using DownNotifier.Core.Helper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DownNotifier.Core.DataAccess
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        private readonly BaseContext _context;
        public RepositoryBase(BaseContext context)
        {
            _context = context;
        }
        public virtual async Task<T> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public virtual async Task<T> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return entity;
        }
        public virtual async Task<bool> DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.Entry(entity).State = EntityState.Deleted;
            return await _context.SaveChangesAsync() > 0;
        }
        public virtual async Task<IEnumerable<T>> GetAllAsync(GenericExpression<T> parameters = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (parameters == null)
                return query;

            if (parameters.disableTracking)
                query = query.AsNoTracking();

            if (parameters.includePaths != null)
                query = parameters.includePaths(query);

            if (parameters.predicate != null)
                query = query.Where(parameters.predicate);

            if (parameters.orderBy != null)
                return await parameters.orderBy(query).ToListAsync();

            string sql = query.ToQueryString();

            return await query.ToListAsync();
        }

        public virtual async Task<T> GetAsync(GenericExpression<T> parameters = null)
        {
            IQueryable<T> query = _context.Set<T>().Where(a => !a.IsDelete);

            if (parameters == null)
                return await query.FirstOrDefaultAsync();

            if (parameters.disableTracking)
                query = query.AsNoTracking();

            if (parameters.includePaths != null)
                query = parameters.includePaths(query);

            if (parameters.predicate != null)
                query = query.Where(parameters.predicate);

            if (parameters.orderBy != null)
                return await parameters.orderBy(query).FirstOrDefaultAsync();

            return await query.FirstOrDefaultAsync();
        }

    }
}
