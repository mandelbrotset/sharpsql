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
            public int Name { get; set; }
        }

        [TestMethod]
        public void SimpleInsert()
        {
            var result = QueryBuilder.InsertInto("Person")
                .Columns()
                .Values()
                .ToSql();

            Assert.AreEqual("INSERT INTO Person", result);
        }
    }
}
