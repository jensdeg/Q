using Qompiler.Types;

namespace Qompiler.Helpers;

public static class EnumExtensions
{
    extension(TokenType type)
    {
        public string Literal => type switch
        {
            TokenType.OpenParenthesis => "(",
            TokenType.CloseParenthesis => ")",
            TokenType.Semicolon => ";",
            TokenType.Equals => "=",
            TokenType.Plus => "+",
            TokenType.Minus => "-",
            TokenType.Star => "*",
            TokenType.FSlash => "/",
            TokenType.Var => "var",
            TokenType.Print => "Print",

            // not actual literals
            TokenType.String => "String",
            TokenType.Number => "Number",
            TokenType.Identifier => "Identifier",
            TokenType.EOF => "EOF",
            _ => throw new NotImplementedException()
        };
    }
}
