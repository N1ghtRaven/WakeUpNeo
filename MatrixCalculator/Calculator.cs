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
                throw new IdentityMatrixException(matrix);
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

        // TODO: Remove me
        public ushort RankO(float[,] matrix)
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

            ushort rank = matrix.GetLength(0) > matrix.GetLength(1) ? (ushort) matrix.GetLength(0) : (ushort) matrix.GetLength(1);
            bool[] line = new bool[matrix.GetLength(1)];
            for (ushort x = 0; x < matrix.GetLength(0); x++)
            {
                ushort y;
                for (y = 0; y < matrix.GetLength(1); y++)
                {
                    if (!line[y] && Math.Abs(matrix[x,y]) > double.Epsilon)
                    {
                        break;
                    }
                }
            
                if ( y == matrix.GetLength(1))
                {
                    rank--;
                }
                else
                {
                    line[y] = true;
                    for (ushort i = (ushort)(x + 1); i < matrix.GetLength(0); i++)
                    {
                        matrix[i, y] /= matrix[x, y];
                    }
                    for (ushort k = 0; k < matrix.GetLength(1); k++)
                    {
                        if (k != y && Math.Abs(matrix[x, k]) > double.Epsilon)
                        {
                            for (ushort i = (ushort)(x + 1); i < matrix.GetLength(0); i++)
                            {
                                matrix[i, k] -= matrix[i, y] * matrix[x, k];
                                //matrix[k, i] -= matrix[y, i] * matrix[k, x];
                            }
                        }
                    }
                }
            }

            ushort real_rang = 0;
            foreach(bool l in line)
            {
                if (l)
                {
                    real_rang++;
                }
            }


           return real_rang;
           
        }

        // TODO: Remove me
        public ushort RankKEK(float[,] matrix)
        {
            int n = matrix.GetLength(0);
            int m = matrix.GetLength(1);

            ushort rank = (ushort)Math.Max(n, m);
            bool[] line_used = new bool[n];
            for (ushort i = 0; i < m; i++)
            {
                ushort j;
                for (j = 0; j < n; j++)
                {
                    if (!line_used[j] && Math.Abs(matrix[j, i]) > double.Epsilon)
                    {
                        break;
                    }
                }

                if (j == n)
                {
                    rank--;
                }
                else
                {
                    line_used[j] = true;
                    for (ushort p = (ushort)(i + 1); p < m; p++)
                    {
                        matrix[j, p] /= matrix[j, i];
                    }
                    for (ushort k = 0; k < n; k++)
                    {
                        if (k != j && Math.Abs(matrix[k, i]) > double.Epsilon)
                        {
                            for (ushort p = (ushort)(i + 1); p < m; p++)
                            {
                                matrix[k, p] -= matrix[j, p] * matrix[k, i];
                            }
                        }
                    }
                }
            }

            return rank;
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
                            // Swap Rows
                            for (ushort i2 = 0; i2 < rank; i2++)
                            {
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

        public float[,] Reverse(float[,] matrix)
        {
            throw new NotImplementedException();
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
