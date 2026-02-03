using Qompiler.Helpers;
using Qompiler.Types;

namespace Qompiler;

public class Lexer(string input)
{
    private readonly List<Token> _tokens = [];
    private readonly string _input = input;

    private int _index = 0;
    private int _line = 1;
    private int _position = 0;

    private readonly ErrorHandler _errorHandler = new(input);

    public List<Token> Tokenize()
    {
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

            else if (char.IsDigit(c))
                ReadNumber();

            else
            {
                switch (c)
                {
                    case ';': AddSimpleToken(TokenType.Semicolon); break;
                    case '(': AddSimpleToken(TokenType.OpenParenthesis); break;
                    case ')': AddSimpleToken(TokenType.CloseParenthesis); break;
                    case '=': AddSimpleToken(TokenType.Equals); break;
                    case '+': AddSimpleToken(TokenType.Plus); break;
                    case '*': AddSimpleToken(TokenType.Star); break;
                    case '-': AddSimpleToken(TokenType.Minus); break;
                    case '/': AddSimpleToken(TokenType.FSlash); break;
                    default:
                        _errorHandler.LexerError($"Unexpected character '{c}'", _line, _position);
                        break;
                }
            }
        }

        _tokens.Add(Token.Create(TokenType.EOF, string.Empty, _line));
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
            AddToken(TokenType.Identifier, start, value);
    }

    private void ReadNumber()
    {
        var start = _index;

        while (char.IsDigit(Peek()))
            Consume();

        if (char.IsLetter(Peek()))
            _errorHandler.LexerError("Unexpected character in number", _line, _position);

        var value = int.Parse(_input[start.._index]);

        AddToken(TokenType.Number, start, value);
    }

    private void ReadString()
    {
        var start = _index;

        Consume(); // opening quote

        while (Peek() != '"')
            Consume();

        Consume(); // closing quote

        var value = _input[(start + 1)..(_index - 1)];

        AddToken(TokenType.String, start, value);
    }

    private void AddSimpleToken(TokenType type)
    {
        var start = _index;
        Consume();
        var lexeme = GetLexeme(start);
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

    private void Consume()
    {
        _index++;
        _position++;
    }

    private char Peek(int offset = 0)
    {
        if (_index >= _input.Length)
            _errorHandler.LexerError("Unexpected end of file", _line, _position);
        return _input[_index + offset];
    }

    private string GetLexeme(int start) => _input[start.._index];

    private void NextLine()
    {
        Consume();
        _position = 0;
        _line++;
    }
}
