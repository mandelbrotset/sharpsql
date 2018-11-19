using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace SharpSql
{
    public static class Extensions
    {
        public static string GetPropertyName<T>(this Expression<Func<T, object>> property)
        {
            if (property.Body is MemberExpression memberExpression)
            {
                return memberExpression.Member.Name;
            }

            if (property.Body is UnaryExpression unaryExpression)
            {
                MemberExpression m = (MemberExpression)unaryExpression.Operand;
                return m.Member.Name;
            }

            throw new NotImplementedException();
        }

        public static string GetParentName<T>(this Expression<Func<T, object>> property)
        {
            if (property.Body is UnaryExpression unaryExpression)
            {
                MemberExpression m = (MemberExpression)unaryExpression.Operand;
                if (m.Expression is MemberExpression parent)
                {
                    return parent.Member.Name;
                }
            }

            throw new NotImplementedException();
        }
        
        public static string GetPropertyName(this Expression<Func<object>> func)
        {
            if (func.Body is MemberExpression memberExpression)
            {
                return memberExpression.Member.Name;
            }

            if (func.Body is UnaryExpression unaryExpression)
            {
                MemberExpression m = (MemberExpression)unaryExpression.Operand;
                return m.Member.Name;
            }

            throw new NotImplementedException();
        }

        public static string GetPropertyType(this Expression<Func<object>> func)
        {
            if (func.Body is MemberExpression memberExpression)
            {
                if (memberExpression.Member is FieldInfo fieldInfo)
                {
                    return fieldInfo.FieldType.Name;
                }
            }

            throw new NotImplementedException();
        }

        public static string GetChildName(this Expression<Func<object>> func)
        {
            if (func.Body is UnaryExpression expression)
            {
                if (expression.Operand is MemberExpression operand)
                {
                    return operand.Member.Name;
                }
            }

            throw new NotImplementedException();
        }

        public static string GetParentName(this Expression<Func<object>> func)
        {
            if (func.Body is UnaryExpression expression)
            {
                if (expression.Operand is MemberExpression operand)
                {
                    if (operand.Expression is MemberExpression parent)
                    {
                        return parent.Member.Name;
                    }
                }
            }

            if (func.Body is MemberExpression memberExpression)
            {
                if (memberExpression.Expression is MemberExpression parentExpression)
                {
                    return parentExpression.Member.Name;
                }
            }

            throw new NotImplementedException();
        }

        public static IEnumerable<Expression<Func<TEntity, object>>> GetProperties<TEntity>(this Type type)
        {
            var result = new List<Expression<Func<TEntity, object>>>();
            var properties = type.GetProperties();
            foreach (var prop in properties)
            {
                var parameter = Expression.Parameter(typeof(TEntity));
                var memberExpression = Expression.Property(parameter, prop);
                var unaryExpression = Expression.Convert(memberExpression, typeof(object));
                var propertyExpression = Expression.Lambda<Func<TEntity, object>>(unaryExpression, parameter);
                yield return propertyExpression;
            }
        }
    }
}
