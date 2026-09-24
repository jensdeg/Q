namespace Qompiler.Types;

public abstract class Expression
{
    public TypeInfo Type { get; set; }
}

public class LiteralExpr : Expression
{
    public object Value { get; set; }
}

public class VariableExpr : Expression
{
    public string Name { get; set; }
}

public class GroupExpr : Expression
{
    public Expression Expr { get; set; }
}

public class BinaryExpr : Expression
{
    public Expression Left { get; set; }
    public TokenType Operator { get; set; }
    public Expression Right { get; set; }
}

public enum TypeInfo
{
    Unkown = default,

    Number,
    String,

    Error,
}
