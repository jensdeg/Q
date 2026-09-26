namespace Qompiler.Types;

public abstract class Statement { }

public class PrintStmt : Statement
{
    public required Expression Expression { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is not PrintStmt statement) return false;
        return Expression.Equals(statement.Expression);
    }

    public override int GetHashCode() => Expression.GetHashCode();
}

public class VarStmt : Statement
{
    public required string Name { get; set; }

    public required Expression Expression { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is not VarStmt statement) return false;
        return
            Name == statement.Name &&
            Expression.Equals(statement.Expression);
    }

    public override int GetHashCode() => HashCode.Combine(Name, Expression);
}

public class ExprStmt : Statement
{
    public required Expression Expression { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is not ExprStmt statement) return false;
        return Expression.Equals(statement.Expression);
    }

    public override int GetHashCode() => Expression.GetHashCode();
}
