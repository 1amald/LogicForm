using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogicForm
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();

            ToolTip toolTip2 = new ToolTip();
            toolTip2.SetToolTip(this.formula, "Для того, чтобы проверить эквивалентность формул, заполните строку в формате : формула1 = формула2");
            toolTip2.SetToolTip(this.button15, "Для того, чтобы проверить эквивалентность формул, заполните строку в формате : формула1 = формула2");
            toolTip2.ShowAlways = true;
            toolTip2.AutoPopDelay = 10000;
            toolTip2.InitialDelay = 5;
            toolTip2.ReshowDelay = 50;
        }

        DataGridView dg = new DataGridView();
        char[] actions = new char[]{ '¬', '∧', '∨', '⊕', '⇒', '⇿' }; // Операции

        int varCount;// Количество переменных
        int rowsCount;
        string[] numeric = new string[0]; // Массив наборов значений
        char[] varArray = new char[0]; // Массив переменных
        string postfix;// Постфиксная запись формулы
        char[] solutions; // Решения

        bool CTB(char a)
        {
            if(a == '0')
            {
                return false;
            }

            return true;
        }
        char BTC(bool a)
        {
            if (a) { return '1'; }
            else { return '0'; }
        }

        void Analyze(string s)// Заполняет поля
        {
            List<char> varList = new List<char>();

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] >= 'A' && s[i] <= 'Z' && !varList.Contains(s[i]))
                {
                    varList.Add(s[i]);
                }
            }
            varList.Sort();

            postfix = ToPostfixForm(s);
            varCount = varList.Count;
            rowsCount = (int)Math.Pow(2, varCount);
            varArray = varList.ToArray();
            

            numeric = new string[rowsCount];

            for (int i = 0; i < rowsCount; i++)
            {
                numeric[i] = Convert.ToString(i, 2);
                while (numeric[i].Length != varCount)
                {
                    numeric[i] = '0' + numeric[i];
                }
            }
            solutions = Results();
        }
        char[] Results()
        {
            char[] res = new char[numeric.Length];

            for(int i = 0; i < numeric.Length; i++)
            {
                string currentForm = postfix;
                for(int j = 0; j < varCount; j++)
                {
                    currentForm = currentForm.Replace(varArray[j], numeric[i][j]);
                }
                res[i] = Calculate(currentForm);
            }

            return res;
        }// Возвращает массив решений
        char Calculate(string s)
        {
            char Operation(char op1, char op2, char action)
            {

                bool Not(bool b)
                {
                    b = !b;
                    return b;
                }
                bool Or(bool a, bool b)
                {
                    if (a | b)
                    {
                        return true;
                    }
                    return false;
                }
                bool And(bool a, bool b)
                {
                    if (a & b)
                    {
                        return true;

                    }
                    return false;
                }
                bool Xor(bool a, bool b)
                {
                    if (a == b)
                    {
                        return false;
                    }
                    return true;
                }
                bool Implc(bool a, bool b)
                {
                    if (a & !b)
                    {
                        return false;
                    }
                    return true;
                }
                bool Equally(bool a, bool b)
                {
                    if (a == b)
                    {
                        return true;
                    }
                    return false;
                }

                if (op2 == '$')
                {
                    return (BTC(Not(CTB(op1))));
                }

                switch (action)
                {
                    case '∧':
                        return (BTC(And(CTB(op1), CTB(op2))));
                    case '∨':
                        return (BTC(Or(CTB(op1), CTB(op2))));
                    case '⊕':
                        return (BTC(Xor(CTB(op1), CTB(op2))));
                    case '⇒':
                        return (BTC(Implc(CTB(op1), CTB(op2))));
                    case '⇿':
                        return (BTC(Equally(CTB(op1), CTB(op2))));
                    default:
                        return '0';
                }

            } // операции

            Stack<char> st = new Stack<char>();
            for (int i = 0; i < s.Length; i++) // считает значение выражения
            {
                if(s[i] == '0' || s[i] == '1')
                {
                    st.Push(s[i]);
                }
                else if(s[i] == '¬')
                {
                    st.Push(Operation(st.Pop(), '$', '¬'));
                }
                else
                {
                    st.Push(Operation(st.Pop(), st.Pop(), s[i]));
                }
            }
            return st.Pop();
        } // Считает значение выражения
        string ToPostfixForm(string formula)
        {
            int Priority(char oper)
            {
                switch (oper)
                {
                    case '¬':
                        return 5;
                    case '∧':
                        return 4;
                    case '∨':
                        return 3;
                    case '⊕':
                        return 3;
                    case '⇒':
                        return 2;
                    case '⇿':
                        return 1;
                    default:
                        return 0;
                }
            }

            Stack<char> st = new Stack<char>();
            Queue<char> qu = new Queue<char>();
            string result = "";

            for (int i = 0; i < formula.Length; i++)
            {
                if ('A' <= formula[i] && 'Z' >= formula[i])
                {
                    qu.Enqueue(formula[i]);
                    continue;
                }

                if (actions.Contains(formula[i]))
                {
                    if (st.Count == 0 || st.Peek() == '(')
                    {
                        st.Push(formula[i]);
                        continue;
                    }

                    if (Priority(formula[i]) > Priority(st.Peek()))
                    {
                        st.Push(formula[i]);
                        continue;
                    }
                    else
                    {
                        while (st.Count != 0 && ((Priority(st.Peek()) >= Priority(formula[i])) || st.Peek() == '('))
                        {
                            qu.Enqueue(st.Pop());
                        }
                        st.Push(formula[i]);
                        continue;
                    }
                }

                if (formula[i] == '(')
                {
                    st.Push(formula[i]);
                }

                if (formula[i] == ')')
                {
                    while (st.Peek() != '(')
                    {
                        qu.Enqueue(st.Pop());
                    }
                    st.Pop();
                }
            }

            foreach (var ch in qu)
            {
                result += ch;
            }
            foreach (var ch in st)
            {
                result += ch;
            }

            return result;
        } // Из инфиксной в постфиксную
        void CreateTable()
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
        }// Создает таблицу
        bool Check(string s)
        {
            if(s.Length==0)
            {
                MessageBox.Show("Введите формулу");
                return false;
            }
            
            s = s.ToUpper();
            s = s.Trim();

            for(int i = 0; i < s.Length; i++)
            {
                if(s[i] >= 'A' & s[i] <= 'Z')
                {
                    break;
                }
                if (i == s.Length - 1)
                {
                    MessageBox.Show("Переменные не найдены");
                    return false;
                }
            }

            for (int i=0;i<s.Length;i++)
            {
                if(!(s[i]>='A'& s[i] <= 'Z') & s[i]!= '¬' & s[i] != '∧'& s[i] != '∨'& s[i] != '⊕'& s[i] != '⇒'& s[i] != '⇿' & s[i] != '(' & s[i] != ')')
                {
                    MessageBox.Show("Недопустимый символ, индекс : " + i);
                    return false;
                }  
            }

            int right = 0;
            int left = 0;

            for (int i = 0; i < s.Length; i++)
            {
                if(s[i]=='(')
                {
                    right++;
                }

                if(s[i]==')')
                {
                    left++;
                }
            }

            if(right!=left)
            {
                MessageBox.Show("Некорректная расстановка скобок");
                return false;
            }

            // проверка на структуру
            string abc = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string binary = "∧∨⊕⇒⇿";
            char uno = '¬';
            void Show()
            {
                MessageBox.Show("Неверная структура формулы");
            }

            s = " " + s + " ";
            for (int i = 1; i < s.Length-2; i++)
            {
                
                if(abc.Contains(s[i]))
                {
                    if(abc.Contains(s[i + 1]) | s[i+1] == uno)
                    {
                        Show();
                        return false;
                    }
                    continue;
                }

                if(binary.Contains(s[i]))
                {
                    if (binary.Contains(s[i+1]) | s[i + 1]==')')
                    {
                        Show();
                        return false;
                    }
                    continue;
                }

                if(s[i] == uno)
                {
                    if (binary.Contains(s[i + 1]) | s[i + 1] == ')')
                    {
                        Show();
                        return false;
                    }
                    continue;
                }

                if(s[i]==')')
                {
                    if (abc.Contains(s[i + 1]) | s[i + 1] == '('| s[i + 1] == uno)
                    {
                        Show();
                        return false;
                    }
                    continue;
                }

                if (s[i] == '(')
                {
                    if (binary.Contains(s[i + 1]) | s[i + 1] == ')')
                    {
                        Show();
                        return false;
                    }
                    continue;
                }
            }

            return true;
        }


        string CheckForSwitch()
        {
            for (int i = 0; i < rowsCount / 2; i++)
            {
                if (Convert.ToString(dg[varCount, i].Value) == Convert.ToString(dg[varCount, rowsCount - 1 - i].Value))
                {
                    return "На наборах противоположных значений переменных формула не принимает противоположное значение.";
                }
            }
            return "На наборах противоположных значений переменных формула принимает противоположное значение.";
        }
        string Fictitious()
        {
            string s = "";
            for (int i = 0; i < varCount; i++)
            {
                bool Fict = true;
                int quantityblocks = Convert.ToInt32(Math.Pow(2, i + 1));
                int lenghtblock = rowsCount / quantityblocks;

                for (int j = 0; j < quantityblocks / 2; j++)
                {
                    for (int k = 0; k < lenghtblock; k++)
                    {
                        if (Convert.ToString(dg[varCount, j * lenghtblock * 2 + k].Value) != Convert.ToString(dg[varCount, j * lenghtblock * 2 + lenghtblock + k].Value))
                        {
                            Fict = false;
                            break;
                        }
                    }

                    if (!Fict)
                    {
                        break;
                    }
                }
                if (Fict)
                {
                    s = s + dg.Columns[i].Name;
                }

            }
            if(s.Length == 0)
            {
                s = "не обнаружены";
            }
            return "Фиктивные переменные: " + s;
        }
        void FillTextBox()
        {
            textBox2.Text = CheckForSwitch() + "\r\n" + Fictitious() + "\r\n" + ToPostfixForm(formula.Text);
            textBox2.ReadOnly = true;
            textBox2.Visible = true;
        }
        
        bool Equivalence(string s1, string s2)
        {
            return true;
        }
        private void button12_Click(object sender, EventArgs e)
        {
            if (!Check(formula.Text))
            {
                return;
            }
            string form = formula.Text;

            Analyze(form);
            CreateTable();
            FillTextBox();
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
            MessageBox.Show("Программа умеет:\n1.Составлять таблицу истинности для логических формул\n2.Определять фиктивные переменные\n3.Определять, верно ли, что на наборах противоположных значений переменных формула принимает противоположное значение\n4.Проверять, являются ли формулы эквивалентными \n\nРазработчик : Дмитриев Александр");
        }
        private void button15_Click(object sender, EventArgs e)
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
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            button15.Enabled= !button15.Enabled;
            left.Enabled = !left.Enabled;
            right.Enabled = !right.Enabled;
            equality.Enabled = !equality.Enabled;
        }
    }
}
