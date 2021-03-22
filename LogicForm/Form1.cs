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

        /*void CreateTable()
        {
            this.Controls.Remove(dg);
            dg = new DataGridView();

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
            this.Controls.Add(dg);
            label2.Visible = true;
        }// Создает таблицу*/
        bool Check(string str)
        {
            bool res = false;

            str = str.ToUpper();
            str = str.Trim();
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
            return true;
        }
        void FillTextBox(Formula f)
        {
            textBox2.Text = f.postfix;
            /*textBox2.Text = CheckForSwitch() + "\r\n" + 
                            Fictitious() + "\r\n" + 
                            "Постфиксная форма: " + ToPostfixForm(formula.Text)+ "\r\n"+
                            "Инфиксная форма: " + ToInfixForm(); */                      
            textBox2.ReadOnly = true;
            textBox2.Visible = true;
        }

        #region
        private void button12_Click(object sender, EventArgs e)
        {
            
            if (!Check(formula.Text))
            {
                return;
            }
            Formula f = new Formula(formula.Text);
            FillTextBox(f);
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
                "4.Преобразовывать формулы из инфиксной нотации в постфиксную \n\n" +
                "Разработчик : Дмитриев Александр");
        }
        /*private void button15_Click(object sender, EventArgs e)
        {
            string s = formula.Text;
            string s1;
            string s2;
            string msg;

            if (s.Contains('='))
            {
                s1 = s.Substring(0, s.IndexOf('='));
                s2 = s.Substring(s.IndexOf('=')+1, s.Length - s.IndexOf('=')-1);
                if (Check(s1) & Check(s2))
                {
                    if (Equivalence(s1, s2))
                    {
                        msg = "Формулы эквивалентны";
                    }
                    else
                    {
                        msg = "Формулы не эквивалентны";
                    }

                    MessageBox.Show(msg);
                }
            }
            else
            {
                MessageBox.Show("Для того, чтобы проверить эквивалентность формул, заполните строку в формате : формула1=формула2");
            }
        }*/
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
