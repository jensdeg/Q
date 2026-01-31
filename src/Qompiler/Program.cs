using Qompiler;

if (args.Length == 0) return;
var fileContent = File.ReadAllText(args[0]);
var filename = Path.GetFileName(args[0]);

Console.WriteLine($"Compiling '{filename}'{Environment.NewLine}");
var lexer = new Lexer();

var tokens = lexer.Tokenize(fileContent);

foreach (var token in tokens)
{
    Console.WriteLine(token);
    Console.WriteLine();
}

Console.WriteLine("Done");