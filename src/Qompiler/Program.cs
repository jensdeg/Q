using Qompiler;
using Qompiler.Helpers;

if (args.Length == 0) return;
(string fileContent, string fileName) = FileManager.GetFile(args[0]);

Console.WriteLine($"Compiling '{fileName}'{Environment.NewLine}");
var lexer = new Lexer(fileContent);
var parser = new Parser();
var analyzer = new SemanticAnalyzer();

var tokens = lexer.Tokenize();

foreach (var token in tokens)
{
    Console.WriteLine(token);
    Console.WriteLine();
}

var statements = parser.Parse(tokens);
var program = analyzer.Analyze(statements);

Console.WriteLine("Done");