using System;

namespace MatrixCalculator
{
    public class CalculateException : ApplicationException
    {
        public CalculateException() {}
        public CalculateException(string message) : base(message) {}
    }

    public class DifferentDimensionException : CalculateException
    {
        public DifferentDimensionException(float[,] matrix_1, float[,] matrix_2)
            : base(string.Format(
                                "Операция невозможна. Различная размерность матриц: A_{0}x{1}, B_{2}x{3}", 
                                matrix_1.GetLength(0), 
                                matrix_1.GetLength(1), 
                                matrix_2.GetLength(0), 
                                matrix_2.GetLength(1)
                                )
                  )
        {}
    }

    public class ColumnsNotEqualRowsException : CalculateException
    {
        public ColumnsNotEqualRowsException(float[,] matrix_1, float[,] matrix_2)
            : base(string.Format(
                                "Операция невозможна. Количество столбцов A не равно количеству строк B: A_{0}x{1}, B_{2}x{3}",
                                matrix_1.GetLength(0),
                                matrix_1.GetLength(1),
                                matrix_2.GetLength(0),
                                matrix_2.GetLength(1)
                                )
                  )
        {}
    }

    public class NotSquareException : CalculateException
    {
        public NotSquareException(float[,] matrix)
            : base(string.Format(
                                "Операция невозможна. Матрица должна быть квадратной: A_{0}x{1}",
                                matrix.GetLength(0),
                                matrix.GetLength(1)
                                )
                  )
        {}
    }

    public class ZeroDeterminantException : CalculateException
    {
        public ZeroDeterminantException(float det) : base(string.Format("Операция невозможна. Матрица вырожденная: detA = {0}", det))
        {}
    }
}
