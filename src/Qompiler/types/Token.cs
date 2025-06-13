namespace Qompiler.types
{
    public class Token
    {
        public TokenType Type { get; set; }
        public Literal? Literal { get; set; } = null;

        public static Token Create(TokenType type, Literal? value)
            => new() { Type = type, Literal = value };

        public static Token Create(TokenType type)
            => new() { Type = type };

        public override string ToString()
        {
            if (Literal != null) return $" - {Type} -> {Literal.Value}";
            else return $" - {Type}";
        }
    }

    public enum TokenType
    {
        Open_Parenthesis, 
        Close_Parenthesis,
        Print,
        Quotation, 
        Semicolon, 
        String_Literal,
        Number_Literal,
        Variable_Literal,
        Literal,
        Var,
        Variable_Name,
        Equals,
        Plus
    }
}
