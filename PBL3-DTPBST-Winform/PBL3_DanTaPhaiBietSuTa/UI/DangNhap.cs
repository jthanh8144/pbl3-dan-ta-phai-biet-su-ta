using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PBL3_DanTaPhaiBietSuTa.DTO;
using PBL3_DanTaPhaiBietSuTa.UI;


namespace PBL3_DanTaPhaiBietSuTa
{
    public partial class DangNhap : Form
    {
        Thread thread;
        private static string key;
        public DangNhap()
        {
            SetKey();
            InitializeComponent();
            IsRememberUser();
            
        }
        private void Setting_Click(object sender, EventArgs e)
        {
            SettingForm setting = new SettingForm();
            setting.ShowDialog();
        }
        private void txtLoginR_Click(object sender, EventArgs e)
        {
            txtAccountR.Text = "";
            txtNameR.Text = "";
            txtEmailR.Text = "";
            txtPassR.Text = "";
            txtRepassR.Text = "";
            gbRegister.Visible = false;
            gbLogin.Visible = true;
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            gbLogin.Visible = false;
            gbRegister.Visible = true;
        }
        private void OpenUserForm(object sender)
        {
            Application.Run(new User());
        }
        private void OpenAdminForm(object sender)
        {
            Application.Run(new AdminForm());
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txtAccount.Text;
            string passWord = EncryptMD5(txtPass.Text);
            UserInfo user = BLL.Instance.GetUserInforByUserName(userName);
            if (BLL.Instance.CheckLogin(userName, passWord))
            {
                if (cbRemember.Checked) //lưu userName và passWord vào file.
                {
                    string rememberUserPath = @Application.StartupPath + @"\Assets\SavedUser\rememberUser.txt";
                    using (StreamWriter sw = File.CreateText(rememberUserPath))
                    {
                        sw.WriteLine(user.UserID);
                    }
                }
                else
                {
                    string rememberUserPath = @Application.StartupPath + @"\Assets\SavedUser\rememberUser.txt";
                    File.Delete(rememberUserPath);
                }
                ShowMessage("Đăng nhập thành công!");
                GetUserLogin(userName);
                this.Dispose();
                if (user.IsAdmin)
                {
                    HomePage.StopSound();
                    thread = new Thread(OpenAdminForm);
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.Start();
                }
                else
                {
                    thread = new Thread(OpenUserForm);
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.Start();
                }
            }
            else
            {
                //Hiện Thông báo đăng nhập thất bại
                ShowMessage("Vui lòng kiểm tra lại thông tin tài khoản!");
                txtAccount.Text = "";
                txtPass.Text = "";
                return;
            }
        }
        private void btnRegisterR_Click(object sender, EventArgs e)
        {
            if (txtPassR.Text != txtRepassR.Text)
            {
                ShowMessage("Pass và Repass không giống nhau!");
                txtPassR.Text = "";
                txtRepassR.Text = "";
                return;
            }
            if (IsValid() == false) return;
            UserInfo newUser = new UserInfo()
            {
                Name = txtNameR.Text,
                Username = txtAccountR.Text,
                Password = EncryptMD5(txtPassR.Text),
                Email = txtEmailR.Text,
            };
            if(BLL.Instance.AddNewUser(newUser))
            {
                ShowMessage("Đăng ký thành công!");
                txtAccountR.Text = "";
                txtEmailR.Text = "";
                txtPassR.Text = "";
                txtRepassR.Text = "";
                txtAccount.Text = "";
                txtPass.Text = "";
                gbRegister.Visible = false;
                gbLogin.Visible = true;
            }   
            else
            {
                ShowMessage("Tên tài khoản đã tồn tại!");
                return;
            }    
        }
        private bool IsRememberUser()
        {
            string rememberUserPath = @Application.StartupPath + @"\Assets\SavedUser\rememberUser.txt";
            int rememberUserID = 0;
            if (File.Exists(rememberUserPath))
            {
                cbRemember.Checked = true;
                try
                {
                    rememberUserID = Convert.ToInt32(File.ReadLines(rememberUserPath).First());
                }
                catch(FormatException)
                {
                    File.Delete(rememberUserPath);
                    return false;
                };
                UserInfo rememberUser = BLL.Instance.GetUserInfoByUserID(rememberUserID);
                if(rememberUser != null)
                {
                    txtAccount.Text = rememberUser.Username;
                    txtPass.Text = DecryptMD5(rememberUser.Password);
                }    
            }
            return true;
        }
        private bool IsValid()
        {
            List<char> list = new List<char>()
            {
                '`', '~', '!', '#', '$', '%', '^', '&', '*', '(', ')', '-', '+', '=',
                '{', '[', ']', '}', '|', ';', ':', ',', '<', '>', '/', '?' 
            };
            if (txtAccountR.Text == "" || txtEmailR.Text == "" || txtPassR.Text == "" || txtRepassR.Text == "")
            {
                //UX viền đỏ
                ShowMessage("Vui lòng nhập đủ thông tin!");
                return false;
            }
            foreach(var l in list)
            {
                if(txtEmailR.Text.Contains(l))
                {
                    ShowMessage("Email không thể chứa các ký tự `,~,!,..");
                    return false;
                }
            }
            if (!txtEmailR.Text.Contains("@"))
            {
                ShowMessage("Địa chỉ Email phải chứa ký tự @!");
                return false;
            }    
            if (txtEmailR.Text.Substring(0, 1) == "@")
            {
                ShowMessage("Email không thể bắt đầu bằng ký tự @!");
                return false;
            }
            return true;
        }
        private void GetUserLogin(string userName)
        {
            UserInfo user = BLL.Instance.GetUserInforByUserName(userName);
            string userLogin = @Application.StartupPath + @"\Assets\SavedUser\Account.txt";
            using (StreamWriter sw = File.CreateText(userLogin))
            {
                sw.WriteLine(user.UserID);
            }
        }
        private void txtAccount_TextChanged(object sender, EventArgs e)
        {
            if (txtAccount.Text == "") txtPass.Text = "";
        }
        private string EncryptMD5(string s)
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
        private string DecryptMD5(string s)
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
                        catch(CryptographicException) { };
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
        private void ShowMessage(string message)
        {
            Notification notification = new Notification();
            notification.Get(message);
            notification.ShowDialog();
        }
        private void DangNhap_Load(object sender, EventArgs e)
        {
            gbLogin.Location = new System.Drawing.Point((this.Size.Width - gbLogin.Size.Width) / 2, 
                (this.Size.Height - gbLogin.Size.Height) / 2);
            gbRegister.Location = new System.Drawing.Point((this.Size.Width - gbRegister.Size.Width) / 2,
                (this.Size.Height - gbRegister.Size.Height) / 2);           
        }
    }
}
