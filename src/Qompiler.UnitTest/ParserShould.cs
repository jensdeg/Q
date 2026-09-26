using AwesomeAssertions;
using Qompiler.Types;
using Qompiler.UnitTest.Fixtures;
using Qompiler.UnitTest.Mocks;

namespace Qompiler.UnitTest;

public class ParserShould
{
    private static Parser GetParserSut(IReadOnlyCollection<Token> tokens) => new([.. tokens])
    {
        ErrorHandler = new MockErrorHandler()
    };

    public static TheoryData<IReadOnlyCollection<Token>, Statement> Statements => new()
    {
        { TokenFixture.PrintString, StatementFixture.PrintString },
        { TokenFixture.PrintNumber, StatementFixture.PrintNumber },
        { TokenFixture.VarString, StatementFixture.VarString },
        { TokenFixture.VarNumber, StatementFixture.VarNumber },
        //{ TokenFixture.BinaryExpression, StatementFixture.BinaryStatement }, // TODO
    };

    [Theory]
    [MemberData(nameof(Statements))]
    public void ParseStatements(IReadOnlyCollection<Token> tokens, Statement expectedStatement)
    {
        var sut = GetParserSut(tokens);

        var result = sut.Parse();

        result.Should().ContainSingle();
        result.First().Should().Be(expectedStatement);
    }
}
