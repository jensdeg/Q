using Qompiler.types;

namespace Qompiler.Test.Fixtures
{
    public static class OperationFixture
    {
        public static readonly List<Operation> Print = [
            Operation.Create(OperationType.Print, [Literal.Create("Test")]),
            Operation.Create(OperationType.Print, [Literal.Create("12345")])
        ];

        public static readonly List<Operation> VariableAssignment = [
            Operation.Create(OperationType.Variable_Assignment,
                [Literal.CreateVariable("TestVariable"), Literal.Create("TestString")]),
            Operation.Create(OperationType.Variable_Assignment,
                [Literal.CreateVariable("TestNumber"), Literal.Create("12345")])
        ];

        public static readonly List<Operation> PrintVariable = [
            Operation.Create(OperationType.Variable_Assignment,
                [Literal.CreateVariable("Hello"), Literal.Create("Hello World")]),
            Operation.Create(OperationType.Print,
                [Literal.CreateVariable("Hello")])
        ];
    }
}
