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
        public string active_user_name = "";
        private int attempts = 3;
        // Launch Password Setup Form
        private void StartPasswordSetup(string user, bool hasRestrictions)
        {
            // TODO: information popup, change open change password form
            DialogResult dr = MessageBox.Show("Необходимо установить новый пароль, чтобы продолжить.", "Внимание", MessageBoxButtons.OK);
            if (dr == DialogResult.OK)
            {
                // TODO: open change password form
                ChangePasswordForm changePasswordForm1 = new ChangePasswordForm(user, hasRestrictions, true, main);
                if(changePasswordForm1.ShowDialog() == DialogResult.;
                main.ChangePassword(user, changePasswordForm1.new_password);

            }
        }
        private void Reset()
        {
            PasswordInput.Text = "";
        }

        public LoginForm(MainForm mf)
        {
            InitializeComponent();
            main = mf;
            main.LogOutput("Окно авторизации запущено");
            
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
                string real_password = main.user_map[username].Password;
                if (real_password == "")
                {
                    StartPasswordSetup(username, false);
                }
                else if (password == real_password)
                {
                    main.LogOutput("User " + username + " authorized successfully\n");
                    MessageBox.Show("Добро пожаловать!");
                    active_user_name = username;
                    Close();
                }
                else
                {
                    main.LogOutput("User " + username + " failed authorization: wrong password");
                    if (attempts == 1)
                    {
                        if (MessageBox.Show("Исчерпан лимит на количество попыток входа") == DialogResult.OK)
                        {
                            Close();
                            main.Close();
                        }
                    }
                    else
                    {
                        attempts--;
                        MessageBox.Show("Неверный пароль, осталось попыток:" + attempts);
                        Reset();
                    }
                }
            }
            else
            {
                main.LogOutput("User " + username + " failed authorization: unknown user");
                MessageBox.Show("Пользователя с указанным именем не существует");
                Reset();
                UsernameTextBox.Text = "";
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
