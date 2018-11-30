using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpSql;
using SharpSql.Restriction;
using SharpSqlTest.TestClasses;

namespace SharpSqlTest
{
    [TestClass]
    public class RestrictionsTest
    {
        [TestMethod]
        public void PropertyEqualsStringRestriction()
        {
            Order _order = null;
            var result = QueryBuilder.Select()
                .From(() => _order)
                .WithColumns(x => x.Reference)
                .Where(() => _order.Release).EqualTo("hello").Build()
                .ToSql();

            Assert.AreEqual("SELECT _order.Reference FROM Order _order WHERE _order.Release = 'hello'", result);
        }

        [TestMethod]
        public void PropertyEqualsIntegerRestriction()
        {
            Order _order = null;
            var result = QueryBuilder.Select()
                .From(() => _order)
                .WithColumns(x => x.Reference)
                .Where(() => _order.Release).EqualTo(4).Build()
                .ToSql();

            Assert.AreEqual("SELECT _order.Reference FROM Order _order WHERE _order.Release = 4", result);
        }

        [TestMethod]
        public void PropertyEqualsPropertyRestriction()
        {
            Order _order = null;
            var result = QueryBuilder.Select()
                .From(() => _order)
                .WithColumns(x => x.Reference)
                .Where(() => _order.Release).EqualTo(() => _order.Return).Build()
                .ToSql();

            Assert.AreEqual("SELECT _order.Reference FROM Order _order WHERE _order.Release = _order.Return", result);
        }

        [TestMethod]
        public void ValueEqualsValueRestriction()
        {
            Order _order = null;
            var result = QueryBuilder.Select()
                .From(() => _order)
                .WithColumns(x => x.Reference)
                .Where("hello1").EqualTo("hello2").Build()
                .ToSql();

            Assert.AreEqual("SELECT _order.Reference FROM Order _order WHERE 'hello1' = 'hello2'", result);
        }

        [TestMethod]
        public void AndRestriction()
        {
            Order _order = null;
            var result = QueryBuilder.Select()
                .From(() => _order)
                .WithColumns(x => x.Reference)
                .Where(() => _order.Return).EqualTo("hello").And("hello2").EqualTo(() => _order.Release).Build()
                .ToSql();

            Assert.AreEqual("SELECT _order.Reference FROM Order _order WHERE _order.Return = 'hello' AND 'hello2' = _order.Release", result);
        }
    }
}
