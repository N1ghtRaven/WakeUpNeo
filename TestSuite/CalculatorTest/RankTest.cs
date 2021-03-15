using MatrixCalculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestSuite.CalculatorTest
{
    [TestClass]
    public class RankTest
    {
        private static ICalculator calculator = new Calculator();

        [TestMethod]
        public void Rank_2x4_Zero_Ok()
        {
            float[,] matrix = new float[2, 4] { { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
            ushort rank = calculator.Rank(matrix);

            Assert.IsTrue(rank == 0, rank.ToString());
        }

        [TestMethod]
        public void Rank_2x4_1_Ok()
        {
            float[,] matrix = new float[2, 4] { { 0, 0, 0, 0 }, { 0, 0, 0, 1 } };
            ushort rank = calculator.Rank(matrix);

            Assert.IsTrue(rank == 1, rank.ToString());
        }

        [TestMethod]
        public void Rank_3x3_2_Ok()
        {
            float[,] matrix = new float[3, 3] { { 4, 5, 6 }, { 7, 8, 9 }, { 5, 4, 3 } };
            ushort rank = calculator.Rank(matrix);

            Assert.IsTrue(rank == 2);
        }

        [TestMethod]
        public void Rank_3x3_3_Ok()
        {
            float[,] matrix = new float[3, 3] { { 4, 5, 6 }, { 7, 8, 9 }, { 5, 9, 9 } };
            ushort rank = calculator.Rank(matrix);

            Assert.IsTrue(rank == 3);
        }

        [TestMethod]
        public void Rank_5x4_4_Ok()
        {
            float[,] matrix = new float[5, 4] { { 435, 345, 234, 345 }, { 234, 345, 456, 435 }, { 234, 345, 546, 25 }, { 345, 36, 456, 345 }, { 234, 234, 345, 234 } };
            ushort rank = calculator.Rank(matrix);

            Assert.IsTrue(rank == 4, rank.ToString());
        }
    }
}
