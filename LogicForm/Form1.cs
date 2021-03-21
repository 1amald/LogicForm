using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

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
        const string actions = "¬∧∨⊕⇒⇿"; // Операции
        const char uno = '¬';
        const string binary = "∧∨⊕⇒⇿";
        const string abc = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string allSimbols = "ABC¬∧∨()⊕⇒⇿DEFGHIJKLMNOPQRSTUVWXYZ";

        int varCount;// Количество переменных
        int rowsCount;// 2 в степени varCount
        string[] numeric = new string[0]; // Массив наборов значений
        char[] varArray = new char[0]; // Массив переменных
        char[] solutions; // Решения

        string postfix;
        string infix;

        bool CTB(char a)
        {
            if(a == '0')
            {
                return false;
            }

            return true;
        }// char to bool
        char BTC(bool a)
        {
            if (a) { return '1'; }
            else { return '0'; }
        } // bool to char
        void Analyze()// Заполняет поля
        {
            List<char> varList = new List<char>();

            for (int i = 0; i < infix.Length; i++)
            {
                if (infix[i] >= 'A' && infix[i] <= 'Z' && !varList.Contains(infix[i]))
                {
                    varList.Add(infix[i]);
                }
            }
            varList.Sort();

            postfix = ToPostfixForm(infix);
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
            solutions = Solutions();
        }
        char[] Solutions()
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
                    if (s[i] == '0' || s[i] == '1')
                    {
                        st.Push(s[i]);
                    }
                    else if (s[i] == '¬')
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
        }// Возвращает массив решений
        static int Priority(char oper)
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
        string ToPostfixForm(string infixFormula)
        {
            Stack<char> st = new Stack<char>();
            Queue<char> qu = new Queue<char>();
            string result = "";

            for (int i = 0; i < infixFormula.Length; i++)
            {
                if ('A' <= infixFormula[i] && 'Z' >= infixFormula[i])
                {
                    qu.Enqueue(infixFormula[i]);
                    continue;
                }

                if (actions.Contains(infixFormula[i]))
                {
                    if (st.Count == 0 || st.Peek() == '(')
                    {
                        st.Push(infixFormula[i]);
                        continue;
                    }

                    if (Priority(infixFormula[i]) > Priority(st.Peek()))
                    {
                        st.Push(infixFormula[i]);
                        continue;
                    }
                    else
                    {
                        while (st.Count != 0 && ((Priority(st.Peek()) >= Priority(infixFormula[i])) || st.Peek() == '('))
                        {
                            qu.Enqueue(st.Pop());
                        }
                        st.Push(infixFormula[i]);
                        continue;
                    }
                }

                if (infixFormula[i] == '(')
                {
                    st.Push(infixFormula[i]);
                }

                if (infixFormula[i] == ')')
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
        class mystr
        {
            public mystr(string val, int pr)
            {
                Value = val;
                Priority = pr;
            }
            public string Value { get; set; }
            public int Priority { get; set; }
            
        }// нужен для перевода из постфиксной в инфиксную
        string ToInfixForm(string postfixFormula)
        { 
            Stack<mystr> st = new Stack<mystr>();
            foreach(var ch in postfixFormula)
            {
                if (ch>= 'A' && ch <= 'Z')
                {
                    st.Push(new mystr(ch.ToString(),0));
                }
                else
                {
                    if(ch == '¬')
                    {
                        var el = st.Pop();
                        el.Value = ch + el.Value;
                        el.Priority = Priority(ch);
                        st.Push(el);
                        continue;
                    }
                    var el1 = st.Pop();
                    var el2 = st.Pop();

                    if(el1.Priority<Priority(ch) && el1.Priority!=0)
                    {
                        el1.Value = '(' + el1.Value + ')';
                    }
                    if (el2.Priority < Priority(ch) && el2.Priority != 0)
                    {
                        el2.Value = '(' + el2.Value + ')';
                    }
                    mystr newStr = new mystr(el2.Value + ch + el1.Value, Priority(ch));
                    st.Push(newStr);
                }
            }
            return st.Pop().Value;
        }
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
            textBox2.Text = CheckForSwitch() + "\r\n" + 
                            Fictitious() + "\r\n" + 
                            "Постфиксная форма: " + ToPostfixForm(formula.Text)+ "\r\n"+
                            "Инфиксная форма: " + ToInfixForm();                       
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
            string form = formula.Text;

            Analyze();
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
