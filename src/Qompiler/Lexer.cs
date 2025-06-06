using Qompiler.types;

namespace Qompiler
{
    public class Lexer
    {
        private static string buf = string.Empty;

        private static readonly List<TokenType> Tokens = [];

        private static readonly List<object> Literals = [];

        private static int index = 0;

        private static string Input = string.Empty;
        public static List<TokenType> Tokenize(string input)
        {
            bool instring = false;
            Input = input;

            foreach (char c in Input)
            {
                buf += c;

                // basic tokens
                if (c == ';') AddToken(TokenType.Semicolon);
                if (c == '(') AddToken(TokenType.Open_Parenthesis);
                if (c == ')') AddToken(TokenType.Close_Parenthesis);

                //tokenizing strings
                if (c == '"' && !instring)
                {
                    AddToken(TokenType.Quotation);
                    instring = true;
                }
                else if (c == '"' && instring)
                {
                    Literals.Add(buf.Remove(buf.Count() - 1));
                    AddToken(TokenType.String_Literal);
                    AddToken(TokenType.Quotation);
                    instring = false;
                }

                //tokenizing numbers
                if(char.IsDigit(c) && !instring)
                {
                    if (!char.IsDigit(Peek()))
                    {
                        Literals.Add(buf);
                        AddToken(TokenType.Number_Literal);
                    }              
                }


                // reading buffer
                if (buf == "Print")AddToken(TokenType.Print);
                if (buf == Environment.NewLine) buf = string.Empty;
                if (string.IsNullOrWhiteSpace(buf) && !instring) buf = string.Empty;

                index++;
            }
            return Tokens;
        }

        public static List<object> GetLiterals()
        {
            return Literals;
        }

        private static void AddToken(TokenType tokenType)
        {
            Tokens.Add(tokenType);
            buf = string.Empty;
        }

        private static char Peek()
        {
            if (index + 1 >= Input.Length) return char.MinValue;
            return Input[index + 1];
        }


    }
}
