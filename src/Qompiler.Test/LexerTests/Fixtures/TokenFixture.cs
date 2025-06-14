using Qompiler.types;

namespace Qompiler.Test.LexerTests.Fixtures
{
    public static class TokenFixture
    {
        public static readonly TokenType[] Print = [
            // print string
            TokenType.Print,
            TokenType.Open_Parenthesis,
            TokenType.Quotation,
            TokenType.String_Literal,
            TokenType.Quotation,
            TokenType.Close_Parenthesis,
            TokenType.Semicolon,
            // print number
            TokenType.Print,
            TokenType.Open_Parenthesis,
            TokenType.Number_Literal,
            TokenType.Close_Parenthesis,
            TokenType.Semicolon
        ];

        public static readonly TokenType[] VariableAssignment = [
            // string
            TokenType.Var,
            TokenType.Variable_Name,
            TokenType.Equals,
            TokenType.Quotation,
            TokenType.String_Literal,
            TokenType.Quotation,
            TokenType.Semicolon,
            // number
            TokenType.Var,
            TokenType.Variable_Name,
            TokenType.Equals,
            TokenType.Number_Literal,
            TokenType.Semicolon
        ];


    }
}
