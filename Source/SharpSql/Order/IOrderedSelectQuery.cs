using System;
using System.Linq.Expressions;

namespace SharpSql.Order
{
    public interface IOrderedSelectQuery<TEntity> : ISelectQuery<TEntity>
    {
        IOrderedSelectQuery<TEntity> ThenBy(Expression<Func<object>> property, SortOrder sortOrder = SortOrder.Ascending);
    }
}
