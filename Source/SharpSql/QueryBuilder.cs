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

        public static IInsertIntoColumnsBuilder InsertInto(string table)
        {
            return new InsertIntoBuilder(table);
        }

        public static IUpdateBuilder<TEntity> Update<TEntity>()
        {
            return new Update<TEntity>();
        }
    }
}
