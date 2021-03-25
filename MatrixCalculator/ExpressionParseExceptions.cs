using System;

namespace MatrixCalculator
{
    public class ExpressionParseException : ApplicationException
    {
        public ExpressionParseException(string message) : base(message) { }
    }

    public class VariableNotFoundException : ExpressionParseException
    {
        public VariableNotFoundException(string var) : base(string.Format("Переменная с именем \"{0}\" не найдена.", var))
        {}
    }

    public class WrongExpressionException : ExpressionParseException
    {
        public WrongExpressionException(string expr) : base(string.Format("Ошибка разбора выражения \"{0}\".", expr))
        {}
    }

    public class EmptyExpressionException : ExpressionParseException
    {
        public EmptyExpressionException(string expr) : base(string.Format("Пустое выражение \"{0}\".", expr))
        {}
    }

    public class FunctionNotDefinedException : ExpressionParseException
    {
        public FunctionNotDefinedException(string func) : base(string.Format("Функция с именем \"{0}\" не была определена.", func))
        {}
    }

    public class FormNumberException : ExpressionParseException
    {
        public FormNumberException(string num) : base(string.Format("Неправильная форма числа \"{0}\".", num))
        {}
    }

    public class SetReservedVariableException : ExpressionParseException
    {
        public SetReservedVariableException(string name) : base(string.Format("Имя перемнной \"{0}\" зарезервировано. Используюте другое.", name))
        {}
    }

    public class ExpectedBracketException : ExpressionParseException
    {
        public ExpectedBracketException(string expr) : base(string.Format("Ожидалась открытая скобка \"{0}\".", expr))
        {}
    }
}
