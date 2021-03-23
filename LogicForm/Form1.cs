using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static LogicForm.Consts;

namespace LogicForm
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();

            /*ToolTip toolTip2 = new ToolTip();
            toolTip2.SetToolTip(this.formula, "Для того, чтобы проверить эквивалентность формул, заполните строку в формате : формула1 = формула2");
            toolTip2.SetToolTip(this.button15, "Для того, чтобы проверить эквивалентность формул, заполните строку в формате : формула1 = формула2");
            toolTip2.ShowAlways = true;
            toolTip2.AutoPopDelay = 10000;
            toolTip2.InitialDelay = 5;
            toolTip2.ReshowDelay = 50;*/
        }

        DataGridView dg = new DataGridView();
        void CreateTable(Formula f)
        {
            this.Controls.Remove(dg);
            dg = new DataGridView();

            int varCount = f.VarCount;
            int rowsCount = f.RowsCount;
            char[] varArray = f.VarArray;
            char[] solutions = f.Solutions;

            dg.ColumnCount = varCount + 1;
            dg.RowCount = rowsCount;

            for (int i = 0; i < varCount; i++) // Для наборов переменных
            {
                dg.Columns[i].Width = 30;
                dg.ColumnHeadersHeight = 30;
                dg.RowHeadersVisible = false;
                dg.Columns[i].Name = Convert.ToString(varArray[i]);
            }

            dg.Columns[varCount].Width = 30; // Для результатов
            dg.ColumnHeadersHeight = 30;
            dg.RowHeadersVisible = false;
            dg.Columns[varCount].Name = "=";

            string[] numeric = Numeric;
            for (int i = 0; i < dg.RowCount; i++) //
            {
                for (int j = 0; j < dg.ColumnCount - 1; j++)
                {
                    dg.Rows[i].Cells[j].Value = numeric[i][j];
                }
                dg.Rows[i].Cells[varCount].Value = solutions[i];
            }

            dg.Size = new Size(210, 300);
            dg.Location = new Point(10, 160);
            dg.BackgroundColor = Color.White;
            dg.ReadOnly = true;
            this.Controls.Add(dg);
            label2.Visible = true;
            dg.ClearSelection();
        }
        void FillTextBox(Formula f)
        {
            char[] fict = f.Fictions;
            bool sw = f.SwitchForm;
            bool get = f.General;
            string inf = f.ClearInfix;
            string sknf = f.Sknf;
            string sdnf = f.Sdnf;
            string post = f.Postfix;

            string postStr = "Постфиксная форма: " + post + "\r\n\r\n";
            string fictStr;
            string swStr;
            string infStr;
            string genStr;
            string sknfStr;
            string sdnfStr;

            #region
            if (fict.Length == 0)
            {
                fictStr = "Фиктивные переменные не найдены.\r\n\r\n";
            }
            else
            {
                fictStr = "Фиктивные переменные: ";
                foreach (var ch in fict)
                {
                    fictStr += ch + " ";
                }
                fictStr += "\r\n\r\n";
            }
            if (sw)
            {
                swStr = "На противоположных наборах значений переменных формула принимает противоположные значения\r\n\r\n";
            }
            else
            {
                swStr = "На противоположных наборах значений переменных формула НЕ принимает противоположные значения\r\n\r\n";
            }
            if (inf != "")
            {
                infStr = "Формула без лишних скобок и повторяющихся отрицаний: " + "\r\n" + inf+"\r\n\r\n";
            }
            else
            {
                infStr = "Лишние скобки не найдены\r\n\r\n";
            }
            if (f.General)
            {
                genStr = "Формула является общезначимой\r\n\r\n";
            }
            else
            {
                genStr = "Формула не является общезначимой\r\n\r\n";
            }
            if (sknf.Length != 0)
            {
                sknfStr = "Скнф: " + sknf + "\r\n\r\n";
            }
            else
            {
                sknfStr = "Скнф не существует\r\n\r\n";
            }
            if (sdnf.Length != 0)
            {
                sdnfStr = "Сднф: " + sdnf + "\r\n\r\n";
            }
            else
            {
                sdnfStr = "Сднф не существует\r\n\r\n";
            }
            #endregion

            textBox2.Text = postStr +
                            fictStr +
                            swStr +
                            infStr +
                            genStr +
                            sknfStr +
                            sdnfStr;
                            
            textBox2.ReadOnly = true;
            textBox2.Visible = true;
        }
        bool Check(ref string str)
        {
            bool res = false;

            str = str.ToUpper();
            str = str.Replace("¬¬", "");
           
            foreach (var ch in str)
            {
                if (abc.Contains(ch))
                {
                    res = true;
                    break;
                }
            }
            if (!res)
            {
                MessageBox.Show("Переменные не найдены");
                return false;
            }// Проверка на наличие переменных

            foreach (var ch in str)
            {
                if(!allSimbols.Contains(ch))
                {
                    MessageBox.Show("Недопустимый символ");
                    return false;
                }  
            }// Недопустимые символы

            try
            {
                Stack<char> st = new Stack<char>();
                foreach(var ch in str)
                {
                    if(ch == '(')
                    {
                        st.Push(ch);
                    }
                    if (ch == ')')
                    {
                        st.Pop();
                    }
                }
                if (st.Count != 0)
                {
                    throw new Exception();
                }
            }
            catch
            {
                MessageBox.Show("Некорректная расстановка скобок");
                return false;
            }// Правильность расстановки скобок

            // проверка на структуру
            void Show()
            {
                MessageBox.Show("Неверная структура формулы");
            }
            #region
            str = " " + str + " ";
            for (int i = 1; i < str.Length-2; i++)
            {
                if(abc.Contains(str[i]))
                {
                    if(abc.Contains(str[i + 1]) | str[i+1] == uno)
                    {
                        Show();
                        return false;
                    }
                    continue;
                }

                if(binary.Contains(str[i]))
                {
                    if (binary.Contains(str[i+1]) | str[i + 1]==')')
                    {
                        Show();
                        return false;
                    }
                    continue;
                }

                if(str[i] == uno)
                {
                    if (binary.Contains(str[i + 1]) | str[i + 1] == ')')
                    {
                        Show();
                        return false;
                    }
                    continue;
                }

                if(str[i]==')')
                {
                    if (abc.Contains(str[i + 1]) | str[i + 1] == '('| str[i + 1] == uno)
                    {
                        Show();
                        return false;
                    }
                    continue;
                }

                if (str[i] == '(')
                {
                    if (binary.Contains(str[i + 1]) | str[i + 1] == ')')
                    {
                        Show();
                        return false;
                    }
                    continue;
                }
            }
            #endregion
            str = str.Trim();
            return true;
        }
        

        #region
        private void button12_Click(object sender, EventArgs e)
        {
            string str = formula.Text;

            if (!Check(ref str))
            {
                return;
            } 
            Formula f = new Formula(str);
            FillTextBox(f);
            CreateTable(f);
        }
        private void A_Click(object sender, EventArgs e)
        {
            int a = formula.SelectionStart;
            formula.Text = formula.Text.Insert(a, "A");
            formula.SelectionStart = a + 1;
        }
        private void button10_Click(object sender, EventArgs e)
        {
            int a = formula.SelectionStart;
            formula.Text = formula.Text.Insert(a, "B");
            formula.SelectionStart = a + 1;
        }
        private void button9_Click(object sender, EventArgs e)
        {
            int a = formula.SelectionStart;
            formula.Text = formula.Text.Insert(a, "C");
            formula.SelectionStart = a + 1;
        }
        private void button8_Click(object sender, EventArgs e)
        {
            int a = formula.SelectionStart;
            formula.Text = formula.Text.Insert(a, "D");
            formula.SelectionStart = a + 1;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            int a = formula.SelectionStart;
            formula.Text = formula.Text.Insert(a, "E");
            formula.SelectionStart = a + 1;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            int a = formula.SelectionStart;
            formula.Text = formula.Text.Insert(a, "¬");
            formula.SelectionStart = a + 1;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int a = formula.SelectionStart;
            formula.Text = formula.Text.Insert(a, "∧");
            formula.SelectionStart = a + 1;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int a = formula.SelectionStart;
            formula.Text = formula.Text.Insert(a, "∨");
            formula.SelectionStart = a + 1;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            int a = formula.SelectionStart;
            formula.Text = formula.Text.Insert(a, "⊕");
            formula.SelectionStart = a + 1;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            int a = formula.SelectionStart;
            formula.Text = formula.Text.Insert(a, "⇒");
            formula.SelectionStart = a + 1;
        }
        private void button7_Click(object sender, EventArgs e)
        {
            int a = formula.SelectionStart;
            formula.Text = formula.Text.Insert(a, "(");
            formula.SelectionStart = a + 1;
        }
        private void button11_Click(object sender, EventArgs e)
        {
            int a = formula.SelectionStart;
            formula.Text = formula.Text.Insert(a, ")");
            formula.SelectionStart = a + 1;
        }
        private void button13_Click(object sender, EventArgs e)
        {
            int a = formula.SelectionStart;
            formula.Text = formula.Text.Insert(a, "⇿");
            formula.SelectionStart = a + 1;
        }
        private void button14_Click(object sender, EventArgs e)
        {
            int a = formula.SelectionStart;
            formula.Text = formula.Text.Insert(a, "=");
            formula.SelectionStart = a + 1;
        }
        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа умеет:\n" +
                "1.Составлять таблицу истинности для логических формул\n" +
                "2.Определять фиктивные переменные\n" +
                "3.Определять, верно ли, что на наборах противоположных значений переменных формула принимает противоположное значение\n" +
                "4.Преобразовывать формулы из инфиксной нотации в постфиксную \n" +
                "5.Находить незначимые скобки \n" +
                "6.Составлять СКНФ и СДНФ \n\n" +
                "Разработчик : студент группы 2012 Дмитриев Александр");
        }
        
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //button15.Enabled= !button15.Enabled;
            left.Enabled = !left.Enabled;
            right.Enabled = !right.Enabled;
            equality.Enabled = !equality.Enabled;
        }

        #endregion
    }
}
