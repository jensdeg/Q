using System.Text;

namespace Qompiler.Helpers;

public static class StringBuilderExtensions
{
    extension(StringBuilder sb)
    {
        public StringBuilder Indent() => sb.Append($"    ");

        public StringBuilder AppendPrintNewLine(object callingClass)
        {
            var codegen = callingClass.GetType().Name.ToLower();
            return codegen switch
            {
                "c" => sb.AppendLine("""printf("\n");"""),
                _ => throw new NotSupportedException("Not supported codegen")
            };
        }
    }
}
