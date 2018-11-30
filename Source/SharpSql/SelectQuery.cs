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
        string Alias { get; }
        Expression<Func<TEntity>> AliasExpression { get; }
        Expression<Func<TEntity, object>>[] Columns { get; }
        OrderBy<TEntity> Order { get; }
        IList<IJoin<TEntity>> Joins { get; }
        IRestriction Restriction { get; set; }

        IJoin<TEntity> Join(Expression<Func<object>> joinAlias, JoinType joinType = JoinType.Inner);
        ISelectQuery<TEntity> WithRestriction(IRestriction restriction);
        IOrderedSelectQuery<TEntity> OrderBy(Expression<Func<object>> property, SortOrder sortOrder = SortOrder.Ascending);
    }

    public class SelectQuery<TEntity> : ISelectQuery<TEntity>, IOrderedSelectQuery<TEntity>
    {
        public SelectQuery(Expression<Func<TEntity, object>>[] columns, Expression<Func<TEntity>> alias)
        {
            Columns = columns;
            var body = (MemberExpression)alias.Body;
            Alias = body.Member.Name;
            AliasExpression = alias;
            Restriction = new EmptyRestriction();
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
        public IRestriction Restriction { get; set; }

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
        
        public ISelectQuery<TEntity> WithRestriction(IRestriction restriction)
        {
            Restriction = restriction;
            return this;
        }
    }
}
