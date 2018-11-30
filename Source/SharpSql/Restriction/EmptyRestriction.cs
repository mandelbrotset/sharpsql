using SharpSql.Select;
using System;
using System.Linq.Expressions;

namespace SharpSql.Restriction
{
    public class EmptyRestriction : IRestriction
    {
        public string ToSql()
        {
            return string.Empty;
        }
    }
}
