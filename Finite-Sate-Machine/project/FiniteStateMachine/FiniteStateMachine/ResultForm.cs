using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FiniteStateMachine
{
    public partial class ResultForm : Form
    {
        public Form1 parent_form;
        public ResultForm(Form1 form1)
        {
            InitializeComponent();
            parent_form = form1;
            
            // Устанавливаем выравнивание таблицы отношений
            foreach (DataGridViewColumn col in table1.Columns)
            {
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            // Устанавливаем выравнивание таблицы кодов
            foreach (DataGridViewColumn col in table2.Columns)
            {
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            View_Result();
        }
        
        public void View_Result()
        {
            // Получаем бинарную длинну X и Y
            int x_binary_size = parent_form.ToBinaryList(parent_form.X_Count, parent_form.X_Count).Count();
            int y_binary_size = parent_form.ToBinaryList(parent_form.X_Count, parent_form.X_Count).Count();

            // Получаем максимальные значения X и Y
            int x_max = parent_form.X.Values.Max();
            int y_max = parent_form.Y.Values.Max();

            // Добавляем необходимое число стобцов в зависимости от бинарной длинны X
            for (int i = x_binary_size; i > 0; i--)
            {
                var column = new DataGridViewTextBoxColumn();
                column.Name = $"x{i}";
                column.FillWeight = 35;
                column.Width = 35;
                column.ReadOnly = true;
                table2.Columns.Insert(0, column);
            }

            // Добавляем необходимое число стобцов в зависимости от бинарной длинны Y
            for (int i = 1; i < y_binary_size+1; i++)
            {
                var column = new DataGridViewTextBoxColumn();
                column.Name = $"y{i}";
                column.FillWeight = 35;
                column.Width = 35;
                column.ReadOnly = true;
                table2.Columns.Add(column);
            }

            // Добавляем в 1ю таблицу кол-во строк равное кол-ву иксов
            // А во второую - всегда 10
            table1.Rows.Add(parent_form.X_Count);
            table2.Rows.Add(10);

            // По умолчанию заполняем таблицу отношений звездочками
            foreach (DataGridViewRow row in table1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Value = "✹";
                }
            }

            //DataGridViewCellStyle style = new DataGridViewCellStyle();
            //style.BackColor = System.Drawing.Color.BurlyWood;
            //table1.Rows[3].Cells[2].Style = style;
            //style.BackColor = System.Drawing.Color.DarkCyan;
            //table1.Rows[5].DefaultCellStyle = style;

            // Заполняем 10 строчек 2й таблицы по константе
            HardCode(x_binary_size, y_binary_size, x_max, y_max);
            // Заполняем таблицу связей
            FillRelationshipTable(x_max);
        }
        
        public void FillRelationshipTable(int x_max)
        {
            for (int i = 0; i < x_max+1; i++)
            {
                var x = parent_form.X.Where(d => d.Value == i).Select(d => d.Key).ToList<string>();
                foreach (string x_key in x)
                {
                    // Получение параметров
                    var s_id1 = (int)Char.GetNumericValue(x_key[1]);    // 2й символ отвечает за id текущего состояния
                    var s_id2 = (int)Char.GetNumericValue(x_key[4]);    // 5й символ отвечает за id следующего состояния
                    string y_key = x_key.Replace('x', 'y');             // Меняем в ключе тип переменной (x на y)
                    int y = parent_form.Y[y_key];                       // Получаем реакцию

                    // Заполнение строки
                    table1.Rows[i].Cells[s_id1+1].Value = $"S{s_id2}/Y{y}";
                    table1.Rows[i].Cells[0].Value = $"X{i}";
                }
            }
        }

        public void HardCode(int x_binary_size, int y_binary_size, int x_max, int y_max)
        {
            int grid_size = x_binary_size + 6 + y_binary_size;
            List<int> list;
            int[] row_array;

            // 0
            list = parent_form.ToBinaryList(parent_form.X["s0_s0_x"], x_max);
            list.AddRange(parent_form.S_Binary[0]);
            list.AddRange(parent_form.ToBinaryList(parent_form.Y["s0_s0_y"], y_max));
            row_array = list.ToArray();

            for (int i = 0; i < grid_size; i++)
            {
                table2.Rows[0].Cells[i].Value = row_array[i];
            }

            // 1
            list = parent_form.ToBinaryList(parent_form.X["s1_s2_x"], x_max);
            list.AddRange(parent_form.S_Binary[1]);
            list.AddRange(parent_form.ToBinaryList(parent_form.Y["s1_s2_y"], y_max));
            row_array = list.ToArray();

            for (int i = 0; i < grid_size; i++)
            {
                table2.Rows[1].Cells[i].Value = row_array[i];
            }

            // 2
            list = parent_form.ToBinaryList(parent_form.X["s2_s3_x"], x_max);
            list.AddRange(parent_form.S_Binary[2]);
            list.AddRange(parent_form.ToBinaryList(parent_form.Y["s2_s3_y"], y_max));
            row_array = list.ToArray();

            for (int i = 0; i < grid_size; i++)
            {
                table2.Rows[2].Cells[i].Value = row_array[i];
            }

            // 3
            list = parent_form.ToBinaryList(parent_form.X["s3_s4_x"], x_max);
            list.AddRange(parent_form.S_Binary[3]);
            list.AddRange(parent_form.ToBinaryList(parent_form.Y["s3_s4_y"], y_max));
            row_array = list.ToArray();

            for (int i = 0; i < grid_size; i++)
            {
                table2.Rows[3].Cells[i].Value = row_array[i];
            }

            // 4
            list = parent_form.ToBinaryList(parent_form.X["s1_s0_x"], x_max);
            list.AddRange(parent_form.S_Binary[4]);
            list.AddRange(parent_form.ToBinaryList(parent_form.Y["s1_s0_y"], y_max));
            row_array = list.ToArray();

            for (int i = 0; i < grid_size; i++)
            {
                table2.Rows[4].Cells[i].Value = row_array[i];
            }

            // 5
            list = parent_form.ToBinaryList(parent_form.X["s4_s1_x"], x_max);
            list.AddRange(parent_form.S_Binary[5]);
            list.AddRange(parent_form.ToBinaryList(parent_form.Y["s4_s1_y"], y_max));
            row_array = list.ToArray();

            for (int i = 0; i < grid_size; i++)
            {
                table2.Rows[5].Cells[i].Value = row_array[i];
            }

            // 6
            list = parent_form.ToBinaryList(parent_form.X["s0_s4_x"], x_max);
            list.AddRange(parent_form.S_Binary[6]);
            list.AddRange(parent_form.ToBinaryList(parent_form.Y["s0_s4_y"], y_max));
            row_array = list.ToArray();

            for (int i = 0; i < grid_size; i++)
            {
                table2.Rows[6].Cells[i].Value = row_array[i];
            }

            // 7
            list = parent_form.ToBinaryList(parent_form.X["s4_s4_x"], x_max);
            list.AddRange(parent_form.S_Binary[7]);
            list.AddRange(parent_form.ToBinaryList(parent_form.Y["s4_s4_y"], y_max));
            row_array = list.ToArray();

            for (int i = 0; i < grid_size; i++)
            {
                table2.Rows[7].Cells[i].Value = row_array[i];
            }

            // 8
            list = parent_form.ToBinaryList(parent_form.X["s2_s1_x"], x_max);
            list.AddRange(parent_form.S_Binary[8]);
            list.AddRange(parent_form.ToBinaryList(parent_form.Y["s2_s1_y"], y_max));
            row_array = list.ToArray();

            for (int i = 0; i < grid_size; i++)
            {
                table2.Rows[8].Cells[i].Value = row_array[i];
            }

            // 9
            list = parent_form.ToBinaryList(parent_form.X["s4_s0_x"], x_max);
            list.AddRange(parent_form.S_Binary[9]);
            list.AddRange(parent_form.ToBinaryList(parent_form.Y["s4_s0_y"], y_max));
            row_array = list.ToArray();

            for (int i = 0; i < grid_size; i++)
            {
                table2.Rows[9].Cells[i].Value = row_array[i];
            }
        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
