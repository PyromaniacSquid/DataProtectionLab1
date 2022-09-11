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
        
        public ChangePasswordForm(string username = "")
        {
            InitializeComponent();
            UsernameStaticBox.Text = username;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            
        }
    }
}
