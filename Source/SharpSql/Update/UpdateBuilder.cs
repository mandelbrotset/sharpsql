using System;
using System.Linq.Expressions;

namespace SharpSql.Update
{
    public interface IUpdateBuilder<TEntity>
    {
        IUpdate<TEntity> Set(Expression<Func<TEntity, object>> property, object value);
    }

    public class UpdateBuilder<TEntity> : IUpdateBuilder<TEntity>
    {
        public UpdateBuilder()
        {

        }

        public IUpdate<TEntity> Set(Expression<Func<TEntity, object>> property, object value)
        {
            var update = new Update<TEntity>();
            update.Set(property, value);
            return update;
        }
    }
}
