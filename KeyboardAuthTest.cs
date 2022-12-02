using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Windows.Forms;
using static WinFormsApp1.MainForm;
//using WinFormsApp1;

namespace WinFormsApp1
{
    public partial class KeyboardAuthTest : Form
    {
        // Пути к панграммам
        private string path_to_tests = "tests";
        private string path_to_auth = "auth";
        private string[] paths;

        // Данные пользователя
        private string user_name;
        private MainForm mf;
        private bool authMode = false;

        bool inTest = false;
        bool inputErr = false;

        int test_id = 0;
        int completed_tests = 0;
        int test_text_idx = 0;
        // Время на каждый тест (мс)
        long[] test_ts;
        long test_end_time = 0;
        int err_count = 0;

        // Используем списки, т.к. нажатие следующей кнопки может наступить раньше отжатия предыдущей

        List<Tuple<KeyEventArgs, long>> KeyDownNotes;
        List<Tuple<KeyEventArgs, long>> KeyUpNotes;
        List<Tuple<KeyPressEventArgs, long>> KeyPressNotes;
        private class KeyMetrics
        {
            public long press_time = 0;
            public long release_time = 0;
            public string character = "_";
            public KeyMetrics(string character, long press_time = 0, long release_t = 0)
            {
                this.character = character;
                this.press_time = press_time;
                this.release_t = release_t;
            }
            public KeyMetrics(long press_time)
            {
                this.press_time = press_time;
                character = "_";
                release_time = 0;
            }
            public KeyMetrics(string character)
            {
                this.character = character;
                press_time = 0;
                release_time = 0;
            }
            public long press_t {
                get { return press_time; }
                set { press_time = value; }
            }
            public long release_t
            {
                get { return release_time; }
                set { release_time = value; }
            }
            public string c
            {
                get { return character; }
                set { character = value; }
            }
        }
        private List<KeyMetrics> KeyMetricsData;

