namespace Qompiler.types;

public enum OperationType
{
    Empty,
    Print,
    Variable_Assignment,
    Add
}

public partial class Operation
{
    public OperationType Type { get; set; }
    public List<Literal> Literals { get; set; } = [];

    public static Operation Create(OperationType type, List<Literal> literals)
        => new() { Type = type, Literals = literals };

    public override string ToString()
    {
        string literals = string.Empty;
        Literals?.ForEach(l => literals += "   - " + l.Value.ToString());
        return $"- {Type}{Environment.NewLine} " +
            $"{literals}";
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Operation operation) return false;
        return Type == operation.Type &&
               Literals.SequenceEqual(operation.Literals);
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(Type, Literals);
    }
}

public partial class Operation
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
