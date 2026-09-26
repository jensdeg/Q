using Qompiler.Types;

namespace Qompiler.UnitTest.Fixtures;

public static class StatementFixture
{
    private static string TestString => "Test";
    private static int TestNumber => 12345;

    private static LiteralExpr StringExpression => new() { Value = TestString };
    private static LiteralExpr NumberExpression => new() { Value = TestNumber };

    public static PrintStmt PrintString => new() { Expression = StringExpression };
    public static PrintStmt PrintNumber => new() { Expression = NumberExpression };
    public static VarStmt VarString => new() { Expression = StringExpression, Name = TestString };
    public static VarStmt VarNumber => new() { Expression = NumberExpression, Name = TestString };
}
