using Qompiler.Helpers;

namespace Qompiler.UnitTest.Mocks;

internal class MockErrorHandler : IErrorHandler
{
    public void Error(string message) 
        => throw new ArgumentException(message);

    public void LexerError(string message, int line, int pos) 
        => throw new ArgumentException($"{message};{line};{pos}");
}
