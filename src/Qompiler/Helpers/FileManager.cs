namespace Qompiler.Helpers;

public class FileManager
{
    public static void WriteFile(string filename, string fileExtension, string content)
    {
        var location = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "build");
        Directory.CreateDirectory(location);
        var file = Path.Combine(location, $"{filename}.{fileExtension}");
        File.WriteAllText(file, content);
    }

    public static (string Content, string Name) GetFile(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"file '{filePath}' not found");
        if (!filePath.EndsWith(".Q", StringComparison.OrdinalIgnoreCase))
            throw new FileNotFoundException("Not a valid Q file, must end with .Q (or .q)");

        return (File.ReadAllText(filePath), Path.GetFileName(filePath));
    }

}
