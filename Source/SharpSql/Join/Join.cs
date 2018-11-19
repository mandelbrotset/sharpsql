using System;
using System.Linq.Expressions;

namespace SharpSql.Join
{
    public interface IJoin<TEntity>
    {
        ISelectQuery<TEntity> On(Expression<Func<object>> property1, Expression<Func<object>> property2);
        string ToSql();
    }

    public class Join<TEntity> : IJoin<TEntity>, ISqlElement
    {
        private readonly ISelectQuery<TEntity> _query;
        private readonly JoinType _joinType;

        public JoinPart Property1 { get; set; }
        public JoinPart Property2 { get; set; }
        public string JoinTable { get; set; }
        public string JoinAlias { get; set; }

        public Join(ISelectQuery<TEntity> query, JoinType joinType, Expression<Func<object>> joinAlias)
        {
            _query = query;
            _joinType = joinType;
            JoinAlias = joinAlias.GetPropertyName();
            JoinTable = joinAlias.GetPropertyType();
        }
        
        public ISelectQuery<TEntity> On(Expression<Func<object>> property1, Expression<Func<object>> property2)
        {
            Property1 = new JoinPart(property1);
            Property2 = new JoinPart(property2);
            return _query;
        }

        public string ToSql()
        {
            return $"{GetJoinType()} {JoinTable} {JoinAlias} ON {Property1.Alias}.{Property1.Property} = {Property2.Alias}.{Property2.Property}";
        }

        private string GetJoinType()
        {
            switch (_joinType)
            {
                case JoinType.Inner:
                    return "INNER JOIN";
                case JoinType.LeftOuter:
                    return "LEFT OUTER JOIN";
                case JoinType.RightOuter:
                    return "RIGHT OUTER JOIN";
                case JoinType.FullOuter:
                    return "FULL OUTER JOIN";
            }

            throw new ArgumentException("Unknown jointype");
        }
    }
}
