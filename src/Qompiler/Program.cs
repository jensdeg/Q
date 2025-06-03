using Qompiler.Tools;

if (args.Length == 0) return;
var fileName = args[0];
var file = FileFinder.Find(fileName);


using var reader = new StreamReader(file);
var content = reader.ReadToEnd();

Console.WriteLine($"Compiling {fileName}");

