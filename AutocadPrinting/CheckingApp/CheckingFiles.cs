using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckingApp
{
    class CheckingFiles
    {
        public static void checkingLayouts(ref ProgressBar pb, ref Label lb)
        {
            if (File.Exists(Path.GetTempPath() + "pdfLayoutListGE.txt"))
            {
                string[] layOuts = File.ReadAllLines(Path.GetTempPath() + "pdfLayoutListGE.txt");
                int x = 0;
                for (int i = 0; i < layOuts.Count(); i++)
                {
                    if (File.Exists(layOuts[i]))
                    {
                        x++;
                    }
                }
                pb.Maximum = layOuts.Count();
                pb.Step = 1;
                pb.Value = x;
                lb.Text = x.ToString() + "/" + layOuts.Count();
            }
            else
            {
                lb.Text = "NO PROGRESS FOUND";
            }
        }
    }
}
