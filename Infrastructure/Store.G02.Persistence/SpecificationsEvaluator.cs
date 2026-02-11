using Microsoft.EntityFrameworkCore;
using Store.G02.Domain.Contracts;
using Store.G02.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Persistence
{
    public static class SpecificationsEvaluator
    {
        public static IQueryable<TEntity> GetQuery<TKey, TEntity>(IQueryable<TEntity> inputQuery, ISpecifications<TKey, TEntity> spec) where TEntity : BaseEntity<TKey>
        {
            var query = inputQuery;
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }
            if (spec.OrderBy is not null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            else if (spec.OrderByDescending is not null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }

            if(spec.IsPagination)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

                query = spec.Includes.Aggregate(query, (current, includeExpression) => current.Include(includeExpression));
            return query;
        }
    }
}
