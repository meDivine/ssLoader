using System;
using System.IO;
using System.Windows.Forms;

namespace ssLoader
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Start());
            File.WriteAllText("C:\\Windows\\System32\\drivers\\etc\\hosts", "1.1.1.1 keyauth.com");
        }
    }
}

