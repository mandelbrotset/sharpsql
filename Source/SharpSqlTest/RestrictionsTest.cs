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
            var result = SelectBuilder.Select()
                .From(() => _order)
                .WithColumns(x => x.Reference)
                .WithRestriction(Restriction.Where(() => _order.Release).EqualTo("hello"))
                .ToSql();

            Assert.AreEqual("SELECT _order.Reference FROM Order _order WHERE _order.Release = 'hello'", result);
        }

        [TestMethod]
        public void PropertyEqualsIntegerRestriction()
        {
            Order _order = null;
            var result = SelectBuilder.Select()
                .From(() => _order)
                .WithColumns(x => x.Reference)
                .WithRestriction(Restriction.Where(() => _order.Release).EqualTo(4))
                .ToSql();

            Assert.AreEqual("SELECT _order.Reference FROM Order _order WHERE _order.Release = 4", result);
        }

        [TestMethod]
        public void PropertyEqualsPropertyRestriction()
        {
            Order _order = null;
            var result = SelectBuilder.Select()
                .From(() => _order)
                .WithColumns(x => x.Reference)
                .WithRestriction(Restriction.Where(() => _order.Release).EqualTo(() => _order.Return))
                .ToSql();

            Assert.AreEqual("SELECT _order.Reference FROM Order _order WHERE _order.Release = _order.Return", result);
        }

        [TestMethod]
        public void ValueEqualsValueRestriction()
        {
            Order _order = null;
            var result = SelectBuilder.Select()
                .From(() => _order)
                .WithColumns(x => x.Reference)
                .WithRestriction(Restriction.Where("hello1").EqualTo("hello2"))
                .ToSql();

            Assert.AreEqual("SELECT _order.Reference FROM Order _order WHERE 'hello1' = 'hello2'", result);
        }

        [TestMethod]
        public void SimpleStandAloneRestriction()
        {
            var restriction = Restriction.Where("hej").EqualTo(3);
            Assert.AreEqual("'hej' = 3", restriction.ToSql());
        }

        [TestMethod]
        public void StandAloneRestrictionAnd()
        {
            var junction = new Disjunction(Restriction.Where("hej").EqualTo("hej1"), Restriction.Where("hej2").EqualTo("hej3"));
            
            Assert.AreEqual("'hej' = 'hej1' OR 'hej2' = 'hej3'", junction.ToSql());
        }

        [TestMethod]
        public void StandAloneRestrictionComplex()
        {
            var disjunction = new Disjunction();
            disjunction.Add(Restriction.Where("hej").EqualTo("hej1"));
            disjunction.Add(Restriction.Where("hej2").EqualTo("hej3"));

            var conjunction = new Conjunction();
            conjunction.Add(Restriction.Where("hej4").EqualTo("hej5"));
            conjunction.Add(Restriction.Where("hej6").EqualTo("hej7"));

            var parentJunction = new Conjunction(disjunction, conjunction);

            Assert.AreEqual("('hej' = 'hej1' OR 'hej2' = 'hej3') AND ('hej4' = 'hej5' AND 'hej6' = 'hej7')", parentJunction.ToSql());
        }
    }
}
