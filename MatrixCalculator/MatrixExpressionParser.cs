using System.Collections.Generic;
using System.Globalization;

namespace MatrixCalculator
{
    /// <summary>
    /// Структура хранящая промежуточный результат.
    /// </summary>
    struct Result
    {
        private float[,] accumulator;
        private string expression;

        public Result(float[,] accumulator, string expression)
        {
            this.accumulator = accumulator;
            this.expression = expression;
        }

        public float[,] Accumulator
        {
            get { return accumulator; }
            set { accumulator = value; }
        }

        public string Expression
        {
            get { return expression; }
            set { expression = value; }
        }
    }

    /// <summary>
    /// Класс парсера матричных выражений.
    /// Содержит необходимые методы для парсинга выражений методом рекурсивного спуска.
    /// </summary>
    public class MatrixExpressionParser
    {
        private readonly Dictionary<string, float[,]> vars = new Dictionary<string, float[,]>();

        /// <summary>
        /// Конструктор класса парсера.
        /// Резервирует переменные для внутренней работы.
        /// </summary>
        public MatrixExpressionParser()
        {
            // Dirty Hack
            vars["T"] = new float[1, 1] { { 0xBABADEDA } };
        }

        /// <summary>
        /// Устанавливает переменную.
        /// </summary>
        /// <exception cref="SetReservedVariableException">Срабатывает когда ведется перезапись зарезервированных переменных.</exception>
        /// <param name="name">Имя переменной.</param>
        /// <param name="value">Значение переменной.</param>
        public void SetVariable(string name, float[,] value)
        {
            if (name.Equals("T"))
            {
                throw new SetReservedVariableException(name);
            }

            vars[name] = value;
        }

        /// <summary>
        /// Удаляет переменную.
        /// </summary>
        /// <exception cref="VariableNotFoundException">Срабатывает когда переменная не найдена.</exception>
        /// <param name="name">Имя переменной.</param>
        public void RemoveVariable(string name)
        {
            if (!vars.ContainsKey(name))
            {
                throw new VariableNotFoundException(name);
            }

            vars.Remove(name);
        }

        /// <summary>
        /// Берет переменную.
        /// </summary>
        /// <returns>
        /// Возвращает значение переменной.
        /// </returns>
        /// <exception cref="VariableNotFoundException">Срабатывает когда переменная не найдена.</exception>
        /// <param name="name">Имя переменной.</param>
        public float[,] GetVariable(string name)
        {
            if (!vars.ContainsKey(name))
            {
                throw new VariableNotFoundException(name);
            }

            return vars[name];
        }

        /// <summary>
        /// Вычисляет значение матричного выражения методом рекурсивного спуска.
        /// </summary>
        /// <returns>
        /// Возвращает значение выражения.
        /// </returns>
        /// <exception cref="EmptyExpressionException">Срабатывает когда выражение пустое.</exception>
        /// <exception cref="WrongExpressionException">Срабатывает когда выражение содержит ошибку.</exception>
        /// <param name="expr">Матричное выражение.</param>
        public float[,] Parse(string expr)
        {
            expr = expr.Trim();
            if (expr.Length == 0)
            {
                throw new EmptyExpressionException(expr);
            }

            Result result = Operator(expr);
            if (result.Expression.Length != 0)
            {
                throw new WrongExpressionException(result.Expression);
            }

            return result.Accumulator;
        }

        /// <summary>
        /// Проводит лексичесий анализ операторов +-*^.
        /// </summary>
        /// <returns>
        /// Возвращает прежуточный результат анализа.
        /// </returns>
        /// <param name="expr">Матричное выражение.</param>
        private Result Operator(string expr)
        {
            Result cur = OpenBracket(expr);
            cur.Expression = cur.Expression.Trim();
            float[,] accum = cur.Accumulator;

            while (cur.Expression.Length > 0)
            {
                // Проверка на содержание оператора в выражении
                if (!(cur.Expression[0] == '+' || cur.Expression[0] == '-' || cur.Expression[0] == '*' || cur.Expression[0] == '^'))
                {
                    break;
                }

                char oper = cur.Expression[0];                
                cur = OpenBracket(cur.Expression.Substring(1));

                // Обработка оператора
                switch (oper)
                {
                    case '+':
                        return new Result(MatrixMath.Add(accum, cur.Accumulator), cur.Expression);
                    case '-':
                        return new Result(MatrixMath.Sub(accum, cur.Accumulator), cur.Expression);
                    case '*':
                        return new Result(MatrixMath.Multi(accum, cur.Accumulator), cur.Expression);
                    case '^':
                        if (cur.Accumulator[0, 0] == -1)
                        {
                            return new Result(MatrixMath.Inverse(accum), cur.Expression);
                        }

                        if (cur.Accumulator[0, 0] == vars["T"][0, 0])
                        {
                            return new Result(MatrixMath.Transpose(accum), cur.Expression);
                        }

                        return new Result(MatrixMath.Pow(accum, cur.Accumulator[0, 0]), cur.Expression);
                }
            }

            return new Result(accum, cur.Expression);
        }

