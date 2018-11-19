using SharpSql.Restriction;

namespace SharpSql.Restriction.Operand
{
    public class RestrictionOperand<TEntity> : IOperand
    {
        private IRestriction<TEntity> _restriction;

        public RestrictionOperand(IRestriction<TEntity> restriction)
        {
            _restriction = restriction;
        }

        public string ToSql()
        {
            return _restriction.ToSql();
        }
    }
}