        bool isCyrOrLatin(char character)
        {
            return (
                ((character >= 'а' && character <= 'я') ||
                (character >= 'А' && character <= 'Я') ||
                (character >= 'a' && character <= 'z') ||
                (character >= 'A' && character <= 'Z') ||
                (Char.IsWhiteSpace(character))) &&
                character != '\n' && character != '\r'
                );
        }
        // Сохраняет/обновляет данные эталона 
        public void ExportMetrics()
        {
            // Вытащим значения пользователя 
            MainForm.User user = mf.user_map[user_name];

            // Инициализируем необходимые переменные
            long test_timespan = 0;
            long total_char_count = 0;
            double avg_speed = 0;
            int start_idx = 0;

            for (int i = 0; i < completed_tests; i++)
            {
                int local_char_count = (int)File.OpenText(paths[i]).ReadToEnd().Length;
                // Подсчет всех букв
                total_char_count += local_char_count;
                test_timespan += test_ts[i];
                // Подсчет средней скорости печати по тестам
                avg_speed += 1 / (double)completed_tests * (KeyUpNotes[start_idx + local_char_count-1].Item2 - KeyDownNotes[start_idx].Item2) / (double)local_char_count;
                start_idx += local_char_count;
            }
            
            // Средняя скорость набора
            user.avgSpeed = user.avgSpeed == 0 ?
                avg_speed :
                // Обновляем старые данные, если таковые имеются
                (0.8 * mf.user_map[user_name].avgSpeed + 0.2 * avg_speed);

            // Доля ошибок
            float rel_err_count = (float)err_count / (float)total_char_count;
            user.avgErrCount = user.avgErrCount == 0 ?
                rel_err_count :
                // Обновляем старые данные, если таковые имеются
                (0.8f * user.avgErrCount + 0.2f * rel_err_count);

            KeyMetricsData = new List<KeyMetrics>();
            // Объединяем все события
            for (int i = 0; i < Math.Min(KeyPressNotes.Count, KeyUpNotes.Count); i++)
            {
                long press_time = KeyDownNotes[i].Item2;
                long release_time = KeyUpNotes[i].Item2;
                string character = KeyPressNotes[i].Item1.KeyChar.ToString().ToLower();
                if (isCyrOrLatin(character[0]) && !Char.IsWhiteSpace(character[0]))
                    KeyMetricsData.Add(new KeyMetrics(character, press_time, release_time));
            }
     
            // Фиксируем новые метрики
            while (KeyMetricsData.Count > 0)
            {
                KeyMetrics data = KeyMetricsData.First();
                // Выборка по текущей букве
                var char_data = KeyMetricsData.Where(x => x.character == data.character && x.press_t<x.release_t);
                // Находим среднее по новым данным
                int ch_count = char_data.Count();
                if (ch_count > 0)
                {
                    long timespans_sum = char_data.Sum(x => (x.release_t - x.press_t));
                    long avgTime = timespans_sum / ch_count;
                    // Фиксируем данные пользователя
                    user.KeyTimeData[data.character] = user.KeyTimeData[data.character] == 0 ?
                        avgTime :
                        // Обновляем старые данные, если таковые имеются
                        (long)(0.8 * user.KeyTimeData[data.character] + 0.2 * avgTime);
                }
                // Удаляем из списка текущую выборку
                KeyMetricsData.RemoveAll(x => x.character == data.character);

            }
            // Фиксируем изменения
            mf.user_map[user_name] = user;
        }
        // Выбирает заданное администратором количество текстов для сбора метрик клавиатурного почерка
        bool LoadSamples(string path, int count)
        {
            // Убедимся, что директории существуют
            if (Directory.Exists(path) && Directory.Exists(path + "\\Eng") && Directory.Exists(path + "\\Rus"))
            {
                // Получаем названия файлов латиницы и кириллицы
                string[] eng_paths = Directory.GetFiles(path + "\\Eng");
                string[] rus_paths = Directory.GetFiles(path + "\\Rus");

                if (eng_paths.Length == 0 || rus_paths.Length == 0) return false;

                Random random = new Random();
                // Глобальная переменная, в которой хранятся пути к текстам
                paths = new string[count];
                // Глобальная переменная, в которой хранятся длительности тестов
                test_ts = new long[count];
                // Текстовый лейбл на форме
                StageLabel.Text = "Этап " + (test_id + 1).ToString() + "/" + count;
                // Случайно выбираем английские панграммы
                {
                    List<int> chosen_tests_indexes_eng = new List<int>();
                    int i = 0;
                    while (i < count / 2)
                    {
                        int index = random.Next(0, eng_paths.Length);
                        if (!chosen_tests_indexes_eng.Contains(index))
                        {
                            chosen_tests_indexes_eng.Add(index);
                            paths[i] = eng_paths[index];
                            i++;
                        }
                    }
                }
                // Случайно выбираем русские панграммы
                {
                    List<int> chosen_tests_indexes_rus = new List<int>();
                    int i = count / 2;
                    while (i < count)
                    {
                        int index = random.Next(0, rus_paths.Length);
                        if (!chosen_tests_indexes_rus.Contains(index))
                        {
                            chosen_tests_indexes_rus.Add(index);
                            paths[i] = rus_paths[index];
                            i++;
                        }
                    }
                }
                return true;
            }
            else return false;
        }

