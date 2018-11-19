using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SharpSql.Order
{
    public class OrderBy<TEntity> : ISqlElement
    {
        private readonly ISelectQuery<TEntity> _query;

        public OrderBy(ISelectQuery<TEntity> query)
        {
            _query = query;
            Sortings = new List<(Expression<Func<object>> property, SortOrder order)>();
        }

        public IList<(Expression<Func<object>> property, SortOrder order)> Sortings { get; set; }

        public void AddOrderBy(Expression<Func<object>> property, SortOrder sortOrder)
        {
            Sortings.Add((property, sortOrder));
        }

        public string ToSql()
        {
            var result = new List<string>();

            foreach (var (property, order) in _query.Order.Sortings)
            {
                var propertyName = property.GetPropertyName();
                var sortOrder = order == SortOrder.Descending ? " DESC" : string.Empty;
                var alias = property.GetParentName();

                result.Add($"{_query.Alias}.{propertyName}{sortOrder}");
            }

            if (!result.Any())
            {
                return string.Empty;
            }

            return $" ORDER BY {string.Join(", ", result)}";
        }
    }
}
