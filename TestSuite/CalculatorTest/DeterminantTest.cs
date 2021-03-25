using MatrixCalculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSuite.MatrixCalculator
{
    [TestClass]
    public class DeterminantTest
    {
        [TestMethod]
        public void Determinant_1x1_Ok()
        {
            float[,] m = new float[1, 1] { { 1 } };
            Assert.IsTrue(MatrixMath.Determinant(m) == 1);
        }

        [TestMethod]
        public void Determinant_2x2_Ok()
        {
            float[,] m = new float[2, 2] { { 1, 2 }, { 3, 4 } };
            float exp = -2;
            float res = MatrixMath.Determinant(m);

            Assert.IsTrue(res == exp);
        }

        [TestMethod]
        public void Determinant_3x3_Ok()
        {
            float[,] m = new float[3, 3] { { 4, 5, 1 }, { 6, 8, 9 }, { 6, 5, 4 } };
            float exp = 80;
            float res = MatrixMath.Determinant(m);

            Assert.IsTrue(res == exp);
        }

        [TestMethod]
        public void Determinant_4x4_Ok()
        {
            float[,] m = new float[4, 4] { { 4, 5, 3, 7 }, { 6, 4, 8, 6 }, { 3, 2, 5, 3 }, { 6, 7, 8, 9 } };
            float exp = 12;
            float res = MatrixMath.Determinant(m);
            
            Assert.IsTrue(res == exp, res.ToString());
        }

        [TestMethod]
        public void Determinant_5x5_Ok()
        {
            float[,] m = new float[5, 5] {
                { 1, 2, 3, 4, 5 },
                { 10, 20, 30, 40, 50 },
                { 100, 200, 300, 400, 500 },
                { 1000, 2000, 3000, 4000, 5000 },
                { 10000, 20000, 30000, 40000, 50000 }
            };
            float exp = 0;
            float res = MatrixMath.Determinant(m);

            Assert.IsTrue(res == exp);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSquareException))]
        public void Determinant_3x4_NotSquareException()
        {
            float[,] m = new float[3, 4] { { 1, 2, 3, 0 }, { 4, 5, 6, 0 }, { 7, 8, 9, 0 } };

            MatrixMath.Determinant(m);
        }
    }
}
