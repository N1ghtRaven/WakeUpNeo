using MatrixCalculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSuite.MatrixCalculator
{
    [TestClass]
    public class PowTest
    {
        [TestMethod]
        public void Pow_2x2_3_Ok()
        {
            float[,] matrix = new float[2, 2] { { 1, 2 }, { 3, 4 } };
            
            float[,] exp = new float[2, 2] { { 37, 54 }, { 81, 118 } };
            float[,] res = MatrixMath.Pow(matrix, 3);

            for (ushort x = 0; x < 2; x++)
            {
                for (ushort y = 0; y < 2; y++)
                {
                    Assert.IsTrue(exp[x, y] == res[x, y]);
                }
            }
        }

    }
}
