using System;
using System.Linq.Expressions;

namespace SharpSql.Restriction
{
    public interface IRestrictionBuilder<TEntity>
    {
        IRestriction<TEntity> EqualTo(object operand);
        IRestriction<TEntity> EqualTo(Expression<Func<object>> operand);
    }
}
