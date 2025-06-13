using Qompiler;
using Qompiler.Tools;

// reading file
if (args.Length == 0) return;
var fileContent = FileManager.Read(args[0]);
var filename = args[0].Split('.')[0];

// Compiling
Console.WriteLine($"Compiling {args[0]}{Environment.NewLine}");

var tokens = Lexer.Tokenize(fileContent);
var operations = Parser.Parse(tokens);
var code = CodeGen.Generate(operations);

// Create file
FileManager.WriteAsm(filename, code);

// Running assembly
Runner.RunAssembly(filename);


//ConsoleWriter.Write("Tokens", tokens);
//ConsoleWriter.Write("Operations", operations);

