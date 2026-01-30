namespace Qompiler.Types;

public class Token
{
    public required TokenType Type { get; set; }
    public required string Lexeme { get; set; }
    public object? Value { get; set; }
    public required int Position { get; set; }

    public static Token Create(TokenType type, string lexeme, int position)
        => new() { Type = type, Lexeme = lexeme, Position = position };

    public static Token CreateLiteral(TokenType type, string lexeme, object value, int position)
        => new() { Type = type, Value = value, Lexeme = lexeme, Position = position };

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