        public KeyboardAuthTest(MainForm mf, string user, bool auth)
        {
            InitializeComponent();
            authMode = auth;
            this.mf = mf;
            this.user_name = user;
            UserInput.ReadOnly = inTest;
            FinishTest.Enabled = false;

            SampleText.Text = "Система ожидает начала нового тестирования.";
            // Инициализация списков с событиями клавиатуры
            KeyDownNotes = new List<Tuple<KeyEventArgs, long>>();
            KeyUpNotes = new List<Tuple<KeyEventArgs, long>>();
            KeyPressNotes = new List<Tuple<KeyPressEventArgs, long>>();

            // Загрузка файлов с текстом
            string directory = authMode ? path_to_auth : path_to_tests;
            int texts_count = authMode ? mf.user_map[user].checkCount : mf.user_map[user].testCount;
            if (!LoadSamples(directory, texts_count)) {
                MessageBox.Show("Не найдены файлы с текстом для проверки.", "Ошибка");
                Close();
            }
            MessageBox.Show("Сейчас вы будете печатать текст на английском языке.\nУбедитесь, что вы сменили раскладку клавиатуры, и начинайте тестирование.", "Внимание!");
        }
        // Подсчитывает отклонение от эталона при аутентификации
        private double AssertMetrics()
        {
            // Вытащим значения пользователя 
            MainForm.User user = mf.user_map[user_name];

            // Инициализируем необходимые переменные
            long test_timespan = 0;
            long total_char_count = 0;
            double avg_speed = 0;
            int start_idx = 0;

            for (int i = 0; i < completed_tests; i++)
            {
                int local_char_count = (int)File.OpenText(paths[i]).ReadToEnd().Length;
                // Подсчет всех букв
                total_char_count += local_char_count;
                test_timespan += test_ts[i];
                // Подсчет средней скорости печати по тестам
                avg_speed += 1 / (double)completed_tests * (KeyUpNotes[start_idx + local_char_count-1].Item2 - KeyDownNotes[start_idx].Item2) / (double)local_char_count;
                start_idx += local_char_count;
            }

            // Компонента скорости набора - относительная погрешность
            double avgSpeedPart = Math.Abs(user.avgSpeed - avg_speed)/user.avgSpeed;
 
            // Доля ошибок в введенных текстах
            float rel_err_count = (float)err_count / (float)total_char_count;
            // Компонента ошибок - абсолютная погрешность
            // Если пользователь не ошибался в тестах на формирование эталона, используем текущие значения как компоненту
            float avgErrPart = user.avgErrCount > 0 ?
                Math.Abs(user.avgErrCount - rel_err_count) : rel_err_count;

            // Подсчет компоненты времени нажатия клавиш

            // Объединяем все события и заполняем данные метрик на каждый символ
            KeyMetricsData = new List<KeyMetrics>();
            for (int i = 0; i < Math.Min(KeyPressNotes.Count,KeyUpNotes.Count); i++)
            {
                long press_time = KeyPressNotes[i].Item2;
                long release_time = KeyUpNotes[i].Item2;
                string character = KeyPressNotes[i].Item1.KeyChar.ToString().ToLower();
                if (isCyrOrLatin(character[0]) && !Char.IsWhiteSpace(character[0]))
                    KeyMetricsData.Add(new KeyMetrics(character, press_time, release_time));
            }
            
            // Компонента метрик
            double metricComponent = 0;
            List<double> metricComponents = new List<double>();
            while (KeyMetricsData.Count > 0)
            {
                KeyMetrics data = KeyMetricsData.First();
                // Выборка по текущей букве
                var char_data = KeyMetricsData.Where(x => x.character == data.character && x.press_t < x.release_t);
               
                // Находим среднее по новым данным
                int ch_count = char_data.Count();
                if (ch_count > 0)
                {
                    long realAvg = user.KeyTimeData[data.character];
                    double ts_sum = 0;
                    foreach (KeyMetrics keyMetrics in char_data)
                    {
                        ts_sum += Math.Pow((keyMetrics.release_t - keyMetrics.press_t - realAvg), 2);
                    }
                    // корень Суммы квадратов / количество = СКО
                    ts_sum = Math.Sqrt(ts_sum) / ch_count;
                    // Добавляем весовой коэффициент к значениям, которые имеют слишком мало данных
                    //double modifier = 0.2 / ch_count;
                    //ts_sum *= (1 - modifier);
                    metricComponents.Add(ts_sum);
                }
                // Удаляем из списка текущую выборку
                KeyMetricsData.RemoveAll(x => x.character == data.character);
            }
            // Смотрим среднее из СКО
            metricComponent = metricComponents.Sum() / metricComponents.Count;
            // Находим среднее по эталону
            double avgMetric = user.KeyTimeData.Average(x => x.Value);
            // Относительная погрешность
            double relMetricComponent = metricComponent / avgMetric;
            // Весовой коэффициент
            double altMetricPart = relMetricComponent;
            
            // Выходное значение - свертка компонент метрик
            return Math.Round(0.7*altMetricPart + 0.3*avgSpeedPart + avgErrPart,2);
        }
        // Кнопка "Начать/Завершить тест"
        private void button1_Click(object sender, EventArgs e)
        {
            inTest = !inTest;
            if (inTest)
            {
                UserInput.Clear();
                UserInput.ReadOnly = false;
                UserInput.Focus();

                // Положение индекса
                test_text_idx = 0;


                // Блокируем кнопку до конца тестирования
                button1.Text = "Завершить тестирование";
                button1.Enabled = false;
                FinishTest.Enabled = false;

                // Выписываем текст в поле Sample
                using (StreamReader sr = File.OpenText(paths[test_id]))
                    SampleText.Text = sr.ReadToEnd();
            }
            else
            {
                test_ts[test_id] = test_end_time - test_ts[test_id];
                // Обновляем счетчик пройденных тестов
                completed_tests++;
                // Восстанавливаем кнопку
                button1.Text = "Начать тестирование";
                button1.Enabled = false;
                UserInput.ReadOnly = true;

                UserInput.Clear();
                SampleText.Focus();
                //MessageBox.Show("Время: " + (test_ts[test_id]).ToString() + " ms\nКоличество ошибок: " + err_count, "Результат");
                // Включаем кнопку "Следующий этап", если не все тесты пройдены
                if (completed_tests == paths.Length)
                {
                    FinishTest.Enabled = true;
                    if (DialogResult.OK == MessageBox.Show("Тестирование завершено. Нажмите на кнопку 'Готово', чтобы выйти", "Тестирование завершено"))
                        FinishTest.Focus();
                    SampleText.Text = "Тестирование окончено.";
                }
                else
                {
                    button2.Enabled = true;
                    if (DialogResult.OK == MessageBox.Show("Готово. Нажмите на кнопку 'Следующий этап', чтобы продолжить тестирование", "Этап завершен"))
                        button2.Focus();
                    SampleText.Text = "Система ожидает начала нового тестирования.";

                }

            }
        }

