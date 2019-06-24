using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutocadPrinting
{
    class GetPaperSizeSetUp
    {
        public static List<string> GetPaperSize(string accoreconsolePath)
        {
            List<string> listOfPaperSize = new List<string>();
            List<string> commands = new List<string>();
            commands.Add("SECURELOAD");
            commands.Add("0");
            commands.Add("NETLOAD");
            commands.Add(AppDomain.CurrentDomain.BaseDirectory + "findPaperSize.dll");
            commands.Add("GE_FINDPAPERSIZE1");
            File.WriteAllLines(Path.GetTempPath() + "GEPaperSizeScript.scr" , commands.ToArray());
            string batCommand = "\"" + accoreconsolePath + "\"" + " /i " + "\"" + AppDomain.CurrentDomain.BaseDirectory + "trashFile.dwg" + "\"" + 
                " /s " + "\"" + Path.GetTempPath() + "GEPaperSizeScript.scr" + "\"" +
                " /isolate \"MyPage\"";
            File.WriteAllText(Path.GetTempPath() + "GEgetPageSize.bat", batCommand);
            Process process = new Process();
            process.StartInfo.FileName = Path.GetTempPath() + "GEgetPageSize.bat";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.WaitForExit();

            if (File.Exists(Path.GetTempPath() + "GEPaperSizeScript.scr")) File.Delete(Path.GetTempPath() + "GEPaperSizeScript.scr");
            if (File.Exists(Path.GetTempPath() + "GEgetPageSize.bat")) File.Delete(Path.GetTempPath() + "GEgetPageSize.bat");

            if(File.Exists(Path.GetTempPath() + "mediaPageSizes.txt"))
            {
                listOfPaperSize = new List<string>(File.ReadAllLines(Path.GetTempPath() + "mediaPageSizes.txt"));
                File.Delete(Path.GetTempPath() + "mediaPageSizes.txt");
                return listOfPaperSize;
            }
            else
            {
                return null;
            }
        }
    }
}
