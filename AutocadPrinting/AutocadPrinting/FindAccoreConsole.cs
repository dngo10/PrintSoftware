using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutocadPrinting
{
    class FindAccoreConsole
    {
        static string programFiles = Environment.ExpandEnvironmentVariables("%ProgramW6432%");
        static string programFilesX86 = Environment.ExpandEnvironmentVariables("%ProgramFiles(x86)%");

        public static bool findAccoreConsole(out string path, out List<int> acVerList)
        {
            int i = 2013;
            int year = DateTime.Now.Year;
            path = programFiles;
            acVerList = new List<int>();
            if (Directory.Exists(programFiles + "\\Autodesk")) {
                path += "\\Autodesk";
                for(; i <= year; i++)
                {
                    if(Directory.Exists(path + "\\AutoCAD " + i.ToString()) && File.Exists(path + "\\AutoCAD " + i.ToString() + "\\accoreconsole.exe"))
                    {
                        acVerList.Add(i);
                    }
                }
            }
            return acVerList != null && acVerList.Count > 0;
        }
        // MUST FIND ACCORECONSLE
    }
}
