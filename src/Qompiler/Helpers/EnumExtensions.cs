using Qompiler.Types;

namespace Qompiler.Helpers;

public static class EnumExtensions
{
    extension(TokenType type)
    {
        public string Lexeme => type switch
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

            // not actual lexemes
            TokenType.String => "String",
            TokenType.Number => "Number",
            TokenType.Identifier => "Identifier",
            TokenType.EOF => string.Empty,
            _ => throw new NotImplementedException()
        };

        public static TokenType FromLexeme(string lexeme) => lexeme switch
        {
            "(" => TokenType.OpenParenthesis,
            ")" => TokenType.CloseParenthesis,
            ";" => TokenType.Semicolon,
            "=" => TokenType.Equals,
            "+" => TokenType.Plus,
            "-" => TokenType.Minus,
            "*" => TokenType.Star,
            "/" => TokenType.FSlash,
            "var" => TokenType.Var,
            "Print" => TokenType.Print,

            // not actual lexemes
            "String" => TokenType.String,
            "Number" => TokenType.Number,
            "Identifier" => TokenType.Identifier,
            "EOF" => TokenType.EOF,
            _ => throw new NotImplementedException()
        };

        public static TokenType FromLexeme(char character)
            => FromLexeme(character.ToString());
    }
}
