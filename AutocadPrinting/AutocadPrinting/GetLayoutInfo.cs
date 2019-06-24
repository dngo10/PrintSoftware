using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutocadPrinting

{
    class GetLayoutInfo
    {
        // Used for [+] button. Get layouts from 1 file *dwg.
        public static void getLayouts(string pathFile, string accorePath, ref CheckBox checkBox)
        {
            
            if (!File.Exists(accorePath))
            {
                System.Windows.Forms.MessageBox.Show("accoreconsole.exe path not found!", "accoreconsole.exe not found", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }

            string assemblyPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            string folderTemp = PrintForm.folderTemp;

            createScript();

            string commandLine = "\"" + accorePath + "\" /readonly /i " + "\"" + pathFile + "\"" + " /s \"" + Path.GetTempPath() + PrintForm.folderTemp + "\\runscript.scr\"" + " /isolate  \"MyCopy\" ";
            string[] lines = new string[1];
            lines[0] = commandLine;
            string batFile = Path.GetTempPath() + folderTemp + "\\getDwgLayouts.bat";
            File.WriteAllLines(batFile, lines);

            Process pro = new Process();
            pro.StartInfo.FileName = batFile;
            if (checkBox.Checked)
            {
                pro.StartInfo.UseShellExecute = true;
                pro.StartInfo.CreateNoWindow = false;
                pro.StartInfo.Arguments = "/ c tracert 8.8.8.8 & pause";
            }
            else
            {
                pro.StartInfo.UseShellExecute = false;
                pro.StartInfo.CreateNoWindow = true;
            }

            pro.Start();
            pro.WaitForExit();

            File.Delete(batFile);
            File.Delete(Path.GetTempPath() + PrintForm.folderTemp + "\\runscript.scr");

        }

        // create script to run when calling process, used for method above.
        public static void createScript()
        {
            
            if(!Directory.Exists(Path.GetTempPath() + PrintForm.folderTemp))
            {
                Directory.CreateDirectory(Path.GetTempPath() + PrintForm.folderTemp);
            }
            List<string> commandScript = new List<string>();
            commandScript.Add("SECURELOAD");
            commandScript.Add("0");
            commandScript.Add("NETLOAD");
            commandScript.Add("\"" + AppDomain.CurrentDomain.BaseDirectory + "pageSetupFinder.dll" + "\"");
            commandScript.Add("GELISTPAGESETUP");
            string tempScriptPath = Path.GetTempPath() + PrintForm.folderTemp + "\\runscript.scr";
            File.WriteAllLines(tempScriptPath, commandScript.ToArray());
        }
    }
}
