using SharpSql.Order;
using SharpSql.Restriction;
using System.Collections.Generic;
using System.Linq;

namespace SharpSql
{
    public class SqlBuilder
    {
        public static string ToSql<TEntity>(SelectQuery<TEntity> query)
        {
            var sql = "SELECT";
            var tableName = typeof(TEntity).Name;
            var alias = query.Alias;
            var columnsPart = GetColumnsPart(query, alias);
            var joinPart = GetJoinPart(query);
            var restrictionsPart = GetRestrictionsPart(query, alias);
            var orderByPart = GetOrderByPart(query, alias);

            sql += $" {columnsPart} FROM {tableName} {alias}";

            if (joinPart.Any())
            {
                sql += $" {string.Join(" ", joinPart)}";
            }

            sql += restrictionsPart;

            
            sql += $"{orderByPart}";
            

            return sql;
        }

        private static string GetColumnsPart<TEntity>(SelectQuery<TEntity> query, string alias)
        {
            var columnNames = query.Columns.Select(x => $"{alias}.{x.GetPropertyName()}").ToList();
            var commaSeparatedColumns = string.Join(", ", columnNames);
            return commaSeparatedColumns;
        }

        private static string GetRestrictionsPart<TEntity>(SelectQuery<TEntity> query, string alias)
        {
            if (query.Restriction is EmptyRestriction<TEntity>)
            {
                return string.Empty;
            }

            return $" WHERE {query.Restriction.ToSql()}";
        }

        private static string GetOrderByPart<TEntity>(SelectQuery<TEntity> query, string alias)
        {
            return query.Order.ToSql();
        }

        private static IList<string> GetJoinPart<TEntity>(SelectQuery<TEntity> query)
        {
            return query.Joins.Select(x => x.ToSql()).ToList();
        }
    }
}
