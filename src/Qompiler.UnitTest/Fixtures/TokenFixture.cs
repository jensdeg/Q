using Qompiler.Helpers;
using Qompiler.Types;

namespace Qompiler.UnitTest.Fixtures;

public static class TokenFixture
{
    private static string TestString => "Test";
    private static string TestStringLexeme => "\"Test\""; // because c# parses strings containing doubleqoutes with backslashes, maybe fix this?
    private static int TestNumber => 12345;

    public static IReadOnlyCollection<Token> PrintString =>
    [
        CreateToken(TokenType.Print),
        CreateToken(TokenType.OpenParenthesis),
        Token.CreateLiteral(TokenType.String, TestStringLexeme, TestString, 1),
        CreateToken(TokenType.CloseParenthesis),
        CreateToken(TokenType.Semicolon),
        CreateToken(TokenType.EOF)
    ];

    public static IReadOnlyCollection<Token> PrintNumber =>
    [
        CreateToken(TokenType.Print),
        CreateToken(TokenType.OpenParenthesis),
        Token.CreateLiteral(TokenType.Number, TestNumber.ToString(), TestNumber, 1),
        CreateToken(TokenType.CloseParenthesis),
        CreateToken(TokenType.Semicolon),
        CreateToken(TokenType.EOF)
    ];

    public static IReadOnlyCollection<Token> VarString =>
    [
        CreateToken(TokenType.Var),
        Token.CreateLiteral(TokenType.Identifier, TestString, TestString, 1),
        CreateToken(TokenType.Equals),
        Token.CreateLiteral(TokenType.String, TestStringLexeme, TestString, 1),
        CreateToken(TokenType.Semicolon),
        CreateToken(TokenType.EOF)
    ];

    public static IReadOnlyCollection<Token> VarNumber =>
    [
        CreateToken(TokenType.Var),
        Token.CreateLiteral(TokenType.Identifier, TestString, TestString, 1),
        CreateToken(TokenType.Equals),
        Token.CreateLiteral(TokenType.Number, TestNumber.ToString(), TestNumber, 1),
        CreateToken(TokenType.Semicolon),
        CreateToken(TokenType.EOF)
    ];

    public static IReadOnlyCollection<Token> BinaryExpression =>
    [
        Token.CreateLiteral(TokenType.Number, "1", 1, 1),
        CreateToken(TokenType.Plus),
        Token.CreateLiteral(TokenType.Number, "2", 2, 1),
        CreateToken(TokenType.Minus),
        CreateToken(TokenType.OpenParenthesis),
        Token.CreateLiteral(TokenType.Number, "3", 3, 1),
        CreateToken(TokenType.Star),
        Token.CreateLiteral(TokenType.Number, "4", 4, 1),
        CreateToken(TokenType.CloseParenthesis),
        CreateToken(TokenType.FSlash),
        Token.CreateLiteral(TokenType.Number, "5", 5, 1),
        CreateToken(TokenType.Semicolon),
        CreateToken(TokenType.EOF),
    ];

    private static Token CreateToken(TokenType type, int line = 1)
        => Token.Create(type, type.Lexeme, line);
}
