using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ECommerce.Domain.Contracts
{
    public interface ISpecifications<TEntity,TKey> where TEntity:BaseEntity<TKey> 
    {
        ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; }
        Expression<Func<TEntity, bool>> Criteria { get; }
    }
}
