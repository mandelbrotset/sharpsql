using System;
using System.Linq.Expressions;

namespace SharpSql.Restriction
{
    public interface IRestriction<TEntity> : ISqlElement
    {
        ISelectQuery<TEntity> Build();
        ISelectQuery<TEntity> And(IRestriction<TEntity> restriction);
        IRestrictionBuilder<TEntity> And(Expression<Func<object>> operand);
        IRestrictionBuilder<TEntity> And(object operand);
    }
}
