using Qompiler.types;

namespace Qompiler.Tools
{
    public static class ConsoleWriter
    {
        public static void Write(string name, List<object> list)
        {
            Console.WriteLine($"{name}");
            list.ForEach(item => Console.WriteLine($" - {item}"));
            Console.WriteLine(string.Empty);
        }
        public static void Write(string name, List<Tuple<Operation, object>> list)
        {
            Console.WriteLine(name);
            list.ForEach(item => Console.WriteLine($" - {item.Item1}, Value: {item.Item2}"));
            Console.WriteLine(string.Empty);
        }
        public static void Write(string name, List<TokenType> list)
        {
            Console.WriteLine(name);
            list.ForEach(item => Console.WriteLine($" - {item}"));
            Console.WriteLine(string.Empty);
        }

    }
}
