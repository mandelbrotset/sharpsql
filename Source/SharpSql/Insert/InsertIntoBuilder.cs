using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SharpSql.Insert
{

    //public class Insert : IInsert
    //{
    //    private string _table;
        
    //    public Insert(string table)
    //    {
            
    //        _table = table;
    //    }
        

    //    public string ToSql()
    //    {
    //        return "";
            
    //    }
    //}
    
    //public interface IInsert : IInsertIntoBuilder, ISqlElement
    //{

    //}

    //public interface IInsertIntoBuilder
    //{
    //    //IInsert Set(string property, object value);
    //}
    
    //public interface IInsertIntoColumnsBuilder<TEntity>
    //{
    //    IInsertIntoValuesBuilder Columns(params Expression<Func<object>>[] columns);
    //}

    //public interface IInsertIntoValuesBuilder
    //{
    //    IInsertInto Values(params object[] values);
    //}

    public interface IInsertInto<TEntity> : ISqlElement
    {

    }

    public class InsertInto<TEntity> : IInsertInto<TEntity>
    {
        private readonly TEntity _value;

        public InsertInto(TEntity value)
        {
            _value = value;
        }

        public string ToSql()
        {
            var properties = typeof(TEntity).GetProperties<TEntity>();
            var columns = GetColumns(properties);
            var values = GetValues(properties);

            var sql = string.Empty;

            sql += $"INSERT INTO {_value.GetType().Name} ({string.Join(", ", columns)}) VALUES ({string.Join(", ", values)})";

            return sql;
        }
        
        private IEnumerable<string> GetColumns(IEnumerable<Expression<Func<TEntity, object>>> properties)
        {
            foreach (var property in properties)
            {
                yield return property.GetPropertyName();
            }
        }

        private IEnumerable<object> GetValues(IEnumerable<Expression<Func<TEntity, object>>> properties)
        {
            foreach (var property in properties)
            {
                var value = property.Compile()(_value);

                if (value is string)
                {
                    yield return $"'{value}'";
                }
                else
                {
                    yield return $"{value.ToString()}";
                }
            }
        }
    }

    //public class InsertIntoBuilder<TEntity> //: IInsertIntoColumnsBuilder, IInsertIntoValuesBuilder
    //{
    //    private readonly string _table;
    //    private Expression<Func<object>>[] _columns;

    //    public InsertIntoBuilder(string table)
    //    {
    //        _table = table;
    //    }

    //    //public IInsertIntoValuesBuilder Columns(params Expression<Func<object>>[] columns)
    //    //{
    //    //    _columns = columns;
    //    //    return this;
    //    //}

    //    //public IInsertInto Values(params object[] values)
    //    //{
    //    //    return new InsertInto(_table, _columns, values);
    //    //}
    //}
}