        /// <summary>
        /// Ищет открывающую скобу.
        /// </summary>
        /// <returns>
        /// Возвращает прежуточный результат анализа.
        /// </returns>
        /// <exception cref="ExpectedBracketException">Срабатывает когда не обнаружена закрывающая скобка.</exception>
        /// <param name="expr">Матричное выражение.</param>
        private Result OpenBracket(string expr)
        {
            // Поиск открывающей скобки
            expr = expr.Trim();
            if (expr[0] == '(')
            {
                Result r = Operator(expr.Substring(1));
                if (r.Expression.Length != 0)
                {
                    r.Expression = r.Expression.Substring(1);
                }
                else
                {
                    throw new ExpectedBracketException(r.Expression);
                }
                return r;
            }

            return FunctionVariable(expr);
        }

        /// <summary>
        /// Проводит лексичесий анализ Функций и переменных.
        /// </summary>
        /// <returns>
        /// Возвращает прежуточный результат анализа.
        /// </returns>
        /// <param name="expr">Матричное выражение.</param>
        private Result FunctionVariable(string expr)
        {
            // Поиск имени по маске. Имя должно начинатся с буквы и может содержать цифры
            string name = "";
            for (ushort i = 0; i < expr.Length && (char.IsLetter(expr[i]) || (char.IsLetterOrDigit(expr[i]) && i > 0)); i++)
            {
                name += expr[i];
            }

            if (name.Length != 0)
            { 
                // Если содержит скобку это функция, если нет это переменная
                if (expr.Length > name.Length && expr[name.Length] == '(')
                {
                    Result r = CloseBracket(Operator(expr.Substring(name.Length + 1)));
                    return ProcessFunction(name, r);
                }
                else
                {
                    return new Result(GetVariable(name), expr.Substring(name.Length));
                }
            }

            return Number(expr);
        }

        /// <summary>
        /// Ищет заурывающую скобу.
        /// </summary>
        /// <returns>
        /// Возвращает прежуточный результат анализа.
        /// </returns>
        /// <exception cref="ExpectedBracketException">Срабатывает когда не обнаружена закрывающая скобка.</exception>
        /// <param name="r">Промежуточный резульат.</param>
        private Result CloseBracket(Result r)
        {
            // Поиск закрывающей скобки
            if (r.Expression.Length != 0 && r.Expression[0] == ')')
            {
                r.Expression = r.Expression.Substring(1);
            }
            else
            {
                throw new ExpectedBracketException(r.Expression);
            }
                
            return r;
        }

        /// <summary>
        /// Проводит лексичесий анализ и выделяет число.
        /// </summary>
        /// <returns>
        /// Возвращает прежуточный результат анализа.
        /// </returns>
        /// <exception cref="FormNumberException">Срабатывает когда число имеет неправильную форму.</exception>
        /// <param name="expr">Матричное выражение.</param>
        private Result Number(string expr)
        {
            // Выделение знака
            bool negative = false;
            if (expr[0] == '-')
            {
                negative = !negative;
                expr = expr.Substring(1);
            }

            // Поиск размера числа по маске
            ushort num_offset = 0;
            for (byte dot = 0; num_offset < expr.Length && (char.IsDigit(expr[num_offset]) || expr[num_offset] == '.'); num_offset++)
            {
                if (expr[num_offset] == '.' && ++dot > 1)
                {
                    throw new FormNumberException(expr.Substring(0, num_offset + 1));
                }
            }

            // Число не найдено
            if (num_offset == 0)
            {
                throw new FormNumberException(expr.Substring(0, num_offset + 1));
            }

            // Конвертация строки в матрицу 1x1 и установка знака
            float num = float.Parse(expr.Substring(0, num_offset), CultureInfo.InvariantCulture);
            float[,] num_mat = new float[1, 1] { { negative ? -num : num } };

            return new Result(num_mat, expr.Substring(num_offset));
        }

        /// <summary>
        /// Вызов обнаруженной функции.
        /// </summary>
        /// <returns>
        /// Возвращает прежуточный результат.
        /// </returns>
        /// <exception cref="FunctionNotDefinedException">Срабатывает когда указанная функция не определена в программе.</exception>
        /// <param name="func">Имя функции.</param>
        /// <param name="r">Промежуточный резульат.</param>
        private Result ProcessFunction(string func, Result r)
        {
            switch (func.ToLower())
            {
                case "inv":
                    return new Result(MatrixMath.Inverse(r.Accumulator), r.Expression);
                case "trans":
                    return new Result(MatrixMath.Transpose(r.Accumulator), r.Expression);
                case "rank":
                    // Упаковка в матрицу 1x1
                    float[,] rank = new float[1, 1] { { MatrixMath.Rank(r.Accumulator) } };
                    return new Result(rank, r.Expression);
                case "det":
                    // Упаковка в матрицу 1x1
                    float[,] det = new float[1, 1] { { MatrixMath.Determinant(r.Accumulator) } };
                    return new Result(det, r.Expression);
                default:
                    throw new FunctionNotDefinedException(func);
            }
        }
    }
}