using MatrixCalculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSuite.MatrixCalculator
{
    [TestClass]
    public class TransposeTest
    {
        [TestMethod]
        public void Transpose_2x4_Ok()
        {
            float[,] matrix = new float[2, 4] { { 4, 7, 2, 1 }, { 3, 9, 8, 6 } };
            float[,] transpose_matrix = MatCalc.Transpose(matrix);

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

            float[,] transpose_matrix = MatCalc.Transpose(matrix);
            float[,] reverse_transpose_matrix = MatCalc.Transpose(transpose_matrix);

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
