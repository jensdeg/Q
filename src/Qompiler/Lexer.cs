using Qompiler.Types;

namespace Qompiler;

public class Lexer
{
    private readonly List<Token> _tokens = [];

    private string _input = string.Empty;
    private int _index = 0;
    private int _line = 1;

    public List<Token> Tokenize(string input)
    {
        _input = input;

        while (ReadingFile)
        {
            char c = Peek();

            if (c == '\n')
                NextLine();

            else if (char.IsWhiteSpace(c))
                Consume();

            else if (c == '"')
                ReadString();

            else if (char.IsLetter(c))
                ReadIdentifier();

            else
            {
                switch (c)
                {
                    case ';': AddSimpleToken(TokenType.Semicolon); break;
                    case '(': AddSimpleToken(TokenType.OpenParenthesis); break;
                    case ')': AddSimpleToken(TokenType.CloseParenthesis); break;
                    case '=': AddSimpleToken(TokenType.Equals); break;
                    case '+': AddSimpleToken(TokenType.Plus); break;
                    default:
                        Error($"Unexpected character '{c}' at line {_line}");
                        break;
                }
            }
        }

        return _tokens;
    }

    private void ReadIdentifier()
    {
        var start = _index;

        while (char.IsLetterOrDigit(Peek()) || Peek() == '_')
            Consume();

        var value = _input[start.._index];

        if (Token.Keywords.TryGetValue(value, out var token))
            AddToken(token, start);
        else
            AddToken(TokenType.Identifier, start);
    }

    private void ReadString()
    {
        var start = _index;

        Consume(); // opening quote

        while (Peek() != '"')
            Consume();

        Consume(); // closing quote

        var value = _input[start..(_index)];

        AddToken(TokenType.String, start, value);
    }

    private void AddSimpleToken(TokenType type)
    {
        var start = _index;
        Consume();
        string lexeme = GetLexeme(start);
        _tokens.Add(Token.Create(type, lexeme, _line));
    }

    private void AddToken(TokenType type, int start)
    {
        var lexeme = GetLexeme(start);
        _tokens.Add(Token.Create(type, lexeme, _line));
    }

    private void AddToken(TokenType type, int start, object value)
    {
        var lexeme = GetLexeme(start);
        _tokens.Add(Token.CreateLiteral(type, lexeme, value, _line));
    }

    private bool ReadingFile => _index < _input.Length;

    private void Consume() => _index++;

    private char Peek(int offset = 0) => _input[_index + offset];

    private string GetLexeme(int start) => _input[start.._index];

    private void NextLine()
    {
        Consume();
        _line++;
    }

    private static void Error(string message)
    {
        Console.WriteLine(message);
        Environment.Exit(-1);
    }
}
