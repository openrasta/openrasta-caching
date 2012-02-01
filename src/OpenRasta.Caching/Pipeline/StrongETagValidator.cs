namespace OpenRasta.Caching.Pipeline
{
    public class StrongETagValidator : ETagValidator
    {
        public string Value { get; private set; }

        public StrongETagValidator(string value)
        {
            Value = value;
        }

        public override bool Matches(string entityTag)
        {
            return entityTag == Value;
        }
        public override string ToString()
        {
            return Value;
        }
    }
}