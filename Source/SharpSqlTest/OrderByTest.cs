using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpSql;
using SharpSql.Order;
using SharpSqlTest.TestClasses;

namespace SharpSqlTest
{
    [TestClass]
    public class OrderByTest
    {
        [TestMethod]
        public void OrderBy()
        {
            Order _order = null;
            var result = QueryBuilder.Select()
                .From(() => _order)
                .WithColumns(x => x.Reference, x => x.Release, x => x.Return)
                .OrderBy(() => _order.Reference)
                .ToSql();

            Assert.AreEqual("SELECT _order.Reference, _order.Release, _order.Return FROM Order _order ORDER BY _order.Reference", result);
        }

        [TestMethod]
        public void OrderByOrderBy()
        {
            Order _order = null;
            var result = QueryBuilder.Select()
                .From(() => _order)
                .WithColumns(x => x.Reference, x => x.Release, x => x.Return)
                .OrderBy(() => _order.Return, SortOrder.Descending)
                .OrderBy(() => _order.Reference)
                .ToSql();

            Assert.AreEqual("SELECT _order.Reference, _order.Release, _order.Return FROM Order _order ORDER BY _order.Return DESC, _order.Reference", result);
        }

        [TestMethod]
        public void OrderByThenBy()
        {
            Order _order = null;
            var result = QueryBuilder.Select()
                .From(() => _order)
                .WithColumns(x => x.Reference, x => x.Release, x => x.Return)
                .OrderBy(() => _order.Return, SortOrder.Descending)
                .ThenBy(() => _order.Reference)
                .ToSql();

            Assert.AreEqual("SELECT _order.Reference, _order.Release, _order.Return FROM Order _order ORDER BY _order.Return DESC, _order.Reference", result);
        }
    }
}
