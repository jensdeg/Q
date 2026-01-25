namespace Qompiler.types
{
    public class Literal
    {
        public object Value { get; set; } = new object();
        public bool IsVariable { get; set; } = false;

        public static Literal Create(object value)
            => new() { Value = value };
        public static Literal CreateVariable(object value)
           => new() { Value = value, IsVariable = true };

        public override bool Equals(object? obj)
        {
            if (obj is not Literal literal) return false;
            return literal.Value.Equals(Value) &&
                   literal.IsVariable == IsVariable;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Value, IsVariable);
        }
    }
}
