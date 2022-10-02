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
using System.Numerics;
using System.Reflection.Emit;

namespace WinFormsApp1
{
    public partial class MainForm : Form
    {
        // Класс пользователя
        public class User
        {
            private string username;
            private string password;
            private bool blocked;
            private bool hasRestrictions;
            public User()
            {
                username = "";
                password = "";
                blocked = false;
                hasRestrictions = true;
            }
            public User(string username, string password, bool blocked, bool hasRestrictions)
            {
                this.username = username;
                this.password = password;
                this.blocked = blocked;
                this.hasRestrictions = hasRestrictions;
            }
            public string Username
            {
                get { return username; }
                set { username = value; }
            }
            public string Password
            {
                get { return password; }
                set { password = value; }
            }
            public bool isBlocked
            {
                get { return blocked; }
                set { blocked = value; }
            }
            public bool hasPasswordRestrictions
            {
                get { return hasRestrictions; }
                set { hasRestrictions = value; }
            }
        }
        // Путь бинарного файла с пользовательскими данными
        string path = "users.dat";
        // Путь текстового лог-файла 
        string log_path = "log.txt";
        // Хранилище зарегистрированных пользователей (Хорев говорил БД избыточно использовать)
        public Dictionary<string, User> user_map = new Dictionary<string, User>();
        
        // Поток записи в лог-файл
        StreamWriter logStream;

        // Флаг сохранения изменений (false, если выход из программы не нес изменений
        // прим. выход во время смены пароля кнопкой "отмена" (в ТЗ прописано что в таком случае либо меняешь пароль, либо завершаешь работу) 
        private bool saveData = true;
        public User activeUser;
        
        // Выход из программы без сохранения (в основном для внешних вызовов из форм)
        public void Terminate()
        {
            saveData = false;
            Close();
        }
        // Вывод в лог (доступен в других формах)
        public void LogOutput(string message)
        {
            logStream.WriteLine(DateTime.Now + " " + message);
        }

        // Сохранение пользовательских данных в файл
        private void SaveUserData()
        {
            LogOutput("Сохраняю изменения");
            File.Delete(path);
            FileStream user_fs = File.Create(path);
            BinaryWriter writer = new BinaryWriter(user_fs, Encoding.Unicode);
            foreach (KeyValuePair<string, User> user_pair in user_map)
            {
                User cur_user = user_pair.Value;
                writer.Write(cur_user.Username);
                writer.Write(cur_user.Password);
                writer.Write(cur_user.isBlocked);
                writer.Write(cur_user.hasPasswordRestrictions);
            }
            LogOutput("Изменения сохранены");
            writer.Close();
            user_fs.Close();
        }
        // Оболочка для интуитивного вызова функции шифровки
        public string PermutationDecryption(string username, string password)
        {
            return PermutationEncryption(username, password, true);
        }


        public string PermutationEncryption (string username, string password, bool decryption = false)
        {   
            string result = "";
         
            // Проверка на длины данных
            if (username.Length > password.Length)
            {
                // Сокращаем ключ до размера пароля
                username = username.Remove(password.Length);
            }
            // Создаем массив индексов перестановки
            List<KeyValuePair<char, int>> indexes = new List<KeyValuePair<char, int>>();
            for (int i = 0; i < username.Length; i++)
            {
                KeyValuePair<char, int> new_pair = new KeyValuePair<char,int>(username[i], i);
                indexes.Add(new_pair);
            }
            // Сортируем по символам имени пользователя
            indexes = indexes.OrderBy(x => x.Key).ToList();

            // Поблочно обрабатываем 
            
            // Текущая буква пароля
            int idx = 0;
            // Длина ключа (имени пользователя)
            int key_length = username.Length;
            while (idx < password.Length)
            {
                string block;
                int chars_left = password.Length - idx;

                // Формируем блок символов
                if (chars_left < key_length)
                {
                    block = password.Substring(idx, chars_left);
                    // Добавить пробелы если последний блок не кратен размеру ключа
                    string addition = new string (' ', key_length - chars_left);
                    block += addition;
                }
                else 
                    block = password.Substring(idx, key_length);


                // Режим расшифровки
                if (decryption)
                {
                    char[] correct_order = new char[key_length];
                    int block_index = 0;
                    

                    foreach (KeyValuePair<char, int> pair in indexes)
                    {
                        correct_order[pair.Value] = block[block_index];
                        block_index++;
                    }
                    for (int i = 0; i < correct_order.Length; i++)
                    {
                        result += correct_order[i];
                    }
                }
                // Шифрование
                else
                { // Записываем элементы блока в результирующее значение по индексу перестановки
                    foreach (KeyValuePair<char, int> pair in indexes)
                    {
                        result += block[pair.Value];
                    }
                }
                idx += key_length;
            }
            if (decryption) return result.TrimEnd();
            return result;
        }
        public string GammaEncryption (string password)
        {
            string result_string = "";
            char[] char_arr = password.ToCharArray(); 

            int G_b = 1;
            for (int i = 0; i < char_arr.Length; i++)
            {
                char c = (char)(char_arr[i] ^ G_b);
                result_string += c;
                G_b = (5 * G_b + 3)%256;
            }
            return result_string;
        }

