using MatrixCalculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSuite.MatrixCalculator
{
    [TestClass]
    public class RankTest
    {
        [TestMethod]
        public void Rank_2x4_Zero_Ok()
        {
            float[,] matrix = new float[2, 4] { { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
            ushort rank = MatrixMath.Rank(matrix);

            Assert.IsTrue(rank == 0, rank.ToString());
        }

        [TestMethod]
        public void Rank_2x2_2_Ok()
        {
            float[,] matrix = new float[2, 2] { { 1, 2 }, { 3, 4 } };
            ushort rank = MatrixMath.Rank(matrix);

            Assert.IsTrue(rank == 2, rank.ToString());
        }

        [TestMethod]
        public void Rank_2x3_2_Ok()
        {
            float[,] matrix = new float[2, 3] { { 5, -7, 0.5F }, { 88, 2, 0 } };
            ushort rank = MatrixMath.Rank(matrix);

            Assert.IsTrue(rank == 2, rank.ToString());
        }

        [TestMethod]
        public void Rank_2x4_1_Ok()
        {
            float[,] matrix = new float[2, 4] { { 0, 0, 0, 0 }, { 0, 0, 0, 1 } };
            ushort rank = MatrixMath.Rank(matrix);

            Assert.IsTrue(rank == 1, rank.ToString());
        }

        [TestMethod]
        public void Rank_3x3_2_Ok()
        {
            float[,] matrix = new float[3, 3] { { 4, 5, 6 }, { 7, 8, 9 }, { 5, 4, 3 } };
            ushort rank = MatrixMath.Rank(matrix);

            Assert.IsTrue(rank == 2);
        }

        [TestMethod]
        public void Rank_3x3_3_Ok()
        {
            float[,] matrix = new float[3, 3] { { 4, 5, 6 }, { 7, 8, 9 }, { 5, 9, 9 } };
            ushort rank = MatrixMath.Rank(matrix);

            Assert.IsTrue(rank == 3);
        }

        [TestMethod]
        public void Rank_3x5_2_Ok()
        {
            float[,] matrix = new float[3, 5] { 
                { 2, -1, 3, -2, 4 }, 
                { 4, -2, 5, 1, 7 }, 
                { 2, -1, 1, 8, 2 }
            };
            ushort rank = MatrixMath.Rank(matrix);

            Assert.IsTrue(rank == 2, rank.ToString());
        }

        [TestMethod]
        public void Rank_3x5_3_Ok()
        {
            float[,] matrix = new float[3, 5] {
                { 13, 65, 88, -9, 16 },
                { 35, 9, -26, 0.3F, 0 },
                { 78, 224, 54, 0.45F, 2 }
            };
            ushort rank = MatrixMath.Rank(matrix);

            Assert.IsTrue(rank == 3, rank.ToString());
        }

        [TestMethod]
        public void Rank_4x5_3_Ok()
        {
            float[,] matrix = new float[4, 5] {
                { -1, 3, 3, 2, 5 },
                { -3, 5, 2, 3, 4 },
                { -3, 1, -5, 0, -7 },
                { -5, 7, 1, 4, 1 }
            };
            ushort rank = MatrixMath.Rank(matrix);

            Assert.IsTrue(rank == 3, rank.ToString());
        }

        [TestMethod]
        public void Rank_4x6_4_Ok()
        {
            float[,] matrix = new float[4, 6] {
                { 654, 53, 5312, 2, 32, -51 },
                { 0.66F, 6, 655, 0.5F, 1, -0.1F },
                { 56, -94, -0.4F, -64, 5, 0.256F },
                { -541, 6, -54, 0.3F, 58, 4 }
            };
            ushort rank = MatrixMath.Rank(matrix);

            Assert.IsTrue(rank == 4, rank.ToString());
        }

        [TestMethod]
        public void Rank_4x7_4_Ok()
        {
            float[,] matrix = new float[4, 7] {
                { 71, -0.3F, 2.614F, 100, 1918, -654, 0.585F },
                { 65, -65, 0.654F, 0.54F, -6, -0.65F, 8948 },
                { 54, 0, 55, 65, 5, -88, -0.666F },
                { 45, 458, 4, 7, 38, 73, -0.3654F }
            };
            ushort rank = MatrixMath.Rank(matrix);

            Assert.IsTrue(rank == 4, rank.ToString());
        }

        [TestMethod]
        public void Rank_5x2_2_Ok()
        {
            float[,] matrix = new float[5, 2] {
                { 9, 0.84F },
                { -554, 758 },
                { 14, 0 },
                { -887, 0.365F },
                { 0.2F, 0.1F }
            };
            ushort rank = MatrixMath.Rank(matrix);

            Assert.IsTrue(rank == 2, rank.ToString());
        }

        [TestMethod]
        public void Rank_5x3_3_Ok()
        {
            float[,] matrix = new float[5, 3] {
                { 0.16F, -8.252F, 522 },
                { -16, 78, -6 },
                { -4.5F, 0.47F, -13 },
                { 75, 0.4F, 456 },
                { -69, 54, -0.47F }
            };
            ushort rank = MatrixMath.Rank(matrix);

            Assert.IsTrue(rank == 3, rank.ToString());
        }

        [TestMethod]
        public void Rank_5x4_4_Ok()
        {
            float[,] matrix = new float[5, 4] { { 435, 345, 234, 345 }, { 234, 345, 456, 435 }, { 234, 345, 546, 25 }, { 345, 36, 456, 345 }, { 234, 234, 345, 234 } };
            ushort rank = MatrixMath.Rank(matrix);

            Assert.IsTrue(rank == 4, rank.ToString());
        }

        [TestMethod]
        public void Rank_6x4_5_Ok()
        {
            float[,] matrix = new float[6, 4] {
                { 689, 5, 0.5F, -25 },
                { 0, -7, 7, -1 },
                { 33, -66, 1, -3 },
                { -8.3F, -0.25F, -0.3F, 31 },
                { 0.36F, 12, -2, 12.36F },
                { -86, -9.6F, 7.25F, 45 }
            };
            ushort rank = MatrixMath.Rank(matrix);

            Assert.IsTrue(rank == 4, rank.ToString());
        }

        [TestMethod]
        public void Rank_6x5_5_Ok()
        {
            float[,] matrix = new float[6, 5] {
                { 78, 0.36F, -73, 8, 17 },
                { 0, 48, 0.33F, -0.33F, 21 },
                { -88, 7.38F, 0.1F, -99, -0.58F },
                { 0, 19, 25, -0.21F, 62 },
                { 56, -5, -6.33F, 0.38F, 2 },
                { 0, 2.36F, 33, 4.3F, -9.4F }
            };
            ushort rank = MatrixMath.Rank(matrix);

            Assert.IsTrue(rank == 5, rank.ToString());
        }
    }
}
