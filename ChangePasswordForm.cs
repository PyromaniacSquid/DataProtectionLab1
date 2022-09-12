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
        public ChangePasswordForm(string username = "", bool hasRestrictions = false)
        {
            InitializeComponent();
            TextLabel.Text = "Пожалуйста, установите новый пароль для пользователя " + username;
            this.hasRestrictions = hasRestrictions;
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
                // TODO:: check password suits the restrictions
            }
            if (NewPasswordInput.Text != ConfirmPasswordInput.Text) return 2;
            return 0;
        }
        private void SaveButton_Click(object sender, EventArgs e)
        {
            switch (CheckRestrictions())
            {
                case 1:
                    if (MessageBox.Show("Пароль должен содержать ...") == DialogResult.OK)
                    {
                        NewPasswordInput.Text = "";
                        ConfirmPasswordInput.Text = "";
                    }
                    break;
                case 2:
                    if (MessageBox.Show("Пароли не совпадают. Попробуйте ещё раз") == DialogResult.OK)
                    {
                        NewPasswordInput.Text = "";
                        ConfirmPasswordInput.Text = "";
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
