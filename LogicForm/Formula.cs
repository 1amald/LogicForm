using System;
using System.Collections.Generic;
using System.Linq;
using static LogicForm.Consts;

namespace LogicForm
{
    public class Formula
    {
        public string postfix;
        string dirtyInfix;
        string clearInfix;

        List<char> varArray = new List<char>(); 
        char[] solutions;

        List<char> fictionsArray = new List<char>();


        public Formula(string dirtyInfix)
        {
            varArray = GetVariables(dirtyInfix);
            this.dirtyInfix = dirtyInfix;
            postfix = ToPostfixForm(dirtyInfix);
            clearInfix = ToInfixForm(postfix);
            solutions = Solutions(postfix);
            fictionsArray = Fictitious(solutions);
        }

        string ToPostfixForm(string infix)
        {
            Stack<char> st = new Stack<char>();
            Queue<char> qu = new Queue<char>();
            string result = "";

            for (int i = 0; i < infix.Length; i++)
            {
                if ('A' <= infix[i] && 'Z' >= infix[i])
                {
                    qu.Enqueue(infix[i]);
                    continue;
                }

                if (Consts.actions.Contains(infix[i]))
                {
                    if (st.Count == 0 || st.Peek() == '(')
                    {
                        st.Push(infix[i]);
                        continue;
                    }

                    if (Priority(infix[i]) > Priority(st.Peek()))
                    {
                        st.Push(infix[i]);
                        continue;
                    }
                    else
                    {
                        while (st.Count != 0 && ((Priority(st.Peek()) >= Priority(infix[i])) || st.Peek() == '('))
                        {
                            qu.Enqueue(st.Pop());
                        }
                        st.Push(infix[i]);
                        continue;
                    }
                }

                if (infix[i] == '(')
                {
                    st.Push(infix[i]);
                }

                if (infix[i] == ')')
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
        string ToInfixForm(string postfixFormula)
        {
            Stack<mystr> st = new Stack<mystr>();
            foreach (var ch in postfixFormula)
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
            return st.Pop().Value;
        }
        List<char> GetVariables(string formula)
        {
            List<char> varList = new List<char>();
            for (int i = 0; i < formula.Length; i++)
            {
                if (formula[i] >= 'A' && formula[i] <= 'Z' && !varList.Contains(formula[i]))
                {
                    varList.Add(formula[i]);
                }
            }
            varList.Sort();
            return varList;
        }
        char[] Solutions(string post)
        {
            string[] numeric = Numeric(varArray.Count);
            char[] res = new char[numeric.Length];

            for (int i = 0; i < numeric.Length; i++)
            {
                string currentForm = post;
                for (int j = 0; j < varArray.Count; j++)
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
        List<char> Fictitious(char[] sol)
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
                        if (sol[j * lenghtblock * 2 + k] != sol[j * lenghtblock * 2 + lenghtblock + k])
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
            return res;
        }

        /*string CheckForSwitch()
        {
            for (int i = 0; i < rowsCount / 2; i++)
            {
                if (Convert.ToString(dg[varCount, i].Value) == Convert.ToString(dg[varCount, rowsCount - 1 - i].Value))
                {
                    return "На наборах противоположных значений переменных формула не принимает противоположное значение.";
                }
            }
            return "На наборах противоположных значений переменных формула принимает противоположное значение.";
        }*/
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
