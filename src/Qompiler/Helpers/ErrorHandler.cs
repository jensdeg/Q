namespace Qompiler.Helpers;

public class ErrorHandler(string input)
{
    private readonly string _input = input;

    public void LexerError(string message, int line, int pos)
    {
        var LineOfCode = _input.Split(Environment.NewLine)[line - 1];

        Console.Error.WriteLine($" | Error at line '{line}':");
        Console.Error.WriteLine(" |    ...");
        Console.Error.WriteLine($" |    {LineOfCode}");
        Console.Error.WriteLine($" |    {ErrorIndicator(LineOfCode, pos)}");
        Console.Error.WriteLine(" |    ...");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Error.WriteLine(message);
        Console.ResetColor();
        Environment.Exit(-1);
    }

    public static void Error(string message)
    {
        Console.WriteLine(message);
        Environment.Exit(-1);
    }

    private static string ErrorIndicator(string lineOfCode, int pos)
    {
        var indicator = string.Empty;
        for (int i = 0; i < lineOfCode.Length; i++)
        {
            if (i == pos)
            {
                indicator += "^";
            }
            else if (i >= pos - 5 && i <= pos + 3)
            {
                indicator += "~";
            }
            else
            {
                indicator += " ";
            }
        }
        return indicator;
    }
}
