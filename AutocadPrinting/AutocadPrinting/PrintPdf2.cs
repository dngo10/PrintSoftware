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
    class PrintPdf2
    {
        private static int numberOfThreads = 2;
        public static string pdfFolder = "Plots";
        private static string generatePdfBatFile = "pdfGenerator";
        private static string batFolder = "batFolder";
        private static string scriptsFolder = "scripts";

        // Main function, create and put single-Page PDF into %temp%\GEPrinting\\Plots
        public static void plotting(List<KeyValuePair<string, List<string>>> printList, string accorePath, string paperSize, string pl, ref CheckBox checkBox, string style)
        {
            List<KeyValuePair<string, List<string>>> printListBreakDown = breakList(printList);

            List<string> scriptPaths = new List<string>();
            List<string> batPaths = new List<string>();
            if (!Directory.Exists(Path.GetTempPath() + PrintForm.folderTemp + "\\" + pdfFolder)) Directory.CreateDirectory(Path.GetTempPath() + PrintForm.folderTemp + "\\" + pdfFolder);
            if (!Directory.Exists(Path.GetTempPath() + PrintForm.folderTemp + "\\" + scriptsFolder)) Directory.CreateDirectory(Path.GetTempPath() + PrintForm.folderTemp + "\\" + scriptsFolder);
            if (!Directory.Exists(Path.GetTempPath() + PrintForm.folderTemp + "\\" + batFolder)) Directory.CreateDirectory(Path.GetTempPath() + PrintForm.folderTemp + "\\" + batFolder);

            //Create multiple bat files (one for each .dwg file)
            for (int i = 0; i < printListBreakDown.Count; i++)
            {
                string batCommand = "";
                if (!Directory.Exists(Path.GetTempPath() + PrintForm.folderTemp + "\\" + pdfFolder + "\\" + printListBreakDown[i].Key.Split('\\').Last().Replace('.', '_')))
                {
                    Directory.CreateDirectory(Path.GetTempPath() + PrintForm.folderTemp + "\\" + pdfFolder + "\\" + printListBreakDown[i].Key.Split('\\').Last().Replace('.', '_'));
                }
                string tempPDFPath = Path.GetTempPath() + PrintForm.folderTemp + "\\" + pdfFolder + "\\" + printListBreakDown[i].Key.Split('\\').Last().Replace('.', '_');
                string scriptPath = Path.GetTempPath() + PrintForm.folderTemp + "\\" + scriptsFolder + "\\script_" + i.ToString() + ".scr";
                scriptPaths.Add(scriptPath);
                if (printListBreakDown[i].Value != null && printListBreakDown[i].Value.Count > 0)
                {
                    batCommand += "\"" + accorePath + "\"" + " /readonly" + " /i \"" + printListBreakDown[i].Key + "\" /s \"" + scriptPath + "\" /isolate \"MYSCRIPT\"";
                }
                createScript(scriptPath, printListBreakDown[i].Value, tempPDFPath, paperSize, pl, style);
                string batPath = Path.GetTempPath() + PrintForm.folderTemp + "\\" + batFolder + "\\" + generatePdfBatFile + "_" + i.ToString() + ".bat";
                File.WriteAllText(batPath, batCommand);
                batPaths.Add(batPath);
            }

            // Runs batfiles in tasks. For now, there are 2 tasks each time.
            // Run task continuously ?
            int iindex = 0;
            bool check = checkBox.Checked;

            List<Task> tasks = new List<Task>();

            while (iindex < batPaths.Count)
            {
                List<int> iList = new List<int>();

                if (iindex < batPaths.Count) { iList.Add(iindex); iindex++; }
                // if you want more thread, add more lines like above;

                foreach (int i in iList)
                {

                    tasks.Add(Task.Run(delegate
                    {
                        Process pro = new Process();
                        pro.StartInfo.FileName = batPaths[i];
                        if (check)
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
                    }));
                }
                while (activeTask(ref tasks) >= numberOfThreads) {
           
                    Task.WaitAny(tasks.ToArray());
                }

                if (activeTask(ref tasks) < numberOfThreads && iList.Last() == batPaths.Count - 1)
                {
                    Task.WaitAll(tasks.ToArray());
                }
            }
        }

        private static List<KeyValuePair<string, List<string>>> breakList(List<KeyValuePair<string, List<string>>> printList)
        {
            List<KeyValuePair<string, List<string>>> brokenList = new List<KeyValuePair<string, List<string>>>();
            foreach(KeyValuePair<string, List<string>> kv in printList)
            {
                if(kv.Value.Count() < 4)
                {
                    string key = kv.Key;
                    List<string> value = kv.Value;
                    brokenList.Add(new KeyValuePair<string, List<string>>(key, value));
                }
                else
                {
                    int lim = 0;
                    while(lim < kv.Value.Count())
                    {
                        string key = kv.Key;
                        List<string> value = new List<string>();
                        if(lim < kv.Value.Count()){value.Add(kv.Value[lim]); lim++;}
                        if(lim < kv.Value.Count()){value.Add(kv.Value[lim]); lim++;}
                        if(lim < kv.Value.Count()){value.Add(kv.Value[lim]); lim++;}

                        brokenList.Add(new KeyValuePair<string, List<string>>(key, value));
                    }
                }
            }

            return brokenList;
        }

        private static int activeTask(ref List<Task> tasks)
        {
            int j = 0;
            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].IsCompleted)
                {
                    tasks.RemoveAt(i);
                    i--;
                }
                else
                {
                    j++;
                }
            }
            return j;
        }

        // Create *.scr files, one for each *.dwg
        public static void createScript(string scriptPath, List<string> layoutName, string tempOutPutFolder, string paperSize, string pl, string style)
        {

            List<string> commandScript = new List<string>();
            commandScript.Add("secureload");
            commandScript.Add("0");
            commandScript.Add("netload");
            commandScript.Add("\"" + "C:\\GEApp_19\\AutoUpdate.dll" + "\"");
            //CIVIL COMMANDS
            //THESE ARE CIVIL COMMAND CODE
            commandScript.Add("-Layer");
            commandScript.Add("thaw");
            commandScript.Add("*FRAME_*");
            commandScript.Add("thaw");
            commandScript.Add("*FNDN_*");
            commandScript.Add("thaw");
            commandScript.Add("*FNDN1_*");
            commandScript.Add("");
            //------------

            foreach (string layout in layoutName)
            {
                string pdfFilePath = tempOutPutFolder + "\\" + layout + ".pdf";
                if (File.Exists(pdfFilePath))
                {
                    File.Delete(pdfFilePath);
                }
                plotOneLayout(ref commandScript, layout, paperSize, pl, pdfFilePath, style);
            }
            commandScript.Add("FILEDIA");
            commandScript.Add("1");
            File.WriteAllLines(scriptPath, commandScript.ToArray());
        }

        public static void plotOneLayout(ref List<string> commandScript, string layoutName, string paperSize, string pl, string tempPDFPath, string style)
        {
            commandScript.Add("-PLOT");
            commandScript.Add("Yes");
            commandScript.Add("\"" + layoutName.Trim() + "\"");
            commandScript.Add("DWG To PDF.pc3");
            commandScript.Add(paperSize.Trim());
            commandScript.Add("Inches");
            commandScript.Add(pl);
            commandScript.Add("No");
            commandScript.Add("Layout");
            commandScript.Add("1:1");
            commandScript.Add("0.00,0.00");
            commandScript.Add("Yes");
            commandScript.Add(style);
            commandScript.Add("Yes");
            commandScript.Add("No");
            commandScript.Add("No");
            commandScript.Add("No");
            commandScript.Add("\"" + tempPDFPath.Trim() + "\"");
            commandScript.Add("No");
            commandScript.Add("Yes");
        }
    }
}
