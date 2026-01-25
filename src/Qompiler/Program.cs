using Qompiler;
using Qompiler.CodeGen;
using Qompiler.Tools;

if (args.Length == 0) return;
var fileContent = File.ReadAllText(args[0]);
var filename = args[0].Split('.')[0];

Console.WriteLine($"Compiling '{args[0]}'{Environment.NewLine}");

var tokens = Lexer.Tokenize(fileContent);
var operations = Parser.Parse(tokens);
var code = Nasm.Generate(operations);

FileManager.WriteAsm(filename, code);

Console.WriteLine("Finished compiling");

Console.WriteLine("Running Build...");
Runner.RunAssembly(filename);