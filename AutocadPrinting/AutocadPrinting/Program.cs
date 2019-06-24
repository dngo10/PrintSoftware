using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutocadPrinting
{
    static class Program
    {
        private static string appPath { get { return @"I:\GEApp\GEDrafting_19\AutoUpdate.dll"; } }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!File.Exists(appPath))
                throw new NotSupportedException();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PrintForm());
            
        }
    }
}
