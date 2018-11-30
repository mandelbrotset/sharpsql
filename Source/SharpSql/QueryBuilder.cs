using SharpSql.Insert;
using SharpSql.Select;
using SharpSql.Update;

namespace SharpSql
{
    public class QueryBuilder
    {
        public static ISelect Select()
        {
            return new Select.Select();
        }

        //public static IInsertIntoColumnsBuilder InsertInto(string table)
        //{
        //    return new InsertIntoBuilder(table);
        //}

        public static IInsertInto<TEntity> InsertInto<TEntity>(TEntity value)
        {
            return new InsertInto<TEntity>(value);
        }

        public static IUpdateBuilder<TEntity> Update<TEntity>()
        {
            return new Update<TEntity>();
        }
    }
}
