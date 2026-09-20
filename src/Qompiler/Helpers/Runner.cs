using System.Diagnostics;

namespace Qompiler.Helpers;

public static class Runner
{
    public static void RunAssembly(string filename)
    {
        var commandToExecute =
            $"wsl nasm -felf64 {filename}.asm && " +
            $"wsl ld {filename}.o -o {filename} && " +
            $"wsl ./{filename}";

        ExecuteBuildCommand(commandToExecute);
    }

    public static void RunC(string filename)
    {
        var commandToExecute = $""""
            clang -Wno-everything {filename}.c -o main.exe
            main.exe
            """";

        ExecuteBuildCommand(commandToExecute);
    }

    private static void ExecuteBuildCommand(string command)
    {
        Process cmd = new();
        cmd.StartInfo.FileName = "cmd.exe";
        cmd.StartInfo.RedirectStandardInput = true;
        cmd.StartInfo.UseShellExecute = false;
        cmd.Start();

        cmd.StandardInput.WriteLine($"cd {Environment.CurrentDirectory}/build");
        cmd.StandardInput.WriteLine(command);
    }
}
