using System;
using System.Linq.Expressions;

namespace SharpSql.Restriction.Operand
{
    public class PropertyOperand : IOperand
    {
        private readonly Expression<Func<object>> _property;

        public PropertyOperand(Expression<Func<object>> property)
        {
            _property = property;
        }

        public string ToSql()
        {
            return $"{_property.GetParentName()}.{_property.GetChildName()}";
        }
    }
}
