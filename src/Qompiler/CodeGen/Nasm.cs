using JensdegTools;
using Qompiler.types;
using System.Text;

namespace Qompiler.CodeGen
{
    public class Nasm // x86-64
    {
        public static string Generate(List<Operation> operations)
        {
            StringBuilder Content = new();

            // start
            Content.AppendLine("global _start");
            Content.AppendLine("_start:");
            
            // code
            Content.AppendLine(GetOperations(operations));

            // exit
            Content.AppendLine("    ;; EXIT");
            Content.AppendLine("    mov rax, 60");
            Content.AppendLine("    mov rdi, 0");
            Content.AppendLine("    syscall");

            // Methods
            Content.AppendLine(";; METHODS");
            Content.AppendLine(GetMethods(operations));

            // data
            Content.AppendLine(";; DATA");
            Content.AppendLine("section .data");
            Content.AppendLine(GetVariableData(operations));

            return Content.ToString();
        }

        private static string GetOperations(List<Operation> operations)
        {
            StringBuilder operationContent = new();
            var variableIndex = 1;
            foreach (var operation in operations) 
            {
                var variableName = string.Empty;
                if(operation.Type == OperationType.Print)
                {
                    var literal = operation.Literal[0];
                    if (!literal.IsVariable)
                    {
                        variableName = variableIndex.ToLetters();
                        variableIndex++;
                    }
                    else
                    {
                        variableName = literal.Value.ToString();
                    }

                    operationContent.AppendLine($"    mov rsi, {variableName}");
                    operationContent.AppendLine($"    mov rdx, {variableName}len");
                    operationContent.AppendLine($"    call print");
                }
            }
            return operationContent.ToString();
        }
        private static string GetVariableData(List<Operation> operations)
        {
            StringBuilder dataSectionContent = new();
            var variableIndex = 1;
            var literals = operations
                .Select(o => o.Literal)
                .Where(l  => l != null)
                .ToList();

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
                            variableName = variableIndex.ToLetters();
                            variableIndex++;
                        }
                        //TODO: maybe dont convert numbers to variables and have a different flow for this?
                        variableValue = literal.Value.ToString();
                    }
                    dataSectionContent.AppendLine($"{variableName}: db '{variableValue}', 10");
                    dataSectionContent.AppendLine($"{variableName}len: equ $-{variableName}");
                }
            }
            return dataSectionContent.ToString();
        }
        private static string GetMethods(List<Operation> operations)
        {
            StringBuilder methodContent = new();
            var operationTypes = operations.Select(o => o.Type);
            if (operationTypes.Contains(OperationType.Print))
            {
                methodContent.AppendLine("print:");
                methodContent.AppendLine("    mov rax, 1");
                methodContent.AppendLine("    mov rdi, 1");
                methodContent.AppendLine("    syscall");
                methodContent.AppendLine("    ret");
            }
            return methodContent.ToString();
        }
    }
}
