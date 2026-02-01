namespace Qompiler.Types;

public abstract class Statement { }

public class PrintStmt : Statement
{
    public required Expression Expression { get; set; }
}

public class VarStmt : Statement
{
    public required string Name { get; set; }

    public required Expression Expression { get; set; }
}

public class ExprStmt : Statement
{
    public required Expression Expression { get; set; }
}
