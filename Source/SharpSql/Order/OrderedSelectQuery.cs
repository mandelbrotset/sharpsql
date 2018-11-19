using System;
using System.Linq.Expressions;

namespace SharpSql.Order
{
    public interface IOrderedSelectQuery<TEntity> : ISelectQuery<TEntity>
    {
        OrderedSelectQuery<TEntity> ThenBy(Expression<Func<object>> property, SortOrder sortOrder = SortOrder.Ascending);
    }

    public class OrderedSelectQuery<TEntity> : SelectQuery<TEntity>, IOrderedSelectQuery<TEntity>
    {
        public OrderedSelectQuery(SelectQuery<TEntity> query) : base(query)
        {
        }

        public OrderedSelectQuery<TEntity> ThenBy(Expression<Func<object>> property, SortOrder sortOrder = SortOrder.Ascending)
        {
            OrderBy(property, sortOrder);
            return this;
        }
    }
}
