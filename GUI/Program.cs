using System;
using System.Windows.Forms;

namespace GUI
{
    static class Program
    {
        /// <summary>
        /// ����� ����� � ����������.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new App());
        }
    }
}
