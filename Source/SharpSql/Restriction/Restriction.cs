using System;
using System.Linq.Expressions;
using SharpSql.Restriction.Operand;

namespace SharpSql.Restriction
{
    public class Restriction : IRestriction, IRestrictionBuilder
    {
        private IOperand _leftOperand;
        private IOperand _rightOperand;
        private Operator _operator;

        private Restriction(IOperand leftOperand)
        {
            _leftOperand = leftOperand;
        }

        public static IRestrictionBuilder Where(object value)
        {
            return new Restriction(new ValueOperand(value));
        }

        public static IRestrictionBuilder Where(Expression<Func<object>> property)
        {
            return new Restriction(new PropertyOperand(property));
        }

        public IRestriction EqualTo(object value)
        {
            _operator = Operator.Equals;
            _rightOperand = new ValueOperand(value);
            return this;
        }

        public IRestriction EqualTo(Expression<Func<object>> property)
        {
            _operator = Operator.Equals;
            _rightOperand = new PropertyOperand(property);
            return this;
        }

        public string ToSql()
        {
            return $"{_leftOperand.ToSql()} {GetOperator()} {_rightOperand.ToSql()}";
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
    }
}
