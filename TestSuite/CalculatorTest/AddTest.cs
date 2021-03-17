using MatrixCalculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSuite.MatrixCalculator
{
    [TestClass]
    public class AddTest
    {
        [TestMethod]
        public void Add_3x3_Ok()
        {

            float[,] m1 = new float[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            float[,] m2 = new float[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            
            float[,] exp = new float[3, 3] { { 2, 4, 6 }, { 8, 10, 12 }, { 14, 16, 18 } };
            float[,] res = MatCalc.Add(m1, m2);

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
        public void Add_3x4_DifferentDimensionException()
        {
            float[,] m1 = new float[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            float[,] m2 = new float[3, 4] { { 1, 2, 3, 0 }, { 4, 5, 6, 0 }, { 7, 8, 9, 0 } };
            
            MatCalc.Add(m1, m2);
        }
    }
}
