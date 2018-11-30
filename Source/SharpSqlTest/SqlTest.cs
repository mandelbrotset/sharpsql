using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpSql;
using SharpSqlTest.TestClasses;

namespace SharpSqlTest
{
    [TestClass]
    public class SqlTest
    {
        [TestMethod]
        public void SelectWithColumnsInEntity()
        {
            Order _order = null;
            var result = QueryBuilder.Select()
                .From(() => _order)
                .WithColumnsInEntity()
                .ToSql();

            Assert.AreEqual("SELECT _order.Reference, _order.Release, _order.Return FROM Order _order", result);
        }

        [TestMethod]
        public void SimpleSelect()
        {
            Order _order = null;
            var result = QueryBuilder.Select()
                .From(() => _order)
                .WithColumns(x => x.Reference, x => x.Release, x => x.Return)
                .ToSql();

            Assert.AreEqual("SELECT _order.Reference, _order.Release, _order.Return FROM Order _order", result);
        }
    }
}
