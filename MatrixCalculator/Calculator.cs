using System;

namespace MatrixCalculator
{
    public class Calculator : ICalculator
    {
        public float[,] Add(float[,] matrix_1, float[,] matrix_2)
        {
            if (matrix_1.GetLength(0) != matrix_2.GetLength(0) || matrix_1.GetLength(1) != matrix_2.GetLength(1))
            {
                throw new DifferentDimensionException(matrix_1, matrix_2);
            }

            float[,] res = new float[matrix_1.GetLength(0), matrix_1.GetLength(1)];
            for (ushort row = 0; row < matrix_1.GetLength(0); row++)
            {
                for (ushort col = 0; col < matrix_1.GetLength(1); col++)
                {
                    res[row, col] = matrix_1[row, col] + matrix_2[row, col];
                }
            }

            return res;
        }

        public float[,] Sub(float[,] matrix_1, float[,] matrix_2)
        {
            if (matrix_1.GetLength(0) != matrix_2.GetLength(0) || matrix_1.GetLength(1) != matrix_2.GetLength(1))
            {
                throw new DifferentDimensionException(matrix_1, matrix_2);
            }

            float[,] res = new float[matrix_1.GetLength(0), matrix_1.GetLength(1)];
            for (ushort row = 0; row < matrix_1.GetLength(0); row++)
            {
                for (ushort col = 0; col < matrix_1.GetLength(1); col++)
                {
                    res[row, col] = matrix_1[row, col] - matrix_2[row, col];
                }
            }

            return res;
        }

        public float[,] Multi(float[,] matrix_1, float[,] matrix_2)
        {
            if (matrix_1.GetLength(1) != matrix_2.GetLength(0))
            {
                throw new ColumnsNotEqualRowsException(matrix_1, matrix_2);
            }

            float[,] res = new float[matrix_1.GetLength(0), matrix_2.GetLength(1)];
            for (ushort row = 0; row < matrix_1.GetLength(0); row++)
            {
                for (ushort col = 0; col < matrix_2.GetLength(1); col++)
                {
                    for (ushort i = 0; i < matrix_2.GetLength(0); i++)
                    {
                        res[row, col] += matrix_1[row, i] * matrix_2[i, col];
                    }
                }
            }

            return res;
        }

        public float[,] Multi(float[,] matrix, float number)
        {
            float[,] res = new float[matrix.GetLength(0), matrix.GetLength(1)];
            for (ushort row = 0; row < matrix.GetLength(0); row++)
            {
                for (ushort col = 0; col < matrix.GetLength(1); col++)
                {
                    res[row, col] = matrix[row, col] * number;
                }
            }

            return res;
        }

        public float Determinant(float[,] matrix)
        {
            if (matrix.GetLength(0) != matrix.GetLength(1))
            {
                throw new NotSquareException(matrix);
            }

            if (matrix.GetLength(0) == 1)
            {
                throw new Dimension_1x1_Exception(matrix);
            }

            switch (matrix.GetLength(0))
            {
                case 2:
                    return Determinant_2x2(matrix);
                case 3:
                    return Determinant_3x3(matrix);
                default:
                    return Determinant_NxN(matrix);
            }
        }

        private float Determinant_2x2(float[,] matrix)
        {
            return ( matrix[0, 0] * matrix[1, 1] ) -( matrix[0, 1] * matrix[1, 0] );
        }

        private float Determinant_3x3(float[,] matrix)
        {
            return (matrix[0, 0] * matrix[1, 1] * matrix[2, 2]) +
                   (matrix[0, 1] * matrix[1, 2] * matrix[2, 0]) +
                   (matrix[0, 2] * matrix[1, 0] * matrix[2, 1]) -
                   (matrix[0, 2] * matrix[1, 1] * matrix[2, 0]) -
                   (matrix[0, 0] * matrix[1, 2] * matrix[2, 1]) -
                   (matrix[0, 1] * matrix[1, 0] * matrix[2, 2]);
        }

        private float Determinant_NxN(float[,] matrix)
        {
            if (matrix.GetLength(0) == 3)
            {
                return Determinant_3x3(matrix);
            }

            float det = 0;
            int degree = 1;
            for ( ushort row = 0; row < matrix.GetLength(0); row++)
            {
                float[,] cut_matrix = CutRowAndCol(matrix, 0, row);
                det = det + (degree * matrix[0,row] * Determinant_NxN(cut_matrix));
                degree = degree * (-1);
            }

            return det;
        }
        
        // http://mindhalls.ru/matrix-determinant-calculation-recursive/
        private float[,] CutRowAndCol(float[,] matrix, ushort row_index, ushort column_index)
        {
            float[,] res = new float[matrix.GetLength(0) - 1, matrix.GetLength(1) - 1];

            byte offset_row = 0;
            for (ushort row = 0; row < matrix.GetLength(0) - 1; row++)
            {
                if (row == row_index)
                {
                    offset_row = 1;
                }

                byte offset_col = 0;
                for (ushort col = 0; col < matrix.GetLength(1) - 1; col++)
                {
                    if (col == column_index)
                    {
                        offset_col = 1;
                    }

                    res[row, col] = matrix[row + offset_row, col + offset_col];
                }
            }

            return res;
        }

