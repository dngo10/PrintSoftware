using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutocadPrinting
{
    class GridViewLayoutManager
    {
        public static List<DwgClass> dwgList = new List<DwgClass>();
        public static DwgClass formingDwgClass(string dwgFilePath, string cadVersion)
        {
            string infoDirectory = Path.GetTempPath() + PrintForm.folderTemp;
            List<LayoutClass> layouts = new List<LayoutClass>();
            if(!File.Exists(infoDirectory + "\\layoutName.txt") || !File.Exists(infoDirectory + "\\xrefList.txt"))
            {
                
                FileInfo fileInfo = new FileInfo(dwgFilePath);
                fileInfo.ToString();
                string messageShow = "FILE: " + dwgFilePath.Split('\\').Last() + "\nCreated by: AutoCAD " + getCadVersion(getCadVerCode(dwgFilePath)) +
                    "\nYour AutoCAD version is: " + cadVersion +
                    "\n--------------------------------\nCAN'T OPEN FILE\n\n" + 
                    "please open error file, saved as different version of *.dwg, run it again."+ 
                    "\nOR\ncheck the <show console> check box, run again, take a picture of that console and send it to developer";
                MessageBox.Show(messageShow, "CAN'T GET LAYOUTS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string directoryPath = Path.GetDirectoryName(dwgFilePath);
                Process.Start(directoryPath);
                return null;
            }
            List<string> layoutTextFile = new List<string>(File.ReadLines(infoDirectory + "\\layoutName.txt"));
            List<string> xrefTextFile   = new List<string>(File.ReadLines(infoDirectory + "\\xrefList.txt"));
            foreach(string line in layoutTextFile)
            {
                string[] parts = line.Split('\t');
                LayoutClass layout = new LayoutClass();
                layout.layoutName = parts[0];
                string[] sizes = parts[1].Split('|');
                layout.layoutWidth = sizes[0];
                layout.layoutHeight = sizes[1];
                layouts.Add(layout);
            }

            DwgClass dwg = new DwgClass(dwgFilePath, layouts, getXref(xrefTextFile));

            dwg.dwgName = dwgFilePath.Split('\\').Last();
            dwg.dwgPath = dwgFilePath;
            return dwg;
        }

        private static List<XrefClass> getXref(List<string> xrefTextFile)
        {
            List<XrefClass> xrefList = new List<XrefClass>();
            foreach(string line in xrefTextFile)
            {
                string[] parts = line.Split('\t');
                xrefList.Add(new XrefClass(parts[0], parts[1]));
            }
            return xrefList;
        }

        public static bool addToGridView(string dwgFilePath, ref DataGridView gridView, string cadVersion)
        {

            DwgClass dwg = formingDwgClass(dwgFilePath, cadVersion);
            if (dwg == null) return false;
            dwgList.Add(dwg);

            int i = gridView.Rows.Add(dwg.dwgName);
            DataGridViewRow r = gridView.Rows[i];
            r.DefaultCellStyle.BackColor = System.Drawing.Color.Khaki;
            r.Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            r.Cells[3].Style.ForeColor = System.Drawing.Color.BlueViolet;

            if (dwg.hasXref())
            {
                gridView.Rows[i].Cells[3].Value = "Good";
                gridView.Rows[i].Cells[3].Style.BackColor = System.Drawing.Color.PaleGreen;
            }
            else
            {
                gridView.Rows[i].Cells[3].Value = "Warning!";
                gridView.Rows[i].Cells[3].Style.BackColor = System.Drawing.Color.PeachPuff;

            }

            foreach (LayoutClass lc in dwg.layoutsList)
            {
                gridView.Rows.Add("", lc.layoutName, lc.layoutHeight + " x " + lc.layoutWidth);
                gridView.Rows[gridView.Rows.Count - 2].Cells[2].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            return true;
        }

        public static void deleteLayout(string dwgName, string layoutName) { 
            foreach(DwgClass dc in dwgList)
            {
                if(dc.dwgName == dwgName)
                {
                    dc.deleteLayout(layoutName);
                }
            }
        }

        public static void deleteDwg(string dwgName)
        {
            if (dwgList.Count == 0) return;
            for(int i = 0; i < dwgList.Count(); i++)
            {
                if(dwgList[i].dwgName == dwgName)
                {
                    dwgList.Remove(dwgList[i]);
                    if(i>0) i--;
                }
            }
        }

        public static bool getDwgPath(string dwgName, out string dwgPath)
        {
            for(int i = 0; i < dwgList.Count; i++)
            {
                if(dwgList[i].dwgName == dwgName)
                {
                    dwgPath = dwgList[i].dwgPath;
                    return true;
                }
            }
            dwgPath = "";
            return false;
        }

        public static bool hasDwg(string dwgName)
        {
            foreach(DwgClass dc in dwgList)
            {
                if(dc.dwgName == dwgName)
                {
                    return true;
                }
            }
            return false;
        }

        public static string getCadVerCode (string path)
        {
            try
            {
                using (FileStream stream = File.OpenRead(path))
                {

                    using (StreamReader streamReader = new StreamReader(stream))
                    {
                        string firstLine = streamReader.ReadLine();
                        string cadVerCode = firstLine.Split(' ').First().Split('\0').First();

                        return cadVerCode;
                    }

                }
            }
            catch
            {
                return "AC1032";
            }
        }

        public static string getCadVersion(string cadCode)
        {
            string[] lines = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "acversionsCodeName.txt");
            Dictionary<string, string> cadVersion = new Dictionary<string, string>();
            foreach(string line in lines)
            {
                string[] temp = line.Split('-');
                if (temp.Count() == 2)
                    cadVersion.Add(temp[0].Trim(), temp[1].Trim());
            }
            if (cadVersion.ContainsKey(cadCode))
            {
                return cadVersion[cadCode];
            }
            else
            {
                return cadCode;
            }
        }
    }
}
