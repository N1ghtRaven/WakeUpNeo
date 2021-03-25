using MatrixCalculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSuite.ParserTest
{
    [TestClass]
    public class MultTest
    {
        [TestMethod]
        public void Multi_2x3_3x2_Ok()
        {
            MatrixExpressionParser math = new MatrixExpressionParser();

            float[,] m1 = new float[2, 3] { { 1, 2, 3 }, { 4, 5, 6 } };
            float[,] m2 = new float[3, 2] { { 1, 2 }, { 3, 4 }, { 5, 6 } };

            math.setVariable("A", m1);
            math.setVariable("B", m2);

            float[,] exp = new float[2, 2] { { 22, 28 }, { 49, 64 } };
            float[,] res = math.Parse("A*B");

            for (ushort x = 0; x < 2; x++)
            {
                for (ushort y = 0; y < 2; y++)
                {
                    Assert.IsTrue(exp[x, y] == res[x, y], "Expexted {0}, but recieve {1}.", exp[x, y], res[x, y]);
                }
            }
        }
    }
}
