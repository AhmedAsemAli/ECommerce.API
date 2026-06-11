using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presistence
{
    internal static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity>entryPoint,ISpecifications<TEntity,TKey> specifications) where TEntity : BaseEntity<TKey>
        {
            var Query = entryPoint;
            if (specifications is not null )
            {
                if (specifications.Criteria is not null)
                {
                    Query = Query.Where(specifications.Criteria);
                }
                if (specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Any())
                {
                    Query = specifications.IncludeExpressions.Aggregate(Query, (CurrentQuery, includeEx) => CurrentQuery.Include(includeEx));
                 
                }
            }
            return Query;
        }
    }
}
