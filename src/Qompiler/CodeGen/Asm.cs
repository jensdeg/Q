using JensdegTools.Extensions;
using Qompiler.Helpers;
using Qompiler.Types;
using System.Text;

namespace Qompiler.CodeGen;

// Nasm x86-64
public class Asm(string fileName, List<Statement> program)
{
    private readonly StringBuilder Main = new();
    private readonly StringBuilder Data = new();

    private readonly List<Statement> Program = program;

    private readonly List<string> Variables = [];

    private int Index = 0;


    public void Generate()
    {
        // Start
        Main.AppendLine("global _start");
        Main.AppendLine("_start:");

        // Code
        foreach (var stmt in Program)
            EmitStatement(stmt);

        // Exit
        Main.AppendLine("    ;; EXIT");
        Main.AppendLine("    mov rax, 60");
        Main.AppendLine("    mov rdi, 0");
        Main.AppendLine("    syscall");

        // Helpers
        EmitHelpers();

        // Data
        Main.AppendLine("section .data");
        Main.AppendLine(Data.ToString());

        FileManager.WriteFile(fileName, "asm", Main.ToString());
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
        Index++;
    }

    private void EmitPrintStatement(PrintStmt stmt)
    {
        EmitExpression(stmt.Expression);
        Main.AppendLine("    call print");
    }

    private void EmitVarStatement(VarStmt stmt)
    {
        EmitExpression(stmt.Expression, stmt.Name);
    }

    
    private void EmitExpression(Expression expr, string? variableName = null)
    {
        switch (expr)
        {
            case LiteralExpr literalExpr: EmitLiteralExpression(literalExpr, variableName); break;
            case VariableExpr varExpr: EmitVarExpression(varExpr); break; 
            default: throw new NotSupportedException();
        }
    }

    private void EmitLiteralExpression(LiteralExpr expr, string? variableName)
    {
        variableName ??= $"str_{Index}";

        Main.AppendLine($"    mov rax, {variableName}");
        Main.AppendLine($"    mov rdx, {variableName}_len");

        Data.AppendLine($"{variableName}: db '{expr.Value}', 10");
        Data.AppendLine($"{variableName}_len: equ $-{variableName}");

        Variables.Add(variableName);
    }

    private void EmitVarExpression(VariableExpr expr)
    {
        Main.AppendLine($"    mov rax, {expr.Name}");
    }

    private void EmitHelpers()
    {
        if (Program.ContainsType(typeof(PrintStmt)))
        {
            Main.AppendLine("print:");
            Main.AppendLine("    mov rsi, rax");
            Main.AppendLine("    mov rax, 1");
            Main.AppendLine("    mov rdi, 1");
            Main.AppendLine("    syscall");
            Main.AppendLine("    ret");
        }
    }
}
