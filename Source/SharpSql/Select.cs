using System;
using System.Linq.Expressions;

namespace SharpSql
{
    public interface ISelect
    {
        IColumnSelector<TEntity> From<TEntity>(Expression<Func<TEntity>> alias);
    }

    public class Select : ISelect
    {
        public IColumnSelector<TEntity> From<TEntity>(Expression<Func<TEntity>> alias)
        {
            return new ColumnSelector<TEntity>(alias);
        }
    }
}
