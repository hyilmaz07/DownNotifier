using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DownNotifier.Core.Helper
{
    public class GenericExpression<T>
    {
        public GenericExpression(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includePaths = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            bool disableTracking = true)
        {
            this.predicate = predicate;
            this.includePaths = includePaths;
            this.orderBy = orderBy;
            this.disableTracking = disableTracking;
        }

        public Expression<Func<T, bool>> predicate { get; set; }
        public Func<IQueryable<T>, IOrderedQueryable<T>> orderBy { get; set; }
        public bool disableTracking { get; set; }
        public Func<IQueryable<T>, IIncludableQueryable<T, object>> includePaths { get; set; }
    }
}
