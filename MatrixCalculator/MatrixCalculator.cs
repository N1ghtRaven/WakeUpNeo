namespace MatrixCalculator
{
    /// <summary>
    /// Основной класс Матричного калькулятора.
    /// Содержит все необходимые методы для основных операций над матрицами.
    /// </summary>
    public class MatCalc
    {
        /// <summary>
        /// Складывает две матрицы с возвращает результат.
        /// </summary>
        /// <returns>
        /// Сумма двух матриц.
        /// </returns>
        /// <exception cref="DifferentDimensionException">Срабатывает когда размерность матриц различаются.</exception>
        /// <param name="matrix_1">Слагаемая матрица.</param>
        /// <param name="matrix_2">Слагаемая матрица.</param>
        public static float[,] Add(float[,] matrix_1, float[,] matrix_2)
        {
            // Проверка размерностей
            if (matrix_1.GetLength(0) != matrix_2.GetLength(0) || matrix_1.GetLength(1) != matrix_2.GetLength(1))
            {
                throw new DifferentDimensionException(matrix_1, matrix_2);
            }

            // Цикличное сложение
            for (ushort row = 0; row < matrix_1.GetLength(0); row++)
            {
                for (ushort col = 0; col < matrix_1.GetLength(1); col++)
                {
                    matrix_1[row, col] += matrix_2[row, col];
                }
            }

            return matrix_1;
        }

        /// <summary>
        /// Вычитает одну матрицу из другой и возвращает результат.
        /// </summary>
        /// <returns>
        /// Разность двух матриц.
        /// </returns>
        /// <exception cref="DifferentDimensionException">Срабатывает когда размерность матриц различаются.</exception>
        /// <param name="matrix_1">Слагаемая матрица.</param>
        /// <param name="matrix_2">Слагаемая матрица.</param>
        public static float[,] Sub(float[,] matrix_1, float[,] matrix_2)
        {
            // Проверка размерностей
            if (matrix_1.GetLength(0) != matrix_2.GetLength(0) || matrix_1.GetLength(1) != matrix_2.GetLength(1))
            {
                throw new DifferentDimensionException(matrix_1, matrix_2);
            }

            // Цикличное вычитание
            for (ushort row = 0; row < matrix_1.GetLength(0); row++)
            {
                for (ushort col = 0; col < matrix_1.GetLength(1); col++)
                {
                     matrix_1[row, col] -= matrix_2[row, col];
                }
            }

            return matrix_1;
        }

        /// <summary>
        /// Умножает одну матрицу на другую и возвращает результат.
        /// </summary>
        /// <returns>
        /// Произведение двух матриц.
        /// </returns>
        /// <exception cref="ColumnsNotEqualRowsException">Срабатывает когда количество столбцов одной матрицы не совпадает с количеством строк второй.</exception>
        /// <param name="matrix_1">Слагаемая матрица.</param>
        /// <param name="matrix_2">Слагаемая матрица.</param>
        public static float[,] Multi(float[,] matrix_1, float[,] matrix_2)
        {
            // Проверка на количество столбцов и количество строк
            if (matrix_1.GetLength(1) != matrix_2.GetLength(0))
            {
                throw new ColumnsNotEqualRowsException(matrix_1, matrix_2);
            }

            // Создание результирующей матрицы
            float[,] res = new float[matrix_1.GetLength(0), matrix_2.GetLength(1)];

            // Циклическое умножение матриц
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

        /// <summary>
        /// Умножает одну матрицу на число и возвращает результат.
        /// </summary>
        /// <returns>
        /// Произведение матрицы на число.
        /// </returns>
        /// <param name="matrix">Слагаемая матрица.</param>
        /// <param name="number">Слагаемое.</param>
        public static float[,] Multi(float[,] matrix, float number)
        {
            // Цикличное умножение элементов матрицы на число
            for (ushort row = 0; row < matrix.GetLength(0); row++)
            {
                for (ushort col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] *= number;
                }
            }

            return matrix;
        }

        /// <summary>
        /// Находит определитель матрицы любой размерности больше "1" и возвращает его.
        /// </summary>
        /// <returns>
        /// Определитель матрицы.
        /// </returns>
        /// <exception cref="NotSquareException">Срабатывает когда матрица является прямоугольной.</exception>
        /// <exception cref="Dimension_1x1_Exception">Срабатывает когда размерность матрицы равна "1".</exception>
        /// <param name="matrix">Матрица для которой вычисляется определитель.</param>
        public static float Determinant(float[,] matrix)
        {
            // Проверка на "квадратность"
            if (matrix.GetLength(0) != matrix.GetLength(1))
            {
                throw new NotSquareException(matrix);
            }

            // Проверка на "размерность"
            if (matrix.GetLength(0) == 1)
            {
                throw new Dimension_1x1_Exception(matrix);
            }

            // На основе размерности вызываем метод определения
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

        /// <summary>
        /// Находит определитель для матрицы 2x2 и возвращает его.
        /// </summary>
        /// <returns>
        /// Определитель матрицы.
        /// </returns>
        /// <param name="matrix">Матрица для которой вычисляется определитель.</param>
        private static float Determinant_2x2(float[,] matrix)
        {
            return ( matrix[0, 0] * matrix[1, 1] ) - ( matrix[0, 1] * matrix[1, 0] );
        }

        /// <summary>
        /// Находит определитель для матрицы 3x3 и возвращает его.
        /// </summary>
        /// <returns>
        /// Определитель матрицы.
        /// </returns>
        /// <param name="matrix">Матрица для которой вычисляется определитель.</param>
        private static float Determinant_3x3(float[,] matrix)
        {
            return (matrix[0, 0] * matrix[1, 1] * matrix[2, 2]) +
                   (matrix[0, 1] * matrix[1, 2] * matrix[2, 0]) +
                   (matrix[0, 2] * matrix[1, 0] * matrix[2, 1]) -
                   (matrix[0, 2] * matrix[1, 1] * matrix[2, 0]) -
                   (matrix[0, 0] * matrix[1, 2] * matrix[2, 1]) -
                   (matrix[0, 1] * matrix[1, 0] * matrix[2, 2]);
        }

        /// <summary>
        /// Находит определитель для матрицы NxN методом Гаусса и возвращает его.
        /// </summary>
        /// <returns>
        /// Определитель матрицы.
        /// </returns>
        /// <param name="matrix">Матрица для которой вычисляется определитель.</param>
        private static float Determinant_NxN(float[,] matrix)
        {
            // Точка выхода из рекурсии
            if (matrix.GetLength(0) == 3)
            {
                return Determinant_3x3(matrix);
            }

            float det = 0;
            for (short row = 0, degree = 1; row < matrix.GetLength(0); row++, degree *= -1)
            {
                // Сложить текущее значение определителя с произведением степени множителя
                // и рекурсивно полученным определнителем от матрицы с удаленной строкой и столбцом
                det += degree * matrix[0,row] * Determinant_NxN(CutRowAndCol(matrix, 0, row));
            }

            return det;
        }

        /// <summary>
        /// Удаляет строку и столбец у матрицы и возвращает его.
        /// </summary>
        /// <returns>
        /// Матрица с удаленной строкой и столбцом.
        /// </returns>
        /// <param name="matrix">Матрица у которой удаляются стока и столбец.</param>
        /// <param name="row_index">Индекс строки.</param>
        /// <param name="column_index">Индекс столбца.</param>
        private static float[,] CutRowAndCol(float[,] matrix, short row_index, short column_index)
        {
            // Инициализация результирующей матрицы с меньшим резмером
            float[,] res = new float[matrix.GetLength(0) - 1, matrix.GetLength(1) - 1];

            byte offset_row = 0;
            for (ushort row = 0; row < matrix.GetLength(0) - 1; row++)
            {
                // Определение удаляемой строки
                if (row == row_index)
                {
                    offset_row = 1;
                }

                byte offset_col = 0;
                for (ushort col = 0; col < matrix.GetLength(1) - 1; col++)
                {
                    // Определение удаляемого стобца
                    if (col == column_index)
                    {
                        offset_col = 1;
                    }

                    // Копирование элементов матрицы с добавленимем смещения если это необходимо
                    res[row, col] = matrix[row + offset_row, col + offset_col];
                }
            }

            return res;
        }

        /// <summary>
        /// Вычисляет ранг матрицы и возвращает его.
        /// </summary>
        /// <returns>
        /// Ранг матрицы.
        /// </returns>
        /// <param name="matrix">Матрица для которой вычисляется ранг.</param>
        public static ushort Rank(float[,] matrix)
        {
            // Проверка на "нулевую матрицу"
            bool zero_flag = true;
            foreach (float elem in matrix)
            {
                if (elem != 0)
                {
                    zero_flag = !zero_flag;
                    break;
                }
            }

            // Если матрица "нулевая" её ранг равен "0"
            if (zero_flag)
            {
                return 0;
            }

            // Объявление размеров (просто для удобства)
            ushort m = (ushort)matrix.GetLength(0);
            ushort n = (ushort)matrix.GetLength(1);

            // Поиск наименьшей стороны матрицы, т.к. ранг не может превосходить min(M,N)
            ushort rank = m < n ? m : n;
            for (ushort row = 0; row < rank; row++)
            {
                // Проверка на "нулевой" элемент главной диагонали 
                if (matrix[row,row] != 0)
                {
                    for (ushort col = 0; col < m; col++)
                    {
                        // Проверка на не принадлежность элемента к главной диагонали
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
                    // Поиск "нулевой" строки
                    bool reduce_flag = true;
                    for (ushort i = (ushort)(row + 1); i < m; i++)
                    {
                        if (matrix[i,row] != 0)
                        {
                            // Обмен двух строк
                            for (ushort i2 = 0; i2 < rank; i2++)
                            {
                                float tmp = matrix[row, i2];
                                matrix[row, i2] = matrix[i, i2];
                                matrix[i, i2] = tmp;
                            }

                            reduce_flag = false;
                            break;
                        }
                    }

                    // Копирование строки проверяемого индекса ранга в нулевую строку
                    if (reduce_flag)
                    {
                        rank--;
                        for (ushort i = 0; i < m; i++)
                        {
                            matrix[i,row] = matrix[i,rank];
                        }
                        row--;
                    }
                }
            }

            return (ushort)(rank == 0 ? 1 : rank);
        }

        /// <summary>
        /// Вычисляет обратную матрицу и возвращает её.
        /// </summary>
        /// <returns>
        /// Обратная матрица.
        /// </returns>
        /// <exception cref="ZeroDeterminantException">Срабатывает когда определитель матрицы равен "0".</exception>
        /// <param name="matrix">Матрица для которой вычисляется обратная матрица.</param>
        public static float[,] Inverse(float[,] matrix)
        {
            // Проверка на "нулевой" определитель
            float det = Determinant(matrix);
            if (Determinant(matrix) == 0)
            {
                throw new ZeroDeterminantException(det);
            }

            ushort dimension = (ushort)matrix.GetLength(0);
            
            // Создание "единичной" матрицы
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
                // Если разрешающий элемент главной диагонали равен "0", поиск возможной замена и последующий обмен строк
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

                // Строка матрицы делится на разрешающий элемент
                for (ushort col = (ushort)(row + 1); col < dimension; col++)
                {
                    matrix[row, col] /= matrix[row, row];
                }

                // Строка "единичной" матрицы делится на разрешающий элемент
                for (ushort col = 0; col < dimension; col++)
                {
                    identity_matrix[row, col] /= matrix[row, row];
                }

                // От нижней строки вычитаем верхнию умноженную на не диагональный элемент
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

            return identity_matrix;
        }

        /// <summary>
        /// Транспонирует матрицу и возвращает её.
        /// </summary>
        /// <returns>
        /// Транспонированная матрица.
        /// </returns>
        /// <param name="matrix">Матрица для которой вычисляется обратная матрица.</param>
        public static float[,] Transpose(float[,] matrix)
        {
            // Создание пустой матрицы N*M
            float[,] res = new float[matrix.GetLength(1), matrix.GetLength(0)];

            // Копирование элеменов в пустую матрицу
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
