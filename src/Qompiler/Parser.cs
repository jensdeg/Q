using Qompiler.types;

namespace Qompiler
{
    public static class Parser
    {
        private static List<Tuple<Operation, object>> Operations = [];
        private static List<TokenType> Buf = [];
        private static int Index = 0;
        public static List<Tuple<Operation, object>> Parse(List<TokenType> tokens, List<object> literals)
        {
            foreach (var token in tokens)
            {
                Buf.Add(token);
                if(token == TokenType.Semicolon)
                {
                    AddOperation(Buf.GetOperation(), literals[Index]);
                }
            }
            return Operations;
        }
        private static Operation GetOperation(this List<TokenType> tokens)
        {      
            if (tokens.CompareTo(Tokens.Print))
            {
                Buf.Clear();
                return Operation.Print;
            }

            throw new ArgumentException("Unknown operation");
        }

        private static bool CompareTo(this List<TokenType> tokens, TokenType[] operationTokens)
        {
            tokens = tokens.Where(t => !Tokens.Literals.Contains(t)).ToList();
            if (tokens.SequenceEqual(operationTokens)) return true;
            return false;
        }

        private static void AddOperation(Operation operation, object value)
        {
            Operations.Add(new Tuple<Operation, object>(operation, value));
            Index++;
        }
    }
}
