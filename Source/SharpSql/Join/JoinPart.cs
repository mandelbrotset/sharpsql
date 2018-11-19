using System;
using System.Linq.Expressions;

namespace SharpSql.Join
{
    public class JoinPart
    {
        public JoinPart(string alias, string property)
        {
            Alias = alias;
            Property = property;
        }

        public JoinPart(Expression<Func<object>> expression)
        {
            Property = expression.GetChildName();   
            Alias = expression.GetParentName();
        }

        public string Alias { get; set; }
        public string Property { get; set; }
    }
}
