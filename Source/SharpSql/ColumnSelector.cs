using SharpSql.Select;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SharpSql
{
    public interface IColumnSelector<TEntity>
    {
        ISelectQuery<TEntity> WithColumns(params Expression<Func<TEntity, object>>[] columns);

        ISelectQuery<TEntity> WithColumnsInEntity();
    }

    public class ColumnSelector<TEntity> : IColumnSelector<TEntity>
    {
        private readonly Expression<Func<TEntity>> _alias;

        public ColumnSelector(Expression<Func<TEntity>> alias)
        {
            _alias = alias;
        }
        
        public ISelectQuery<TEntity> WithColumns(params Expression<Func<TEntity, object>>[] columns)
        {
            return new SelectQuery<TEntity>(columns, _alias);
        }

        public ISelectQuery<TEntity> WithColumnsInEntity()
        {
            var columns = typeof(TEntity).GetProperties<TEntity>();
            return new SelectQuery<TEntity>(columns.ToArray(), _alias);
        }
    }
}
