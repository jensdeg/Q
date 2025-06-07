using Qompiler;
using Qompiler.Tools;

// reading file
if (args.Length == 0) return;
var fileContent = FileManager.Read(args[0]);
var filename = args[0].Split('.')[0];

// Compiling
Console.WriteLine($"Compiling {args[0]}{Environment.NewLine}");

var tokens = Lexer.Tokenize(fileContent);
var literals = Lexer.GetLiterals();
var operations = Parser.Parse(tokens, literals);
var Code = CodeGen.Generate(operations);

// create file
FileManager.WriteAsm(filename, Code);

// running assembly
Runner.RunAssembly(filename);


//ConsoleWriter.Write("Literals", literals);
//ConsoleWriter.Write("Tokens", tokens);
//ConsoleWriter.Write("Operations", operations);

