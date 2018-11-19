using System;

namespace SharpSql.Restriction.Operand
{
    public class ValueOperand : IOperand
    {
        private object _value;

        public ValueOperand(object value)
        {
            _value = value;
        }

        public string ToSql()
        {
            if (_value is string || _value is Guid)
            {
                return $"'{_value.ToString()}'";
            }

            if (_value is int || _value is long || _value is short)
            {
                return $"{_value.ToString()}";
            }

            throw new NotImplementedException($"The value type { _value.GetType() } is not supported.");
        }
    }
}
