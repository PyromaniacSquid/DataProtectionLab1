using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
                string letter = "abcdefghijklmnopqrstuvwxyzабвгдежзийклмнопрстуфхцчшщъыьэюя";
                
                Dictionary<string, long> new_user_dict = new Dictionary<string, long>();
                for (int i = 0; i < 58; i++)
                {
                   new_user_dict.Add(Char.ConvertFromUtf32(letter[i]), 0);
                }
                /*
                for (char alpha = 'a'; alpha <= 'z'; alpha++)
                    new_user_dict.Add(alpha, 0);
                for (char alpha = 'а'; alpha <= 'я'; alpha++)
                    new_user_dict.Add(alpha, 0);
                */

                mf.AddUserData(new_username, "", false, true, true, new_user_dict, 0, 0, 12,8, 0.5);
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
            keyboardAuthCheck.Checked = cur_user.hasKeyboardAuthentication;
            StagesTextBox.Text = cur_user.testCount.ToString();
            ChecksTextBox.Text = cur_user.checkCount.ToString();
            LimitTextBox.Text = cur_user.errLim.ToString();


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

        private void keyboardAuthCheck_CheckedChanged(object sender, EventArgs e)
        {
            bool state = keyboardAuthCheck.Checked;
            ChecksTextBox.Enabled = state;
            StagesTextBox.Enabled = state;
            LimitTextBox.Enabled = state;
            checksLabel.Enabled = state;
            stagesLabel.Enabled = state;
            limitLabel.Enabled = state;
            mf.user_map[cur_user.Username].hasKeyboardAuthentication = state;
        }

        private void StagesTextBox_TextChanged(object sender, EventArgs e)
        {
            if (mf.user_map[cur_user.Username].hasKeyboardAuthentication)
            {
                int stages = Convert.ToInt32(StagesTextBox.Text);
                int files_count = Directory.GetFiles("tests\\Eng").Length * 2;
                if (stages < 10)
                {
                    if (MessageBox.Show("Вы пытаетесь установить количество проверок меньше 10." +
                                        "\n Измеряемые данные могут быть необъективными." +
                                        "\nВы уверены, что хотите продолжить?",
                        "Внимание!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        stages = Math.Max(2, stages);
                        StagesTextBox.Text = stages.ToString();
                    }
                    else
                    {
                        stages = 10;
                        StagesTextBox.Text = stages.ToString();
                    }
                }
                else if (stages > files_count)
                {
                    MessageBox.Show("Вы пытаетесь установить количество проверок больше " + files_count +
                    "\n Такое количество проверок не поддерживается.", "Внимание!", MessageBoxButtons.OK);

                    stages = files_count;
                    StagesTextBox.Text = stages.ToString();
                }
                mf.user_map[cur_user.Username].testCount = stages;
            }
        }

        private void StagesTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешить только цифры
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
        }

        private void ChecksTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешить только цифры
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
        }

        private void LimitTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешить только цифры
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            // Одна десят. точка
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
            if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
        }

        private void ChecksTextBox_TextChanged(object sender, EventArgs e)
        {
            if (mf.user_map[cur_user.Username].hasKeyboardAuthentication)
            {
                int stages = Convert.ToInt32(ChecksTextBox.Text);
                int files_count = Directory.GetFiles("auth\\Eng").Length * 2;
                if (stages < 4)
                {
                    if (MessageBox.Show("Вы пытаетесь установить количество проверок меньше 4." +
                                        "\n Измеряемые данные могут быть необъективными." +
                                        "\n Рекомендуется повысить порог непохожести, если вы хотите проводить мало проверок." +
                                        "\nВы уверены, что хотите продолжить?",
                        "Внимание!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        stages = Math.Max(2, stages);
                        ChecksTextBox.Text = stages.ToString();
                    }
                    else
                    {
                        stages = 4;
                        ChecksTextBox.Text = stages.ToString();
                    }
                }
                else if (stages > files_count)
                {
                    MessageBox.Show("Вы пытаетесь установить количество проверок больше " + files_count +
                    "\nДанное количество проверок не поддерживается.", "Внимание!", MessageBoxButtons.OK);
                    stages = files_count;
                    ChecksTextBox.Text = stages.ToString();
                }
                mf.user_map[cur_user.Username].checkCount = stages;
            }
        }

        private void LimitTextBox_TextChanged(object sender, EventArgs e)
        {
            if (mf.user_map[cur_user.Username].hasKeyboardAuthentication)
            {
                double limit = Convert.ToDouble(LimitTextBox.Text);
                if (limit < 0.1)
                {
                    if (MessageBox.Show("Вы пытаетесь установить допустимый уровень непохожести меньше 0.1." +
                                        "\nПри недостаточном количестве данных пользователь не сможет пройти порог" +
                                        "\nВы уверены, что хотите продолжить?",
                        "Внимание!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        limit = Math.Max(0.05, limit);
                        LimitTextBox.Text = limit.ToString();
                    }
                    else
                    {
                        limit = 0.1;
                        LimitTextBox.Text = limit.ToString();
                    }
                }
                else if (limit > 0.5)
                {
                    MessageBox.Show("Вы пытаетесь установить допустимый уровень непохожести больше 0.5" +
                    "\nДанное значение является небезопасным.", "Внимание!", MessageBoxButtons.OK);
                }
                mf.user_map[cur_user.Username].errLim = limit;
            }
        }
    }
}
