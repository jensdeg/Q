namespace Qompiler.Types;

public class Token
{
    public required TokenType Type { get; set; }
    public required string Lexeme { get; set; }
    public object? Value { get; set; }
    public required int Line { get; set; }

    public static Token Create(TokenType type, string lexeme, int line)
        => new() { Type = type, Lexeme = lexeme, Line = line };

    public static Token CreateLiteral(TokenType type, string lexeme, object value, int line)
        => new() { Type = type, Value = value, Lexeme = lexeme, Line = line };

    public readonly static Dictionary<string, TokenType> Keywords = new()
    {
        { "Print", TokenType.Print },
        { "Var", TokenType.Var }
    };
}

public enum TokenType
{
    OpenParenthesis,
    CloseParenthesis,
    Semicolon,
    Equals,
    Plus,

    String,
    Number,

    Print,
    Var,

    Identifier,
    EOF
}