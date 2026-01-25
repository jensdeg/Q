using FluentAssertions;
using Qompiler.Test.Fixtures;
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace Qompiler.Test
{
    public class LexerShould
    {
        private readonly string FilePath = "./Qfiles/";

        public LexerShould()
        {
            Lexer.Clear();
        }

        [Fact]
        public void TokenizePrintStatements()
        {
            // Arrange
            var content = ReadFile("Print.Q");
            var expectedTokens = TokenFixture.Print;

            // Act
            var tokens = Lexer.Tokenize(content);

            // Assert
            tokens.Should().NotBeEmpty();
            tokens.Should().Equal(expectedTokens);
        }

        [Fact]
        public void TokenizeVariableAssignment()
        {
            // Arrange
            var content = ReadFile("VariableAssignment.Q");
            var expectedTokens = TokenFixture.VariableAssignment;

            // Act
            var tokens = Lexer.Tokenize(content);

            // Assert
            tokens.Should().NotBeEmpty();
            tokens.Should().Equal(expectedTokens);
        }

        [Fact]
        public void TokenizePrintVariable()
        {
            // Arrange
            var content = ReadFile("PrintVariable.Q");
            var expectedTokens = TokenFixture.PrintVariable;

            // Act
            var tokens = Lexer.Tokenize(content);

            // Assert
            tokens.Should().NotBeEmpty();
            tokens.Should().Equal(expectedTokens);
        }

        // helper methods
        private string ReadFile(string fileName)
        {
            var stream = File.OpenRead(FilePath + fileName);
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}
