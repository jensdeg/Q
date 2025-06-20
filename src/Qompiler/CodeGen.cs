using Qompiler.types;

namespace Qompiler
{
    public class CodeGen
    {
        private static string Content = string.Empty;
        private static int VariableIndex = 0;

        //TODO: better data assignemnt for literals dont have a varibale assignment
        private static readonly string Variables = "abcdefghijklmopqrstuvwxyz";

        /// <summary>
        /// x86-64 NASM
        /// </summary>
        /// <param name="operations"></param>
        /// <returns></returns>
        public static string Generate(List<Operation> operations)
        {
            // start
            Content += "global _start" + Environment.NewLine;
            Content += "_start:" + Environment.NewLine;

            // code
            Content += GetOperations(operations);

            // exit
            Content += "    mov rax, 60" + Environment.NewLine;
            Content += "    mov rdi, 0" + Environment.NewLine;
            Content += "    syscall" + Environment.NewLine;

            // data
            Content += "section .data:" + Environment.NewLine;
            Content += GetVariableData(operations);

            return Content;
        }

        private static string GetOperations(List<Operation> operations)
        {
            var operationContent = string.Empty;
            foreach (var operation in operations) 
            {
                var variableName = string.Empty;
                if(operation.Type == OperationType.Print)
                {
                    var literal = operation.Literal[0];
                    if (!literal.IsVariable)
                    {
                        variableName = Variables[VariableIndex].ToString();
                        VariableIndex++;
                    }
                    else
                    {
                        variableName = literal.Value.ToString();
                    }

                    operationContent += $"    mov rax, 1 {Environment.NewLine}";
                    operationContent += $"    mov rdi, 1 {Environment.NewLine}";
                    operationContent += $"    mov rsi, {variableName} {Environment.NewLine}";
                    operationContent += $"    mov rdx, {variableName}len {Environment.NewLine}";
                    operationContent += $"    syscall {Environment.NewLine}";
                }
            }
            VariableIndex = 0;
            return operationContent;
        }


        private static string GetVariableData(List<Operation> operations)
        {
            var literals = operations
                .Select(o => o.Literal)
                .Where(l  => l != null)
                .ToList();
            var dataSectionContent = string.Empty;

            //TODO: possible issue when calling multiple variables the same name
            foreach (var OperationLiterals in literals)
            {
                var variableName = string.Empty;
                var variableValue = string.Empty;
                foreach (var literal in OperationLiterals)
                {

                    if (literal.IsVariable)
                    {
                        variableName = literal.Value.ToString();
                        continue;
                    }
                    else
                    {
                        if(variableName == string.Empty)
                        {
                            variableName = Variables[VariableIndex].ToString();
                            VariableIndex++;
                        }
                        //TODO: maybe dont convert numbers to variables and have a different flow for this?
                        variableValue = literal.Value.ToString();
                    }
                    dataSectionContent += $"{variableName}: db '{variableValue}', 10" + Environment.NewLine;
                    dataSectionContent += $"{variableName}len: equ $-{variableName}" + Environment.NewLine;
                }
            }
            VariableIndex = 0;
            return dataSectionContent;
        }
    }
}
