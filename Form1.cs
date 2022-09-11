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
    public partial class LoginForm : Form
    {
        string path = "users.dat";
        string log_path = "log.txt";
        Dictionary<string, string> user_map = new Dictionary<string, string>();
        StreamWriter logStream;
        private void GetUserData()
        {
            FileStream user_fs;
            if (!File.Exists(path))
            {
                user_fs = File.Create(path);
            }
            else user_fs = File.OpenRead(path);
            BinaryReader reader = new BinaryReader(user_fs);
            string username; string password;
            while (user_fs.Length != user_fs.Position)
            {
                username = reader.ReadString();
                password = reader.ReadString();
                user_map[username] = password;
            }
            
        }
        public LoginForm()
        {
            if (File.Exists(log_path))
            {
                File.Delete(log_path);
            }
            //File.CreateText(log_path);
            logStream = new StreamWriter(log_path);
            logStream.WriteLine("Программа запущена");

            InitializeComponent();
            GetUserData();
            logStream.WriteLine("Сбор информации о пользователях завершен.");
            logStream.WriteLine("Найдено пользователей: " + user_map.Count);
            
        }

        private void UsernameLabel_Click(object sender, EventArgs e)
        {

        }

        private void PasswordLabel_Click(object sender, EventArgs e)
        {

        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            // Get username and password from textboxes
            string username = UsernameInput.Text;
            string password = PasswordInput.Text;
            // Check if user exists
            if (user_map.ContainsKey(username))
            {
                // Check for an unchanged password
                if (user_map[username] == "")
                {
                    // TODO: information popup, change open change password form
                    DialogResult dr = MessageBox.Show("Необходимо сменить пароль, чтобы продолжить.", "Внимание", MessageBoxButtons.OK);
                    if (dr == DialogResult.OK)
                    {
                        // TODO: open change password form
                        ChangePasswordForm changePasswordForm1 = new ChangePasswordForm();
                        changePasswordForm1.Show();
                    }
                }
                if (password == user_map[username])
                {
                    logStream.Write("User " + username + " authorized successfully\n");
                    // TODO: proceed doing smth
                }
                else
                {
                    logStream.Write("User " + username + " failed authorization: wrong password");
                    // TODO: error popup, clean password textbox
                }
            }
            else
            {
                logStream.Write("User " + username + " failed authorization: unknown user");
                // TODO: error popup, ask to contact admin for user creation
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void UsernameInput_TextChanged(object sender, EventArgs e)
        {

        }

        private void PasswordInput_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
