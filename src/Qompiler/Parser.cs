using Qompiler.types;

namespace Qompiler
{
    public static class Parser
    {
        private static readonly List<Operation> _Operations = [];
        private static readonly List<Token> Buf = [];

        public static List<Operation> Parse(List<Token> tokens)
        {
            foreach (var token in tokens)
            {
                Buf.Add(token);
                if (token.Type == TokenType.Semicolon)
                {
                    _Operations.Add(GetOperation(Buf));
                }
            }
            return _Operations;
        }
        private static Operation GetOperation(this List<Token> tokens)
        {
            var tokenTypes = tokens
                .Select(t => t.Type)
                .ToList();
            var literals = Buf
                .Select(t => t.Literal)
                .Where(l => l is not null)
                .Cast<Literal>()
                .ToArray();

            if (tokenTypes.CompareTo(Operations.Print))
            {
                Buf.RemoveTokens(tokens);
                return Operation.Create(OperationType.Print, literals);
            }
            if (tokenTypes.CompareTo(Operations.VariableAssignment))
            {
                Buf.RemoveTokens(tokens);
                return Operation.Create(OperationType.Variable_Assignment, literals);
            }

            throw new ArgumentException("Unknown operation");
        }

        private static bool CompareTo(this List<TokenType> tokens, TokenType[] operationTokens)
        {
            tokens = [.. tokens.Where(t => operationTokens.Contains(t))];
            if (tokens.SequenceEqual(operationTokens)) return true;
            return false;
        }

        private static void RemoveTokens(this List<Token> tokens, List<Token> tokensToRemove)
        {
            var tokens2 = new List<Token>(tokensToRemove);
            foreach (var token in tokens2)
            {
                tokens.Remove(token);
            }
        }
    }
}
