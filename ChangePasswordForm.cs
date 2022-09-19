using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class ChangePasswordForm : Form
    {
        public string new_password;
        private bool hasRestrictions;
        private bool new_user;
        private MainForm mf;
        private string username;
        public ChangePasswordForm(string username = "", bool hasRestrictions = false, bool new_user = false, MainForm mf = null)
        {
            InitializeComponent();
            this.username = username;
            this.hasRestrictions = hasRestrictions;
            this.mf = mf; 
            this.new_user = new_user;
            TextLabel.Text = "Пожалуйста, установите новый пароль для пользователя " + username;
            OldPasswordBox.Enabled = !new_user;
            OldPasswordLabel.Enabled = !new_user;
            
        }

        private void Reset()
        {
            OldPasswordBox.Text = "";
            NewPasswordInput.Text = "";
            ConfirmPasswordInput.Text = "";
        }

        // Check Password restrictions and confirm password
        // Codes: 1 - password doesn't match restrictions
        //        2 - passwords do not match
        //        0 - everything is fine
        private int CheckRestrictions()
        {
            // Restrictions Check
            if (hasRestrictions)
            {
                bool latin = false;
                bool cyrillic = false;
                bool operators = false;
                string password = NewPasswordInput.Text.ToLower();
                for (int i = 0; i < password.Length; i++)
                {
                    latin = latin || (password[i] >= 'a' && password[i] <= 'z');
                    cyrillic = cyrillic || (password[i] >= 'а' && password[i] <= 'я');
                    operators = operators || (password[i] == '+' || password[i] == '-' || password[i] == '/' || password[i] == '*');
                }
                if (!(latin && cyrillic && operators)) return 1;
            }
            if (NewPasswordInput.Text != ConfirmPasswordInput.Text) return 2;
            return 0;
        }
        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (!new_user)
            {
                // Assert old password is correct
                if (OldPasswordBox.Text != mf.user_map[username].Password)
                {
                    if (MessageBox.Show("Старый пароль введен неверно") == DialogResult.OK)            
                        return;
                }
            }
            switch (CheckRestrictions())
            {
                case 1:
                    if (MessageBox.Show("Пароль должен содержать латинские буквы, символы кириллицы и знаки арифметических операций") == DialogResult.OK)
                    {
                        Reset();
                    }
                    break;
                case 2:
                    if (MessageBox.Show("Пароли не совпадают. Попробуйте ещё раз") == DialogResult.OK)
                    {
                        Reset();
                    }
                    break;
                default:
                    new_password = NewPasswordInput.Text;
                    if (MessageBox.Show("Пароль успешно изменён") == DialogResult.OK) Close();
                    break;
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            new_password = "";
            Close();
        }
    }
}
