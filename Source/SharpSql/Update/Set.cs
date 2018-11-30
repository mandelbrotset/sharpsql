using System;
using System.Linq.Expressions;

namespace SharpSql.Update
{
    public class Set<TEntity> : ISqlElement
    {
        private readonly Expression<Func<TEntity, object>> _property;
        private readonly object _value;

        public Set(Expression<Func<TEntity, object>> property, object value)
        {
            _property = property;
            _value = value;
        }

        public string ToSql()
        {
            var propertyName = _property.GetPropertyName();

            string valueString;

            if (_value is string)
            {
                valueString = $"'{_value}'";
            } else
            {
                valueString = $"{_value.ToString()}";
            }
            
            return $"{propertyName} = {valueString}";
        }
    }
}
