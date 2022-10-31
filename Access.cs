using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Access : Form
    {
        MainForm mf;
        bool firstLaunch = false;
        int attempts = 3;
        bool success = false;
        public Access(MainForm mf, bool firstLaunch = false)
        {
            this.mf = mf;
            this.firstLaunch = firstLaunch;
            InitializeComponent();
            if (!firstLaunch) {
                WelcomeLabel.Text = "Для начала работы введите парольную фразу";
                PasswordConfirmInput.Enabled = false;
                PasswordConfirmInput.Visible= false;
                PasswordConfirmLabel.Visible = false;
                OKButton.Text = "Получить доступ";
            }
        }

        private bool AssertPasswordsAreSame()
        {
            if (PasswordConfirmInput.Text != PasswordInput.Text)
            {
                if (MessageBox.Show("Парольные фразы не совпадают") == DialogResult.OK)
                {
                    attempts--;
                    PasswordInput.Text = "";
                    PasswordConfirmInput.Text = "";
                }
                return false;
            }
            else return true;

        }
        private void OKButton_Click(object sender, EventArgs e)
        {
            string passphrase = PasswordInput.Text;
            if (firstLaunch)
            {
                if (AssertPasswordsAreSame())
                {
                    if (MessageBox.Show("Парольная фраза успешно установлена") == DialogResult.OK)
                    {
                        // SET NEW PASSWORD FOR KEY GENERATION HERE...
                        mf.GenerateEncryptionKey(passphrase, true);
                        this.DialogResult = DialogResult.OK;
                        success = true;
                        Close();
                    } 
                }
                else
                {
                    if (attempts == 0)
                    {
                        this.DialogResult = DialogResult.Abort;
                        Close();
                    }
                }
            }
            else
            {
                // CHECK DECODE...
                mf.GenerateEncryptionKey(passphrase, false);
                this.DialogResult = DialogResult.OK;
                success = true;
                Close();
            }
        }

        private void Access_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!success && this.DialogResult != DialogResult.Abort)
                this.DialogResult = DialogResult.Cancel;
        }

        private void PasswordInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) OKButton_Click(sender, e);
        }

        private void PasswordConfirmInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) OKButton_Click(sender, e);
        }
    }
}
