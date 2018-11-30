namespace SharpSql.Restriction
{
    public class EmptyRestriction : IRestriction
    {
        public string ToSql()
        {
            return string.Empty;
        }
    }
}
