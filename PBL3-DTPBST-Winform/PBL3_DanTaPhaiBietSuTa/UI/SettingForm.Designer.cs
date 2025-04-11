
namespace PBL3_DanTaPhaiBietSuTa.UI
{
    partial class SettingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingForm));
            this.btnSoundOn = new PBL3_DanTaPhaiBietSuTa.OvalPictureBox();
            this.btnHelp = new PBL3_DanTaPhaiBietSuTa.OvalPictureBox();
            this.btnExit = new PBL3_DanTaPhaiBietSuTa.OvalPictureBox();
            this.btnSoundOff = new PBL3_DanTaPhaiBietSuTa.OvalPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnSoundOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHelp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSoundOff)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSoundOn
            // 
            this.btnSoundOn.BackColor = System.Drawing.Color.DarkGray;
            this.btnSoundOn.Image = ((System.Drawing.Image)(resources.GetObject("btnSoundOn.Image")));
            this.btnSoundOn.Location = new System.Drawing.Point(-1, -1);
            this.btnSoundOn.Name = "btnSoundOn";
            this.btnSoundOn.Size = new System.Drawing.Size(71, 71);
            this.btnSoundOn.TabIndex = 1;
            this.btnSoundOn.TabStop = false;
            this.btnSoundOn.Click += new System.EventHandler(this.btnSoundOn_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.BackColor = System.Drawing.Color.DarkGray;
            this.btnHelp.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp.Image")));
            this.btnHelp.Location = new System.Drawing.Point(76, -1);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(71, 71);
            this.btnHelp.TabIndex = 2;
            this.btnHelp.TabStop = false;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.DarkGray;
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(153, -1);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(71, 71);
            this.btnExit.TabIndex = 3;
            this.btnExit.TabStop = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSoundOff
            // 
            this.btnSoundOff.BackColor = System.Drawing.Color.DarkGray;
            this.btnSoundOff.Image = ((System.Drawing.Image)(resources.GetObject("btnSoundOff.Image")));
            this.btnSoundOff.Location = new System.Drawing.Point(-1, -1);
            this.btnSoundOff.Name = "btnSoundOff";
            this.btnSoundOff.Size = new System.Drawing.Size(71, 71);
            this.btnSoundOff.TabIndex = 5;
            this.btnSoundOff.TabStop = false;
            this.btnSoundOff.Visible = false;
            this.btnSoundOff.Click += new System.EventHandler(this.btnSoundOff_Click);
            // 
            // SettingForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(229, 72);
            this.Controls.Add(this.btnSoundOff);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnSoundOn);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SettingForm";
            ((System.ComponentModel.ISupportInitialize)(this.btnSoundOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHelp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSoundOff)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private OvalPictureBox btnSoundOn;
        private OvalPictureBox btnHelp;
        private OvalPictureBox btnExit;
        private OvalPictureBox btnSoundOff;
    }
}