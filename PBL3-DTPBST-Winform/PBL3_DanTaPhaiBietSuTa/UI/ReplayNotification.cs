using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBL3_DanTaPhaiBietSuTa.UI
{
    public partial class ReplayNotification : Form
    {
        public delegate void GetStageID(int ID);
        public GetStageID Sender;
        public static bool isUnlockNextLevel;
        static int stageID;
        Thread thread;
        private void getStageID(int ID)
        {
            stageID = ID;
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

        public ReplayNotification()
        {
            InitializeComponent();
            Sender = new GetStageID(getStageID);
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void ReplayNotification_Load(object sender, EventArgs e)
        {
            label1.Text = "";
            if (isUnlockNextLevel)
            {
                btnNext.Image = Image.FromFile(@Application.StartupPath + @"\Assets\Image\next.png");
            }
            else
            {
                btnNext.Image = Image.FromFile(@Application.StartupPath + @"\Assets\Image\notnext.png");
                btnNext.Cursor = Cursors.Default;
                btnNext.Enabled = false;
            }
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            Play.isPlayAgain = false;
            this.Dispose();
        }

        private void btnReplay_Click(object sender, EventArgs e)
        {
            Play.isPlayAgain = true;
            this.Dispose();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            stageID++;
            Play.isNextLevel = true;
            DisposeForm();
        }
        private void OpenPlayForm(object sender)
        {
            Play play = new Play();
            play.Sender(stageID);
            Application.Run(play);
        }
        private void DisposeForm()
        {
            this.Dispose();
            thread = new Thread(OpenPlayForm);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
    }
}
