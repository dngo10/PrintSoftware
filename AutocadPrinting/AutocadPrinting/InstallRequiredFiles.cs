using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutocadPrinting
{
    class InstallRequiredFiles
    {
        public static void addpc3AndctbFile(string cadVer)
        {
            string AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            AppDataPath += "\\Autodesk\\AutoCAD " + cadVer;
            if (!Directory.Exists(AppDataPath)) return;
            string[] subVersionCadPath = Directory.GetDirectories(AppDataPath);
            if (subVersionCadPath == null || subVersionCadPath.Count() == 0) return;
            AppDataPath = subVersionCadPath[0] + "\\enu\\Plotters";
            if (Directory.Exists(AppDataPath))
            {
                try
                {
                    string pc3File = AppDomain.CurrentDomain.BaseDirectory + "DWG To PDF.pc3";
                    File.Copy(pc3File, AppDataPath + "\\DWG To PDF.pc3");
                }
                catch
                {

                }
            }
            if (!File.Exists(AppDataPath + "\\Plot Styles\\GEC_STANDARD.ctb"))
            {
                string pc3File = AppDomain.CurrentDomain.BaseDirectory + "GEC_STANDARD.ctb";
                File.Copy(pc3File, AppDataPath + "\\Plot Styles\\GEC_STANDARD.ctb");
                //File.Copy(pc3File, AppDataPath)
            }
        }

    }
}
