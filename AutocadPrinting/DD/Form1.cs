﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            dropBoxHandler db = new dropBoxHandler();
            db.downloadVersion().Wait();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
