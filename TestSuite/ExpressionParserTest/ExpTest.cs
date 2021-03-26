using MatrixCalculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestSuite.ParserTest
{
    [TestClass]
    public class ExpTest
    {
        [TestMethod]
        public void Transpose_2x4_Ok()
        {
            MatrixExpressionParser math = new MatrixExpressionParser();

            float[,] matrix = new float[2, 4] { { 4, 7, 2, 1 }, { 3, 9, 8, 6 } };
            math.SetVariable("A", matrix);

            float[,] transpose_matrix = math.Parse("A^T");
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
        public void Pow_2x2_3_Ok()
        {
            MatrixExpressionParser math = new MatrixExpressionParser();

            float[,] matrix = new float[2, 2] { { 1, 2 }, { 3, 4 } };
            float[,] exp = new float[2, 2] { { 37, 54 }, { 81, 118 } };

            math.SetVariable("A", matrix);

            float[,] pow_matrix = math.Parse("A^3");
            for (ushort x = 0; x < pow_matrix.GetLength(0); x++)
            {
                for (ushort y = 0; y < pow_matrix.GetLength(1); y++)
                {
                    Assert.IsTrue(exp[x, y] == pow_matrix[x, y]);
                }
            }
        }

        [TestMethod]
        public void Inverse_Ok()
        {
            MatrixExpressionParser math = new MatrixExpressionParser();

            float[,] m = new float[4, 4] {
                { 6, -5, 8, 4 },
                { 9, 7, 5, 2 },
                { 7, 5, 3, 7 },
                { -4, 8, -8, -3 }
            };

            math.SetVariable("A", m);

            float[,] e = new float[4, 4] {
                { 5.56F, -0.77F, -0.93F, 4.73F },
                { -3, 0.5F, 0.5F, -2.5F },
                { -5.36F, 0.87F, 0.83F, -4.63F },
                { -1.12F, 0.04F, 0.36F, -0.96F }
            };

            float[,] res = math.Parse("A^-1");
            for (ushort x = 0; x < m.GetLength(0); x++)
            {
                for (ushort y = 0; y < m.GetLength(1); y++)
                {
                    Assert.IsTrue((float)Math.Round(res[x, y], 4) == e[x, y], string.Format("Expected {0}, but have {1}", e[x, y], res[x, y]));
                }
            }
        }

    }
}
