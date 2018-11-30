using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpSql;
using SharpSql.Restriction;

namespace SharpSqlTest
{
    [TestClass]
    public class UpdateTest
    {
        private class Person
        {
            public int Age { get; set; }
            public int Name { get; set; }
        }

        [TestMethod]
        public void SimpleUpdate()
        {
            Person Person = null;
            var result = QueryBuilder.Update<Person>()
                .Set(x => x.Age, 42)
                .Set(x => x.Name, "Zlatan")
                .WithRestriction(Restriction.Where(() => Person.Age).EqualTo(50))
                .ToSql();

            Assert.AreEqual("UPDATE Person SET Age = 42, Name = 'Zlatan' WHERE Person.Age = 50", result);
        }
    }
}
