using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutocadPrinting
{
    // THIS FILE IS USED WHEN USING PUBLISHING
    // currently not used in Program.
    class DSDgenerator
    {
        public static string dsdFile = "printOut.dsd";
        public static void generateDsdFile(string folderPath, List<string> layouts, string oce, string outputPath)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("[DWF6Version]\r\nVer=1\r\n[DWF6MinorVersion]\r\nMinorVer=1\r\n");
            foreach(string layout in layouts)
            {
                strBuilder.Append(getLayoutItem(folderPath, layout, oce));
            }
            strBuilder.Append(setUpDetail(outputPath));
            //File.WriteAllText(Path.GetTempPath() + pdfForm.folderTemp + "\\" + dsdFile, strBuilder.ToString());
        }

        public static void generateDsdFile(string folderPath, string dwgRelPath, List<string> layouts, string oce, string outputFolder)
        {
           // StringBuilder strBuilder = new 
        }

        private static string getLayoutItem(string folderPath, string layout, string oce)
        {
            string layoutName = getLayoutName(layout);
            string fileName = getFileName(layout);
            string fullPath = getFullPath(folderPath, layout);
            string element0 = fileName;
            string element1 = layoutName;
            string element2 = fullPath;
            string element3 = layoutName;
            string element4 = oce;
            string element5 = fullPath;
            string result = string.Format("[DWF6Sheet:{0}-{1}]\r\nDWG={2}\r\nLayout={3}\r\nSetup={4}\r\nOriginalSheetPath={5}\r\nHas Plot Port=0\r\nHas3DDWF=0\r\n", 
                element0, element1, element2, element3, element4, element5);
            return result;
        }

        private static string getLayoutName(string layout)
        {
            return layout.Split('\\').Last();
        }

        private static string getFileName(string layout)
        {
            string[] path = layout.Split('\\');
            string fileName = path[path.Count() - 2];
            int i = fileName.IndexOf(".dwg");
            fileName = fileName.Substring(0, i);
            return fileName;
        }

        private static string getFullPath(string folderPath, string layout)
        {
            string[] path = layout.Split('\\');
            string relativeFilePath = "";
            for(int i = 0; i < path.Count()-1; i++)
            {
                relativeFilePath += path[i];
                if (i < path.Count() - 2)
                {
                    relativeFilePath += "\\";
                }
            }
            return folderPath + "\\" +  relativeFilePath;
        }

        private static string setUpDetail(string outputPath) // outputPath must include filename .ex : "...\out.pdf"
        {
            string element0 = outputPath;
            string element1 = getOutPutPath(outputPath);
            StringBuilder sBuilder = new StringBuilder();
            string setUpDetails = string.Format("[Target]\r\nType=6\r\nDWF={0}\r\nOUT={1}\r\n", element0, element1);
            sBuilder.Append(setUpDetails);
            sBuilder.Append("PWD=\r\n");
            //sBuilder.Append("[MRU Local]\r\n");
            //sBuilder.Append("MRU=0\r\n");
            sBuilder.Append("[MRU Sheet List]\r\n");
            sBuilder.Append("MRU=0\r\n");
            sBuilder.Append("[PdfOptions]\r\n");
            sBuilder.Append("IncludeHyperlinks=TRUE\r\n");
            sBuilder.Append("CreateBookmarks=TRUE\r\n");
            sBuilder.Append("CaptureFontsInDrawing=TRUE\r\n");
            sBuilder.Append("ConvertTextToGeometry=FALSE\r\n");
            sBuilder.Append("VectorResolution=1200\r\n");
            sBuilder.Append("RasterResolution=400\r\n");
            sBuilder.Append("[AutoCAD Block Data]\r\n");
            sBuilder.Append("IncludeBlockInfo=0\r\n");
            sBuilder.Append("BlockTmplFilePath=\r\n");
            sBuilder.Append("[SheetSet Properties]\r\n");
            sBuilder.Append("IsSheetSet=FALSE\r\n");
            sBuilder.Append("IsHomogeneous=FALSE\r\n");
            sBuilder.Append("SheetSet Name=\r\n");
            sBuilder.Append("NoOfCopies=1\r\n");
            sBuilder.Append("PlotStampOn=FALSE\r\n");
            sBuilder.Append("ViewFile=FALSE\r\n");
            sBuilder.Append("JobID=0\r\n");
            sBuilder.Append("SelectionSetName=\r\n");
            sBuilder.Append("AcadProfile=<<Unnamed Profile>>\r\n");
            sBuilder.Append("CategoryName=\r\n");
            sBuilder.Append("LogFilePath=\r\n");
            sBuilder.Append("IncludeLayer=TRUE\r\n");
            sBuilder.Append("LineMerge=FALSE\r\n");
            sBuilder.Append("CurrentPrecision=\r\n");
            sBuilder.Append("PromptForDwfName=TRUE\r\n");
            sBuilder.Append("PwdProtectPublishedDWF=FALSE\r\n");
            sBuilder.Append("PromptForPwd=FALSE\r\n");
            sBuilder.Append("RepublishingMarkups=FALSE\r\n");
            sBuilder.Append("PublishSheetSetMetadata=FALSE\r\n");
            sBuilder.Append("PublishSheetMetadata=FALSE\r\n");
            sBuilder.Append("3DDWFOptions=0 1\r\n");
            return sBuilder.ToString();
        }
        
        private static string getOutPutPath(string outPutPath)
        {
            string[] strs = outPutPath.Split('\\');
            string result = "";
            for(int i = 0; i < strs.Count()-1; i++)
            {
                result += strs[i] + "\\";
            }
            return result;
        }
    }
}
