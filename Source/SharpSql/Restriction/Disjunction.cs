namespace SharpSql.Restriction
{
    public class Disjunction : Junction
    {
        public Disjunction(IRestriction restriction) : base(restriction) { }

        public Disjunction(params IRestriction[] restrictions) : base(restrictions) { }

        protected override LogicOperator LogicOperator => LogicOperator.Or;
    }
}
