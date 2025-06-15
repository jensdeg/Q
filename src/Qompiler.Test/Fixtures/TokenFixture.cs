using Qompiler.types;

namespace Qompiler.Test.Fixtures
{
    public static class TokenFixture
    {
        public static readonly List<Token> Print = [
            // print string
            Token.Create(TokenType.Print),
            Token.Create(TokenType.Open_Parenthesis),
            Token.Create(TokenType.Quotation),
            Token.Create(TokenType.String_Literal, Literal.Create("Test")),
            Token.Create(TokenType.Quotation),
            Token.Create(TokenType.Close_Parenthesis),
            Token.Create(TokenType.Semicolon),
            // print number
            Token.Create(TokenType.Print),
            Token.Create(TokenType.Open_Parenthesis),
            Token.Create(TokenType.Number_Literal, Literal.Create("12345")),
            Token.Create(TokenType.Close_Parenthesis),
            Token.Create(TokenType.Semicolon)
        ];

        public static readonly List<Token> VariableAssignment = [
            // string
            Token.Create(TokenType.Var),
            Token.Create(TokenType.Variable_Name, Literal.CreateVariable("TestVariable")),
            Token.Create(TokenType.Equals),
            Token.Create(TokenType.Quotation),
            Token.Create(TokenType.String_Literal, Literal.Create("TestString")),
            Token.Create(TokenType.Quotation),
            Token.Create(TokenType.Semicolon),
            // number
            Token.Create(TokenType.Var),
            Token.Create(TokenType.Variable_Name, Literal.CreateVariable("TestNumber")),
            Token.Create(TokenType.Equals),
            Token.Create(TokenType.Number_Literal, Literal.Create("12345")),
            Token.Create(TokenType.Semicolon)
        ];       
    }
}
