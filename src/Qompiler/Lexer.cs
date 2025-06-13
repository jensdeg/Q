using Qompiler.types;

namespace Qompiler
{
    public class Lexer
    {
        private static string buf = string.Empty;

        private static readonly List<Token> Tokens = [];

        private static int index = 0;

        private static string Input = string.Empty;
        public static List<Token> Tokenize(string input)
        {
            bool instring = false;
            bool InVariableName = false;
            Input = input;
            var variableValue = string.Empty;
            var variableName = string.Empty;

            foreach (char c in Input)
            {
                buf += c;
                
                
                if (!instring && !InVariableName)
                {
                    // basic tokens
                    if (c == ';') AddToken(TokenType.Semicolon);
                    if (c == '(') AddToken(TokenType.Open_Parenthesis);
                    if (c == ')') AddToken(TokenType.Close_Parenthesis);
                    if (c == '=') AddToken(TokenType.Equals);

                    // reading buffer
                    if (buf == "Print") AddToken(TokenType.Print);
                    if (buf == "var")
                    {
                        AddToken(TokenType.Var);
                        InVariableName = true;
                    }
                    if(Tokens.Select(t => t?.Literal?.Value).Contains(buf))
                    {
                        AddToken(TokenType.Variable_Literal, Literal.CreateVariable(buf));
                    }
                }
                if (InVariableName && c == '=') 
                {
                    variableName = buf[..^1].TrimEnd();
                    AddToken(TokenType.Variable_Name, Literal.CreateVariable(variableName));
                    AddToken(TokenType.Equals);
                    InVariableName = false;
                }

                // tokenizing strings
                if (c == '"' && !instring)
                {
                    AddToken(TokenType.Quotation);
                    instring = true;
                }
                else if (c == '"' && instring)
                {
                    var stringValue = buf[..^1];
                    AddToken(TokenType.String_Literal, Literal.Create(stringValue));
                    AddToken(TokenType.Quotation);
                    instring = false;
                }

                //TODO: fix for calling variable names in code with numbers
                // tokenizing numbers
                if(char.IsDigit(c) && !instring && !InVariableName)
                {
                    if (!char.IsDigit(Peek()))
                    {
                        var digitValue = buf;
                        AddToken(TokenType.Number_Literal, Literal.Create(digitValue));
                    }              
                }

                if (buf == Environment.NewLine) buf = string.Empty;
                if (string.IsNullOrWhiteSpace(buf) && !instring) buf = string.Empty;
                index++;
            }
            return Tokens;
        }

        private static void AddToken(TokenType tokenType)
        {
            Tokens.Add(Token.Create(tokenType));
            buf = string.Empty;
        }
        private static void AddToken(TokenType tokenType, Literal value)
        {
            Tokens.Add(Token.Create(tokenType, value));
        }

        private static char Peek()
        {
            if (index + 1 >= Input.Length) return char.MinValue;
            return Input[index + 1];
        }
    }
}
