using AwesomeAssertions;
using Qompiler.Helpers;
using Qompiler.Types;
using Qompiler.UnitTest.Fixtures;
using Qompiler.UnitTest.Mocks;

namespace Qompiler.UnitTest;

public class LexerShould
{
    private static Lexer GetLexerSut(string input) => new(input)
    {
        ErrorHandler = new MockErrorHandler()
    };

    public static TheoryData<char> InvalidCharacters =>
    [
        ',','.',':','?','!','"','`','[',']','{','}','<',
        '>','&','|','^','%','~','_','@','#','$','€','£'
    ];

    public static TheoryData<char> ValidCharacters =>
    [
        '(', ')', ';', '=', '+', '-', '*', '/'
    ];

    public static TheoryData<string, IReadOnlyCollection<Token>> Statements => new()
    {
        { """Print("Test");""",     TokenFixture.PrintString      },
        { "Print(12345);",          TokenFixture.PrintNumber      },
        { """var Test = "Test";""", TokenFixture.VarString        },
        { "var Test = 12345;",      TokenFixture.VarNumber        },
        { "1 + 2 - (3 * 4) / 5;",   TokenFixture.BinaryExpression },
    };

    [Theory]
    [MemberData(nameof(ValidCharacters))]
    public void TokenizeSingleValidCharacter(char character)
    {
        var sut = GetLexerSut($"{character}");
        var expectedToken = Token.Create(TokenType.FromLexeme(character), character.ToString(), 1);

        var result = sut.Tokenize();

        result.Should().HaveCount(2);
        result.First().Should().BeEquivalentTo(expectedToken);
    }

    [Theory]
    [MemberData(nameof(InvalidCharacters))]
    public void ErrorWithInvalidCharacter(char character)
    {
        var sut = GetLexerSut($"{character}");

        var action = () => sut.Tokenize();

        action.Should().ThrowExactly<ArgumentException>($"Unexpected character '{character}';1;1");
    }

    [Theory]
    [MemberData(nameof(Statements))]
    public void TokenizeStatements(string input, IReadOnlyCollection<Token> expectedTokens)
    {
        var sut = GetLexerSut(input);

        var result = sut.Tokenize();

        result.Should().Equal(expectedTokens);
    }
}
