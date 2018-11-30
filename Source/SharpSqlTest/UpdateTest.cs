using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpSql;

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
            var result = QueryBuilder.Update<Person>()
                .Set(x => x.Age, 42)
                .Set(x => x.Name, "Zlatan")
                .ToSql();

            Assert.AreEqual("UPDATE Person SET Age = 42, Name = 'Zlatan'", result);
        }
    }
}
