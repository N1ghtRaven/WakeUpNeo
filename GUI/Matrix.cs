namespace GUI
{
    public struct MatrixSize
    {
        readonly int rows, columns;

        public MatrixSize(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
        }

        public int Rows
        {
            get { return rows; }
        }

        public int Columns
        {
            get { return columns; }
        }
    }

    public class Matrix<T>
    {
        private readonly T[,] matrix = null;
        private readonly MatrixSize size;
        private readonly string name;

        /// <summary>
        /// Конструктор класса.
        /// Инициализирует существующую матрицу.
        /// </summary>
        public Matrix(string name, T[,] matrix, MatrixSize size)
        {
            this.name = name;
            this.matrix = matrix;
            this.size = size;
        }

        public T this[int x, int y]
        {
            get { return matrix[x, y]; }
            set { matrix[x, y] = value; }
        }

        public MatrixSize Size
        {
            get { return size; }
        }

        public string Name
        {
            get { return name; }
        }

        public T[,] RawMatrix
        {
            get { return matrix; }
        }
    }
}
