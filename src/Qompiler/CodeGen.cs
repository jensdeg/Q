using Qompiler.types;

namespace Qompiler
{
    public class CodeGen
    {
        private static string Content = string.Empty;

        /// <summary>
        /// x86-64 NASM
        /// </summary>
        /// <param name="operations"></param>
        /// <returns></returns>
        public static string Generate(List<Tuple<Operation, object?>> operations)
        {
            var PrintOperations = GetPrintOperation(operations);
            // start
            Content += "global _start" + Environment.NewLine;
            Content += "_start:" + Environment.NewLine;

            // code
            Content += PrintOperations.Item1;

            // exit
            Content += "    mov rax, 60" + Environment.NewLine;
            Content += "    mov rdi, 0" + Environment.NewLine;
            Content += "    syscall" + Environment.NewLine;

            // data
            Content += "section .data:" + Environment.NewLine;
            Content += PrintOperations.Item2;

            return Content;
        }

        private static Tuple<string, string> GetPrintOperation(List<Tuple<Operation, object?>> operations)
        {
            var variables = "abcdefghijklmopqrstuvwxyz";
            var index = 0;
            var dataSectionContent = string.Empty;
            var operationContent = string.Empty;

            foreach (var operation in operations)
            {
                if (operation.Item2 is null) continue;
                var literal = operation.Item2.ToString();
                var variablename = variables[index];

                // data
                dataSectionContent += $"{variablename}: db '{literal}', 10" + Environment.NewLine;
                dataSectionContent += $"{variablename}len: equ $-{variablename}" + Environment.NewLine;

                // operation
                operationContent += "    mov rdi, 1" + Environment.NewLine;
                operationContent += "    mov rax, 1" + Environment.NewLine;
                operationContent += "    mov rsi, " + variablename + Environment.NewLine;
                operationContent += "    mov rdx, " + variablename + "len" + Environment.NewLine;
                operationContent += "    syscall" + Environment.NewLine;

                index++;
            }
            return new Tuple<string, string>(operationContent, dataSectionContent);
        }
    }
}
