namespace Qompiler.Types;

public abstract class Expression
{
    public TypeInfo Type { get; set; }
}

public class LiteralExpr : Expression
{
    public required object Value { get; init; }
}

public class VariableExpr : Expression
{
    public required string Name { get; init; }
}

public class GroupExpr : Expression
{
    public required Expression Expr { get; init; }
}

public class BinaryExpr : Expression
{
    public required Expression Left { get; init; }
    public required TokenType Operator { get; init; }
    public required Expression Right { get; init; }
}

public enum TypeInfo
{
    Unkown = default,

    Number,
    String,

    Error,
}
