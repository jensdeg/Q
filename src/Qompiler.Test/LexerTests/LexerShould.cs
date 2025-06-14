using FluentAssertions;
using Qompiler;
using Qompiler.Test.LexerTests.Fixtures;
using Qompiler.types;
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace Qompiler.Test.LexerTests
{
    public class LexerShould
    {
        private readonly string FilePath = "./LexerTests/Qfiles/";

        public LexerShould()
        {
            Lexer.Clear();
        }

        [Fact]
        public void TokenizePrintStatements()
        {
            // Arrange
            var content = ReadFile("Print.Q");
            var expectedTokenTypes = TokenFixture.Print;
            object[] expectedLiterals = ["Test","12345"];

            // Act
            var tokens = Lexer.Tokenize(content);

            // Assert
            var tokenTypes = GetTokenTypes(tokens);
            var tokenLiterals = GetLiterals(tokens);

            tokens.Should().NotBeEmpty();
            tokenTypes.Should().Equal(expectedTokenTypes);
            tokenLiterals.Select(l => l?.Value).Should().Equal(expectedLiterals);
            tokenLiterals.Select(l => l?.IsVariable).Should().Equal([false, false]);
        }

        [Fact]
        public void TokenizeVariableAssignment()
        {
            // Arrange
            var content = ReadFile("Variable.Q");
            var expectedTokenTypes = TokenFixture.VariableAssignment;
            object[] expectedLiterals = [
                "TestVariable",
                "TestString",
                "TestNumber",
                "12345"];

            // Act
            var tokens = Lexer.Tokenize(content);
            var tokenTypes = GetTokenTypes(tokens);
            var tokenLiterals = GetLiterals(tokens);

            // Assert
            tokens.Should().NotBeEmpty();
            tokenTypes.Should().Equal(expectedTokenTypes);
            tokenLiterals.Select(l => l?.Value).Should().Equal(expectedLiterals);
            tokenLiterals.Select(l => l?.IsVariable).Should().Equal([true, false, true, false]);
        }


        // helper methods
        private static List<TokenType> GetTokenTypes(List<Token> tokens)
            => tokens
                .Select(t => t.Type)
                .ToList();

        private static List<Literal?> GetLiterals(List<Token> tokens)
            => tokens
                .Where(t => t.Literal is not null)
                .Select(t => t.Literal)
                .ToList();

        private string ReadFile(string fileName)
        {
            var stream = File.OpenRead(FilePath + fileName);
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}
