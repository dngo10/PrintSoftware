using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LayoutBuffering
{
    public partial class Form1 : Form
    {
        private static string folderTemp = "GEPrinting";
        private static int parentWidth, parentHeight, parentTop, parentLefth;
        public long i = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            getPosition();
            InitTimer();
            this.Top = parentTop + parentHeight / 2 - this.Height/2;
            this.Left = parentLefth + parentWidth / 2 - this.Width/2;
            //this.ShowIcon = false;
            //this.ShowInTaskbar = false;
        }

        public void InitTimer()
        {
            timer1 = new System.Windows.Forms.Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 200; // in miliseconds
            timer1.Start();
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            string fileName = "";
            if (File.Exists(Path.GetTempPath() + folderTemp + "\\fileName.txt"))
            {
                fileName = File.ReadAllText(Path.GetTempPath() + folderTemp + "\\fileName.txt");
            }
            i++;
            long k = 3;
            long re = i % k;
            string temp = "";

            for(long t = 0; t < re; t++)
            {
                temp += "_";
            }
            label1.Text = "Loading " + fileName + temp;

            Random rnd = new Random();
            int temp1 = rnd.Next(0, 255);
            int temp2 = rnd.Next(0, 255);
            int temp3 = rnd.Next(0, 255);
            int temp4 = rnd.Next(0, 255);
            label1.ForeColor = Color.FromArgb(temp1, temp2, temp3, temp4);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            try
            {

                foreach (Process proc in Process.GetProcessesByName("AutoCAD component"))
                {
                    proc.Kill();
                }

            }
            catch
            {

            }
            try
            {
                foreach (Process proc in Process.GetProcessesByName("accoreconsole"))
                {
                    proc.Kill();
                }
            }
            catch
            {

            }
            try
            {
                foreach (Process proc in Process.GetProcessesByName("PrintForm"))
                {
                    proc.Kill();
                }
            }
            catch
            {

            }
            try
            {
                foreach (Process proc in Process.GetProcessesByName("AutocadPrinting"))
                {
                    proc.Kill();
                }
            }
            catch
            {

            }

        }

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void getPosition()
        {
            string[] positions = File.ReadAllLines(Path.GetTempPath() + "printFormPosition.txt");
            parentWidth = Convert.ToInt32(positions[0]);
            parentHeight = Convert.ToInt32(positions[1]);
            parentTop = Convert.ToInt32(positions[2]);
            parentLefth = Convert.ToInt32(positions[3]);
        }
    }
}
