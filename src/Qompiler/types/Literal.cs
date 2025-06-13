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
    }
}
