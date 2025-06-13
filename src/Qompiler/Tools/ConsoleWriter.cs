using Qompiler.types;

namespace Qompiler.Tools
{
    public static class ConsoleWriter
    {
        public static void Write(string name, List<Literal> list)
        {
            Console.WriteLine($"{name}");
            list.ForEach(item => Console.WriteLine($" - {item.Value}"));
            Console.WriteLine(string.Empty);
        }
        public static void Write(string name, List<Operation> operations)
        {
            Console.WriteLine(name);
            operations.ForEach(o => Console.WriteLine(o));
            Console.WriteLine(string.Empty);
        }
        public static void Write(string name, List<Token> tokens)
        {
            Console.WriteLine(name);
            tokens.ForEach(t => Console.WriteLine(t));
            Console.WriteLine(string.Empty);
        }
    }
}
