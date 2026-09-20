using Qompiler;
using Qompiler.CodeGen;
using Qompiler.Helpers;

if (args.Length == 0) return;
(string fileContent, string fileName) = FileManager.GetFile(args[0]);

Console.WriteLine($"Compiling '{fileName}'{Environment.NewLine}");

var tokens = new Lexer(fileContent).Tokenize();
var statements = new Parser(tokens).Parse();
var program = new SemanticAnalyzer(statements).Analyze();

new C(fileName, program).Generate();

Runner.RunC(fileName);

Console.WriteLine("Done");
