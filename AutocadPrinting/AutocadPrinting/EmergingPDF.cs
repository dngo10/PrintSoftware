using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutocadPrinting
{
    class EmergingPDF
    {
        // Use PDF sharp 
        public static void mergePdf(List<KeyValuePair<string, List<string>>> printList, string outputFile, out bool done)
        {
            List<string> pdfPathList = getPdfPathList(printList);
            if(pdfPathList == null || pdfPathList.Count == 0)
            {
                MessageBox.Show("Couldn't Generate Any Layout", "No Layout Generated", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                done = false;
                return;
            }


            using(PdfDocument pdf = new PdfDocument())
            {
                foreach (string page in pdfPathList)
                {
                    using (PdfDocument pdfDoc = PdfReader.Open(page, PdfDocumentOpenMode.Import))
                    {
                        for (int i = 0; i < pdfDoc.PageCount; i++)
                        {
                            pdf.AddPage(pdfDoc.Pages[i]);
                        }
                    }
                }
                pdf.Save(outputFile);
                done = true;
            }
        }

        public static List<string> getPdfPathList(List<KeyValuePair<string, List<string>>> printList)
        {
            List<string> pdfPathList = new List<string>();
            string plotPath = Path.GetTempPath() + PrintForm.folderTemp + "\\" + PrintPdf2.pdfFolder;
            foreach(KeyValuePair<string, List<string>> kv in printList)
            {
                string dwgFolder = plotPath + "\\" + kv.Key.Split('\\').Last().Replace('.', '_');
                foreach(string layoutName in kv.Value)
                {
                    string pdfPath = dwgFolder + "\\" + layoutName + ".pdf";
                    if (File.Exists(pdfPath))
                    {
                        pdfPathList.Add(pdfPath);
                    }
                }
            }
            return pdfPathList;
        }


    }
}
