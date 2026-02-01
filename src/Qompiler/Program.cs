using Qompiler;

if (args.Length == 0) return;
var fileContent = File.ReadAllText(args[0]);
var filename = Path.GetFileName(args[0]);

Console.WriteLine($"Compiling '{filename}'{Environment.NewLine}");
var lexer = new Lexer();
var parser = new Parser();
var analyzer = new SemanticAnalyzer();

var tokens = lexer.Tokenize(fileContent);

foreach (var token in tokens)
{
    Console.WriteLine(token);
    Console.WriteLine();
}

var statements = parser.Parse(tokens);
var program = analyzer.Analyze(statements);

Console.WriteLine("Done");