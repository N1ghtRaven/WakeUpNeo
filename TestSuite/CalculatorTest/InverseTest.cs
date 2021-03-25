using MatrixCalculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestSuite.MatrixCalculator
{
    [TestClass]
    public class InverseTest
    {
        [TestMethod]
        public void Inverse_1x1_Ok()
        {
            float[,] m = new float[1, 1] { { 2 } };
            float[,] e = new float[1, 1] { { 0.5F } };
            float[,] r = MatrixMath.Inverse(m);

            Assert.IsTrue(e[0, 0] == r[0, 0]);
        }

        [TestMethod]
        public void Inverse_2x2_Ok()
        {
            float[,] m = new float[2, 2] { { 3, -5 }, { 1, -2 } };
            float[,] e = new float[2, 2] { { 2, -5 }, { 1, -3 } };

            float[,] res = MatrixMath.Inverse(m);
            for (ushort x = 0; x < m.GetLength(0); x++)
            {
                for (ushort y = 0; y < m.GetLength(1); y++)
                {
                    Assert.IsTrue((float)Math.Round(res[x, y]) == e[x, y], string.Format("Expected {0}, but have {1}", e[x, y], res[x, y]));
                }
            }
        }

        [TestMethod]
        public void Inverse_2x2_2_Ok()
        {
            float[,] m = new float[2, 2] { { 1, 2 }, { 3, 4 } };
            float[,] e = new float[2, 2] { { -2, 1 }, { 1.5F, -0.5F } };

            float[,] res = MatrixMath.Inverse(m);
            for (ushort x = 0; x < m.GetLength(0); x++)
            {
                for (ushort y = 0; y < m.GetLength(1); y++)
                {
                    Assert.IsTrue(res[x, y] == e[x, y], string.Format("Expected {0}, but have {1}", e[x, y], res[x, y]));
                }
            }
        }

        [TestMethod]
        public void Inverse_2x2_3_Ok()
        {
            float[,] m = new float[2, 2] { { 1, 2 }, { 3, 5 } };
            float[,] e = new float[2, 2] { { -5, 2 }, { 3, -1 } };

            float[,] res = MatrixMath.Inverse(m);
            for (ushort x = 0; x < m.GetLength(0); x++)
            {
                for (ushort y = 0; y < m.GetLength(1); y++)
                {
                    Assert.IsTrue((float)Math.Round(res[x, y]) == e[x, y], string.Format("Expected {0}, but have {1}", e[x, y], res[x, y]));
                }
            }
        }

        [TestMethod]
        public void Inverse_3x3_Ok()
        {
            float[,] m = new float[3, 3] { { 3, 0, 2 }, { 2, 0, -2 }, { 0, 1, 1 } };
            float[,] e = new float[3, 3] { { 0.2F, 0.2F, 0 }, { -0.2F, 0.3F, 1 }, { 0.2F, -0.3F, 0 } };

            float[,] res = MatrixMath.Inverse(m);
            for (ushort x = 0; x < m.GetLength(0); x++)
            {
                for (ushort y = 0; y < m.GetLength(1); y++)
                {
                    Assert.IsTrue((float)Math.Round(res[x, y], 4) == e[x, y], string.Format("Expected {0}, but have {1}", e[x, y], res[x, y]));
                }
            }
        }

        [TestMethod]
        public void Inverse_4x4_Ok()
        {
            float[,] m = new float[4, 4] { 
                { 6, -5, 8, 4 },
                { 9, 7, 5, 2 },
                { 7, 5, 3, 7 },
                { -4, 8, -8, -3 }
            };

            float[,] e = new float[4, 4] {
                { 5.56F, -0.77F, -0.93F, 4.73F },
                { -3, 0.5F, 0.5F, -2.5F },
                { -5.36F, 0.87F, 0.83F, -4.63F },
                { -1.12F, 0.04F, 0.36F, -0.96F }
            };

            float[,] res = MatrixMath.Inverse(m);
            for (ushort x = 0; x < m.GetLength(0); x++)
            {
                for (ushort y = 0; y < m.GetLength(1); y++)
                {
                    Assert.IsTrue((float)Math.Round(res[x, y], 4) == e[x, y], string.Format("Expected {0}, but have {1}", e[x, y], res[x, y]));
                }
            }
        }

        [TestMethod]
        public void Inverse_5x5_Ok()
        {
            float[,] m = new float[5, 5] {
                { 0, -2, 2, 0, 0 },
                { 0, 0, 0, 1, 0 },
                { 1, 2, 1, -2, 2 },
                { 0, 2, 2, -1, 1 },
                { 2, 0, -2, 0, -2 }
            };

            float[,] e = new float[5, 5] {
                { 0.25F, 0.625F, 0.375F, -0.125F, 0.3125F },
                { -0.25F, 0.125F, -0.125F, 0.375F, 0.0625F },
                { 0.25F, 0.125F, -0.125F, 0.375F, 0.0625F },
                { 0, 1, 0, 0, 0 },
                { 0, 0.5F, 0.5F, -0.5F, -0.25F }
            };

            float[,] res = MatrixMath.Inverse(m);
            for (ushort x = 0; x < m.GetLength(0); x++)
            {
                for (ushort y = 0; y < m.GetLength(1); y++)
                {
                    Assert.IsTrue((float)Math.Round(res[x, y], 4) == e[x, y], string.Format("Expected {0}, but have {1}", e[x, y], res[x, y]));
                }
            }
        }

        [TestMethod]
        public void Inverse_6x6_Ok()
        {
            float[,] m = new float[6, 6] {
                { 1, 2, 0, 2, 0, 1 },
                { 2, 1, -1, -1, 0, -2 },
                { 1, 1, 2, 0, 0, 0 },
                { 0, 0, -2, -2, -1, 1 },
                { -1, 2, 1, 1, 2, 0 },
                { 0, 0, 2, 0, 0, 0 }
            };

            float[,] e = new float[6, 6] {
                { -0.8F, -0.7F, 2.9F, -0.6F, -0.3F, -3.7F },
                { 0.8F, 0.7F, -1.9F, 0.6F, 0.3F, 2.7F },
                { 0, 0, 0, 0, 0, 0.5F },
                { 0.4F, 0.1F, -0.7F, -0.2F, -0.1F, 0.6F },
                { -1.4F, -1.1F, 3.7F, -0.8F, 0.1F, -5.1F },
                { -0.6F, -0.9F, 2.3F, -0.2F, -0.1F, -2.9F }
            };

            float[,] res = MatrixMath.Inverse(m);
            for (ushort x = 0; x < m.GetLength(0); x++)
            {
                for (ushort y = 0; y < m.GetLength(1); y++)
                {
                    Assert.IsTrue((float)Math.Round(res[x, y], 4) == e[x, y], string.Format("Expected {0}, but have {1}", e[x, y], res[x, y]));
                }
            }
        }

        [TestMethod]
        public void Inverse_7x7_Ok()
        {
            float[,] m = new float[7, 7] {
                { 0, 0, 0, 0, 1, 0, 0 },
                { 2, 0, -1, 2, 0, 0, 1 },
                { 0, 1, 0, 2, -2, 0, 0 },
                { 0, 0, 0, 2, 0, 2, 0 },
                { 1, 1, -1, 1, -2, 0, -1 },
                { 0, 2, 0, 0, 0, -2, 0 },
                { -1, 1, -2, 0, 0, 0, 0 }
            };

            float[,] e = new float[7, 7] {
                { -1, 0.25F, -0.75F, 0.375F, 0.25F, 0.375F, -0.25F },
                { -2, 0, -1, 1, 0, 1, 0 },
                { -0.5F, -0.125F, -0.125F, 0.3125F, -0.125F, 0.3125F, -0.375F },
                { 2, 0, 1, -0.5F, 0, -0.5F, 0 },
                { 1, 0, 0, 0, 0, 0, 0 },
                { -2, 0, -1, 1, 0, 0.5F, 0 },
                { -2.5F, 0.375F, -0.625F, 0.5625F, -0.625F, 0.5625F, 0.125F }
            };

            float[,] res = MatrixMath.Inverse(m);
            for (ushort x = 0; x < m.GetLength(0); x++)
            {
                for (ushort y = 0; y < m.GetLength(1); y++)
                {
                    Assert.IsTrue((float)Math.Round(res[x, y], 4) == e[x, y], string.Format("Expected {0}, but have {1}", e[x, y], res[x, y]));
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ZeroDeterminantException))]
        public void Inverse_2x2_ZeroDeterminant_Exception()
        {
            float[,] m = new float[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
            MatrixMath.Inverse(m);
        }
    }
}
