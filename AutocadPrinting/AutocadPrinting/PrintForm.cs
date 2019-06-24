using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutocadPrinting
{
    public partial class PrintForm : Form
    {
        public static string errorText = "err.txt";

        public static string folderTemp = "GEPrinting";
        public static string currentChosenAutocad = "GEcurrentAutocad.txt";
        string accoreConsolePath = "";
        List<int> cadVer = new List<int>();
        Dictionary<string, string> cadVerCodeDict = new Dictionary<string, string>();

        public PrintForm()
        {
            if(File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\" + errorText))
            {
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\" + errorText);
            }
            // GET PAPER SIZE HERE
            
            InitializeComponent();
            fileTypeComboBox.SelectedIndex = 0;
            writeError("Init Component done");

            string[] styleCTB = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "styles.txt");

            styleComboBox.Items.AddRange(styleCTB);
            styleComboBox.SelectedIndex = 0;
            this.KeyPreview = true;
            List<string> orientationList = new List<string>();

            writeError("Init 1 passed");

            orientationList.Add("Landscape");
            orientationList.Add("Portrait");
            orientationComboBox.Items.AddRange(orientationList.ToArray());
            orientationComboBox.SelectedIndex = 0;


            outPutLocationTextBox.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\out.pdf";
            //clean temp folder
            if (Directory.Exists(Path.GetTempPath() + folderTemp))
            {
                Directory.Delete(Path.GetTempPath() + folderTemp, recursive: true);
            }

            writeError("Init 2 passed");

        }

        private static void writeError(string message)
        {
            File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "\\" + errorText, message + Environment.NewLine);
        }

        // Find accoreconsole
        // Add requiredFile (doesn't work so far)
        private void PrintForm_Load(object sender, EventArgs e)
        {
            //ErrorReport.getError();

            if (!FindAccoreConsole.findAccoreConsole(out accoreConsolePath, out cadVer))
            {
                writeError("Load, accoreconosle failed");

                DialogResult temp = MessageBox.Show("accoreconsole.exe NOT FOUND, please specify the path", "accoreconsole not found", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                Application.Exit();
                return;
            }

            writeError("Load, if passed");

            foreach (int year in cadVer)
            {
                cadVersionsComboBox.Items.Add(year);
            }

            writeError("Load, loop passed");

            if (File.Exists(Path.GetTempPath() + currentChosenAutocad))
            {
                List<string> lines = new List<string>(File.ReadAllLines(Path.GetTempPath() + currentChosenAutocad));
                if (lines != null || lines.Count == 1)
                {
                    int selectedIndex = int.Parse(lines[0]);
                    if (selectedIndex >= 0 && selectedIndex < cadVersionsComboBox.Items.Count) cadVersionsComboBox.SelectedIndex = selectedIndex;

                }
            }

            else
            {
                cadVersionsComboBox.SelectedIndex = 0;
            }

            writeError("Load, get currenCad passed");



            //InstallRequiredFiles.addpc3AndctbFile(cadVersionsComboBox.SelectedItem.ToString());

        }

        private void AddLayoutsToGridView(List<string> filePaths)
        {
            writeError("Enter AddLayoutsToGridView.");
            getWindowPostion();
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            Process process = Process.Start(basePath + "LayoutBuffering.exe");

            Refresh();
            WindowState = FormWindowState.Minimized;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;


            foreach (string fileName in filePaths)
            {
                //ImportLayoutsLabel.Visible = true;
                //this.Refresh();
                if (!Directory.Exists(Path.GetTempPath() + folderTemp)) Directory.CreateDirectory(Path.GetTempPath() + folderTemp);
                File.WriteAllText(Path.GetTempPath() + folderTemp + "\\fileName.txt", Path.GetFileName(fileName));
                getLayouts(fileName);
                //ImportLayoutsLabel.Visible = false;
                if (!GridViewLayoutManager.addToGridView(fileName, ref dwgGridView, cadVersionsComboBox.SelectedItem.ToString()))
                {
                    break;
                }

                PlotStylesComboBox.SelectedIndex = gettingSizeOfPaper();
                File.Delete(Path.GetTempPath() + folderTemp + "\\layoutName.txt");
                File.Delete(Path.GetTempPath() + folderTemp + "\\xrefList.txt");
                
            }
            process.Kill();
            if (File.Exists(Path.GetTempPath() + "printFormPosition.txt"))
            {
                File.Delete(Path.GetTempPath() + "printFormPosition.txt");
            }
            this.ShowIcon = true;
            this.ShowInTaskbar = true;
            WindowState = FormWindowState.Normal;
            writeError("Exit AddLayoutsToGridView.");
        }
        
        private void AddButton_Click(object sender, EventArgs e)
        {
            writeError("Enter Add DWG");

            OpenFileDialog fileBrowser = new OpenFileDialog();
            fileBrowser.Multiselect = true;
            fileBrowser.Filter = "dwg file| *.dwg";
            fileBrowser.Title = "Select a *dwg file";
            if (fileBrowser.ShowDialog() == DialogResult.OK)
            {
                // Start process
                // read text only
                if (!canGetLayOut(ref fileBrowser))
                {
                    MessageBox.Show("CANNOT GET LAYOUT\n\n *DWG FILE(S) ARE GENERATED USING HIGHER VERSION OF AUTOCAD\n\ncheck message box for details",
                        "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                List<string> filePaths = new List<string>(fileBrowser.FileNames);

                writeError("Enter Add to Grid View");
                AddLayoutsToGridView(filePaths);

            }
        }

        // Check version of *.dwg, make sure current autocad can read it.
        private bool canGetLayOut(ref OpenFileDialog fileBrowser)
        {
            writeError("Enter can Get layout");
            bool check = true;
            int i = 1;
            messageTextBox.Clear();
            string usingCadCode = findAuToCadCode(cadVersionsComboBox.SelectedItem.ToString());
            foreach (string fileName in fileBrowser.FileNames)
            {
                messageTextBox.Text += i.ToString() + ". " + Path.GetFileName(fileName) + "---[CAD VERSION: ";
                string CADCODE = GridViewLayoutManager.getCadVerCode(fileName);
                string CADVERSION = GridViewLayoutManager.getCadVersion(CADCODE);
                messageTextBox.Text += CADVERSION + "]";
                if (String.Compare(usingCadCode, CADCODE) < 0)
                {
                    check = false;
                    messageTextBox.Text += "**--FAILED--**";
                }
                messageTextBox.Text += "\n";
                i++;
            }
            return check;
        }

        private bool canGetLayOut(List<string> paths)
        {
            writeError("Enter can Get Layout 1.");
            bool check = true;
            int i = 1;
            messageTextBox.Clear();
            string usingCadCode = findAuToCadCode(cadVersionsComboBox.SelectedItem.ToString());
            foreach (string fileName in paths)
            {
                messageTextBox.Text += i.ToString() + ". " + Path.GetFileName(fileName) + "---[CAD VERSION: ";
                string CADCODE = GridViewLayoutManager.getCadVerCode(fileName);
                string CADVERSION = GridViewLayoutManager.getCadVersion(CADCODE);
                messageTextBox.Text += CADVERSION + "]";
                if (String.Compare(usingCadCode, CADCODE) < 0)
                {
                    check = false;
                    messageTextBox.Text += "**--FAILED--**";
                }
                messageTextBox.Text += "\n";
                i++;
            }
            return check;
        }

        private void getLayouts(string dwgPath)
        {
            writeError("Enter getLayouts");
            string accorePath = accoreconsoleLocationTextBox.Text;
            GetLayoutInfo.getLayouts(dwgPath, accorePath, ref showConsoleCheckBox);
        }

        private void cadVersionsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            accoreconsoleLocationTextBox.Text = accoreConsolePath + "\\AutoCAD " + cadVersionsComboBox.Text + "\\accoreconsole.exe";

            string[] plotterSizes = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "paperPlotSizes.txt");
            if(plotterSizes == null || plotterSizes.Count() == 0)
            {
                MessageBox.Show("paperPlotSizes.txt not found", "plotSize not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<string> defaultPaperSize = new List<string>();
            foreach (string str in plotterSizes)
            {
                defaultPaperSize.Add(str.Trim());
            }

            List<string> currPaperSize = GetPaperSizeSetUp.GetPaperSize(accoreconsoleLocationTextBox.Text);
            if (currPaperSize == null || currPaperSize.Count == 0)
            {
                //MessageBox.Show("couldn't run filePaperSize.dll", "filePaperSize.dll failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                foreach (string str in currPaperSize)
                {
                    if (!defaultPaperSize.Contains(str.Trim()))
                    {
                        defaultPaperSize.Add(str.Trim());
                    }
                }
            }
            int temp = -1;
                

            if(PlotStylesComboBox.SelectedItem != null)
            {
                temp = PlotStylesComboBox.SelectedIndex;
            }

            PlotStylesComboBox.Items.Clear();
            PlotStylesComboBox.Items.AddRange(defaultPaperSize.ToArray());
            if (PlotStylesComboBox.Items.Count <= temp || temp == -1)
                PlotStylesComboBox.SelectedIndex = 25;
            else
                PlotStylesComboBox.SelectedIndex = temp;

            InstallRequiredFiles.addpc3AndctbFile(cadVersionsComboBox.SelectedItem.ToString());
            File.WriteAllText(Path.GetTempPath() + currentChosenAutocad, cadVersionsComboBox.SelectedIndex.ToString());
        }

        private void fileTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void accoreconsoleBrowserButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.FileName = "accoreconsole";
            openFileDialog.Filter = "Executable Files |*.exe";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                if (Path.GetFileName(openFileDialog.FileName.ToString()) == "accoreconsole.exe")
                {
                    accoreconsoleLocationTextBox.Text = openFileDialog.FileName.ToString();
                }
            }
        }

        private void LocationButton_Click(object sender, EventArgs e)
        {

            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string outputName = "out.pdf";
                    if (outPutLocationTextBox.Text != null && outPutLocationTextBox.Text != "")
                    {
                        string[] temp = outPutLocationTextBox.Text.Split('\\');
                        if(temp.Last().Contains(".pdf") && temp.Last().Split('.').Count() > 1)
                        {
                            outputName = temp.Last();
                        }
                    }

                    int i = 1;
                    string tempOutPutFile = fbd.SelectedPath.ToString() + "\\" + outputName;
                    while (File.Exists(tempOutPutFile)) {
                        tempOutPutFile = fbd.SelectedPath.ToString() + string.Format("\\{0}_{1}.pdf", outputName, i);

                        i++;
                    }
                    outPutLocationTextBox.Text = tempOutPutFile;
                }
            }
            
        }

        private void dwgGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Delete == e.KeyCode)
            {
                DataGridViewSelectedRowCollection rc = dwgGridView.SelectedRows;
                foreach(DataGridViewRow r in rc)
                {
                    if(r.Cells[2].Value == null)
                    {
                        int i = dwgGridView.Rows.IndexOf(r);
                        
                        //remove blank row, ignore
                        if (i == dwgGridView.RowCount - 1) continue;

                        GridViewLayoutManager.deleteDwg(dwgGridView.Rows[i].Cells[0].Value.ToString());
                        if(i == dwgGridView.RowCount - 2)
                        {
                            dwgGridView.Rows.RemoveAt(i);
                            e.Handled = true;
                            continue;
                        }

                        i++;
                        int j = i;
                        while(j < dwgGridView.RowCount-1 && dwgGridView.Rows[j].Cells[0].Value.ToString() == "")
                        {
                            dwgGridView.Rows.RemoveAt(j);
                        }
                        dwgGridView.Rows.RemoveAt(i-1);
                        continue;
                    }
                    else if (r.Cells[2].Value != null)
                    {
                        bool run = true;
                        int index = dwgGridView.Rows.IndexOf(r);
                        int j = index;
                        while(dwgGridView.Rows[j].Cells[0].Value.ToString() == "" && j >= 0)
                        {
                            if (j > 0) j--;
                            else { run = false; break; }
                        }

                        
                        if (run && j >= 0)
                        {
                            GridViewLayoutManager.deleteLayout(dwgGridView.Rows[j].Cells[0].Value.ToString(), r.Cells[1].Value.ToString());
                        }
                        if((index == dwgGridView.RowCount - 2 && j == index - 1) || (j == index-1 && dwgGridView.Rows[index+1].Cells[2].Value == null))
                        {
                            GridViewLayoutManager.deleteDwg(dwgGridView.Rows[j].Cells[0].Value.ToString());
                            dwgGridView.Rows.Remove(r);
                            dwgGridView.Rows.RemoveAt(j);
                            e.Handled = true;
                            return;
                        }

                        dwgGridView.Rows.Remove(r);
                    }
                }
                e.Handled = true;
                PlotStylesComboBox.SelectedIndex = gettingSizeOfPaper();
            }
        }

        private void minusButton_Click(object sender, EventArgs e)
        {
            KeyEventArgs e1 = new KeyEventArgs(Keys.Delete);
            dwgGridView_KeyDown(sender, e1);
        }

        private void upButton_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rc = dwgGridView.SelectedRows;
            // MOVE WHO BLOCK
            if (rc.Count == 1 && rc[0].Cells[0].Value != null && rc[0].Cells[0].Value.ToString() != "" && rc[0].Cells[2].Value == null)
            {
                int swapTime = 1;
                int layoutNums = 1;
                int index = dwgGridView.Rows.IndexOf(rc[0]);
                int tempIndex = index;

                if (index <= 1) return;


                while (index - swapTime >= 0 && dwgGridView.Rows[index - swapTime].Cells[2].Value != null) swapTime++;
                while (index + layoutNums <= dwgGridView.RowCount - 2 && dwgGridView.Rows[index + layoutNums].Cells[2].Value != null) layoutNums++;

                for (int i = 0; i < layoutNums; i++)
                {
                    for (int j = 0; j < swapTime; j++)
                    {
                        swapGridRow(tempIndex - j, tempIndex - j - 1);
                    }
                    tempIndex += 1;
                }
            }
            else
            {
                for (int i = 0; i < dwgGridView.RowCount; i++)
                {
                    if (rc.Contains(dwgGridView.Rows[i]) && dwgGridView.Rows[i].Cells[0].Value.ToString() == "" && dwgGridView.Rows[i - 1].Cells[0].Value.ToString() == "")
                    {
                        if (i > 1)
                            swapGridRow(i, i - 1);
                    }
                }
            }
            

            foreach(DataGridViewRow r in dwgGridView.Rows)
            {
                if(!rc.Contains(r))
                    r.Selected = false;
                else r.Selected = true;
            }
        }

        private void swapGridRow(int a, int b)
        {
            var r2 = dwgGridView.Rows[a];
            var r1 = dwgGridView.Rows[b];
            dwgGridView.Rows.Remove(r1);
            dwgGridView.Rows.Insert(a, r1);
            dwgGridView.Rows.Remove(r2);
            dwgGridView.Rows.Insert(b, r2);
        }

        private void DownButton_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rc = dwgGridView.SelectedRows;
            // MOVE THE WHOLE BLOCK DOWN

            if (rc.Count == 1 && rc[0].Cells[0].Value != null && rc[0].Cells[0].Value.ToString() != "" && rc[0].Cells[2].Value == null)
            {
                int index = dwgGridView.Rows.IndexOf(rc[0]);
                int tempIndex = index + 1;
                while (tempIndex < dwgGridView.RowCount - 1 && dwgGridView.Rows[tempIndex].Cells[2].Value != null) tempIndex++;
                if (tempIndex > dwgGridView.RowCount - 2) return;
                else
                {
                    dwgGridView.Rows[index].Selected = false;
                    dwgGridView.Rows[tempIndex].Selected = true;
                    upButton_Click(sender, e);
                }
            }
            else
            {

                for (int i = dwgGridView.RowCount - 2; i >= 1; i--)
                {
                    if (i < dwgGridView.RowCount - 2 && dwgGridView.Rows[i].Selected && (string)dwgGridView.Rows[i].Cells[0].Value == "" && (string)dwgGridView.Rows[i + 1].Cells[0].Value == "" && !rc.Contains(dwgGridView.Rows[i + 1]))
                        swapGridRow(i + 1, i);
                }
            }
            foreach (DataGridViewRow r in dwgGridView.Rows)
            {
                if (!rc.Contains(r))
                    r.Selected = false;
                else r.Selected = true;
            }
        }

        private void dwgGridView_DoubleClick(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rc = dwgGridView.SelectedRows;
            if (rc.Count == 0) return;
            DataGridViewRow r = rc[0];
            int i = dwgGridView.Rows.IndexOf(r);
            while((string)dwgGridView.Rows[i].Cells[0].Value == "" && i > 0)
            {
                i--;
            }
            if((string)dwgGridView.Rows[i].Cells[0].Value != "" && i != dwgGridView.Rows.Count - 1)
            {
                string dwgName = dwgGridView.Rows[i].Cells[0].Value.ToString();
                foreach(DwgClass dc in GridViewLayoutManager.dwgList)
                {
                    if(dc.dwgName == dwgName)
                    {
                        //write to textBox
                        writeToMessageBox(dc.xrefStatus);
                    }
                }
            }
        }

        private void writeToMessageBox(List<XrefClass> xrefList)
        {
            messageTextBox.Clear();
            int i = 1;
            foreach(XrefClass xc in xrefList)
            {
                if (xc.status != "Resolved")
                {
                    messageTextBox.Text += i.ToString() + ". " + xc.xrefName + " ---> [" + xc.status + "]\n";
                    i++;
                }
            }
        }

        private bool checkCondition()
        {
            if(outPutLocationTextBox.Text == null || outPutLocationTextBox.Text == "")
            {
                DialogResult temp = MessageBox.Show("no output path found", "NO OUTPUT PATH", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }

            if(!File.Exists(accoreconsoleLocationTextBox.Text))
            {
                DialogResult temp = MessageBox.Show("accoreconsole.exe path not found", "NO ACCORECONSOLE.EXE", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }

            if (!isGridViewContainsLayers())
            {
                DialogResult temp = MessageBox.Show("there is no layer to print", "No layer found", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }

            return true;
        }


        private bool isGridViewContainsLayers()
        {
            if (dwgGridView.RowCount <= 1) return false;
            for (int i = 0; i < dwgGridView.RowCount-1; i++)
            {
                if (dwgGridView.Rows[i].Cells[0].Value.ToString() == "")
                {
                    return true;
                }
            }
            return false;
        }

        private void publishButton_Click(object sender, EventArgs e)
        {
            writeError("Enter publishButton_Click");
            if (checkCondition())
            {
                writeError("Pass checkCondition");
                //ErrorReport.getError();
                List<KeyValuePair<string, List<string>>> printList = createListToPrint();
                List<string> pathPdfs = getPdfPathList(printList);

                File.WriteAllLines(Path.GetTempPath() + "pdfLayoutListGE.txt", pathPdfs.ToArray());
                getWindowPostion();
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                Process process = Process.Start(basePath + "CheckingApp.exe");

                WindowState = FormWindowState.Minimized;
                this.ShowIcon = false;
                this.ShowInTaskbar = false;
                //TESTING
                writeError("Enter plotting");
                PrintPdf2.plotting(printList, accoreconsoleLocationTextBox.Text, PlotStylesComboBox.SelectedItem.ToString(), orientationComboBox.SelectedItem.ToString(), ref showConsoleCheckBox, styleComboBox.SelectedItem.ToString());

                process.Kill();
                process.WaitForExit();

                this.ShowIcon = true;
                this.ShowInTaskbar = true;

                WindowState = FormWindowState.Normal;

                Refresh();

                writeError("Enter after plotting");

                if (File.Exists(Path.GetTempPath() + "pdfLayoutListGE.txt"))
                {
                    try
                    {
                        File.Delete(Path.GetTempPath() + "pdfLayoutListGE.txt");
                    } catch
                    {

                    }
                }

                writeError("Enter after plotting 1");

                if (File.Exists(Path.GetTempPath() + "printFormPosition.txt"))
                {
                    try
                    {
                        File.Delete(Path.GetTempPath() + "printFormPosition.txt");
                    }
                    catch
                    {

                    }
                }
                writeError("Enter after plotting 2");
                checkIfFileExist(pathPdfs);

                //Get time Lasped
                string timeLapsed = File.ReadAllText(Path.GetTempPath() + "GEtimeLapse.txt");
                messageTextBox.Text += "   [" + timeLapsed + "]";

                if(File.Exists(Path.GetTempPath() + "GEtimeLapse.txt"))
                {
                    try
                    {
                        File.Delete(Path.GetTempPath() + "GEtimeLapse.txt");
                    }
                    catch { }
                }
                writeError("Enter after plotting 3");


                if (fileTypeComboBox.SelectedIndex == 0)
                {
                    int i = 1;
                    List<string> outPutTempPath = new List<string>(outPutLocationTextBox.Text.Split('\\'));
                    string outputName = outPutTempPath.Last().Split('.').First();
                    outPutTempPath.Remove(outPutTempPath.Last());
                    string tempOutPutFolder = "";
                    foreach (string p in outPutTempPath) tempOutPutFolder += p + "\\";
                    string tempOutPutFile = outPutLocationTextBox.Text;
                    while (File.Exists(tempOutPutFile))
                    {
                        tempOutPutFile = tempOutPutFolder + outputName + "_" + i.ToString() + ".pdf";
                        i++;
                    }
                    bool done = true;
                    EmergingPDF.mergePdf(printList, tempOutPutFile, out done);
                    if (done)
                    {
                        Process.Start(tempOutPutFile);
                    }
                    else
                    {
                        messageTextBox.Text = "GENERATING PDF FAILED, couldn't get layouts.";
                    }
                }
                else
                {
                    List<string> outPutPath = new List<string>(outPutLocationTextBox.Text.Split('\\'));
                    outPutPath.Remove(outPutPath.Last());
                    string output = "";
                    foreach(string path in outPutPath) { output += path + "\\"; }
                    output += "plots";
                    string tempOutPut = output;
                    int i = 1;
                    while (Directory.Exists(tempOutPut))
                    {
                        tempOutPut = output + "_" + i.ToString();
                        i++;
                    }
                    //Directory.CreateDirectory(tempOutPut);
                    Directory.Move(Path.GetTempPath() + folderTemp + "\\" + PrintPdf2.pdfFolder, tempOutPut);
                    Process.Start(tempOutPut);
                    outPutLocationTextBox.Text = tempOutPut;
                }

                writeError("Enter after plotting 4");


                if (Directory.Exists(Path.GetTempPath() + folderTemp))
                {
                    Directory.Delete(Path.GetTempPath() + folderTemp, recursive: true);
                }
            }
        }

        private List<KeyValuePair<string, List<string>>> createListToPrint()
        {
            List<KeyValuePair<string, List<string>>> printList = new List<KeyValuePair<string, List<string>>>();
            for(int i = 0; i < dwgGridView.RowCount - 1; i++)
            {
                if(dwgGridView.Rows[i].Cells[0].Value != null && (string)dwgGridView.Rows[i].Cells[0].Value != "")
                {
                    
                    string keyString = dwgGridView.Rows[i].Cells[0].Value.ToString();
                    string keyPath = "";
                    bool a = GridViewLayoutManager.getDwgPath(keyString, out keyPath);
                    int j = i + 1;
                    List<string> layoutList = new List<string>();
                    while(j < dwgGridView.RowCount-1 && (string)dwgGridView.Rows[j].Cells[0].Value == "")
                    {
                        layoutList.Add(dwgGridView.Rows[j].Cells[1].Value.ToString());
                        j++;
                    }
                    i = j-1;
                    KeyValuePair<string, List<string>> kv = new KeyValuePair<string, List<string>>(keyPath, layoutList);
                    printList.Add(kv);
                }
            }
            return printList;
        }

        private void checkIfFileExist(List<string> pathFiles)
        {
            messageTextBox.Text = "";
            int i = 1;
            foreach(string path in pathFiles) {
                if (!File.Exists(path))
                {
                    messageTextBox.Text += i.ToString() + ". Layout:" + path.Split('\\').Last().Split('.').First() + "NOT FOUND.\n";
                    i++;
                }
            }
            if (i == 1)
            {
                messageTextBox.Text = "Everything OK";
            }
        }

        // DETECT SIZE OF PAPER

        private int gettingSizeOfPaper()
        {
            if(GridViewLayoutManager.dwgList == null || GridViewLayoutManager.dwgList.Count == 0)
            {
                return 25;
            }

            Dictionary<int, int> width = new Dictionary<int, int>();
            Dictionary<int, int> height = new Dictionary<int, int>();

            width.Add(36, 0); width.Add(42, 0); width.Add(48, 0);
            height.Add(24, 0); height.Add(30, 0); height.Add(36, 0);

            foreach(DwgClass dc in GridViewLayoutManager.dwgList)
            {
                foreach(LayoutClass lc in dc.layoutsList)
                {
                    double h1 = Double.Parse(lc.layoutHeight);
                    double w1 = Double.Parse(lc.layoutWidth);
                    addPoint(ref width, w1);
                    addPoint(ref height, h1);
                }
            }
            int w = getMostPoint(width);
            int h = getMostPoint(height);

            if (h == 24 && w == 36)
            {
                return foundSize("(24.00 x 36.00 Inches)", PlotStylesComboBox);
            } else if (h == 36 && w == 48)
            {
                return foundSize("(36.00 x 48.00 Inches)", PlotStylesComboBox);
            } else if (h == 30 && w == 42)
            {
                return foundSize("(30.00 x 42.00 Inches)", PlotStylesComboBox);
            } else if (h == 24 && w == 42)
            {
                return foundSize("(24.00 x 42.00 Inches)", PlotStylesComboBox);
            } else if (h == 30 && w == 48) 
            {
                return foundSize("(30.00 x 48.00 Inches)", PlotStylesComboBox);
            }
            return 25;
        }

        private int foundSize(string sizeToFind, ComboBox bc)
        {
            int i = 0;
            foreach(var item in bc.Items)
            {
                if (item.ToString().Contains(sizeToFind))
                {
                    return i;
                }
                i++;
            }
            return 25;
        }

        private void addPoint(ref Dictionary<int, int> dimension, double num)
        {
            Dictionary<int, int> temp = new Dictionary<int, int>(dimension);
            foreach(int i in dimension.Keys)
            {
                if(Math.Abs(num - i) < 0.5)
                {
                    temp[i] += 1;
                }
            }
            dimension = temp;
        }

        private int getMostPoint(Dictionary<int, int> dimension)
        {
            int temp = -1;
            foreach(int i in dimension.Keys)
            {
                if (temp == -1) temp = i;
                else
                {
                    if(dimension[i] > dimension[temp])
                    {
                        temp = i;
                    }
                }
            }
            return temp;
        }

        public static List<string> getPdfPathList(List<KeyValuePair<string, List<string>>> printList)
        {
            List<string> pdfPathList = new List<string>();
            string plotPath = Path.GetTempPath() + PrintForm.folderTemp + "\\" + PrintPdf2.pdfFolder;
            foreach (KeyValuePair<string, List<string>> kv in printList)
            {
                string dwgFolder = plotPath + "\\" + kv.Key.Split('\\').Last().Replace('.', '_');
                foreach (string layoutName in kv.Value)
                {
                    string pdfPath = dwgFolder + "\\" + layoutName + ".pdf";
                    pdfPathList.Add(pdfPath);
                }
            }
            return pdfPathList;
        }

        private void getWindowPostion()
        {
            List<string> position = new List<string>();
            position.Add(this.Width.ToString()); //Width
            position.Add(this.Height.ToString()); //Height
            position.Add(this.Top.ToString());
            position.Add(this.Left.ToString());

            File.WriteAllLines(Path.GetTempPath() + "printFormPosition.txt", position);
        }

        private string findAuToCadCode(string version)
        {
            string[] temp = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "acversionsCodeName.txt");
            foreach(string line in temp)
            {
                if (line.Contains(version))
                {
                    return line.Split('-').First().Trim();
                }
            }
            return "AC1032";
        }

        private void dwgGridView_DragDrop(object sender, DragEventArgs e)
        {
            string[] fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            List<string> paths = new List<string>();
            foreach(string file in fileList)
            {
                string ext = System.IO.Path.GetExtension(file);
                if(ext.ToLower() == ".dwg")
                {
                    paths.Add(file);
                }
            }

            if (canGetLayOut(paths))
            {
                AddLayoutsToGridView(paths);
            }

        }

        private void dwgGridView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Link;
        }

        private void dwgGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
