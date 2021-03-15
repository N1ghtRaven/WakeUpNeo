using MatrixCalculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestSuite.CalculatorTest
{
    [TestClass]
    public class TransposeTest
    {
        private static ICalculator calculator = new Calculator();

        [TestMethod]
        public void Transpose_2x4_Ok()
        {
            float[,] matrix = new float[2, 4] { { 4, 7, 2, 1 }, { 3, 9, 8, 6 } };
            float[,] transpose_matrix = calculator.Transpose(matrix);

            float[,] exp = new float[4, 2] { { 4, 3 }, { 7, 9 }, { 2, 8 }, { 1, 6 } };

            for (ushort x = 0; x < transpose_matrix.GetLength(0); x++)
            {
                for (ushort y = 0; y < transpose_matrix.GetLength(1); y++)
                {
                    Assert.IsTrue(exp[x, y] == transpose_matrix[x, y]);
                }
            }
        }

        [TestMethod]
        public void Transpose_Reverse_Ok()
        {
            float[,] matrix = new float[2, 4] { { 4, 7, 2, 1 }, { 3, 9, 8, 6 } };

            float[,] transpose_matrix = calculator.Transpose(matrix);
            float[,] reverse_transpose_matrix = calculator.Transpose(transpose_matrix);

            for (ushort x = 0; x < matrix.GetLength(0); x++)
            {
                for (ushort y = 0; y < matrix.GetLength(1); y++)
                {
                    Assert.IsTrue(matrix[x, y] == reverse_transpose_matrix[x, y]);
                }
            }
        }
    }
}
