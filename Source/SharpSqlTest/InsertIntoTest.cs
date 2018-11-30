using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpSql;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpSqlTest
{
    [TestClass]
    public class InsertIntoTest
    {
        private class Person
        {
            public int Age { get; set; }
            public string Name { get; set; }
        }

        [TestMethod]
        public void SimpleInsert()
        {
            var person = new Person
            {
                Age = 42,
                Name = "Sven"
            };

            var result = QueryBuilder.InsertInto(person).ToSql();

            Assert.AreEqual("INSERT INTO Person (Age, Name) VALUES (42, 'Sven')", result);
        }
    }
}
