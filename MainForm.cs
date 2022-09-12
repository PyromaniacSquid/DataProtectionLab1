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
            public byte[] username;
            public byte[] password;
            public bool blocked;
            public bool hasRestrictions;
            public User()
            {
                username = new byte[128];
                password = new byte[128];
                blocked = false;
                hasRestrictions = true;
            }
            public User(string username, string password, bool blocked, bool hasRestrictions)
            {
                this.username = Encoding.Unicode.GetBytes(username);
                this.password = Encoding.Unicode.GetBytes(password);
                this.blocked = blocked;
                this.hasRestrictions = hasRestrictions;
            }
        }
        int initial_user_count = 0;
        string path = "users.dat";
        string log_path = "log.txt";
        public Dictionary<string, User> user_map = new Dictionary<string, User>();
        StreamWriter logStream;

        User activeUser;

        public void LogOutput(string message)
        {
            logStream.WriteLine(DateTime.Now + " " + message);
        }
        
        byte[] SafeRead(FileStream fs, int offset, int len)
        {
            byte[] buffer = new byte[128];
            int bytes_read = fs.Read(buffer, offset, 128);
            if (bytes_read == 0)
            {
                LogOutput("Ничего не считано, т.к. недостаточно байт в источнике");
            }
            return buffer;
        }


        public void ChangePassword(string user, string password)
        {
            User cur_user = user_map[user];
            cur_user.password = new byte[128];
            Encoding.Unicode.GetBytes(password).CopyTo(cur_user.password, 0);
            //cur_user.password = Encoding.Unicode.GetBytes(password);
            user_map[user] = cur_user;

            FileStream user_fs = File.Open(path, FileMode.Open);
            // Search in binary file the location of user's password
            // Since every record is 128+128+2+2 = 260 bytes, check first 128 and jump 260 ahead if it's not the one;
            byte[] buffer = new byte[128];
            int offset = 0;
            user_fs.Read(buffer, 0, 128);
            string username = Encoding.Unicode.GetString(buffer);
            username = username.Remove(username.IndexOf('\0'));
            while (username != user)
            {
                user_fs.Seek(132, SeekOrigin.Current);
                user_fs.Read(buffer, 0, 128);
            }
            user_fs.Write(Encoding.Unicode.GetBytes(password), 0, 128);
            user_fs.Close();
        }

        // Read User Data from Binary file
        private void GetUserData()
        {
            FileStream user_fs;
            if (!File.Exists(path))
            {
                user_fs = File.Create(path);
            }
            else
            {
                user_fs = File.OpenRead(path);

                int user_count = 0;
                // TODO:: try to make it async

                while (user_fs.Length != user_fs.Position)
                {
                    int offset = 0;
                    // Setup
                    byte[] buffer = new byte[128];
                   
                    User new_user = new User();
                    // Read 128 bytes of username (64 unicode chars)
                    user_fs.Read(buffer, 0, 128);
                    //offset += 128;
                    buffer.CopyTo(new_user.username,0);
                    //new_user.username = buffer;
                    // Read 128 bytes of password (64 unicode chars)
                    user_fs.Read(buffer, 0, 128);
                    //offset += 128;
                    buffer.CopyTo(new_user.password,0);
                    //new_user.password = buffer;
                    // Read 2 bytes for block status
                    user_fs.Read(buffer, 0, 2);
                    //offset += 2;
                    new_user.blocked = BitConverter.ToBoolean(buffer);
                    // Read 2 bytes for restriction status
                    user_fs.Read(buffer, 0, 2);
                    //offset += 2;
                    new_user.hasRestrictions = BitConverter.ToBoolean(buffer);

                    string username_s = Encoding.Unicode.GetString(new_user.username);
                    username_s = username_s.Remove(username_s.IndexOf('\0'));
                    user_map[username_s] = new_user;
                    user_count++;
                    //user_fs.Seek(offset, SeekOrigin.Begin);
                }
                LogOutput("Сбор информации о пользователях завершен.\nНайдено пользователей: " + user_count);
               
            }
            user_fs.Close();
        }

        public void AddUserData(string username, string password, bool blocked, bool hasRestrictions)
        {

            user_map[username] = new User(username, password, blocked, hasRestrictions); 
            LogOutput("Открываю бинарный файл с записями");
            FileStream user_fs = File.Open(path, FileMode.Open);
            LogOutput("Файл открыт");
            byte[] buffer = new byte[128];

            Encoding.Unicode.GetBytes(username).CopyTo(buffer, 0);
            user_fs.Write(buffer, 0, 128);
            //user_fs.Write(Encoding.Unicode.GetBytes(username), 0, 128);
            //user_fs.Seek(128, SeekOrigin.Current);

            Encoding.Unicode.GetBytes(password).CopyTo(buffer, 0);
            user_fs.Write(buffer, 0, 128);
            //user_fs.Write(Encoding.Unicode.GetBytes(password), 0, 128);
            //user_fs.Seek(128, SeekOrigin.Current);

            BitConverter.GetBytes(blocked).CopyTo(buffer, 0);
            user_fs.Write(buffer, 0, 2);
            //user_fs.Write(buffer, 0, 128); user_fs.Write(BitConverter.GetBytes(blocked), 0, 2);
            //user_fs.Seek(2, SeekOrigin.Current);

            BitConverter.GetBytes(hasRestrictions).CopyTo(buffer, 0);
            user_fs.Write(buffer, 0, 2);
            //user_fs.Write(BitConverter.GetBytes(hasRestrictions), 0, 2);
            user_fs.Close();
            LogOutput("Записал информацию о новом пользователе");
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

            if (BitConverter.ToString(activeUser.username) == "admin")
            {
                AdminPanelButton.Enabled = true;
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            logStream.Close();
        }
    }
}
