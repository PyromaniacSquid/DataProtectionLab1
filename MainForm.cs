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
using System.Security.Cryptography;
using System.Text.Json.Serialization;

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

        // Ключ шифрования
        byte[] _key;
        // Случайное значение для ключа
        byte[] _salt;

        // Путь бинарного файла с пользовательскими данными
        string path = "users.dat";
        // Путь текстового лог-файла 
        string log_path = "log.txt";
        // Путь временного файла с расшфированными данными
        private readonly string temp_path = Path.GetTempFileName();


        // Хранилище зарегистрированных пользователей (Хорев говорил БД избыточно использовать)
        public Dictionary<string, User> user_map = new Dictionary<string, User>();

        // Поток записи в лог-файл
        StreamWriter logStream;

        // Флаг сохранения изменений (false, если выход из программы не нес изменений
        // прим. выход во время смены пароля кнопкой "отмена" (в ТЗ прописано что в таком случае либо меняешь пароль, либо завершаешь работу) 
        private bool saveData = true;
        public User activeUser;

        public bool ContainsAdmin()
        {
            return user_map.ContainsKey("admin");
        }

        // Генерация нового ключа шифрования по заданной парольной фразе
        public void GenerateEncryptionKey(string passphrase, bool new_passphrase)
        {
            if (new_passphrase)
            {
                _salt = new byte[32];
                new RNGCryptoServiceProvider().GetBytes(_salt);
            }
            else
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                using (BinaryReader br = new BinaryReader(fs))
                    _salt = br.ReadBytes(32);
            }

            var passphraseBytes = Encoding.Unicode.GetBytes(passphrase);
            var bytes = passphraseBytes.Concat(_salt).ToArray();

            MD5 md5 = MD5.Create();
            var key = md5.ComputeHash(bytes);
            _key = new PasswordDeriveBytes(key, _salt).GetBytes(32);

        }

        // Шифрование данных по ключу
        public byte[] EncryptUserData(byte[] TempFileData, byte[] Key)
        {
            byte[] encrypted_user_data;
            byte[] IV;

            Aes aes = Aes.Create();
            aes.Key = Key;
            aes.GenerateIV();
            IV = aes.IV;
            aes.Mode = CipherMode.CFB;
            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                using (var bw = new BinaryWriter(cs))
                {
                    bw.Write(TempFileData);
                }
                encrypted_user_data = ms.ToArray();
            }
            var combinedIvCt = new byte[IV.Length + encrypted_user_data.Length];
            Array.Copy(IV, 0, combinedIvCt, 0, IV.Length);
            Array.Copy(encrypted_user_data, 0, combinedIvCt, IV.Length, encrypted_user_data.Length);
            return combinedIvCt;
        }

        // Шифрует пользовательские даныне и записывает их в файл
        public void SaveUserDataFile()
        {
            byte[] userData = File.ReadAllBytes(temp_path);
            byte[] encryptedUserData = EncryptUserData(userData, _key);
            using (var fs = new FileStream(path, FileMode.Create))
            using (var bw = new BinaryWriter(fs))
            {
                bw.Write(_salt);
                bw.Write(encryptedUserData);
            }
            LogOutput("Данные внесены в файл");
        }

        // Расшифровка данных по ключу во временный файл
        public byte[] DecryptUserData(byte[] data, byte[] key)
        {
            byte[] decrypted_data;
            Aes aes = Aes.Create();

            aes.Key = key;
            byte[] IV = new byte[aes.BlockSize / 8];
            byte[] cipherText = new byte[data.Length - IV.Length];
            Array.Copy(data, IV, IV.Length);
            Array.Copy(data, IV.Length, cipherText, 0, cipherText.Length);
            aes.IV = IV;
            aes.Mode = CipherMode.CFB;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using (var ms = new MemoryStream(cipherText))
            using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            using (var sr = new BinaryReader(cs))
            {
                //UNSAFE
                decrypted_data = sr.ReadBytes(cipherText.Length);
            }

            return decrypted_data;
        }

        public bool DecryptUserDataFile()
        {
            byte[] allData = File.ReadAllBytes(path);
            byte[] encryptedUserData = new byte[allData.Length-32];
            Array.Copy(allData, _salt, 32);
            Array.Copy(allData, 32, encryptedUserData, 0, allData.Length-32);
            try
            {
                var userData = DecryptUserData(encryptedUserData, _key);
                File.WriteAllBytes(temp_path, userData);
                return true;
            }
            catch (CryptographicException e)
            {
                return false;
            }
            
        }

        // Выход из программы без сохранения (в основном для внешних вызовов из форм)
        public void Terminate()
        {
            saveData = false;
            Close();
        }
        // Вывод в лог (доступен в других формах)
        public void LogOutput(string message)
        {
            using (var logStream = new FileStream(log_path, FileMode.Append))
            using (var logWriter = new StreamWriter(logStream))
                logWriter.WriteLine(DateTime.Now + " " + message);
        }

        // Сохранение пользовательских данных во временный файл
        private void SaveUserData()
        {
            LogOutput("Сохраняю изменения");
            LogOutput("Вношу данные во временный файл");
            File.Delete(temp_path);
            using (var user_fs = File.OpenWrite(temp_path))
                using (BinaryWriter writer = new BinaryWriter(user_fs, Encoding.Unicode))
                {
                    foreach (KeyValuePair<string, User> user_pair in user_map)
                    {
                        User cur_user = user_pair.Value;
                        writer.Write(cur_user.Username);
                        writer.Write(cur_user.Password);
                        writer.Write(cur_user.isBlocked);
                        writer.Write(cur_user.hasPasswordRestrictions);
                    }
                }
                LogOutput("Данные внесены, запускаю шифрование");
            SaveUserDataFile();
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

            if (!File.Exists(path))
            {
                // Первый запуск программы
                using (var fs = File.Create(path))
                //
                AddUserData("admin", "", false, false);
            }
            else
            {
                LogOutput("Дешифрую данные пользователей");
                if (!DecryptUserDataFile())
                {
                    LogOutput("Ошибка дешифровки");
                }
                else
                {
                    LogOutput("Дешифровка закончена");


                    // Сбор пользовательских данных

                    int user_count = 0;

                    using (var fs = File.OpenRead(temp_path))
                    using (var br = new BinaryReader(fs, Encoding.Unicode))
                    {

                        while (br.BaseStream.Position != br.BaseStream.Length)
                        {
                            User new_user = new User();
                            try
                            {
                                new_user.Username = br.ReadString();
                                new_user.Password = br.ReadString();
                                new_user.isBlocked = br.ReadBoolean();
                                new_user.hasPasswordRestrictions = br.ReadBoolean();
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
                    }
                    LogOutput("Сбор информации о пользователях завершен.\nНайдено пользователей: " + user_count);
                }
            }
        }

        // Инициализация лог-файла
        private void InitializeLog()
        {
            if (File.Exists(log_path))
            {
                File.Delete(log_path);
            }
            //logStream = new StreamWriter(log_path);
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
            File.Delete(temp_path);
            //logStream.Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Панель ввода/установки парольной фразы шифрования.
            
            DialogResult AccessDR = new Access(this, !File.Exists(path)).ShowDialog();

            string msg;
            switch (AccessDR)
            {
                case DialogResult.Abort:
                    msg = "Превышен лимит неверного ввода парольной фразы.";
                    break;
                case DialogResult.Cancel:
                    msg = "Парольная фраза не была установлена";
                    break;
                default:
                    msg = File.Exists(path) ? "Парольная фраза введена. В случае неверного ввода программа закончит работу." : "Парольная фраза установлена.";
                    break;
            }
            MessageBox.Show(msg);


            // Выход в случае неуспешной установки парольной фразы.
            if (AccessDR != DialogResult.OK)
            {
                LogOutput("ОШИБКА: " + msg);
                Terminate();
            }
            else
            {

                GetUserData();

                if (!ContainsAdmin())
                {
                    LogOutput("ОШИБКА: Не найден администратор, предполагается ошибка ввода парольной фразы шифрования"); 
                    if (MessageBox.Show("Неверно введена парольная фраза.") == DialogResult.OK)
                    Terminate();
                }
                else
                {
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
