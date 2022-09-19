using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class MainForm : Form
    {
        public class User
        {
            private string username;
            private string password;
            private bool blocked;
            private bool hasRestrictions;
            public User()
            {
                username = "";
                password = "";
                blocked = false;
                hasRestrictions = true;
            }
            public User(string username, string password, bool blocked, bool hasRestrictions)
            {
                this.username = username;
                this.password = password;
                this.blocked = blocked;
                this.hasRestrictions = hasRestrictions;
            }
            public string Username
            {
                get { return username; }
                set { username = value; }
            }
            public string Password
            {
                get { return password; }
                set { password = value; }
            }
            public bool isBlocked
            {
                get { return blocked; }
                set { blocked = value; }
            }
            public bool hasPasswordRestrictions
            {
                get { return hasRestrictions; }
                set { hasRestrictions = value; }
            }
        }
        string path = "users.dat";
        string log_path = "log.txt";
        public Dictionary<string, User> user_map = new Dictionary<string, User>();
        StreamWriter logStream;

        private bool saveData = true;
        public User activeUser;

        public void Terminate()
        {
            saveData = false;
            Close();
        }
        public void LogOutput(string message)
        {
            logStream.WriteLine(DateTime.Now + " " + message);
        }

        private void SaveUserData()
        {
            LogOutput("Сохраняю изменения");
            File.Delete(path);
            FileStream user_fs = File.Create(path);
            BinaryWriter writer = new BinaryWriter(user_fs, Encoding.Unicode);
            foreach (KeyValuePair<string, User> user_pair in user_map)
            {
                User cur_user = user_pair.Value;
                writer.Write(cur_user.Username);
                writer.Write(cur_user.Password);
                writer.Write(cur_user.isBlocked);
                writer.Write(cur_user.hasPasswordRestrictions);
            }
            LogOutput("Изменения сохранены");
            writer.Close();
            user_fs.Close();
        }

        public void ChangePassword(string username, string password)
        {
            user_map[username].Password = password;
            LogOutput("Изменен пароль пользователя " + username);
        }
        public void AddUserData(string username, string password, bool blocked, bool hasRestrictions)
        {

            user_map[username] = new User(username, password, blocked, hasRestrictions);
            LogOutput("Записал информацию о новом пользователе");
        }

        // Read User Data from Binary file
        private void GetUserData()
        {
            FileStream user_fs;
            BinaryReader reader;
            if (!File.Exists(path))
            {
                user_fs = File.Create(path);
                AddUserData("admin", "", false, false);
            }
            else
            {
                user_fs = File.OpenRead(path);
                reader = new BinaryReader(user_fs, Encoding.Unicode);
                int user_count = 0;


                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    User new_user = new User();
                    try {
                        new_user.Username = reader.ReadString();
                        new_user.Password = reader.ReadString();
                        new_user.isBlocked = reader.ReadBoolean();
                        new_user.hasPasswordRestrictions = reader.ReadBoolean();
                    }
                    catch (Exception e)
                    {
                        DialogResult dr = MessageBox.Show("Вызвано исключение: " + e.Message + "\n Возможно, пользовательские данные были повреждены");
                        if (dr == DialogResult.OK)
                        {
                            Close();
                        }
                    }
                    user_map[new_user.Username] = new_user;
                    user_count++;
                }

                LogOutput("Сбор информации о пользователях завершен.\nНайдено пользователей: " + user_count);
                reader.Close();
            }
            user_fs.Close();
        }




        private void InitializeLog()
        {
            if (File.Exists(log_path))
            {
                File.Delete(log_path);
            }
            logStream = new StreamWriter(log_path);
            LogOutput("Лог-файл создан");
        }


        public MainForm()
        {
            InitializeComponent();
            InitializeLog();
            LogOutput("Программа запущена");
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChangePasswordForm changePasswordForm = new ChangePasswordForm(activeUser.Username, activeUser.hasPasswordRestrictions, false, this);
            if (changePasswordForm.ShowDialog() == DialogResult.OK)
            {
                ChangePassword(activeUser.Username, changePasswordForm.new_password);
            }
            changePasswordForm.Dispose();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (saveData) SaveUserData();
            logStream.Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            GetUserData();
            LoginForm loginForm = new LoginForm(this);
            DialogResult loginRes = loginForm.ShowDialog();
            if (loginRes == DialogResult.OK)
            {
                activeUser = user_map[loginForm.active_user_name];
                if (activeUser.Username == "admin")
                {
                    AdminPanelButton.Enabled = true;
                    AdminPanelButton.Visible = true;
                }
            }
            else if (loginRes == DialogResult.Abort)
            {
                Terminate();
            }
            else
            {
                Close();
            }

        }

        private void AdminPanelButton_Click(object sender, EventArgs e)
        {
            AdminPanel adm_p = new AdminPanel(this);
            adm_p.ShowDialog();
        }
        private void ShowAboutForm(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }
    }
}
