using System;
using System.Linq.Expressions;
using SharpSql.Restriction.Operand;

namespace SharpSql.Restriction
{
    public class Restriction<TEntity> : IRestriction<TEntity>, IRestrictionBuilder<TEntity>
    {
        private Operator _operator;
        private IOperand _leftOperand;
        private IOperand _rightOperand;
        private ISelectQuery<TEntity> _query;

        public Restriction(IOperand leftOperand, ISelectQuery<TEntity> query)
        {
            _query = query;
            _leftOperand = leftOperand;
        }

        public Restriction(object leftOperand, ISelectQuery<TEntity> query)
        {
            _query = query;
            _leftOperand = new ValueOperand(leftOperand);
        }

        public Restriction(Expression<Func<object>> leftOperand, ISelectQuery<TEntity> query)
        {
            _query = query;
            _leftOperand = new PropertyOperand(leftOperand);
        }
        
        public IRestriction<TEntity> EqualTo(Expression<Func<object>> operand)
        {
            _operator = Operator.Equals;
            _rightOperand = new PropertyOperand(operand);
            return this;
        }

        public IRestriction<TEntity> EqualTo(object operand)
        {
            _operator = Operator.Equals;
            _rightOperand = new ValueOperand(operand);
            return this;
        }

        public string ToSql()
        {
            return $"{_leftOperand.ToSql()} {GetOperator()} {_rightOperand.ToSql()}";
        }

        public ISelectQuery<TEntity> Build()
        {
            _query.Restriction = this;
            return _query;
        }
        
        private string GetOperator()
        {
            switch (_operator)
            {
                case Operator.Equals:
                    return "=";
                case Operator.GreaterThan:
                    return ">";
                case Operator.LesserThan:
                    return "<";
                case Operator.Like:
                    return "LIKE";
            }

            throw new NotImplementedException();
        }

        public ISelectQuery<TEntity> And(IRestriction<TEntity> restriction)
        {
            return ParentRestriction<TEntity>.CreateParentRestriction(this, LogicOperator.And, _query, restriction);
        }

        public IRestrictionBuilder<TEntity> And(Expression<Func<object>> operand)
        {
            return new ParentRestriction<TEntity>(this, LogicOperator.And, _query, operand);
        }

        public IRestrictionBuilder<TEntity> And(object operand)
        {
            return new ParentRestriction<TEntity>(this, LogicOperator.And, _query, operand);
        }
    }
}
