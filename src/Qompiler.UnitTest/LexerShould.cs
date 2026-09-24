using AwesomeAssertions;
using Qompiler.Helpers;
using Qompiler.Types;
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

    [Theory]
    [MemberData(nameof(InvalidCharacters))]
    public void ErrorWithInvalidCharacter(char character)
    {
        var sut = GetLexerSut($"{character}");

        var action = () => sut.Tokenize();

        action.Should().ThrowExactly<ArgumentException>($"Unexpected character '{character}';1;1");
    }

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
}
