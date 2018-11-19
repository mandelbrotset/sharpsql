using System;
using System.Linq.Expressions;

namespace SharpSql.Restriction
{
    public class EmptyRestriction<TEntity> : IRestriction<TEntity>
    {
        private readonly ISelectQuery<TEntity> _query;

        public EmptyRestriction(ISelectQuery<TEntity> query)
        {
            _query = query;
        }

        public IRestrictionBuilder<TEntity> And(object operand)
        {
            throw new System.NotImplementedException();
        }

        public ISelectQuery<TEntity> And(IRestriction<TEntity> restriction)
        {
            throw new NotImplementedException();
        }

        public IRestrictionBuilder<TEntity> And(Expression<Func<object>> operand)
        {
            throw new NotImplementedException();
        }

        public ISelectQuery<TEntity> Build()
        {
            return _query;
        }

        public string ToSql()
        {
            return string.Empty;
        }
    }
}
