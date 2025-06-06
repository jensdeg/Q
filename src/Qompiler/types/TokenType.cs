namespace Qompiler.types
{
    public enum TokenType
    {
        Open_Parenthesis, 
        Close_Parenthesis,
        Print,
        Quotation, 
        Semicolon, 
        String_Literal,
        Number_Literal,
        Literal, 
    }

    public static class Tokens
    {
        public static TokenType[] Literals = [
            TokenType.String_Literal,
            TokenType.Number_Literal,
            TokenType.Literal,
            TokenType.Quotation
        ];

        public static TokenType[] Print = [
            TokenType.Print,
            TokenType.Open_Parenthesis,
            TokenType.Close_Parenthesis,
            TokenType.Semicolon
        ];
    }

}
