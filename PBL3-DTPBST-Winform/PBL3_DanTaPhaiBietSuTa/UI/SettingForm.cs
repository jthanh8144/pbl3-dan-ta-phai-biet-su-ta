using System;
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
    public partial class SettingForm : Form
    {
        public static bool isPlaySound;
        public SettingForm()
        {
            InitializeComponent();
            DisplaySoundBtn();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSoundOn_Click(object sender, EventArgs e)
        {
            isPlaySound = false;
            DisplaySoundBtn();
        }

        private void btnSoundOff_Click(object sender, EventArgs e)
        {
            isPlaySound = true;
            DisplaySoundBtn();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            Help h = new Help();
            h.ShowDialog();
        }
        private void DisplaySoundBtn()
        {
            if(isPlaySound)
            {
                HomePage.PlaySound();
                btnSoundOff.Visible = false;
                btnSoundOn.Visible = true;
            }  
            else
            {
                HomePage.StopSound();
                btnSoundOff.Visible = true;
                btnSoundOn.Visible = false;
            }    
        }
    }
}
