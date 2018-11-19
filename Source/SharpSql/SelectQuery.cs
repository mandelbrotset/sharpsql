using SharpSql.Join;
using SharpSql.Order;
using SharpSql.Restriction;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SharpSql
{
    public interface ISelectQuery<TEntity> : ISqlElement
    {
        Expression<Func<TEntity, object>>[] Columns { get; }
        string Alias { get; }
        Expression<Func<TEntity>> AliasExpression { get; }
        OrderBy<TEntity> Order { get; }
        IList<IJoin<TEntity>> Joins { get; }

        IOrderedSelectQuery<TEntity> OrderBy(Expression<Func<object>> property, SortOrder sortOrder = SortOrder.Ascending);

        IJoin<TEntity> Join(Expression<Func<object>> joinAlias, JoinType joinType = JoinType.Inner);

        IRestrictionBuilder<TEntity> Where(object operand);
        IRestrictionBuilder<TEntity> Where(Expression<Func<object>> operand);
        ISqlElement Restriction { get; set; }
    }

    public class SelectQuery<TEntity> : ISelectQuery<TEntity>, IOrderedSelectQuery<TEntity>
    {
        public SelectQuery(Expression<Func<TEntity, object>>[] columns, Expression<Func<TEntity>> alias)
        {
            Columns = columns;
            var body = (MemberExpression)alias.Body;
            Alias = body.Member.Name;
            AliasExpression = alias;
            Restriction = new EmptyRestriction<TEntity>(this);
            Order = new OrderBy<TEntity>(this);
            Joins = new List<IJoin<TEntity>>();
        }

        public SelectQuery(ISelectQuery<TEntity> query)
        {
            Columns = query.Columns;
            Alias = query.Alias;
            AliasExpression = query.AliasExpression;
            Restriction = query.Restriction;
            Order = query.Order;
            Joins = query.Joins;
        }
        
        public Expression<Func<TEntity, object>>[] Columns { get; private set; }
        public string Alias { get; }
        public Expression<Func<TEntity>> AliasExpression { get; }
        public OrderBy<TEntity> Order { get; private set; }
        public IList<IJoin<TEntity>> Joins { get; set; }
        public ISqlElement Restriction { get; set; }

        public IJoin<TEntity> Join(Expression<Func<object>> joinAlias, JoinType joinType = JoinType.Inner)
        {
            var join = new Join<TEntity>(this, joinType, joinAlias);
            Joins.Add(join);
            return join;
        }

        public IOrderedSelectQuery<TEntity> OrderBy(Expression<Func<object>> property, SortOrder sortOrder = SortOrder.Ascending)
        {
            Order.AddOrderBy(property, sortOrder);
            return this;
        }

        public IOrderedSelectQuery<TEntity> ThenBy(Expression<Func<object>> property, SortOrder sortOrder = SortOrder.Ascending)
        {
            OrderBy(property, sortOrder);
            return this;
        }

        public string ToSql()
        {
            return SqlBuilder.ToSql(this);
        }
        
        public IRestrictionBuilder<TEntity> Where(Expression<Func<object>> operand)
        {
            return new Restriction<TEntity>(operand, this);
        }

        public IRestrictionBuilder<TEntity> Where(object operand)
        {
            return new Restriction<TEntity>(operand, this);
        }
    }
}
