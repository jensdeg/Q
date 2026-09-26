namespace Qompiler.Types;

public abstract class Expression
{
    public TypeInfo Type { get; set; }
}

public class LiteralExpr : Expression
{
    public required object Value { get; init; }

    public override bool Equals(object? obj)
    {
        if (obj is not LiteralExpr expression) return false;
        return
            Type == expression.Type &&
            Value.ToString() == expression.Value.ToString();
    }

    public override int GetHashCode() => HashCode.Combine(Type, Value);
}

public class VariableExpr : Expression
{
    public required string Name { get; init; }

    public override bool Equals(object? obj)
    {
        if (obj is not VariableExpr expression) return false;
        return
            Type == expression.Type &&
            Name == expression.Name;
    }

    public override int GetHashCode() => HashCode.Combine(Type, Name);
}

public class GroupExpr : Expression
{
    public required Expression Expr { get; init; }

    public override bool Equals(object? obj)
    {
        if (obj is not GroupExpr expression) return false;
        return Expr.Equals(expression);
    }

    public override int GetHashCode() => HashCode.Combine(Type, Expr);
}

public class BinaryExpr : Expression
{
    public required Expression Left { get; init; }
    public required TokenType Operator { get; init; }
    public required Expression Right { get; init; }

    public override bool Equals(object? obj)
    {
        if (obj is not BinaryExpr expression) return false;
        return
            Type == expression.Type &&
            Operator == expression.Operator &&
            Left.Equals(expression.Left) &&
            Right.Equals(expression.Right);
    }

    public override int GetHashCode() => HashCode.Combine(Type, Left, Operator, Right);
}

public enum TypeInfo
{
    Unkown = default,

    Number,
    String,

    Error,
}
