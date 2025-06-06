using Qompiler;
using Qompiler.Tools;

// reading file
if (args.Length == 0) return;
var fileContent = FileReader.Read(args[0]);

//Compiling
Console.WriteLine($"Compiling {args[0]}{Environment.NewLine}");

var tokens = Lexer.Tokenize(fileContent);
var literals = Lexer.GetLiterals();
var operations = Parser.Parse(tokens, literals);

ConsoleWriter.Write("Literals", literals);
ConsoleWriter.Write("Tokens", tokens);
ConsoleWriter.Write("Operations", operations);

