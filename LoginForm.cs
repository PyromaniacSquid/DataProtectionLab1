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
        MainForm main;
        
        // Launch Password Setup Form
        private void StartPasswordSetup(string user, bool hasRestrictions)
        {
            // TODO: information popup, change open change password form
            DialogResult dr = MessageBox.Show("Необходимо установить новый пароль, чтобы продолжить.", "Внимание", MessageBoxButtons.OK);
            if (dr == DialogResult.OK)
            {
                // TODO: open change password form
                ChangePasswordForm changePasswordForm1 = new ChangePasswordForm(user, hasRestrictions);
                changePasswordForm1.ShowDialog();
                main.ChangePassword(user, changePasswordForm1.new_password);

            }
        }

        public LoginForm(MainForm mf)
        {
            InitializeComponent();
            main = mf;
            main.LogOutput("Окно авторизации запущено");
            if (main.user_map.Count == 0)
            {
                
                mf.AddUserData("admin", "", false, true);
                StartPasswordSetup("admin", true);
            }
            
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
            string username = UsernameTextBox.Text.ToLower();
            string password = PasswordInput.Text;
            // Check if user exists
            if (main.user_map.ContainsKey(username))
            {
                // Check for an unchanged password
                string real_password = Encoding.Unicode.GetString(main.user_map[username].password);
                real_password = real_password.Remove(real_password.IndexOf('\0'));
                if (real_password == "")
                {
                    StartPasswordSetup(username, false);
                    /*
                    // TODO: information popup, change open change password form
                    DialogResult dr = MessageBox.Show("Необходимо установить пароль, чтобы продолжить.", "Внимание", MessageBoxButtons.OK);
                    if (dr == DialogResult.OK)
                    {
                        // TODO: open change password form
                        ChangePasswordForm changePasswordForm1 = new ChangePasswordForm();
                        changePasswordForm1.Show();
                    }*/
                }
                if (password == real_password)
                {
                    main.LogOutput("User " + username + " authorized successfully\n");
                    MessageBox.Show("Добро пожаловать!");
                    Close();
                    // TODO: proceed doing smth
                }
                else
                {
                    main.LogOutput("User " + username + " failed authorization: wrong password");
                    // TODO: error popup, clean password textbox
                    MessageBox.Show("Неверный пароль");
                }
            }
            else
            {
                main.LogOutput("User " + username + " failed authorization: unknown user");
                MessageBox.Show("Пользователя с указанным именем не существует");
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

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoginButton_Click(sender, e);
            }
        }
    }
}
