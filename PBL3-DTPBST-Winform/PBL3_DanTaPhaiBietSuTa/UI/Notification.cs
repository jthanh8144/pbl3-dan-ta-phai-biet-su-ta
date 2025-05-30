﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace PBL3_DanTaPhaiBietSuTa.UI
{
    public partial class Notification : Form
    {
        public delegate void GetMessage(string message);
        public GetMessage Get;
        string mess = "";
        private void getMessage(string s)
        {
            mess = s;
        }
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );

        public Notification()
        {
            InitializeComponent();
            Get = new GetMessage(getMessage);
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Notification_Load(object sender, EventArgs e)
        {
            //label1.Text = mess + Environment.NewLine + "abc";
            label1.Text = mess;
            label1.Location = new Point((this.Size.Width - label1.Size.Width) / 2, 40);
        }
    }
}
