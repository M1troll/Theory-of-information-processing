using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FiniteStateMachine
{
    public partial class Form1 : Form
    {
        public int X_Count;
        public int Y_Count;
        public Dictionary<string, int> X;
        public Dictionary<string, int> Y;
        public Dictionary<int, Dictionary<string, List<int>>> X_Binary;
        public Dictionary<int, Dictionary<string, List<int>>> Y_Binary;
        public Dictionary<int, int []> S_Binary;

        public Form1()
        {
            InitializeComponent();

            // Инициализируем все параметры
            X_Count = 0;
            Y_Count = 0;
            X = new Dictionary<string, int>();
            Y = new Dictionary<string, int>();
            X_Binary = new Dictionary<int, Dictionary<string, List<int>>>();
            Y_Binary = new Dictionary<int, Dictionary<string, List<int>>>();

            // Так как S фиксированное кол-во, то заполняем их бинарное представление
            S_Binary = new Dictionary<int, int[]>
            {
                [0] = new int[6] { 0, 0, 0, 0, 0, 0 },
                [1] = new int[6] { 0, 0, 1, 0, 1, 0 },
                [2] = new int[6] { 0, 1, 0, 0, 1, 1 },
                [3] = new int[6] { 0, 1, 1, 1, 0, 0 },
                [4] = new int[6] { 0, 0, 1, 0, 0, 0 },
                [5] = new int[6] { 1, 0, 0, 0, 0, 1 },
                [6] = new int[6] { 0, 0, 0, 1, 0, 0 },
                [7] = new int[6] { 1, 0, 0, 1, 0, 0 },
                [8] = new int[6] { 0, 1, 0, 0, 0, 1 },
                [9] = new int[6] { 1, 0, 0, 0, 0, 0 }
            };

            // Заполняем ячейки по умолчанию
            Fill_Click(null, null);
        }

        private void param_changed(object sender, EventArgs e)
        {
            // При изменеии ячейки получаем её текст и имя
            TextBox tb = (TextBox)sender;
            string text = tb.Text;
            string name = tb.Name;
            int param_num = -1;

            // Если значение в поле введено корректно, то сохраняем его
            try
            {
                if (text.Length != 0)
                {
                    param_num = Convert.ToInt32(text);
                    if (X.ContainsKey(name))
                    {
                        X[name] = param_num;
                    }
                    else if (Y.ContainsKey(name))
                    {
                        Y[name] = param_num;
                    }
                }
            }
            // В противном случае выводим сообщение об ошибке
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Параметр имеет неверный формат",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        public bool Check_Params()
        {
            // Если в одном из словрей есть значение -1, то как минимум один из параметров не задан
            if (X.ContainsValue(-1) || Y.ContainsValue(-1))
            {
                return false;
            }
            return true;
        }

        public List<int> ToBinaryList(int num, int count)
        {
            //  Препобразуем десятиричное число в список его двоичного представления
            List<int> list = new List<int>();
            string str = Convert.ToString(num, 2);
            int[] arr = str.Select(x => x - '0').ToArray();

            // Определяем минимальный размер двоичного кода
            int min = Convert.ToString(count, 2).Length;

            foreach (int n in arr)
            {
                list.Add(n);
            }

            // Приписываем недостающие нули
            while(list.Count < min)
            {
                list.Insert(0, 0);
            }

            return list;
        }

        public void Calculate_variables()
        {
            // Объединяем словари параметров X и Y
            var double_dictionary = X.Zip(Y, (x, y) => new { x, y });

            // Словари, зранящие уникальные параметры и их количество
            Dictionary<int, int> x_elements = new Dictionary<int, int>();
            Dictionary<int, int> y_elements = new Dictionary<int, int>();

            // Подсчитываем количество уникальных параметров
            foreach (var pair in double_dictionary)
            {
                int x_value = pair.x.Value;
                int y_value = pair.y.Value;

                if (!x_elements.ContainsKey(x_value))
                    x_elements.Add(x_value, 0);
                else if (x_elements.ContainsKey(x_value))
                    x_elements[x_value]++;

                if (!y_elements.ContainsKey(y_value))
                    y_elements.Add(y_value, 0);
                else if (y_elements.ContainsKey(y_value))
                    y_elements[y_value]++;
            }

            // Количество уникальных параметров X и Y
            X_Count = x_elements.Count();
            Y_Count = y_elements.Count();

            // Формируем двоичное представление параметров X
            foreach(int key in x_elements.Keys)
            {
                // Получаем бинарное представление ключа и его длинну
                List<int> list = ToBinaryList(key, x_elements.Count());
                int count = list.Count();

                // Записываем данные в словарь
                X_Binary.Add(key, new Dictionary<string, List<int>>
                {
                    ["length"] = new List<int> { count, },
                    ["code"] = list,
                });
            }

            // Формируем двоичное представление параметров Y
            foreach (int key in y_elements.Keys)
            {
                // Получаем бинарное представление ключа и его длинну
                List<int> list = ToBinaryList(key, y_elements.Count());
                int count = list.Count();

                // Записываем данные в словарь
                Y_Binary.Add(key, new Dictionary<string, List<int>>
                {
                    ["length"] = new List<int> { count, },
                    ["code"] = list,
                });
            }

        }

        private void RunBtn_Click(object sender, EventArgs e)
        {
            // Запускает процесс вычислений
            // Очищаем переменные
            X_Binary = new Dictionary<int, Dictionary<string, List<int>>>();
            Y_Binary = new Dictionary<int, Dictionary<string, List<int>>>();

            // Проверяем все ли параметры заполнены
            if (Check_Params())
            {
                Calculate_variables();
                Form result_form = new ResultForm(this);
                result_form.ShowDialog();
            }
            // В противном случае просим дозаполнить ячейки
            else
            {
                MessageBox.Show(
                    "Какой-то из параметров не был задан",
                    "Внимание",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            // Завершает работу приложения
            Application.Exit();
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            // Очищаем все X-ячейки
            X = new Dictionary<string, int>
            {
                ["s0_s0_x"] = -1,
                ["s0_s4_x"] = -1,
                ["s1_s0_x"] = -1,
                ["s1_s2_x"] = -1,
                ["s2_s1_x"] = -1,
                ["s2_s3_x"] = -1,
                ["s3_s4_x"] = -1,
                ["s4_s4_x"] = -1,
                ["s4_s0_x"] = -1,
                ["s4_s1_x"] = -1,
            };

            foreach (String key in X.Keys)
            {
                var text_box = this.Controls[key];
                text_box.Text = "";
            }

            // Очищаем все Y-ячейки
            Y = new Dictionary<string, int>
            {
                ["s0_s0_y"] = -1,
                ["s0_s4_y"] = -1,
                ["s1_s0_y"] = -1,
                ["s1_s2_y"] = -1,
                ["s2_s1_y"] = -1,
                ["s2_s3_y"] = -1,
                ["s3_s4_y"] = -1,
                ["s4_s4_y"] = -1,
                ["s4_s0_y"] = -1,
                ["s4_s1_y"] = -1
            };

            foreach (String key in Y.Keys)
            {
                var text_box = this.Controls[key];
                text_box.Text = "";
            }
        }

        private void Fill_Click(object sender, EventArgs e)
        {
            // Заполняем все X-ячейки по стандарту
            Dictionary<string, int> dict_x = new Dictionary<string, int>
            {
                ["s0_s0_x"] = 0,
                ["s0_s4_x"] = 3,
                ["s1_s0_x"] = 2,
                ["s1_s2_x"] = 1,
                ["s2_s1_x"] = 4,
                ["s2_s3_x"] = 1,
                ["s3_s4_x"] = 1,
                ["s4_s4_x"] = 3,
                ["s4_s0_x"] = 4,
                ["s4_s1_x"] = 2
            };

            foreach (String key in dict_x.Keys)
            {
                var text_box = this.Controls[key];
                text_box.Text = Convert.ToString(dict_x[key]);
            }
            X = dict_x;

            // Заполняем все Y-ячейки по стандарту
            Dictionary<string, int> dict_y = new Dictionary<string, int>
            {
                ["s0_s0_y"] = 1,
                ["s0_s4_y"] = 3,
                ["s1_s0_y"] = 3,
                ["s1_s2_y"] = 0,
                ["s2_s1_y"] = 0,
                ["s2_s3_y"] = 1,
                ["s3_s4_y"] = 4,
                ["s4_s4_y"] = 4,
                ["s4_s0_y"] = 2,
                ["s4_s1_y"] = 2
            };

            foreach (String key in dict_y.Keys)
            {
                var text_box = this.Controls[key];
                text_box.Text = Convert.ToString(dict_y[key]);
            }
            Y = dict_y;
        }
    }
}
