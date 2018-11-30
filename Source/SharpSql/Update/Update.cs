using SharpSql.Restriction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SharpSql.Update
{
    public interface IUpdate<TEntity> : IUpdateBuilder<TEntity>, ISqlElement
    {
        //IRestrictionBuilder<TEntity> Where(object operand);
        //IRestrictionBuilder<TEntity> Where(Expression<Func<object>> operand);
    }

    public class Update<TEntity> : IUpdate<TEntity>
    {
        private readonly IList<Set<TEntity>> _sets;

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


            return $"UPDATE {tableName} SET {string.Join(", ", _sets.Select(x => x.ToSql()))}";
        }

        //public IRestrictionBuilder<TEntity> Where(object operand)
        //{
        //    throw new NotImplementedException();
        //}

        //public IRestrictionBuilder<TEntity> Where(Expression<Func<object>> operand)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
