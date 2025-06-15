using FluentAssertions;
using Qompiler.Test.Fixtures;

namespace Qompiler.Test
{
    public class ParserShould
    {
        public ParserShould()
        {
            Parser.Clear();
        }

        [Fact]
        public void ParsePrint()
        {
            // Arrange
            var expectedOperations = OperationFixture.Print;

            // Act
            var operations = Parser.Parse(TokenFixture.Print);

            // Assert
            operations.Should().NotBeEmpty();
            operations.Should().Equal(expectedOperations);
        }

        [Fact]
        public void ParseVariableAssignment()
        {
            // Arrange
            var expectedOperations = OperationFixture.VariableAssignment;

            // Act
            var operations = Parser.Parse(TokenFixture.VariableAssignment);

            // Assert
            operations.Should().NotBeEmpty();
            operations.Should().Equal(expectedOperations);
        }
    }
}
