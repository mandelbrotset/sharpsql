using System;
using System.Linq.Expressions;

namespace SharpSql.Restriction
{
    public interface IRestrictionBuilder
    {
        IRestriction EqualTo(object value);
        IRestriction EqualTo(Expression<Func<object>> property);
    }
}
