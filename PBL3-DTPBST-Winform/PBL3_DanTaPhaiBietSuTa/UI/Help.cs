﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBL3_DanTaPhaiBietSuTa.UI
{
    public partial class Help : Form
    {
        public Help()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void Help_Load(object sender, EventArgs e)
        {
            label1.Location = new Point((this.Size.Width - label1.Size.Width) / 2, (this.Size.Width - label1.Size.Width) / 2);
        }
    }
}
