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
        int initial_user_count = 0;
        string path = "users.dat";
        string log_path = "log.txt";
        public Dictionary<string, User> user_map = new Dictionary<string, User>();
        StreamWriter logStream;

        public User activeUser;

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
           
            /*
            LogOutput("Открываю бинарный файл с записями");
            FileStream user_fs = File.Open(path, FileMode.Open);
            BinaryReader reader = new BinaryReader(user_fs, Encoding.Unicode);
            LogOutput("Файл открыт");

            string cur_username = reader.ReadString();
            while (cur_username != username)
            {
                // Flush stream
                reader.ReadString(); // we're unsure on length so just skip password string
                reader.BaseStream.Seek(4, SeekOrigin.Current); // skip 4 bytes of boolean variables
                cur_username = reader.ReadString();
            }
            // Now we're in position, start writing
            BinaryWriter writer = new BinaryWriter(user_fs, Encoding.Unicode);
            writer.BaseStream.
            writer.Write(password);
            writer.Close();
            user_fs.Close();
            */
            LogOutput("Изменен пароль пользователя " + username);
        }
        public void AddUserData(string username, string password, bool blocked, bool hasRestrictions)
        {

            user_map[username] = new User(username, password, blocked, hasRestrictions);
            /*
            LogOutput("Открываю бинарный файл с записями");
            FileStream user_fs = File.Open(path, FileMode.Append);
            LogOutput("Файл открыт");

            BinaryWriter writer = new BinaryWriter(user_fs, Encoding.Unicode);
            writer.Write(username);
            writer.Write(password);
            writer.Write(blocked);
            writer.Write(hasRestrictions);
            writer.Close();
            user_fs.Close();
            */
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
                AddUserData("admin", "", false, true);
            }
            else
            {
                user_fs = File.OpenRead(path);
                reader = new BinaryReader(user_fs, Encoding.Unicode);
                int user_count = 0;
                // TODO:: try to make it async

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
            GetUserData();
            LoginForm loginForm = new LoginForm(this);
            loginForm.ShowDialog();
            if (loginForm.active_user_name != "") activeUser = user_map[loginForm.active_user_name];
            else Close();
            /*if (activeUser.Username == "admin")
            {
                AdminPanelButton.Enabled = true;
            }*/
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChangePasswordForm changePasswordForm = new ChangePasswordForm(activeUser.Username, activeUser.hasPasswordRestrictions, false, this);
            changePasswordForm.ShowDialog();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (activeUser != null) SaveUserData();
            logStream.Close();
        }
    }
}
