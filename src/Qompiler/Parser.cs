using Qompiler.Types;

namespace Qompiler;

public class Parser
{
    private readonly List<Statement> _statements = [];

    private List<Token> _tokens = [];
    private int _index = 0;

    public List<Statement> Parse(List<Token> tokens)
    {
        _tokens = tokens;

        while (ReadingTokens)
            _statements.Add(ParseStatement());

        return _statements;
    }


    /// Statements
    private Statement ParseStatement()
    {
        if (Match(TokenType.Print)) return ParsePrintStatement();
        if (Match(TokenType.Var)) return ParseVarStatement();

        return ParseExprStmt();
    }

    private PrintStmt ParsePrintStatement()
    {
        Consume(TokenType.OpenParenthesis);
        var expression = ParseExpression();
        Consume(TokenType.CloseParenthesis);
        Consume(TokenType.Semicolon);

        return new PrintStmt { Expression = expression };
    }

    private VarStmt ParseVarStatement()
    {
        var name = Peek().Value!.ToString()!;
        Consume(TokenType.Identifier);
        Consume(TokenType.Equals);
        var expr = ParseExpression();
        Consume(TokenType.Semicolon);

        return new VarStmt { Name = name, Expression = expr };
    }

    private ExprStmt ParseExprStmt()
    {
        var expr = ParseExpression();
        Consume(TokenType.Semicolon);
        return new ExprStmt { Expression = expr };
    }


    /// Expressions
    private Expression ParseExpression() => ParseTerm();

    private Expression ParseTerm()
    {
        var expr = ParseFactor();

        while (Match(TokenType.Plus, TokenType.Minus))
        {
            var _operator = Peek(-1).Type;
            var right = ParseFactor();
            expr = new BinaryExpr { Left = expr, Operator = _operator, Right = right };
        }

        return expr;
    }

    private Expression ParseFactor()
    {
        var expr = ParsePrimary();
        while (Match(TokenType.Star, TokenType.FSlash))
        {
            var _operator = Peek(-1).Type;
            var right = ParsePrimary();
            expr = new BinaryExpr { Left = expr, Operator = _operator, Right = right };
        }
        return expr;
    }

    private Expression ParsePrimary()
    {
        if (Match(TokenType.String, TokenType.Number))
            return new LiteralExpr { Value = Peek(-1).Value! };

        if (Match(TokenType.OpenParenthesis))
        {
            var expr = ParseExpression();
            Consume(TokenType.CloseParenthesis);
            return new GroupExpr { Expr = expr };
        }

        if (Match(TokenType.Identifier))
        {
            var name = Peek(-1).Value!.ToString()!;
            return new VariableExpr { Name = name };
        }

        Error("error parsing primary"); // TODO: better error message
        return null;
    }


    private void Consume(TokenType type)
    {
        if (_index >= _tokens.Count)
            Error($"Unexpected end of tokens");
        if (Peek().Type != type)
            Error($"Expected: {type}"); // TODO: better error message
        _index++;
    }

    private Token Peek(int offset = 0) => _tokens[_index + offset];

    private bool Match(params TokenType[] types)
    {
        foreach (var type in types)
        {
            if (Peek().Type == type)
            {
                Consume(type);
                return true;
            }
        }
        return false;
    }

    private bool ReadingTokens => Peek().Type != TokenType.EOF;

    private static void Error(string message)
    {
        Console.Error.WriteLine(message);
        Environment.Exit(-1);
    }

}
