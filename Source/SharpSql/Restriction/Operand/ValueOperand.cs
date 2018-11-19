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
            return $"'{_value.ToString()}'";
        }
    }
}
