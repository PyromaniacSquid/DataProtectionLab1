using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class AdminPanel : Form
    {
        MainForm mf;
        string current_user;
        MainForm.User cur_user;
        public AdminPanel(MainForm mf = null)
        {
            InitializeComponent();
            this.mf = mf;
        }

        private void AdminPanel_Load(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, MainForm.User> pair in mf.user_map)
            {
                UserListBox.Items.Add(pair.Key);
            }
            current_user = mf.activeUser.Username;
            UserListBox.SelectedItem = current_user;
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string new_username = newUserBox.Text;
            if (new_username == "" || mf.user_map.ContainsKey(new_username))
            {
                MessageBox.Show("Пользователь с таким именем уже существует");
            }
            else
            {
                mf.AddUserData(new_username, "", false, true);
                UserListBox.Items.Add(new_username);
                UserListBox.SelectedItem = new_username;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            mf.user_map[cur_user.Username].isBlocked = BlockedState.Checked;
        }

        private void UserListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            cur_user = mf.user_map[UserListBox.SelectedItem.ToString()];
            BlockedState.Checked = cur_user.isBlocked;
            PWRestrictionsState.Checked = cur_user.hasPasswordRestrictions;
            DeleteUserButton.Enabled = cur_user.Username != "admin";
        }

        private void DeleteUserButton_Click(object sender, EventArgs e)
        {
            string username = UserListBox.SelectedItem.ToString();
            if (username != "admin")
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить пользователя?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    mf.user_map.Remove(username);
                    UserListBox.Items.Remove(username);
                    UserListBox.SelectedIndex = 0;
                } 
            }
        }

        private void PWRestrictionsState_CheckedChanged(object sender, EventArgs e)
        {
            mf.user_map[cur_user.Username].hasPasswordRestrictions = PWRestrictionsState.Checked;
        }
    }
}
