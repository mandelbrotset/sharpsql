using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpSql.Restriction
{
    public abstract class Junction : IRestriction
    {
        protected abstract LogicOperator LogicOperator { get; }
        protected List<IRestriction> _restrictions;

        public Junction()
        {
            _restrictions = new List<IRestriction>();
        }

        public Junction(IRestriction restriction) : this()
        {
            _restrictions.Add(restriction);
        }

        public Junction(params IRestriction[] restrictions) : this()
        {
            _restrictions.AddRange(restrictions);
        }

        public Junction Add(params IRestriction[] restrictions)
        {
            _restrictions.AddRange(restrictions);
            return this;
        }

        public string ToSql()
        {
            var logicOperator = GetOperator();
            
            var result = string.Join($" {logicOperator} ", _restrictions.Select(x => 
            {
                var sql = x.ToSql();
                return x is Junction ? $"({sql})" : sql;
            }));

            if (string.IsNullOrWhiteSpace(result))
            {
                throw new InvalidOperationException("Junction contains no restrictions.");
            }

            return $"{result}";
        }

        private string GetOperator()
        {
            switch (LogicOperator)
            {
                case LogicOperator.And:
                    return "AND";
                case LogicOperator.Or:
                    return "OR";
            }

            throw new NotImplementedException();
        }
    }
}
