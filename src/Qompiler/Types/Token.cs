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

    public override bool Equals(object? obj)
    {
        if (obj is not Token token) return false;

        return
            Type == token.Type &&
            Lexeme == token.Lexeme &&
            Value?.ToString() == token.Value?.ToString() &&
            Line == token.Line;
    }

    public override int GetHashCode()
    {
        if (Value is not null) return HashCode.Combine(Type, Lexeme, Value, Line);
        else return HashCode.Combine(Type, Lexeme, Line);
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
