using MatrixCalculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSuite.ParserTest
{
    [TestClass]
    public class ComplextTest
    {
        [TestMethod]
        public void Complex_Ok()
        {
            MatrixExpressionParser math = new MatrixExpressionParser();

            float[,] m1 = new float[2, 2] { { 1, 2 }, { 2, 2 } };
            float[,] m2 = new float[2, 2] { { 4, 2 }, { 2, 1 } };
            float[,] m3 = new float[2, 2] { { 1, 2 }, { 1, 4 } };

            math.SetVariable("A", m1);
            math.SetVariable("B", m2);
            math.SetVariable("C", m3);

            float[,] exp = new float[2, 2] { { 116, 83 }, { 63.5F, 68.5F } };
            float[,] res = math.Parse("((A^rank(A)) * C) + ((B + (C^-1)) + ((B^3) - (A^T)))");

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
