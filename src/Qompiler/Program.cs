using Qompiler;
using Qompiler.Tools;
using Qompiler.types;

if (args.Length == 0) return;
var fileContent = FileReader.Read(args[0]);
Console.WriteLine($"Compiling {args[0]}{Environment.NewLine}");

var tokens = Lexer.Tokenize(fileContent);
var literals = Lexer.GetLiterals();
var operations = Parser.Parse(tokens, literals);


literals.ForEach(literal => Console.WriteLine($"Literal: {literal}"));

Console.WriteLine("");

tokens.ToList().ForEach(token => Console.WriteLine($"Token: {token}"));

Console.WriteLine("");

operations.ForEach(operations => Console.WriteLine($"Operation: {operations.Item1}, Value: {operations.Item2}"));





