namespace Qompiler.Tools;

public class FileManager
{
    public static void WriteAsm(string filename, string content)
    {
        var Location = $"{Environment.CurrentDirectory}/build";
        Directory.CreateDirectory(Location);
        File.WriteAllText($"{Location}/{filename}.asm", content);
    }
}
