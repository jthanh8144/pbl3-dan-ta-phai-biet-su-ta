using PBL3_DanTaPhaiBietSuTa.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBL3_DanTaPhaiBietSuTa.UI
{
    public partial class AdminForm : Form
    {
        bool isNavOpen = true;
        private static string key;
        Thread threadLogout;
        public AdminForm()
        {
            SetKey();
            InitializeComponent();
            CBBFilter();
            DisplayDashboard();
            DisplayUser();
            DisplayQuestion();
        }
        private void DisplayDashboard()
        {
            string path = @Application.StartupPath + @"\Assets\FeedBack";
            var numOfUser = BLL.Instance.GetListUserInfor().Count;
            var numOfQuestion = BLL.Instance.GetAllQuestion().Count();
            int numOfFeedback = Directory.GetFiles(path, "*.txt", SearchOption.TopDirectoryOnly).Length;
            lbUser.Text = numOfUser.ToString();
            lbFeedback.Text = numOfFeedback.ToString();
            lbQuestion.Text = numOfQuestion.ToString();
        }
        private void DisplayUser()
        {
            var listUser = BLL.Instance.GetListUserInfor();
            UserDGV.DataSource = listUser;
        }
        private void DisplayQuestion()
        {
            comboBox1.SelectedIndex = 0;
            var listQuestion = BLL.Instance.GetAllQuestion();
            questionDGV.DataSource = listQuestion;
        }
        private void CBBFilter()
        {
            comboBox1.Items.AddRange(new string[]
            {
                "Tất cả",
                "Level 1",
                "Level 2",
                "Level 3",
                "Level 4",
                "Level 5"
            });
            comboBox1.SelectedIndex = 0;
        }
        private void menuPanel_Click(object sender, EventArgs e)
        {
            if (isNavOpen)
            {
                panel6.Location = new System.Drawing.Point(50, -2);
                panel8.Location = new System.Drawing.Point(50, -2);
                panel9.Location = new System.Drawing.Point(50, -2);

                this.Size = new Size(726, this.Height);

                navBar.Size = new Size(45, navBar.Height);
                homePanel.Size = new Size(45, homePanel.Height);
                userPanel.Size = new Size(45, userPanel.Height);
                levelPanel.Size = new Size(45, levelPanel.Height);
                feedbackPanel.Size = new Size(45, feedbackPanel.Height);
                securityPanel.Size = new Size(45, securityPanel.Height);
                logoutPanel.Size = new Size(45, logoutPanel.Height);
                isNavOpen = false;
            }
            else
            {
                panel6.Location = new System.Drawing.Point(166, -2);
                panel8.Location = new System.Drawing.Point(166, -2);
                panel9.Location = new System.Drawing.Point(166, -2);

                this.Size = new Size(842, this.Height);

                navBar.Size = new Size(163, navBar.Height);
                homePanel.Size = new Size(160, homePanel.Height);
                userPanel.Size = new Size(160, userPanel.Height);
                levelPanel.Size = new Size(160, levelPanel.Height);
                feedbackPanel.Size = new Size(160, feedbackPanel.Height);
                securityPanel.Size = new Size(160, securityPanel.Height);
                logoutPanel.Size = new Size(160, logoutPanel.Height);
                isNavOpen = true;
            }
        }
        private void Feedbackbtn_Click(object sender, EventArgs e)
        {
            Process.Start(@Application.StartupPath + @"\Assets\FeedBack");
        }
        private void MD5Keybtn_Click(object sender, EventArgs e)
        {
            panel6.Visible = true;
            panel8.Visible = false;
            panel9.Visible = false;
            if (File.Exists(@Application.StartupPath + @"\Assets\key.mpv"))
            {
                string oldKey = File.ReadAllLines(@Application.StartupPath + @"\Assets\key.mpv").First().ToString();
                var process = Process.Start(@Application.StartupPath + @"\Assets\key.mpv");
                process.WaitForExit();
                if (String.Compare(oldKey, File.ReadLines(@Application.StartupPath + @"\Assets\key.mpv").First()) != 0)
                {
                    if (File.ReadAllLines(@Application.StartupPath + @"\Assets\key.mpv").First() == null)
                        File.Delete(@Application.StartupPath + @"\Assets\key.mpv");
                    else
                        key = File.ReadLines(@Application.StartupPath + @"\Assets\key.mpv").First();
                    ShowMessage("key vừa được thay đổi, dữ liệu sẽ được thay đổi!");
                    var listUser = BLL.Instance.GetListUserInfor();
                    List<string> listOldPass = new List<string>();
                    foreach (var user in listUser)
                    {
                        listOldPass.Add(DecryptMD5(user.Password, oldKey));
                    }
                    int index = 0;
                    foreach (var user in listUser)
                    {
                        user.Password = EncryptMD5(listOldPass[index], key);
                        BLL.Instance.UpdateUserInfor(user);
                        index++;
                    }
                }
            }
            else
            {
                DialogResult d = MessageBox.Show("Không tồn tại key MD5 nào! \nTạo mới?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                switch (d)
                {
                    case DialogResult.Yes:
                        using (StreamWriter sw = File.CreateText(@Application.StartupPath + @"\Assets\key.mpv"))
                        {
                            sw.WriteLine("PBL3-2021");
                        }
                        Process.Start(@Application.StartupPath + @"\Assets\key.mpv");
                        break;
                    case DialogResult.No:
                        break;
                }
            }
            DisplayUser();
        }
        private void Dashboardbtn_Click(object sender, EventArgs e)
        {
            panel8.Visible = true;
            panel9.Visible = false;
            panel6.Visible = false;
        }
        private void userIcon_Click(object sender, EventArgs e)
        {
            panel6.Visible = true;
            panel8.Visible = false;
            panel9.Visible = false;
        }
        private void Questionbtn_Click(object sender, EventArgs e)
        {
            panel9.Visible = true;
            panel8.Visible = false;
            panel6.Visible = false;
        }
        private void logoutPanel_Click(object sender, EventArgs e)
        {
            this.Dispose();
            HomePage.PlaySound();
            threadLogout = new Thread(OpenLoginForm);
            threadLogout.SetApartmentState(ApartmentState.STA);
            threadLogout.Start();
        }
        private void saveuserBtn_Click(object sender, EventArgs e)
        {
            if (addUsercb.Checked)
            {
                if (!IsValid())
                {
                    return;
                }
                UserInfo user = new UserInfo()
                {
                    Username = tbUsername.Text,
                    Password = EncryptMD5(tbPass.Text, key),
                    Name = tbName.Text,
                    Email = tbEmail.Text
                };
                if (BLL.Instance.AddNewUser(user))
                {
                    ShowMessage("Thêm tài khoản thành công!");
                    DisplayUser();
                    addUsercb.Checked = false;
                    tbUsername.Text = "";
                    tbPass.Text = "";
                    tbName.Text = "";
                    tbEmail.Text = "";
                    DisplayDashboard();
                    return;
                }
                else
                {
                    ShowMessage("Tên tài khoản đã tồn tại!");
                    return;
                }
            }
            else
            {
                int userID = GetIdFromDGV(UserDGV, "UserID");
                UserInfo user = new UserInfo()
                {
                    UserID = userID,
                    Username = tbUsername.Text,
                    Password = EncryptMD5(tbPass.Text, key),
                    Name = tbName.Text,
                    Email = tbEmail.Text
                };
                if (BLL.Instance.UpdateUserInfor(user))
                {
                    ShowMessage("Cập nhật tài khoản thành công!");
                    DisplayUser();
                    return;
                }
                else
                {
                    ShowMessage("Có lỗi xảy ra, vui lòng thử lại sau!");
                    return;
                }
            }
        }
        private void UserDGV_SelectionChanged(object sender, EventArgs e)
        {
            if (UserDGV.SelectedCells.Count > 0)
            {
                int userID = GetIdFromDGV(UserDGV, "UserID");
                var user = BLL.Instance.GetUserInfoByUserID(userID);
                tbUsername.Text = user.Username;
                try
                {
                    tbPass.Text = DecryptMD5(user.Password, key);
                }
                catch (Exception) { };
                tbName.Text = user.Name;
                tbEmail.Text = user.Email;
            }
        }
        private void addUsercb_CheckedChanged(object sender, EventArgs e)
        {
            tbUsername.Text = "";
            tbPass.Text = "";
            tbName.Text = "";
            tbEmail.Text = "";
            if (addUsercb.Checked)
            {
                tbUsername.Enabled = true;
                UserDGV.Enabled = false;
                deleteUserBtn.Enabled = false;
            }
            else
            {
                tbUsername.Enabled = false;
                UserDGV.Enabled = true;
                deleteUserBtn.Enabled = true;
            }
        }
        private void clearUserBtn_Click(object sender, EventArgs e)
        {
            tbUsername.Text = "";
            tbPass.Text = "";
            tbName.Text = "";
            tbEmail.Text = "";
        }
        private void deleteUserBtn_Click(object sender, EventArgs e)
        {
            if (tbUsername.Text == "" || UserDGV.Rows.Count == 0)
            {
                ShowMessage("No row is seleted!");
                return;
            }
            else
            {
                int userID = GetIdFromDGV(UserDGV, "UserID");
                DialogResult d = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                switch (d)
                {
                    case DialogResult.Yes:
                        if (BLL.Instance.DeleteUser(userID))
                        {
                            ShowMessage("Xóa thành công!");
                        }
                        else
                            ShowMessage("Có lỗi xảy ra, vui lòng thử lại sau!");
                        break;
                    case DialogResult.No:
                        break;
                }
            }
            DisplayDashboard();
            DisplayUser();
        }
        private void questionDGV_SelectionChanged(object sender, EventArgs e)
        {
            if (questionDGV.SelectedCells.Count > 0)
            {
                int questionID = GetIdFromDGV(questionDGV, "QuestionID");
                var question = BLL.Instance.GetQuestionByQuestionID(questionID);
                tbQuestion.Text = question.QuestionContent;
                tbKeyA.Text = question.KeyA;
                tbKeyB.Text = question.KeyB;
                tbKeyC.Text = question.KeyC;
                tbKeyD.Text = question.KeyD;
                string answer = question.Answer;

                if (String.Compare(tbKeyA.Text, answer) == 0)
                {
                    radioButton1.Checked = true;
                    return;
                }
                if (String.Compare(tbKeyB.Text, answer) == 0)
                {
                    radioButton2.Checked = true;
                    return;
                }
                if (String.Compare(tbKeyC.Text, answer) == 0)
                {
                    radioButton3.Checked = true;
                    return;
                }
                if (String.Compare(tbKeyD.Text, answer) == 0)
                {
                    radioButton4.Checked = true;
                    return;
                }
            }
        }
        private void refreshQuestionbtn_Click(object sender, EventArgs e)
        {
            DisplayQuestion();
            comboBox1.SelectedIndex = 0;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var levelIndex = comboBox1.SelectedIndex;
            List<Question> listQuestion = new List<Question>();
            switch (levelIndex)
            {
                case 0:
                    DisplayQuestion();
                    break;
                case 1:
                    listQuestion = BLL.Instance.GetQuestionsByStage(1).OrderBy(p => p.TimeStop).ToList();
                    questionDGV.DataSource = listQuestion;
                    break;
                case 2:
                    listQuestion = BLL.Instance.GetQuestionsByStage(2).OrderBy(p => p.TimeStop).ToList();
                    questionDGV.DataSource = listQuestion;
                    break;
                case 3:
                    listQuestion = BLL.Instance.GetQuestionsByStage(3).OrderBy(p => p.TimeStop).ToList();
                    questionDGV.DataSource = listQuestion;
                    break;
                case 4:
                    listQuestion = BLL.Instance.GetQuestionsByStage(4).OrderBy(p => p.TimeStop).ToList();
                    questionDGV.DataSource = listQuestion;
                    break;
                case 5:
                    listQuestion = BLL.Instance.GetQuestionsByStage(5).OrderBy(p => p.TimeStop).ToList();
                    questionDGV.DataSource = listQuestion;
                    break;
                default:
                    listQuestion = BLL.Instance.GetQuestionsByStage(1);
                    questionDGV.DataSource = listQuestion;
                    break;
            }
        }
        private void deleteQuestionbtn_Click(object sender, EventArgs e)
        {
            if (questionDGV.Rows.Count == 0)
            {
                ShowMessage("No row is seleted!");
                return;
            }
            else
            {
                int questionID = GetIdFromDGV(questionDGV, "QuestionID");
                DialogResult d = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                switch (d)
                {
                    case DialogResult.Yes:
                        if (BLL.Instance.DeleteQuestion(questionID))
                        {
                            ShowMessage("Xóa thành công!");
                        }
                        else
                            ShowMessage("Có lỗi xảy ra, vui lòng thử lại sau!");
                        break;
                    case DialogResult.No:
                        break;
                }
            }
            DisplayDashboard();
            DisplayQuestion();
        }
        private void saveQuestionbtn_Click(object sender, EventArgs e)
        {
            Question question = new Question();
            question.QuestionID = GetIdFromDGV(questionDGV, "QuestionID");
            question.QuestionContent = tbQuestion.Text;
            question.KeyA = tbKeyA.Text;
            question.KeyB = tbKeyB.Text;
            question.KeyC = tbKeyC.Text;
            question.KeyD = tbKeyD.Text;

            if (radioButton1.Checked)
                question.Answer = tbKeyA.Text;
            if (radioButton2.Checked)
                question.Answer = tbKeyB.Text;
            if (radioButton3.Checked)
                question.Answer = tbKeyC.Text;
            if (radioButton4.Checked)
                question.Answer = tbKeyD.Text;

            if (BLL.Instance.UpdateQuestion(question))
            {
                ShowMessage("Cập nhật thành công!");
            }
            else
            {
                ShowMessage("Có lỗi xảy ra, vui lòng thử lại sau!");
            }
            DisplayQuestion();
        }
        private string EncryptMD5(string s, string key)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateEncryptor())
                    {
                        byte[] textBytes = UTF8Encoding.UTF8.GetBytes(s);
                        byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
                        return Convert.ToBase64String(bytes, 0, bytes.Length);
                    }
                }
            }
        }
        private string DecryptMD5(string s, string key)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateDecryptor())
                    {
                        try
                        {
                            byte[] cipherBytes = Convert.FromBase64String(s);
                            byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

                            return UTF8Encoding.UTF8.GetString(bytes);
                        }
                        catch (CryptographicException) { };
                        return "";
                    }
                }
            }
        }
        private static void SetKey()
        {
            if (File.Exists(@Application.StartupPath + @"\Assets\key.mpv"))
            {
                try
                {
                    key = File.ReadAllLines(@Application.StartupPath + @"\Assets\key.mpv").First();
                }
                catch (Exception)
                {
                    File.Delete(@Application.StartupPath + @"\Assets\key.mpv");
                    key = "";
                }
            }
            else
            {
                key = "";
            }
        }
        private int GetIdFromDGV(DataGridView dataGridView, string idColumnName)
        {
            int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView.Rows[selectedrowindex];
            return Convert.ToInt32(selectedRow.Cells[idColumnName].Value);
        }
        private bool IsValid()
        {
            List<char> list = new List<char>()
            {
                '`', '~', '!', '#', '$', '%', '^', '&', '*', '(', ')', '-', '+', '=',
                '{', '[', ']', '}', '|', ';', ':', ',', '<', '>', '/', '?'
            };
            if (tbUsername.Text == "" || tbPass.Text == "" || tbEmail.Text == "")
            {
                //UX viền đỏ
                ShowMessage("Vui lòng nhập đủ thông tin!");
                return false;
            }
            foreach (var l in list)
            {
                if (tbEmail.Text.Contains(l))
                {
                    ShowMessage("Email không thể chứa các ký tự `,~,!,..");
                    return false;
                }
            }
            if (!tbEmail.Text.Contains("@"))
            {
                ShowMessage("Địa chỉ Email phải chứa ký tự @!");
                return false;
            }
            if (tbEmail.Text.Substring(0, 1) == "@")
            {
                ShowMessage("Email không thể bắt đầu bằng ký tự @!");
                return false;
            }
            return true;
        }
        private void ShowMessage(string message)
        {
            Notification notification = new Notification();
            notification.Get(message);
            notification.ShowDialog();
        }
        private void HoverBackground(object sender, EventArgs e)
        {
            ((Panel)sender).BackColor = Color.DodgerBlue;
        }
        private void LeaveBackground(object sender, EventArgs e)
        {
            ((Panel)sender).BackColor = Color.Black;
        }
        private void LabelHover(object sender, EventArgs e)
        {
            if (((Label)sender).Name == homePanel.Controls[0].Name)
            {
                HoverBackground(homePanel, e);
                return;
            }
            if (((Label)sender).Name == userPanel.Controls[0].Name)
            {
                HoverBackground(userPanel, e);
                return;
            }
            if (((Label)sender).Name == levelPanel.Controls[0].Name)
            {
                HoverBackground(levelPanel, e);
                return;
            }
            if (((Label)sender).Name == feedbackPanel.Controls[0].Name)
            {
                HoverBackground(feedbackPanel, e);
                return;
            }
            if (((Label)sender).Name == securityPanel.Controls[0].Name)
            {
                HoverBackground(securityPanel, e);
                return;
            }
            if (((Label)sender).Name == logoutPanel.Controls[0].Name)
            {
                HoverBackground(logoutPanel, e);
                return;
            }
        }
        private void LabelLeaver(object sender, EventArgs e)
        {
            if (((Label)sender).Name == homePanel.Controls[0].Name)
            {
                LeaveBackground(homePanel, e);
                return;
            }
            if (((Label)sender).Name == userPanel.Controls[0].Name)
            {
                LeaveBackground(userPanel, e);
                return;
            }
            if (((Label)sender).Name == levelPanel.Controls[0].Name)
            {
                LeaveBackground(levelPanel, e);
                return;
            }
            if (((Label)sender).Name == feedbackPanel.Controls[0].Name)
            {
                LeaveBackground(feedbackPanel, e);
                return;
            }
            if (((Label)sender).Name == securityPanel.Controls[0].Name)
            {
                LeaveBackground(securityPanel, e);
                return;
            }
            if (((Label)sender).Name == logoutPanel.Controls[0].Name)
            {
                LeaveBackground(logoutPanel, e);
                return;
            }
        }
        private void PictureHover(object sender, EventArgs e)
        {
            if (((PictureBox)sender).Name == homePanel.Controls[1].Name)
            {
                HoverBackground(homePanel, e);
                return;
            }
            if (((PictureBox)sender).Name == userPanel.Controls[1].Name)
            {
                HoverBackground(userPanel, e);
                return;
            }
            if (((PictureBox)sender).Name == levelPanel.Controls[1].Name)
            {
                HoverBackground(levelPanel, e);
                return;
            }
            if (((PictureBox)sender).Name == feedbackPanel.Controls[1].Name)
            {
                HoverBackground(feedbackPanel, e);
                return;
            }
            if (((PictureBox)sender).Name == securityPanel.Controls[1].Name)
            {
                HoverBackground(securityPanel, e);
                return;
            }
            if (((PictureBox)sender).Name == logoutPanel.Controls[1].Name)
            {
                HoverBackground(logoutPanel, e);
                return;
            }
        }
        private void PictureLeaver(object sender, EventArgs e)
        {
            if (((PictureBox)sender).Name == homePanel.Controls[1].Name)
            {
                LeaveBackground(homePanel, e);
                return;
            }
            if (((PictureBox)sender).Name == userPanel.Controls[1].Name)
            {
                LeaveBackground(userPanel, e);
                return;
            }
            if (((PictureBox)sender).Name == levelPanel.Controls[1].Name)
            {
                LeaveBackground(levelPanel, e);
                return;
            }
            if (((PictureBox)sender).Name == feedbackPanel.Controls[1].Name)
            {
                LeaveBackground(feedbackPanel, e);
                return;
            }
            if (((PictureBox)sender).Name == securityPanel.Controls[1].Name)
            {
                LeaveBackground(securityPanel, e);
                return;
            }
            if (((PictureBox)sender).Name == logoutPanel.Controls[1].Name)
            {
                LeaveBackground(logoutPanel, e);
                return;
            }
        }
        private void OpenLoginForm(object sender)
        {
            Application.Run(new DangNhap());
        }
        private void AdminForm_Load(object sender, EventArgs e)
        {
            panel6.Location = new System.Drawing.Point(166, -2);
            panel8.Location = new System.Drawing.Point(166, -2);
            panel9.Location = new System.Drawing.Point(166, -2);

            panel8.Visible = true;
            panel9.Visible = false;
            panel6.Visible = false;
        }
    }
}
