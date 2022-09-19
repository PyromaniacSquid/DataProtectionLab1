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
        // Объект основной формы
        MainForm mf;
        string current_user;
        MainForm.User cur_user;
        public AdminPanel(MainForm mf = null)
        {
            InitializeComponent();
            this.mf = mf;
        }

        // Открытие формы
        private void AdminPanel_Load(object sender, EventArgs e)
        {
            // Заполнение выпадающего списка 
            foreach (KeyValuePair<string, MainForm.User> pair in mf.user_map)
            {
                UserListBox.Items.Add(pair.Key); // Можно было использовать DataSource (или как там), но я забыл про него
            }
            current_user = mf.activeUser.Username;
            UserListBox.SelectedItem = current_user;
        }

        // Добавление нового пользователя
        private void button1_Click(object sender, EventArgs e)
        {
            string new_username = newUserBox.Text.ToLower();
            if (new_username == "") MessageBox.Show("Недопустимое имя пользователя"); // ТЗ не указывал ограничения на имя пользователя, но пустой это уже перебор
            else if (mf.user_map.ContainsKey(new_username))
                MessageBox.Show("Пользователь с таким именем уже существует");
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

        // Обновление значений в элементах интерфейса
        private void UserListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            cur_user = mf.user_map[UserListBox.SelectedItem.ToString()];
            BlockedState.Checked = cur_user.isBlocked;
            PWRestrictionsState.Checked = cur_user.hasPasswordRestrictions;

            DeleteUserButton.Enabled = cur_user.Username != "admin";    // Админа нельзя удалить
            BlockedState.Enabled = cur_user.Username != "admin";        // Заблокировать тоже нельзя
                                                                        // А снять ограничения с пароля можно, разрешаю даже дописать это сюда
        }

        // Удаление пользователя
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

        private void newUserBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }
    }
}
