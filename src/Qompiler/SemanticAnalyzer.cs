using Qompiler.Types;

namespace Qompiler;

public class SemanticAnalyzer
{
    private readonly Dictionary<string, TypeInfo> _variables = [];

    public List<Statement> Analyze(List<Statement> program) 
    { 
        foreach (var statement in program)
            AnalyzeStatement(statement);

        return program;
    }


    /// Statements
    private void AnalyzeStatement(Statement stmt)
    {
        switch (stmt)
        {
            case VarStmt varStmt: AnalyzeVarStatement(varStmt); break;
            case PrintStmt printStmt: AnalyzePrintStatement(printStmt); break;
            case ExprStmt exprStmt: AnalyzeExprStatement(exprStmt); break;
            default: throw new NotSupportedException();
        }
    }

    private void AnalyzeVarStatement(VarStmt stmt)
    {
        var type = AnalyzeExpression(stmt.Expression);

        if (_variables.ContainsKey(stmt.Name))
            Error("variable already exists");

        _variables[stmt.Name] = type;
    }

    private void AnalyzePrintStatement(PrintStmt stmt)
        => AnalyzeExpression(stmt.Expression);

    private void AnalyzeExprStatement(ExprStmt stmt)
        => AnalyzeExpression(stmt.Expression);


    /// Expressions
    private TypeInfo AnalyzeExpression(Expression expr)
    {
        return expr switch
        {
            LiteralExpr literalExpr => AnalyzeLiteralExpression(literalExpr),
            VariableExpr variableExpr => AnalyzeVariableExpression(variableExpr),
            BinaryExpr binaryExpr => AnalyzeBinaryExpression(binaryExpr),
            GroupExpr groupExpr => AnalyzeGroupExpression(groupExpr),
            _ => throw new Exception(),
        };
    }

    private static TypeInfo AnalyzeLiteralExpression(LiteralExpr expr)
    {
        expr.Type = expr.Value switch
        {
            int => TypeInfo.Number,
            string => TypeInfo.String,
            _ => TypeInfo.Error,
        };

        return expr.Type;
    }

    private TypeInfo AnalyzeVariableExpression(VariableExpr expr)
    {
        if (_variables.TryGetValue(expr.Name, out var type))
            expr.Type = type;
        else
            Error("Variable Not Declared");

        return expr.Type;
    }

    private TypeInfo AnalyzeBinaryExpression(BinaryExpr binaryExpr)
    {
        var leftType = AnalyzeExpression(binaryExpr.Left);
        var rightType = AnalyzeExpression(binaryExpr.Right);

        if (leftType != rightType)
            Error($"Cant perform operation '{binaryExpr.Operator}' on '{leftType}' and '{rightType}'");

        if (leftType != TypeInfo.Number)
            Error($"cant Perform operation '{binaryExpr.Operator}' on '{leftType}'"); /// TODO: allow string concatination

        binaryExpr.Type = leftType;
        return binaryExpr.Type;
    }

    private TypeInfo AnalyzeGroupExpression(GroupExpr groupExpr)
    {
        groupExpr.Type = AnalyzeExpression(groupExpr.Expr);
        return groupExpr.Type;
    }

    private static void Error(string message)
    {
        throw new Exception($"Error: {message}");
    }

}
