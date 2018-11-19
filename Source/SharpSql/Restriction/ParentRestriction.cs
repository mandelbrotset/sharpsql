using SharpSql.Restriction.Operand;
using System;
using System.Linq.Expressions;

namespace SharpSql.Restriction
{
    public class ParentRestriction<TEntity> : IRestriction<TEntity>, IRestrictionBuilder<TEntity>
    {
        private readonly ISelectQuery<TEntity> _query;
        private readonly IOperand _tempLeftOperator;

        public ParentRestriction(IRestriction<TEntity> leftChild, LogicOperator logicOperator, ISelectQuery<TEntity> query)
        {
            _query = query;
            LeftChild = new RestrictionOperand<TEntity>(leftChild);
            Operator = logicOperator;
        }

        public ParentRestriction(IRestriction<TEntity> leftChild, LogicOperator logicOperator, ISelectQuery<TEntity> query, object leftValue) : this(leftChild, logicOperator, query)
        {
            _tempLeftOperator = new ValueOperand(leftValue);
        }

        public ParentRestriction(IRestriction<TEntity> leftChild, LogicOperator logicOperator, ISelectQuery<TEntity> query, Expression<Func<object>> leftProperty) : this(leftChild, logicOperator, query)
        {
            _tempLeftOperator = new PropertyOperand(leftProperty);
        }

        public ParentRestriction(IRestriction<TEntity> leftChild, LogicOperator logicOperator, ISelectQuery<TEntity> query, Expression<Func<object>> leftProperty, IRestriction<TEntity> rightChild) : this(leftChild, logicOperator, query)
        {
            RightChild = new RestrictionOperand<TEntity>(rightChild);
        }

        public LogicOperator Operator { get; set; }
        public IOperand LeftChild { get; set; }
        public IOperand RightChild { get; set; }

        public static ISelectQuery<TEntity> CreateParentRestriction(IRestriction<TEntity> leftChild, LogicOperator logicOperator, ISelectQuery<TEntity> query, IRestriction<TEntity> rightChild)
        {
            var parentRestriction = new ParentRestriction<TEntity>(leftChild, logicOperator, query, rightChild);
            query.Restriction = parentRestriction;
            return query;
        }

        public IRestriction<TEntity> EqualTo(object operand)
        {
            var rightRestriction = new Restriction<TEntity>(_tempLeftOperator, _query)
                .EqualTo(operand);
            RightChild = new RestrictionOperand<TEntity>(rightRestriction);
            return this;
        }

        public IRestriction<TEntity> EqualTo(Expression<Func<object>> operand)
        {
            var rightRestriction = new Restriction<TEntity>(_tempLeftOperator, _query)
                .EqualTo(operand);
            RightChild = new RestrictionOperand<TEntity>(rightRestriction);
            return this;
        }
        
        public ISelectQuery<TEntity> Build()
        {
            _query.Restriction = this;
            return _query;
        }

        public string ToSql()
        {
            return $"{LeftChild.ToSql()} {GetOperator()} {RightChild.ToSql()}";
        }

        private string GetOperator()
        {
            switch (Operator)
            {
                case LogicOperator.And:
                    return "AND";
                case LogicOperator.Or:
                    return "OR";
            }

            throw new NotImplementedException();
        }

        public IRestrictionBuilder<TEntity> And(object operand)
        {
            throw new NotImplementedException();
        }

        public IRestrictionBuilder<TEntity> And(Expression<Func<object>> operand)
        {
            throw new NotImplementedException();
        }

        ISelectQuery<TEntity> IRestriction<TEntity>.And(IRestriction<TEntity> restriction)
        {
            throw new NotImplementedException();
        }
    }
}
