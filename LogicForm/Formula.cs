using System;
using System.Collections.Generic;
using System.Linq;
using static LogicForm.Consts;

namespace LogicForm
{
    public class Formula
    {
        string postfix;
        string dirtyInfix;
        string clearInfix;
        string sknf = "";
        string sdnf = "";

        List<char> varArray = new List<char>(); 
        char[] solutions;

        List<char> fictionsArray = new List<char>();
        bool switchForm;

        public Formula(string dirtyInfix)
        {
            this.dirtyInfix = dirtyInfix;
            SetVariables();
            SetPostfixForm();
            SetInfixForm();
            SetSolutions();
            SetFictitious();
            SetSwitch();
            SetSknfAndSdnf();
        }

        public int VarCount => varArray.Count;
        public int RowsCount => (int)Math.Pow(2, VarCount);
        public char[] Solutions => solutions;
        public char[] VarArray => varArray.ToArray();
        public string Postfix => postfix;
        public string ClearInfix
        {
            get
            {
                if(dirtyInfix.Length!= clearInfix.Length)
                {
                    return clearInfix;
                }
                return "";
            }
        }
        public bool SwitchForm => switchForm;
        public char[] Fictions => fictionsArray.ToArray();
        public bool General
        {
            get
            {
                if (solutions.Contains('0'))
                {
                    return false;
                }
                return true;
            }
        }
        public string Sknf => sknf;
        public string Sdnf => sdnf;


        void SetPostfixForm()
        {
            Stack<char> st = new Stack<char>();
            Queue<char> qu = new Queue<char>();
            string result = "";

            for (int i = 0; i < dirtyInfix.Length; i++)
            {
                if ('A' <= dirtyInfix[i] && 'Z' >= dirtyInfix[i])
                {
                    qu.Enqueue(dirtyInfix[i]);
                    continue;
                }

                if (Consts.actions.Contains(dirtyInfix[i]))
                {
                    if (st.Count == 0 || st.Peek() == '(')
                    {
                        st.Push(dirtyInfix[i]);
                        continue;
                    }

                    if (Priority(dirtyInfix[i]) > Priority(st.Peek()))
                    {
                        st.Push(dirtyInfix[i]);
                        continue;
                    }
                    else
                    {
                        while (st.Count != 0 && ((Priority(st.Peek()) >= Priority(dirtyInfix[i])) || st.Peek() == '('))
                        {
                            qu.Enqueue(st.Pop());
                        }
                        st.Push(dirtyInfix[i]);
                        continue;
                    }
                }

                if (dirtyInfix[i] == '(')
                {
                    st.Push(dirtyInfix[i]);
                }

                if (dirtyInfix[i] == ')')
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

            postfix =  result;
        } 
        void SetInfixForm()
        {
            Stack<mystr> st = new Stack<mystr>();
            foreach (var ch in postfix)
            {
                if (ch >= 'A' && ch <= 'Z')
                {
                    st.Push(new mystr(ch.ToString(), 0));
                }
                else
                {
                    if (ch == '¬')
                    {
                        var el = st.Pop();
                        el.Value = ch + el.Value;
                        el.Priority = Priority(ch);
                        st.Push(el);
                        continue;
                    }
                    var el1 = st.Pop();
                    var el2 = st.Pop();

                    if (el1.Priority < Priority(ch) && el1.Priority != 0)
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
            clearInfix =  st.Pop().Value;
        }
        void SetVariables()
        {
            List<char> varList = new List<char>();
            for (int i = 0; i < dirtyInfix.Length; i++)
            {
                if (dirtyInfix[i] >= 'A' && dirtyInfix[i] <= 'Z' && !varList.Contains(dirtyInfix[i]))
                {
                    varList.Add(dirtyInfix[i]);
                }
            }
            varList.Sort();
            varArray =  varList;
            InicilizeNumeric(varList.Count);
        }
        void SetSolutions()
        {
            string[] numeric = Numeric;
            char[] res = new char[numeric.Length];

            for (int i = 0; i < numeric.Length; i++)
            {
                string currentForm = postfix;
                for (int j = 0; j < varArray.Count; j++)
                {
                    currentForm = currentForm.Replace(varArray[j], numeric[i][j]);
                }
                res[i] = Calculate(currentForm);
            }

            solutions =  res;

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
        }
        void SetFictitious()
        {
            List<char> res = new List<char>();
            for (int i = 0; i < varArray.Count; i++)
            {
                bool Fict = true;
                int quantityblocks = Convert.ToInt32(Math.Pow(2, i + 1));
                int lenghtblock = (int)Math.Pow(2,varArray.Count) / quantityblocks;

                for (int j = 0; j < quantityblocks / 2; j++)
                {
                    for (int k = 0; k < lenghtblock; k++)
                    {
                        if (solutions[j * lenghtblock * 2 + k] != solutions[j * lenghtblock * 2 + lenghtblock + k])
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
                    res.Add(varArray[i]);
                }
            }
            fictionsArray =  res;
        }
        void SetSwitch()
        {
            for (int i = 0; i < solutions.Length / 2; i++)
            {
                if (solutions[i] == solutions[solutions.Length - 1 - i])
                {
                    switchForm = false;
                }
            }
            switchForm =  true;
        }
        void SetSknfAndSdnf()
        {
            List<string> zero = new List<string>();
            List<string> one = new List<string>();
            for(int i = 0;i<solutions.Length;i++)
            {
                if(solutions[i] == '0')
                {
                    zero.Add(Numeric[i]);
                    continue;
                }
                one.Add(Numeric[i]);
            }

            if (zero.Count != 0)
            {
                for (int i = 0; i < zero.Count; i++)
                {
                    string part = "";
                    for (int j = 0; j < zero[i].Length; j++)
                    {
                        if (zero[i][j] == '0')
                        {
                            part += varArray[j];
                        }
                        else
                        {
                            part += "¬" + varArray[j];
                        }
                        part += '∨';
                    }
                    part = part.Substring(0, part.Length - 1);
                    if (zero.Count != 1)
                    {
                        part = '(' + part + ')';
                    }
                    sknf += part + '∧';
                }
                sknf = sknf.Substring(0, sknf.Length - 1);
            }

            if (one.Count != 0)
            {
                for (int i = 0; i < one.Count; i++)
                {
                    string part = "";
                    for (int j = 0; j < one[i].Length; j++)
                    {
                        if (one[i][j] == '1')
                        {
                            part += varArray[j];
                        }
                        else
                        {
                            part += "¬" + varArray[j];
                        }
                        part += '∧';
                    }
                    part = part.Substring(0, part.Length - 1);
                    if (one.Count != 1)
                    {
                        part = '(' + part + ')';
                    }
                    sdnf += part + '∨';
                }
                sdnf = sdnf.Substring(0, sdnf.Length - 1);
            }
        }
    }  

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


}