        private void UserInput_KeyDown(object sender, KeyEventArgs e)
        {
            // Фиксируем время
            long time = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            
            if (test_text_idx >= SampleText.Text.Length)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                if (e.KeyCode != Keys.ShiftKey && e.KeyCode != Keys.Enter && e.KeyCode != Keys.Menu && e.KeyCode != Keys.ControlKey)
                    KeyDownNotes.Add(new Tuple<KeyEventArgs, long>(e, time));
                // Если нажата клавиша Delete/Backspace, убираем статус ошибки
                if (inTest && inputErr && e.KeyCode == Keys.Back)
                {
                    err_count++;
                }
                else e.Handled = true;
            }
        }

        private void UserInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Фиксируем время
            long time = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            if (test_text_idx >= SampleText.Text.Length)
                e.Handled = true;
            else
            {
                if (test_text_idx == 0)
                    test_ts[test_id] = time;
                KeyPressNotes.Add(new Tuple<KeyPressEventArgs, long>(e, time));

                if (//isCyrOrLatin(e.KeyChar) &&
                    inTest &&
                    !inputErr &&
                    test_text_idx < SampleText.Text.Length
                    )
                {
                    string c_exp = Char.ConvertFromUtf32(SampleText.Text[test_text_idx]);
                    string c = Char.ConvertFromUtf32(e.KeyChar);
                    // Проверяем, верный ли введен символ
                    if (c == c_exp)
                    {
                        mf.LogOutput(c + ": pr_time: " + time);
                        test_text_idx++;
                        // Завершаем тестирование, если был последний символ
                        if (test_text_idx == SampleText.Text.Length)
                        {
                            //button1.Enabled = true;
                        }
                    }
                    // Неверный символ - фиксируем состояние ошибки
                    else inputErr = true;
                }
                else e.Handled = true;
            }
        }

        private void UserInput_KeyUp(object sender, KeyEventArgs e)
        {
            // Фиксируем время
            long time = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            if (test_text_idx > SampleText.Text.Length)
                e.Handled = true;
            else
            {
                if (e.KeyCode != Keys.ShiftKey && e.KeyCode != Keys.Enter && e.KeyCode != Keys.Menu && e.KeyCode != Keys.ControlKey)
                    KeyUpNotes.Add(new Tuple<KeyEventArgs, long>(e, time));

                // Получаем введенный символ
                string c = Char.ConvertFromUtf32(e.KeyValue);

                // Заканчиваем тест по отпуску последней буквы
                if (test_text_idx == SampleText.Text.Length)
                {
                    button1.Enabled = true;
                    UserInput.ReadOnly = true;
                    test_end_time = time;
                }

                // Если нажата клавиша Delete/Backspace, убираем статус ошибки
                if (inputErr &&
                    (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                    )
                    inputErr = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            test_id++;
            StageLabel.Text = "Этап " + (test_id + 1).ToString() + "/" + paths.Length;
            button1.Enabled = true;
            button2.Enabled = false;
            if (test_id == test_ts.Length / 2)
            {
                MessageBox.Show("Сейчас вы будете печатать текст на русском языке.\nУбедитесь, что вы сменили раскладку клавиатуры, и продолжайте тестирование.", "Внимание!");
            }
            button1.Focus();
        }

        private void FinishTest_Click(object sender, EventArgs e)
        {
            if (authMode)
            {
                double P = AssertMetrics();
                if (MessageBox.Show("Значение непохожести: " + P) == DialogResult.OK)
                    if (P <= mf.user_map[user_name].errLim)
                    {
                        MessageBox.Show("Авторизация успешна!");
                        this.DialogResult = DialogResult.Yes;
                    }
                    else
                    {
                        this.DialogResult = DialogResult.Abort;
                    }
                Close();
            }
            else { 

                ExportMetrics();
                if (MessageBox.Show("Данные сохранены.") == DialogResult.OK)
                    this.DialogResult = DialogResult.Yes;
                Close();
            }
        }
    }
}
