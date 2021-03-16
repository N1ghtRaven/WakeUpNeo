using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixCalculator
{
    public class CalculateException : ApplicationException
    {
        public CalculateException() { }
        public CalculateException(string message) : base(message) { }
    }

    public class DifferentDimensionException : CalculateException
    {
        public DifferentDimensionException()
        {}

        public DifferentDimensionException(float[,] matrix_1, float[,] matrix_2)
            : base(String.Format(
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
        public ColumnsNotEqualRowsException()
        {}

        public ColumnsNotEqualRowsException(float[,] matrix_1, float[,] matrix_2)
            : base(String.Format(
                                "Операция невозможна. Количество столбцов A не равно количеству строк B: A_{0}x{1}, B_{2}x{3}",
                                matrix_1.GetLength(0),
                                matrix_1.GetLength(1),
                                matrix_2.GetLength(0),
                                matrix_2.GetLength(1)
                                )
                  )
        { }
    }

    public class NotSquareException : CalculateException
    {
        public NotSquareException()
        { }

        public NotSquareException(float[,] matrix)
            : base(String.Format(
                                "Операция невозможна. Матрица должна быть квадратной: A_{0}x{1}",
                                matrix.GetLength(0),
                                matrix.GetLength(1)
                                )
                  )
        { }
    }

    public class Dimension_1x1_Exception : CalculateException
    {
        public Dimension_1x1_Exception()
        { }

        public Dimension_1x1_Exception(float[,] matrix)
            : base(String.Format(
                                "Операция невозможна. Матрица не должна быть единичной: A_{0}x{1}",
                                matrix.GetLength(0),
                                matrix.GetLength(1)
                                )
                  )
        { }
    }

    public class ZeroDeterminant_Exception : CalculateException
    {
        public ZeroDeterminant_Exception()
        { }

        public ZeroDeterminant_Exception(float det)
            : base(String.Format(
                                "Операция невозможна. Определитель должен быть отличен от \"0\": detA = {0}",
                                det
                                )
                  )
        { }
    }

    public class NotRectangularException : CalculateException
    {
        public NotRectangularException()
        { }

        public NotRectangularException(float[,] matrix)
            : base(String.Format(
                                "Операция невозможна. Матрица должна быть прямоугольной: A_{0}x{1}",
                                matrix.GetLength(0),
                                matrix.GetLength(1)
                                )
                  )
        { }
    }
}
