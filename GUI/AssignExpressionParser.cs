using MatrixCalculator;
using System.Globalization;
using System.Text.RegularExpressions;

namespace GUI
{
    /// <summary>
    /// Структура хранящая промежуточный результат.
    /// </summary>
    struct Result
    {
        private float accumulator;
        private string var_name;
        private string expression;

        public Result(string expression, string var_name)
        {
            accumulator = 0;
            this.expression = expression;
            this.var_name = var_name;
        }

        public float Accumulator
        {
            get { return accumulator; }
            set { accumulator = value; }
        }

        public string Expression
        {
            get { return expression; }
            set { expression = value; }
        }

        public string VariableName
        {
            get { return var_name; }
            set { var_name = value; }
        }
    }

    public class AssignExpressionParser
    {
        /// <summary>
        /// Проверияет выражение на принадлежность присваюваещему выражению.
        /// </summary>
        /// <returns>
        /// Возвращает принадлежность выражению.
        /// </returns>
        /// <param name="expr">Присваюваещее выражение.</param>
        public static bool IsAssignCommand(string expr)
        {
            Regex assign = new Regex("[A-я]*\\s*=((\\s*[0-9-.]\\s*)*:?)*");
            return assign.Match(expr).Value.Equals(expr);
        }

        /// <summary>
        /// Разбирает присваюващее выражение в матрицу.
        /// </summary>
        /// <returns>
        /// Возвращает матрицу.
        /// </returns>
        /// <exception cref="WrongExpressionException">Срабатывает когда выражение содержит лишние данные или ошибку.</exception>
        /// <exception cref="EmptyExpressionException">Срабатывает когда выражение не содержит данных.</exception>
        /// <param name="expr">Присваюваещее выражение.</param>
        public static Matrix<float> Parse(string expr)
        {
            // Проверка на пустую строку
            expr = expr.Trim();
            if (expr.Length == 0)
            {
                throw new EmptyExpressionException(expr);
            }

            Result r = GetName(expr);
            // Вычислить размер массива
            MatrixSize matrixSize = new MatrixSize(GetColumnCount(r), GetRowCount(r));

            float[,] matrix = new float[matrixSize.Rows, matrixSize.Columns];
            for (int row = 0; row < matrixSize.Rows; row++)
            {
                for (int col = 0; col < matrixSize.Columns; col++)
                {
                    // Убрать двоеточие, достать число и положить в массив
                    r.Expression = r.Expression.Replace(':', ' ');
                    r = Number(r);
                    matrix[row, col] = r.Accumulator;
                }
            }

            // Проверка на размер оставшегося выражения
            if (r.Expression.Length > 0)
            {
                throw new WrongExpressionException(r.Expression);
            }

            return new Matrix<float>(r.VariableName, matrix, matrixSize);
        }

        /// <summary>
        /// Выделяет имя переменной из выражения.
        /// </summary>
        /// <returns>
        /// Возвращает промежуточный резульат.
        /// </returns>
        /// <param name="expr">Выражение присваивания.</param>
        private static Result GetName(string expr)
        {
            expr = expr.Trim();
            string var_name = expr.Substring(0, expr.IndexOf('=')).Trim();

            return new Result(expr[expr.IndexOf('=')..], var_name);
        }

        /// <summary>
        /// Удаляет знак "=" и пробелы из выражения.
        /// </summary>
        /// <returns>
        /// Возвращает промежуточный резульат.
        /// </returns>
        /// <param name="r">Промежуточный результат.</param>
        private static Result SkipEqu(Result r)
        {
            r.Expression = r.Expression.Trim();
            r.Expression = r.Expression[(r.Expression.IndexOf('=') + 1)..];

            return r;
        }

        /// <summary>
        /// Вычисляет количество строк в выражении.
        /// </summary>
        /// <returns>
        /// Возвращает количество строк.
        /// </returns>
        /// <param name="r">Промежуточный результат.</param>
        private static int GetRowCount(Result r)
        {
            r = SkipEqu(r);
            if (r.Expression.IndexOf(':') == -1)
            {
                return 1;
            }

            r.Expression = r.Expression.Substring(0, r.Expression.IndexOf(':')).Trim();

            int row = 0;
            while (r.Expression.Length > 0)
            {
                r.Expression = Number(r).Expression;
                row++;
            }

            return row;
        }

        /// <summary>
        /// Вычисляет количество столбцов в выражении.
        /// </summary>
        /// <returns>
        /// Возвращает количество столбцов.
        /// </returns>
        /// <param name="r">Промежуточный результат.</param>
        private static int GetColumnCount(Result r)
        {
            r = SkipEqu(r);
            int column = 1;
            foreach (char c in r.Expression)
            {
                if (c == ':')
                {
                    column++;
                }
            }

            return column;
        }

        /// <summary>
        /// Проводит лексичесий анализ и выделяет число.
        /// </summary>
        /// <returns>
        /// Возвращает прежуточный результат анализа.
        /// </returns>
        /// <exception cref="FormNumberException">Срабатывает когда число имеет неправильную форму.</exception>
        /// <exception cref="EmptyExpressionException">Срабатывает когда нехватает чисел в матрице.</exception>
        /// <param name="r">Промежуточный результат.</param>
        private static Result Number(Result r)
        {
            r = SkipEqu(r);
            string expr = r.Expression.Trim();
            if (expr.Length == 0)
            {
                throw new EmptyExpressionException(expr);
            }

            // Выделение знака
            bool negative = false;
            if (expr[0] == '-')
            {
                negative = !negative;
                expr = expr[1..];
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

            r.Accumulator = negative ? -num : num;
            r.Expression = expr[num_offset..];
            return r;
        }

    }
}
