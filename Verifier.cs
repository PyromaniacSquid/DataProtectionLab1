using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Verifier : Form
    {
        public Verifier()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = textBox1.Text != "";
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.I4)]
        public static extern int GetKeyboardType(int nTypeFlag);

        private byte[] GetInfo()
        {
            byte[] res;
            // Имя пользователя
            string username = Environment.UserName;
            // Имя компьютера
            string machine_name = Environment.MachineName;
            // Путь к ОС
            string OS_path = Environment.GetEnvironmentVariable("windir");
            // Путь к систмным файлам ОС
            string System32_path = Environment.SystemDirectory;
            // Тип+подтип клавиатуры
            string keyboard_type = GetKeyboardType(0).ToString();
            string keyboard_subtype = GetKeyboardType(1).ToString();
            // Высота экрана
            string screen_height = SystemInformation.PrimaryMonitorSize.Height.ToString();
            // Набор дисковых устройств
            string[] drives = Environment.GetLogicalDrives();
            // Метка тома на который идет установка
            string volume = Directory.GetDirectoryRoot(Directory.GetCurrentDirectory());


            using (var ms = new MemoryStream())
            {
                using (var sw = new StreamWriter(ms))
                {
                    sw.Write(username);
                    sw.Write(machine_name);
                    sw.Write(OS_path);
                    sw.Write(System32_path);
                    sw.Write(keyboard_type);
                    sw.Write(keyboard_subtype);
                    sw.Write(screen_height);
                    foreach (string s in drives)
                        sw.Write(s);
                    sw.Write(volume);
                }
                res = ms.ToArray();
            }
            return res;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            byte[] info = GetInfo();
            var hash = new SHA256CryptoServiceProvider().ComputeHash(info);
            byte[] reg = (byte[])Registry.GetValue("HKEY_CURRENT_USER\\Software\\" + textBox1.Text, "Signature", -1);

            if (reg == null)
            {
                MessageBox.Show("Подпись не найдена", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!reg.SequenceEqual(hash))
            {
                MessageBox.Show("Неверная подпись.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                Close();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
