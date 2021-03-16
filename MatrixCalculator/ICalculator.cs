using System;

namespace MatrixCalculator
{
    public interface ICalculator
    {
        float[,] Add(float[,] matrix_1, float[,] matrix_2);
        float[,] Sub(float[,] matrix_1, float[,] matrix_2);
        float[,] Multi(float[,] matrix_1, float[,] matrix_2);
        float[,] Multi(float[,] matrix, float number);
        float Determinant(float[,] matrix);
        float[,] Transpose(float[,] matrix);
        float[,] Inverse(float[,] matrix);
        ushort Rank(float[,] matrix);
    }
}
