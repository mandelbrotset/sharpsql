using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SharpSql.Restriction;

namespace SharpSql.Update
{
    public interface IUpdate<TEntity> : IUpdateBuilder<TEntity>, ISqlElement
    {
        IUpdate<TEntity> WithRestriction(IRestriction restriction);
    }

    public class Update<TEntity> : IUpdate<TEntity>
    {
        private readonly IList<Set<TEntity>> _sets;
        public IRestriction Restriction { get; set; }

        public Update()
        {
            _sets = new List<Set<TEntity>>();
        }

        public IUpdate<TEntity> Set(Expression<Func<TEntity, object>> property, object value)
        {
            _sets.Add(new Set<TEntity>(property, value));
            return this;
        }

        public string ToSql()
        {
            var tableName = typeof(TEntity).Name;
            var sql = $"UPDATE {tableName} SET {string.Join(", ", _sets.Select(x => x.ToSql()))}";
            
            var restriction = Restriction.ToSql();
            if (!string.IsNullOrWhiteSpace(restriction))
            {
                sql += $" WHERE {restriction}";
            }

            return sql;
        }

        public IUpdate<TEntity> WithRestriction(IRestriction restriction)
        {
            Restriction = restriction;
            return this;
        }
    }
}
