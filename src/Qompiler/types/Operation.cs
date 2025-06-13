namespace Qompiler.types
{
    public enum OperationType
    {
        Empty,
        Print,
        Variable_Assignment,
        Add
    }

    public class Operation
    {
        public OperationType Type { get; set; }
        public Literal[] Literal { get; set; } = [];

        public static Operation Create(OperationType type, Literal[] literal)
            => new() { Type = type, Literal = literal };

        public override string ToString()
        {
            string literals = string.Empty;
            Literal?.ToList().ForEach(l => literals += "   - " + l.Value.ToString());
            return $"- {Type}{Environment.NewLine} " +
                $"{literals}";
        }
    }

    public static class Operations
    {
        public static readonly TokenType[] Print = [
            TokenType.Print,
            TokenType.Open_Parenthesis,
            TokenType.Close_Parenthesis
        ];

        public static readonly TokenType[] VariableAssignment = [
            TokenType.Var,
            TokenType.Variable_Name,
            TokenType.Equals
        ];

        public static readonly TokenType[] Add = [
            TokenType.Number_Literal,
            TokenType.Plus,
            TokenType.Number_Literal,
        ];
    }
}
