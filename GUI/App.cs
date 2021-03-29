using MatrixCalculator;
using System;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GUI
{
    public partial class App : Form
    {
        private readonly MatrixExpressionParser expressionParser = new MatrixExpressionParser();
        private readonly History history = new History();

        public App()
        {
            InitializeComponent();
            AdaptiveResize();
        }

        /// <summary>
        /// Перераспределяет свободное пространство.
        /// </summary>
        private void AdaptiveResize()
        {
            // TODO: Доделать резиновую верстку
            statusLabel.Location = new Point(5, ClientSize.Height - 20);
            commandBox.Location = new Point(statusLabel.Location.X, statusLabel.Location.Y - 25);
            commandBox.Size = new Size(ClientSize.Width - (commandBox.Location.X * 2), Height);
            matrixPanel.Size = new Size(ClientSize.Width, ClientSize.Height - 50);
        }

        private void App_Resize(object sender, EventArgs e)
        {
            AdaptiveResize();
        }

        /// <summary>
        /// Выводит на экран сообщение о статусе исполнения команды.
        /// </summary>
        /// <param name="status">Статус.</param>
        private void SetStatus(string status)
        {
            statusLabel.Text = "Статус: " + status;
        }

        private void commandBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            TextBox commandBox = (TextBox)sender;
            switch (e.KeyCode)
            {
                case Keys.Up:
                    commandBox.Text = history.GetOlder(commandBox.Text);
                    break;
                case Keys.Down:
                    commandBox.Text = history.GetYounger(commandBox.Text);
                    break;
            }
        }

        private void commandBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                TextBox commandBox = (TextBox)sender;

                // Запомнить введенный текст
                history.Put(commandBox.Text);

                Matrix<float> matrix;
                // Если выражение похоже на присваивание
                if (AssignExpressionParser.IsAssignCommand(commandBox.Text))
                {
                    // Попытка разобрать выражение присваивания, если в процессе возникнет ошибка поместить в статус бар и прервать исполнение инструкций.
                    try
                    {
                        matrix = AssignExpressionParser.Parse(commandBox.Text);
                    }
                    catch (ExpressionParseException ex) { SetStatus(ex.Message); commandBox.Text = ""; return; }

                    // Если переменная уже существует, вывести уведомление в статус бар и прервать исполнение инструкций.
                    try
                    {
                        expressionParser.GetVariable(matrix.Name);
                        SetStatus(string.Format("Переменная \"{0}\" уже объявлена.", matrix.Name));
                        commandBox.Text = "";
                        return;
                    }
                    catch (VariableNotFoundException) {}

                    // Создать вкладку и вывести на нее матрицу.
                    TabPage tab = AddTab(matrix);
                    ShowMatrix(tab, matrix, true);
                }
                // Если не похоже, то пытаемся его посчитать
                else
                {
                    // Попытка разобрать матричное выражение и посчитать его, если в процессе будут ошибки вывести в статус бар и прерват дальнейнее выполнение.
                    try
                    {
                        float[,] m = expressionParser.Parse(commandBox.Text);
                        matrix = new Matrix<float>("result", m, new MatrixSize(m.GetLength(0), m.GetLength(1)));

                        // Очистить результирующую вкладку и вывести на неё матрицу
                        resultTab.Controls.Clear();
                        ShowMatrix(resultTab, matrix, false);
                        SetStatus("Выполнено.");
                    }
                    catch (ExpressionParseException ex) { SetStatus(ex.Message); }
                    catch (CalculateException ex) { SetStatus(ex.Message); }
                }
                commandBox.Text = "";
            }
        }

        /// <summary>
        /// Добавляет вкладку на панель.
        /// </summary>
        /// <returns>
        /// Объект вкладки.
        /// </returns>
        /// <param name="matrix">Матрица.</param>
        private TabPage AddTab(Matrix<float> matrix)
        {
            // Создать вкладку с именем
            TabPage tab = new TabPage(matrix.Name);

            // Создать кнопку закрытия вкладки
            Button btn = new Button
            {
                Location = new Point(0, 0),
                Size = new Size(ClientSize.Width - 10, 21),
                Text = "Удалить переменную",
                Font = new Font("Segoe UI", 8, FontStyle.Regular)
            };
            // Отработка нажатия
            btn.Click += new EventHandler(
                (o, e) => 
                {
                    // Вытащить вкладку
                    TabPage tab = (TabPage)((Button)o).Parent;
                    string var_name = tab.Text;

                    // Удалить переменную из контекста и вкладку
                    expressionParser.RemoveVariable(var_name);
                    matrixPanel.Controls.Remove(tab);
                    SetStatus(string.Format("Переменная \"{0}\" была удалена.", var_name));
                });

            // Добавить кнопку на вкладку
            tab.Controls.Add(btn);

            // Добавить переменную в контекст и добавить вкладку на панель
            expressionParser.SetVariable(matrix.Name, matrix.RawMatrix);
            matrixPanel.TabPages.Insert(matrixPanel.TabPages.Count - 1, tab);
            SetStatus(string.Format("Переменная \"{0}\" была создана.", matrix.Name));

            return tab;
        }

        /// <summary>
        /// Добавляет вкладку на панель.
        /// </summary>
        /// <returns>
        /// Объект вкладки.
        /// </returns>
        /// <param name="tab">Вкладка.</param>
        /// <param name="matrix">Матрица.</param>
        /// <param name="editable">Возможность изменять значение.</param>
        private void ShowMatrix(TabPage tab, Matrix<float> matrix, bool editable)
        {
            // Удалить с панели все кроме кнопок
            foreach (Control cont in tab.Controls)
            {
                if (cont.GetType() != typeof(Button))
                {
                    tab.Controls.Remove(cont);
                }
            }

            for (int row = 0; row < matrix.Size.Rows; row++)
            {
                for (int col = 0; col < matrix.Size.Columns; col++)
                {
                    // Создать текстовое поле
                    TextBox tb = new TextBox
                    {
                        // Присвоить значение
                        Text = matrix[row, col].ToString(),
                        // Выровнять по центру
                        TextAlign = HorizontalAlignment.Center,
                        // Добавить метаданные для возможности изменения значения в матрице
                        Tag = string.Format("{0}_{1}_{2}", row, col, tab.Text),
                        Size = new Size(30, 20),
                        Location = new Point(40 * col, (35 * row) + 25),
                        // Возможность вносить изменения в матрицу
                        Enabled = editable
                    };
                    // Отслеживание при нажатие клавиш
                    tb.KeyPress += new KeyPressEventHandler(
                        (s, e) =>
                        {
                            // Разрешить ввод только цифр, минуса и точки
                            e.Handled = !(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == '-' || e.KeyChar == '.');

                            // Отловить нажатие Enter
                            if (e.KeyChar == (char)Keys.Enter)
                            {
                                // Преобразовать в текстовое поле
                                TextBox text_box = (TextBox)s;

                                // Разобрать метаданные
                                string[] tag = text_box.Tag.ToString().Split("_");
                                int row = int.Parse(tag[0]);
                                int col = int.Parse(tag[1]);
                                string var_name = tag[2];

                                // Достать переменную по имени из контекста
                                float[,] matrix = expressionParser.GetVariable(var_name);

                                // Проверить на правильную запись числа в поле 
                                Regex num = new Regex("-?[0-9]{1,}\\.?[0-9]{0,}");
                                if (num.Match(text_box.Text).Value.Equals(text_box.Text))
                                {
                                    // Конвертировать значение в число и изменить значение в матрице
                                    matrix[row, col] = float.Parse(text_box.Text, CultureInfo.InvariantCulture);

                                    // Переопределить переменную
                                    expressionParser.RemoveVariable(var_name);
                                    expressionParser.SetVariable(var_name, matrix);

                                    SetStatus(string.Format("Переменная была изменена - {0}[{1}, {2}] = {3}", var_name, row, col, matrix[row, col]));
                                }
                                // Если число некорректно, вернуть старое значение
                                else
                                {
                                    text_box.Text = matrix[row, col].ToString();
                                    SetStatus("Неверная форма числа. Значение возвращено.");
                                }
                            }
                        });

                    // Добавить поле ввода на вкладку
                    tab.Controls.Add(tb);
                }
            }
        }


    }
}
