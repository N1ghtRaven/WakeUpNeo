using MatrixCalculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSuite.MatrixCalculator
{
    [TestClass]
    public class SubTest
    {
        [TestMethod]
        public void Sub_3x3_3x3_Ok()
        {
            float[,] m1 = new float[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            float[,] m2 = new float[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };

            float[,] exp = new float[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
            float[,] res = MatrixMath.Sub(m1, m2);

            for (ushort x = 0; x < 3; x++)
            {
                for (ushort y = 0; y < 3; y++)
                {
                    Assert.IsTrue(exp[x, y] == res[x, y]);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(DifferentDimensionException))]
        public void Sub_3x3_3x4_DifferentDimensionException()
        {
            float[,] m1 = new float[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            float[,] m2 = new float[3, 4] { { 1, 2, 3, 0 }, { 4, 5, 6, 0 }, { 7, 8, 9, 0 } };

            MatrixMath.Sub(m1, m2);
        }
    }
}
