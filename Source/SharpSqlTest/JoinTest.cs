using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpSql;
using SharpSql.Join;
using SharpSqlTest.TestClasses;

namespace SharpSqlTest
{
    [TestClass]
    public class JoinTest
    {
        [TestMethod]
        public void SimpleInnerJoin()
        {
            Site _site = null;
            Order _order = null;
            var result = QueryBuilder.Select()
                .From(() => _order)
                .WithColumns(x => x.Reference, x => x.Release, x => x.Return)
                .Join(() => _site, JoinType.Inner).On(() => _site.Id, () => _order.Return)
                .ToSql();

            Assert.AreEqual("SELECT _order.Reference, _order.Release, _order.Return FROM Order _order INNER JOIN Site _site ON _site.Id = _order.Return", result);
        }

        [TestMethod]
        public void MultipleJoins()
        {
            Site _site1 = null;
            Site _site2 = null;
            Order _order1 = null;
            var result = QueryBuilder.Select()
                .From(() => _order1)
                .WithColumns(x => x.Reference, x => x.Release, x => x.Return)
                .Join(() => _site1, JoinType.LeftOuter).On(() => _site1.Id, () => _order1.Return)
                .Join(() => _site2, JoinType.RightOuter).On(() => _site2.Id, () => _order1.Release)
                .ToSql();

            Assert.AreEqual("SELECT _order1.Reference, _order1.Release, _order1.Return FROM Order _order1 LEFT OUTER JOIN Site _site1 ON _site1.Id = _order1.Return RIGHT OUTER JOIN Site _site2 ON _site2.Id = _order1.Release", result);
        }
    }
}
