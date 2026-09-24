namespace Qompiler.Types;

public class Token
{
    public required TokenType Type { get; init; }
    public required string Lexeme { get; init; }
    public object? Value { get; private set; }
    public required int Line { get; init; }

    public static Token Create(TokenType type, string lexeme, int line)
        => new() { Type = type, Lexeme = lexeme, Line = line };

    public static Token CreateLiteral(TokenType type, string lexeme, object value, int line)
        => new() { Type = type, Value = value, Lexeme = lexeme, Line = line };

    public static readonly Dictionary<string, TokenType> Keywords = new()
    {
        { "Print", TokenType.Print },
        { "var", TokenType.Var }
    };

    public override string ToString()
    {
        return Value is null
            ? $"[{Type}]: '{Lexeme}'"
            : $"[{Type}]: '{Value}'";
    }
}

public enum TokenType
{
    OpenParenthesis,
    CloseParenthesis,
    Semicolon,
    Equals,
    Plus,
    Minus,
    Star,
    FSlash,

    String,
    Number,

    Print,
    Var,

    Identifier,
    EOF
}
