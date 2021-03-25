using MatrixCalculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSuite.MatrixCalculator
{
    [TestClass]
    public class MultiTest
    {
        [TestMethod]
        public void Multi_3x3_3_Ok()
        {
            float[,] m = new float[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            float num = 3;

            float[,] exp = new float[3, 3] { { 3, 6, 9 }, { 12, 15, 18 }, { 21, 24, 27 } };
            float[,] res = MatCalc.Multi(m, num);

            for (ushort x = 0; x < 3; x++)
            {
                for (ushort y = 0; y < 3; y++)
                {
                    Assert.IsTrue(exp[x, y] == res[x, y]);
                }
            }
        }

        [TestMethod]
        public void Multi_3x3_1x1_Ok()
        {
            float[,] m = new float[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            float[,] n = new float[1, 1] { { 3 } };

            float[,] exp = new float[3, 3] { { 3, 6, 9 }, { 12, 15, 18 }, { 21, 24, 27 } };
            float[,] res = MatCalc.Multi(m, n);

            for (ushort x = 0; x < 3; x++)
            {
                for (ushort y = 0; y < 3; y++)
                {
                    Assert.IsTrue(exp[x, y] == res[x, y]);
                }
            }
        }

        [TestMethod]
        public void Multi_3x3_0_Ok()
        {
            float[,] m = new float[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            float num = 0;

            float[,] exp = new float[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
            float[,] res = MatCalc.Multi(m, num);

            for (ushort x = 0; x < 3; x++)
            {
                for (ushort y = 0; y < 3; y++)
                {
                    Assert.IsTrue(exp[x, y] == res[x, y]);
                }
            }
        }

        [TestMethod]
        public void Multi_3x3_3x3_Ok()
        {
            float[,] m1 = new float[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            float[,] m2 = new float[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };

            float[,] exp = new float[3, 3] { { 30, 36, 42 }, { 66, 81, 96 }, { 102, 126, 150 } };
            float[,] res = MatCalc.Multi(m1, m2);

            for (ushort x = 0; x < 3; x++)
            {
                for (ushort y = 0; y < 3; y++)
                {
                    Assert.IsTrue(exp[x, y] == res[x, y]);
                }
            }
        }

        [TestMethod]
        public void Multi_3x2_2x3_Ok()
        {
            float[,] m1 = new float[3, 2] { { 1, 2 }, { 4, 5 }, { 7, 8 } };
            float[,] m2 = new float[2, 3] { { 1, 2, 3 }, { 4, 5, 6 } };

            float[,] exp = new float[3, 3] { { 9, 12, 15 }, { 24, 33, 42 }, { 39, 54, 69 } };
            float[,] res = MatCalc.Multi(m1, m2);

            for (ushort x = 0; x < 3; x++)
            {
                for (ushort y = 0; y < 3; y++)
                {
                    Assert.IsTrue(exp[x, y] == res[x, y]);
                }
            }
        }

        [TestMethod]
        public void Multi_2x3_3x2_Ok()
        {
            float[,] m1 = new float[2, 3] { { 1, 2, 3 }, { 4, 5, 6 } };
            float[,] m2 = new float[3, 2] { { 1, 2 }, { 3, 4 }, { 5, 6 } };

            float[,] exp = new float[2, 2] { { 22, 28 }, { 49, 64 } };
            float[,] res = MatCalc.Multi(m1, m2);

            for (ushort x = 0; x < 2; x++)
            {
                for (ushort y = 0; y < 2; y++)
                {
                    Assert.IsTrue(exp[x, y] == res[x, y]);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ColumnsNotEqualRowsException))]
        public void Multi_3x3_4x3_ColumnsNotEqualRowsException()
        {
            float[,] m1 = new float[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            float[,] m2 = new float[4, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 }, { 0, 0, 0 } };

            MatCalc.Multi(m1, m2);
        }

        [TestMethod]
        [ExpectedException(typeof(ColumnsNotEqualRowsException))]
        public void Multi_3x3_2x3_ColumnsNotEqualRowsException()
        {
            float[,] m1 = new float[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            float[,] m2 = new float[2, 3] { { 1, 2, 3 }, { 4, 5, 6 } };

            MatCalc.Multi(m1, m2);
        }
    }
}
