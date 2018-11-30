using System;
using System.Linq.Expressions;

namespace SharpSql.Insert
{

    public class Insert : IInsert
    {
        private string _table;
        
        public Insert(string table)
        {
            
            _table = table;
        }
        

        public string ToSql()
        {
            return "";
            
        }
    }
    
    public interface IInsert : IInsertIntoBuilder, ISqlElement
    {

    }

    public interface IInsertIntoBuilder
    {
        //IInsert Set(string property, object value);
    }
    
    public interface IInsertIntoColumnsBuilder
    {
        IInsertIntoValuesBuilder Columns(params Expression<Func<object>>[] columns);
    }

    public interface IInsertIntoValuesBuilder
    {
        IInsertInto Values(params object[] values);
    }

    public interface IInsertInto : ISqlElement
    {

    }

    public class InsertInto : IInsertInto
    {
        private readonly string _table;
        private readonly Expression<Func<object>>[] _columns;
        private readonly object[] _values;

        public InsertInto(string table, Expression<Func<object>>[] columns, object[] values)
        {
            _table = table;
            _columns = columns;
            _values = values;
        }

        public string ToSql()
        {
            return $"INSERT INTO {_table}";
        }
    }

    public class InsertIntoBuilder : IInsertIntoColumnsBuilder, IInsertIntoValuesBuilder
    {
        private readonly string _table;
        private Expression<Func<object>>[] _columns;

        public InsertIntoBuilder(string table)
        {
            _table = table;
        }

        public IInsertIntoValuesBuilder Columns(params Expression<Func<object>>[] columns)
        {
            _columns = columns;
            return this;
        }

        public IInsertInto Values(params object[] values)
        {
            return new InsertInto(_table, _columns, values);
        }
    }
}
