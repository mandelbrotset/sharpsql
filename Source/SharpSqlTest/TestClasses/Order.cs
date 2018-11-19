using System;

namespace SharpSqlTest.TestClasses
{
    public class Order
    {
        public string Reference { get; set; }
        public Guid Release { get; set; }
        public Guid Return { get; set; }
    }
}
