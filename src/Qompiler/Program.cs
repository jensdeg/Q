using Qompiler;

if (args.Length == 0) return;
var fileContent = File.ReadAllText(args[0]);
var filename = args[0].Split('.')[0];

Console.WriteLine($"Compiling '{args[0]}'{Environment.NewLine}");
var lexer = new Lexer();

var tokens = lexer.Tokenize(fileContent);

foreach (var token in tokens)
{
    Console.WriteLine(token);
    Console.WriteLine();
}

Console.WriteLine("Done");