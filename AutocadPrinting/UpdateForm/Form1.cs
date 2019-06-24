using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dropbox.Api;

//mWYakKSUMVAAAAAAAAAAHA6pg3g7sHNDpok7z6sxbouPqVc73BBtkQOwpoJERxgy
namespace UpdateForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            dropBoxHandler dbh = new dropBoxHandler();
            dbh.downloadVersion().Wait();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //var task = dropBoxHandler.downloadVersion();

        }


    }
}
