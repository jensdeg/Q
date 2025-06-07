using System.Diagnostics;

namespace Qompiler.Tools
{
    public static class Runner
    {
        public static void RunAssembly(string filename)
        {
            var NasmLocation = FileManager.Find("");

            var commandToExecute =
                $"wsl nasm -felf64 {filename}.asm && " +
                $"wsl ld {filename}.o -o {filename} && " +
                $"wsl ./{filename}";

            Process cmd = new();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine($"cd {NasmLocation}");
            cmd.StandardInput.WriteLine(commandToExecute);
        }
    }
}
