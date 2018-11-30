namespace SharpSql.Restriction
{
    public class Conjunction : Junction
    {
        public Conjunction(IRestriction restriction) : base(restriction) { }

        public Conjunction(params IRestriction[] restrictions) : base(restrictions) { }

        protected override LogicOperator LogicOperator => LogicOperator.And;
    }
}