        public ushort Rank(float[,] matrix)
        {
            bool zero_flag = true;
            foreach (ushort elem in matrix)
            {
                if (elem != 0)
                {
                    zero_flag = !zero_flag;
                    break;
                }
            }

            if (zero_flag)
            {
                return 0;
            }

            ushort m = (ushort)matrix.GetLength(0);
            ushort n = (ushort)matrix.GetLength(1);

            ushort rank = m < n ? m : n;
            for (ushort row = 0; row < rank; row++)
            {
                if (matrix[row,row] != 0)
                {
                    for (ushort col = 0; col < m; col++)
                    {
                        if (col != row)
                        {
                            float multi = matrix[col,row] / matrix[row,row];
                            for (ushort i = 0; i < rank; i++)
                            {
                                matrix[col,i] -= multi * matrix[row,i];
                            }
                        }
                    }
                }
                else
                {
                    bool reduce_flag = true;
                    for (ushort i = (ushort)(row + 1); i < m; i++)
                    {
                        if (matrix[i,row] != 0)
                        {
                            for (ushort i2 = 0; i2 < rank; i2++)
                            {
                                // Try Optimize
                                float temp = matrix[row, i2];
                                matrix[row, i2] = matrix[i, i2];
                                matrix[i, i2] = temp;
                            }

                            reduce_flag = false;
                            break;
                        }
                    }

                    if (reduce_flag)
                    {
                        rank--;
                        for (int i = 0; i < m; i++)
                        {
                            matrix[i,row] = matrix[i,rank];
                        }
                        row--;
                    }
                }
            }

            return (ushort)(rank == 0 ? 1 : rank);
        }

        // https://forum.vingrad.ru/topic-193218.html
        public float[,] Inverse(float[,] matrix)
        {
            float det = Determinant(matrix);
            if (Determinant(matrix) == 0)
            {
                throw new ZeroDeterminant_Exception(det);
            }

            ushort dimension = (ushort)matrix.GetLength(0);
            float[,] identity_matrix = new float[dimension, dimension];

            for (ushort row = 0; row < dimension; row++)
            {
                for (ushort col = 0; col < dimension; col++)
                {
                    if (row == col)
                    {
                        identity_matrix[row,col] = 1;
                    }
                }
            }

            for (ushort row = 0; row < dimension; row++)
            {
                // Если Разрешающий = 0 поиск и обмен строк
                if (matrix[row, row] == 0)
                {
                    for (ushort row_2 = 0; row_2 < dimension; row_2++)
                    {
                        if (matrix[row_2, row] != 0)
                        {
                            for (ushort i = 0; i < dimension; i++)
                            {
                                float tmp = matrix[row, i];
                                matrix[row, i] = matrix[row_2, i];
                                matrix[row_2, i] = tmp;

                                tmp = identity_matrix[row, i];
                                identity_matrix[row, i] = identity_matrix[row_2, i];
                                identity_matrix[row_2, i] = tmp;
                            }
                        }
                    }

                    // Повторная проверка
                    if (matrix[row, row] == 0)
                    {
                        continue;
                    }
                }

                for (ushort col = (ushort)(row + 1); col < dimension; col++)
                {
                    matrix[row, col] /= matrix[row, row];
                }

                for (ushort col = 0; col < dimension; col++)
                {
                    identity_matrix[row, col] /= matrix[row, row];
                }
                matrix[row, row] /= matrix[row, row];

                if (row > 0)
                {
                    for (ushort row_2 = 0; row_2 < row; row_2++)
                    {
                        for (ushort col_2 = 0; col_2 < dimension; col_2++)
                        {
                            identity_matrix[row_2, col_2] = identity_matrix[row_2, col_2] - identity_matrix[row, col_2] * matrix[row_2, row];
                        }
                        for (ushort col_2 = (ushort)(dimension - 1); col_2 >= row; col_2--)
                        {
                            matrix[row_2, col_2] = matrix[row_2, col_2] - matrix[row, col_2] * matrix[row_2, row];
                        }
                    }
                }
                for (ushort row_2 = (ushort)(row + 1); row_2 < dimension; row_2++)
                {
                    for (ushort col_2 = 0; col_2 < dimension; col_2++)
                    {
                        identity_matrix[row_2, col_2] = identity_matrix[row_2, col_2] - identity_matrix[row, col_2] * matrix[row_2, row];
                    }
                    for (ushort col_2 = (ushort)(dimension - 1); col_2 > row; col_2--)
                    {
                        matrix[row_2, col_2] = matrix[row_2, col_2] - matrix[row, col_2] * matrix[row_2, row];
                    }
    
                }
            }

            // Rounding
            for (ushort row = 0; row < dimension; row++)
            {
                for (ushort col = 0; col < dimension; col++)
                {
                    identity_matrix[row, col] = (float)Math.Round(identity_matrix[row, col], 7);
                }
            }

            return identity_matrix;
        }


        public float[,] Transpose(float[,] matrix)
        {
            float[,] res = new float[matrix.GetLength(1), matrix.GetLength(0)];

            for (ushort x = 0; x < res.GetLength(0); x++)
            {
                for (ushort y = 0; y < res.GetLength(1); y++)
                {
                    res[x, y] = matrix[y,x];
                }
            }

            return res;
        }
    }
}
