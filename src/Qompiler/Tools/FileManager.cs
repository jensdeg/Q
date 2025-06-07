using System.Globalization;

namespace Qompiler.Tools
{
    public class FileManager
    {
        /// <summary>
        /// This is very specific to the Qompiler project structure.
        /// made so it works with both the IDE and the command line.
        /// very brittle, but it works for now.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string Find(string fileName, bool createFile = false)
        {
            var directoryPath = Environment.CurrentDirectory;
            if (directoryPath.Contains("bin"))
            {
                directoryPath = directoryPath.Substring(0, directoryPath.IndexOf("bin", StringComparison.Ordinal));
            }
            if (directoryPath.Contains('Q'))
            {
                directoryPath = directoryPath.Substring(0, directoryPath.IndexOf("src", StringComparison.Ordinal));
                var fullPath = directoryPath + @"examples\" + fileName;
                if (File.Exists(fullPath))
                {
                    return fullPath;
                }
                if (createFile)
                {
                    File.Create(fullPath).Close();
                    return fullPath;
                }
                return fullPath;
            }
            throw new NotImplementedException("Not supported Yet");
        }

        public static string Read(string filename)
        {
            var filePath = Find(filename);
            using var reader = new StreamReader(filePath);
            return reader.ReadToEnd();
        }

        public static void WriteAsm(string filename, string content)
        {
            var Location = Find("") + $"/{filename}";
            Directory.CreateDirectory(Location);
            using var writer = new StreamWriter($"{Location}/{filename}.asm", false);
            writer.Write(content);
        }
    }
}
