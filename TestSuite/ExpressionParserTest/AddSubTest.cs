using MatrixCalculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSuite.ParserTest
{
    [TestClass]
    public class AddSubTest
    {
        [TestMethod]
        public void Add_Ok()
        {
            MatrixExpressionParser math = new MatrixExpressionParser();

            float[,] m1 = new float[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            float[,] m2 = new float[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };

            math.setVariable("A", m1);
            math.setVariable("B", m2);

            float[,] exp = new float[3, 3] { { 2, 4, 6 }, { 8, 10, 12 }, { 14, 16, 18 } };
            float[,] res = math.Parse("A+B");

            for (ushort x = 0; x < 3; x++)
            {
                for (ushort y = 0; y < 3; y++)
                {
                    Assert.IsTrue(exp[x, y] == res[x, y]);
                }
            }
        }

        [TestMethod]
        public void Sub_Ok()
        {
            MatrixExpressionParser math = new MatrixExpressionParser();

            float[,] m1 = new float[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            float[,] m2 = new float[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };

            math.setVariable("A", m1);
            math.setVariable("B", m2);

            float[,] exp = new float[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
            float[,] res = math.Parse("A-B");

            for (ushort x = 0; x < 3; x++)
            {
                for (ushort y = 0; y < 3; y++)
                {
                    Assert.IsTrue(exp[x, y] == res[x, y]);
                }
            }
        }

    }
}
