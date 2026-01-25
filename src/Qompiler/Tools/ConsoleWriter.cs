using Qompiler.types;

namespace Qompiler.Tools;

public static class ConsoleWriter
{
    public static void Write(this List<Literal> literals)
    {
        Console.WriteLine(nameof(literals));
        literals.ForEach(item => Console.WriteLine($" - {item.Value}"));
        Console.WriteLine(string.Empty);
    }
    public static void Write(this List<Operation> operations)
    {
        Console.WriteLine(nameof(operations));
        operations.ForEach(o => Console.WriteLine(o));
        Console.WriteLine(string.Empty);
    }
    public static void Write(this List<Token> tokens)
    {
        Console.WriteLine(nameof(tokens));
        tokens.ForEach(t => Console.WriteLine(t));
        Console.WriteLine(string.Empty);
    }
}
