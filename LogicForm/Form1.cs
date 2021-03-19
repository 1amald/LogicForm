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
            toolTip2.SetToolTip(this.textBox1, "Для того, чтобы проверить эквивалентность формул, заполните строку в формате : формула1 = формула2");
            toolTip2.SetToolTip(this.button15, "Для того, чтобы проверить эквивалентность формул, заполните строку в формате : формула1 = формула2");
            toolTip2.ShowAlways = true;
            toolTip2.AutoPopDelay = 10000;
            toolTip2.InitialDelay = 5;
            toolTip2.ReshowDelay = 50;
        }


        DataGridView dg = new DataGridView();
        char[] Actions = new char[]{ '¬', '∧', '∨', '⊕', '⇒', '⇿' };
        int variables;
        int rows;

        char[,] MasTrue = new char[0, 0];
        char[,] MasTrue2 = new char[0, 0];
        List<char> var = new List<char>();

        bool ITB(int a)
        {
            if(a==0)
            {
                return false;
            }
            return true;
        }
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
            for (int i = 0; i < rows/2; i++)
            {
                if (Convert.ToString(dg[variables, i].Value) == Convert.ToString(dg[variables, rows - 1 - i].Value))
                {
                    return "На наборах противоположных значений переменных формула не принимает противоположное значение.";
                }
            }
            return "На наборах противоположных значений переменных формула принимает противоположное значение.";
        }

        string Fictitious()
        {
            string s = "";
            for (int i = 0; i < variables; i++)
            {
                bool Fict = true;
                int quantityblocks = Convert.ToInt32(Math.Pow(2, i + 1));
                int lenghtblock = rows / quantityblocks;

                for (int j = 0; j < quantityblocks / 2; j++)
                {
                    for (int k = 0; k < lenghtblock; k++)
                    {
                        if (Convert.ToString(dg[variables, j * lenghtblock * 2 + k].Value) != Convert.ToString(dg[variables, j * lenghtblock * 2 + lenghtblock + k].Value))
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
            textBox2.Text = CheckForSwitch() + "\r\n" + Fictitious();
            textBox2.ReadOnly = true;
            textBox2.Visible = true;
        }

        void PostFix()
        {
            Stack<char> first = new Stack<char>();

        }

        void CreateTAB()
        {
            this.Controls.Remove(dg);
            dg = new DataGridView();

            dg.ColumnCount = variables + 1;
            dg.RowCount = rows;
            var.Add('=');

            for (int i = 0; i < variables + 1; i++)
            {
                dg.Columns[i].Width = 30;
                dg.ColumnHeadersHeight = 30;
                dg.RowHeadersVisible = false;
                dg.Columns[i].Name = Convert.ToString(var[i]);
            }

            for (int i = 0; i < dg.RowCount; i++)
            {
                for (int j = 0; j < dg.ColumnCount; j++)
                {
                    dg.Rows[i].Cells[j].Value = Convert.ToString(MasTrue[i, j]);
                }
            }

            dg.Size = new Size(210, 300);
            dg.Location = new Point(10 , 160);
            dg.BackgroundColor = Color.White;
            this.Controls.Add(dg);
            label2.Visible = true;
        }

        
        void CreateMasTrue(string s)
        {

            string formula = s;
            variables = 0;
            var = new List<char>();
            // ищем переменные создаем, заполняем лист переменных
            for (int i = 0; i < formula.Length; i++)
            {
                if (formula[i] >= 'A' & formula[i] <= 'Z')
                {
                    if (!var.Contains(formula[i]))
                    {
                        var.Add(formula[i]);
                        variables++;
                    }
                }
            }
            var.Sort();


            rows = Convert.ToInt32(Math.Pow(2, Convert.ToDouble(variables)));
            MasTrue = new char[rows, variables+1];
            for (int i=0;i< rows; i++)
            {
                string s1 = Convert.ToString(i, 2);
                string currentstring = formula;
                while (s1.Length != variables)
                {
                    s1 = '0' + s1;
                }

                for(int k = 0;k<var.Count;k++)
                {
                    currentstring = currentstring.Replace(var[k], s1[k]);
                }

                for (int j = 0;j<variables;j++)
                {
                    MasTrue[i, j] = s1[j];
                }
                char res = Curr(currentstring);
                MasTrue[i, variables] = res;
                
            }
        }    

        
        char Curr(string currentstr)
        {
            string Sub(string s) // выполняем операции внутри скобок
            {
                char Operation(string ss)
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

                    if (ss.Length == 2)
                    {
                        return (BTC(Not(CTB(ss[1]))));
                    }

                    if (ss[1] == '∧') { return (BTC(And(CTB(ss[0]), CTB(ss[2])))); }

                    if (ss[1] == '∨') { return (BTC(Or(CTB(ss[0]), CTB(ss[2])))); }

                    if (ss[1] == '⊕') { return (BTC(Xor(CTB(ss[0]), CTB(ss[2])))); }

                    if (ss[1] == '⇒') { return (BTC(Implc(CTB(ss[0]), CTB(ss[2])))); }

                    return (BTC(Equally(CTB(ss[0]), CTB(ss[2]))));
                    
                } // операции

                string subcurrentstr = s;
                for (int i = 0; i < Actions.Length; i++)
                {
                    for (int j = 0; j < subcurrentstr.Length; j++)
                    {
                        if (subcurrentstr[j] == Actions[i]) // нашли операцию в строчке
                        {
                            if (i == 0) // если это унарная операция
                            {
                                subcurrentstr = subcurrentstr.Insert(j, Operation(subcurrentstr.Substring(j, 2)).ToString());
                                subcurrentstr = subcurrentstr.Remove(j + 1, 2);
                            }

                            else // если бинарная
                            {
                                subcurrentstr = subcurrentstr.Insert(j - 1, Operation(subcurrentstr.Substring(j - 1, 3)).ToString());
                                subcurrentstr = subcurrentstr.Remove(j, 3);
                            }
                        }
                    }
                }
                return subcurrentstr; // возвращаем значение 
            }

            while (currentstr.Length > 1)
            {
                for (int i = 0; i < currentstr.Length; i++)
                {
                    if (currentstr.Contains('(')) // если находим не находим скобки,тогда выполняем sub
                    {
                        if (currentstr[i] == ')')
                        {
                            for (int j = i; j >= 0; j--)
                            {
                                if (currentstr[j] == '(')
                                {
                                    currentstr = currentstr.Insert(j, Sub(currentstr.Substring(j + 1, i - j - 1)));
                                    currentstr = currentstr.Remove(j + 1, i - j + 1);
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    else
                    {
                        currentstr = Sub(currentstr);
                    }
                }
            }

            return Convert.ToChar(currentstr);
        }

        bool Equivalence(string s1, string s2)
        {

            char[,] arr1 = new char[0, 0];
            CreateMasTrue(s1);
            arr1 = MasTrue;

            char[,] arr2 = new char[0, 0];
            CreateMasTrue(s2);
            arr2 = MasTrue;

            if (arr1.GetLength(0) != arr2.GetLength(0))
            {
                return false;
            }

            for (int i = 0; i < variables; i++)
            {
                if (arr1[i, variables] != arr2[i, variables])
                {
                    return false;
                }
            }

            return true;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if(Check(textBox1.Text))
            {
                CreateMasTrue(textBox1.Text);
                CreateTAB();
                FillTextBox();
            }
        }

        private void A_Click(object sender, EventArgs e)
        {
            int a = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(a, "A");
            textBox1.SelectionStart = a + 1;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int a = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(a, "B");
            textBox1.SelectionStart = a + 1;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int a = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(a, "C");
            textBox1.SelectionStart = a + 1;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int a = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(a, "D");
            textBox1.SelectionStart = a + 1;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int a = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(a, "E");
            textBox1.SelectionStart = a + 1;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int a = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(a, "¬");
            textBox1.SelectionStart = a + 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int a = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(a, "∧");
            textBox1.SelectionStart = a + 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int a = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(a, "∨");
            textBox1.SelectionStart = a + 1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int a = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(a, "⊕");
            textBox1.SelectionStart = a + 1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int a = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(a, "⇒");
            textBox1.SelectionStart = a + 1;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int a = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(a, "(");
            textBox1.SelectionStart = a + 1;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int a = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(a, ")");
            textBox1.SelectionStart = a + 1;
        }


        private void button13_Click(object sender, EventArgs e)
        {
            int a = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(a, "⇿");
            textBox1.SelectionStart = a + 1;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            int a = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(a, "=");
            textBox1.SelectionStart = a + 1;
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа умеет:\n1.Составлять таблицу истинности для логических формул\n2.Определять фиктивные переменные\n3.Определять, верно ли, что на наборах противоположных значений переменных формула принимает противоположное значение\n4.Проверять, являются ли формулы эквивалентными \n\nРазработчик : Дмитриев Александр");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            string s = textBox1.Text;
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
            button11.Enabled = !button11.Enabled;
            button7.Enabled = !button7.Enabled;
            button14.Enabled = !button14.Enabled;
        }
    }
}