        public string Decrypt(string username, string password)
        {
            return PermutationDecryption(username, GammaEncryption(password));
        }
        public string Encrypt(string username, string password)
        {
            return GammaEncryption(PermutationEncryption(username, password));
        }

        // Изменение пароля (вызывается в ChangePasswordForm)
        public void ChangePassword(string username, string password)
        {
            // Запускаем шифрование
            user_map[username].Password = Encrypt(username, password);
            LogOutput("Изменен пароль пользователя " + username);
            LogOutput("Пароль до шифрования " + password);
            LogOutput("Пароль после шифрования " + user_map[username].Password);
        }
        
        // Добавление нового пользователя
        public void AddUserData(string username, string password, bool blocked, bool hasRestrictions)
        {
            user_map[username] = new User(username, password, blocked, hasRestrictions);
            LogOutput("Записал информацию о новом пользователе " + username);
        }

        // Чтение пользовательских данных из бинарного файла
        private void GetUserData()
        {
            FileStream user_fs;
            BinaryReader reader;

            if (!File.Exists(path))
            {
                // Первый запуск программы
                user_fs = File.Create(path);
                AddUserData("admin", "", false, false);
            }
            else
            {
                // Сбор пользовательских данных
                user_fs = File.OpenRead(path);
                reader = new BinaryReader(user_fs, Encoding.Unicode);
                int user_count = 0;


                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    User new_user = new User();
                    try {
                        new_user.Username = reader.ReadString();
                        new_user.Password = reader.ReadString();                        
                        new_user.isBlocked = reader.ReadBoolean();
                        new_user.hasPasswordRestrictions = reader.ReadBoolean();
                    }
                    catch (Exception e)
                    {
                        DialogResult dr = MessageBox.Show("Вызвано исключение: " + e.Message + "\n Возможно, пользовательские данные были повреждены");
                        if (dr == DialogResult.OK)
                        {
                            Close();
                        }
                    }
                    user_map[new_user.Username] = new_user;
                    user_count++;
                }

                LogOutput("Сбор информации о пользователях завершен.\nНайдено пользователей: " + user_count);
                reader.Close();
            }
            user_fs.Close();
        }

        // Инициализация лог-файла
        private void InitializeLog()
        {
            if (File.Exists(log_path))
            {
                File.Delete(log_path);
            }
            logStream = new StreamWriter(log_path);
            LogOutput("Лог-файл создан");
        }

        public MainForm()
        {
            InitializeComponent();
            InitializeLog();
            pictureBox1.Image = pictureBox1.InitialImage;
            LogOutput("Программа запущена");
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        // Кнопка "Завершение работы"
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Кнопка "Сменить пароль"
        private void button1_Click(object sender, EventArgs e)
        {
            ChangePasswordForm changePasswordForm = new ChangePasswordForm(activeUser.Username, activeUser.hasPasswordRestrictions, false, this);
            // Проверка на выход с сохранением
            if (changePasswordForm.ShowDialog() == DialogResult.OK)
            {
                ChangePassword(activeUser.Username, changePasswordForm.new_password);
            }
            changePasswordForm.Dispose();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (saveData) SaveUserData();
            logStream.Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            GetUserData();
            // Открытие формы входа
            LoginForm loginForm = new LoginForm(this);
            DialogResult loginRes = loginForm.ShowDialog();
            
            // Вход успешен
            if (loginRes == DialogResult.OK)
            {
                activeUser = user_map[loginForm.active_user_name];
                if (activeUser.Username == "admin")
                {
                    AdminPanelButton.Enabled = true;
                    AdminPanelButton.Visible = true;
                }
            }
            // Отмена при установке пароля/трехкратная ошибка в пароле
            else if (loginRes == DialogResult.Abort)
            {
                LogOutput("Пользователь прервал вход или достигнуто три ошибки ввода пароля");
                Terminate();
            }
            // Отказ от входа 
            else
            {
                // Пароль мог быть установлен, сохраняем изменения
                LogOutput("Пользователь закрыл программу, не войдя в профиль");
                Close();
            }

        }

        // Панель администратора
        private void AdminPanelButton_Click(object sender, EventArgs e)
        {
            AdminPanel adm_p = new AdminPanel(this);
            adm_p.ShowDialog();
        }
        // Окно "О программе"
        private void ShowAboutForm(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
