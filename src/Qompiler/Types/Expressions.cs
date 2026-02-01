namespace Qompiler.Types;

public abstract class Expression
{
    public TypeInfo Type { get; set; }
}

public class LiteralExpr : Expression
{
    public required object Value { get; set; }
}
public class VariableExpr : Expression
{
    public required string Name { get; set; }
}
public class GroupExpr : Expression
{
    public required Expression Expr { get; set; }
}
public class BinaryExpr : Expression
{
    public required Expression Left { get; set; }
    public required TokenType Operator { get; set; }
    public required Expression Right { get; set; }
}

public enum TypeInfo
{
    Unkown = default,

    Number,
    String,

    Error,
}
