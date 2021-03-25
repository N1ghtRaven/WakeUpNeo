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
            // Если матрица 1 содержит один элемент вызов метода умножения на число
            if (matrix_1.GetLength(0) == 1 && matrix_1.GetLength(1) == 1)
            {
                return Multi(matrix_2, matrix_1[0, 0]);
            }

            // Если матрица 2 содержит один элемент вызов метода умножения на число
            if (matrix_2.GetLength(0) == 1 && matrix_2.GetLength(1) == 1)
            {
                return Multi(matrix_1, matrix_2[0, 0]);
            }

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
        /// Возводит матрицу в степень и возвращает результат.
        /// </summary>
        /// <returns>
        /// Матрица возведенная в степень.
        /// </returns>
        /// <param name="matrix">Матрица.</param>
        /// <param name="power">Степень.</param>
        public static float[,] Pow(float[,] matrix, float power)
        {
            float[,] res = matrix;
            while (--power > 0)
            {
                res = Multi(res, matrix);
            }

            return res;
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

            // На основе размерности вызываем метод определения
            switch (matrix.GetLength(0))
            {
                case 1:
                    return Determinant_1x1(matrix);
                case 2:
                    return Determinant_2x2(matrix);
                case 3:
                    return Determinant_3x3(matrix);
                default:
                    return Determinant_NxN(matrix);
            }
        }

        /// <summary>
        /// Находит определитель для матрицы 1x1 и возвращает его.
        /// </summary>
        /// <returns>
        /// Определитель матрицы.
        /// </returns>
        /// <param name="matrix">Матрица для которой вычисляется определитель.</param>
        private static float Determinant_1x1(float[,] matrix)
        {
            return matrix[0, 0];
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
        /// Вычисляет ранг матрицы методом перебора миноров и возвращает его.
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
            ushort n = (ushort)matrix.GetLength(0);
            ushort m = (ushort)matrix.GetLength(1);

            ushort rank = 0;
            for (ushort minor_dimension = 2; minor_dimension <= (m < n ? m : n); minor_dimension++)
            {
                // Создание матрицы минора
                float[,] minor_mat = new float[minor_dimension, minor_dimension];
                for (ushort row = 0; row < (n - (minor_dimension - 1)); row++)
                {
                    for (ushort col = 0; col < (m - (minor_dimension - 1)); col++)
                    {
                        for (ushort row_2 = 0; row_2 < minor_dimension; row_2++)
                        {
                            for (ushort col_2 = 0; col_2 < minor_dimension; col_2++)
                            {
                                minor_mat[row_2, col_2] = matrix[row + row_2, col + col_2];
                            }
                        }

                        // Определение минора
                        if (Determinant(minor_mat) != 0)
                        {
                            rank = minor_dimension;
                        }
                    }
                }
            }

            return (ushort)(rank == 0 ? 1 : rank);
        }

        /// <summary>
        /// Вычисляет обратную матрицу методом разложения определителя и возвращает её.
        /// </summary>
        /// <returns>
        /// Обратная матрица.
        /// </returns>
        /// <exception cref="ZeroDeterminantException">Срабатывает когда определитель матрицы равен "0".</exception>
        /// <param name="matrix">Матрица для которой вычисляется обратная матрица.</param>
        public static float[,] Inverse(float[,] matrix)
        {
            // Проверка на "нулевой" определитель вырожденная матрица
            float determinant = Determinant(matrix);
            if (determinant == 0)
            {
                throw new ZeroDeterminantException(determinant);
            }
            determinant = 1 / determinant;

            ushort dimension = (ushort)matrix.GetLength(0);
            float[,] inverse_mat = new float[dimension, dimension];

            // Обратная матрица для 1x1
            if (dimension == 1)
            {
                inverse_mat[0, 0] = determinant;
                return inverse_mat;
            }

            bool sign = true;
            for (short row = 0; row < dimension; row++)
            {
                bool sign2 = sign;
                for (short col = 0; col < dimension; col++)
                {
                    // Удаление строки и столбца и вычисление определителя для неё
                    float det = Determinant(CutRowAndCol(matrix, col, row));
                    // Установка знака определителю
                    inverse_mat[row, col] = determinant * (sign2 ? det : -det);
                    // Инверсия знака
                    sign2 = !sign2;
                }
                // Инверсия знака
                sign = !sign;
            }

            return inverse_mat;
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
