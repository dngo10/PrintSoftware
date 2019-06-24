using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace CheckingApp
{
    public partial class Form1 : Form
    {
        private static int parentWidth, parentHeight, parentTop, parentLefth;
        long tick = 0;
        public Form1()
        {
            InitializeComponent();
            backgroundWorker1.WorkerReportsProgress = true;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
            InitTimer();

            getPosition();
            this.Top = parentTop + parentHeight / 2 - this.Height / 2;
            this.Left = parentLefth + parentWidth / 2 - this.Width / 2;
        }

        public void InitTimer()
        {
            timer2 = new System.Windows.Forms.Timer();
            timer2.Tick += new EventHandler(timer2_Tick);
            timer2.Interval = 1000; // in miliseconds
            timer2.Start();


            timer3 = new System.Windows.Forms.Timer();
            timer3.Tick += new EventHandler(timer3_Tick);
            timer3.Interval = 1000; // in miliseconds
            timer3.Start();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            CheckingFiles.checkingLayouts(ref progressBar, ref label1);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            var allProcesses = Process.GetProcesses();
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
                foreach (Process proc in Process.GetProcessesByName("accoreconsole.exe"))
                {
                    proc.Kill();
                }
            }
            catch
            {

            }
     
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            long val = ++tick;
            string sec = (val % 60).ToString("D2");
            string minutes = ((val/60)%60).ToString("D2");
            string hours = (val/(60*60)).ToString();
            timeRunLabel.Text = String.Format("{0}:{1,2:0}:{2,2:0}",hours,minutes,sec);
            File.WriteAllText(Path.GetTempPath() + "GEtimeLapse.txt", timeRunLabel.Text);
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
