using Qompiler.Helpers;
using Qompiler.Types;
using System.Text;

namespace Qompiler.CodeGen;

public class C(string fileName, List<Statement> program)
{
    private readonly List<Statement> Program = program;
    private readonly StringBuilder Main = new();

    public void Generate()
    {
        Main.AppendLine("#include <stdio.h>");
        Main.AppendLine("int main(void){");

        foreach (var stmt in Program)
            EmitStatement(stmt);

        Main.Indent().AppendLine("return 0;");
        Main.AppendLine("}");


        FileManager.WriteFile(fileName, "c", Main.ToString());
    }

    private void EmitStatement(Statement stmt)
    {
        switch (stmt)
        {
            case PrintStmt printStmt: EmitPrintStatement(printStmt); break;
            case VarStmt varStmt: EmitVarStatement(varStmt); break;
            case ExprStmt: /* do nothing */; break;
            default: throw new NotSupportedException();
        }
    }

    private void EmitPrintStatement(PrintStmt stmt)
    {
        if (stmt.Expression.Type == TypeInfo.String)
        {
            Main.Indent().AppendLine($"printf({EmitExpression(stmt.Expression)});");
            Main.Indent().AppendPrintNewLine(this);
        }
        else if (stmt.Expression.Type == TypeInfo.Number)
        {
            Main.Indent().AppendLine($"""printf("%d", {EmitExpression(stmt.Expression)}\\n);""");
            Main.Indent().AppendPrintNewLine(this);
        }
    }

    private void EmitVarStatement(VarStmt stmt)
    {
        switch (stmt.Expression.Type)
        {
            case TypeInfo.String:
                Main.Indent().AppendLine($"char* {stmt.Name} = {EmitExpression(stmt.Expression)};"); break;
            case TypeInfo.Number:
                Main.Indent().AppendLine($"int {stmt.Name} = {EmitExpression(stmt.Expression)};"); break;
        }
    }

    private static string EmitExpression(Expression expr)
    {
        return expr switch
        {
            LiteralExpr literalExpr => '"' + literalExpr.Value.ToString() + '"',
            VariableExpr varExpr => varExpr.Name.ToString()!,
            _ => throw new NotSupportedException()
        };
    }
}
