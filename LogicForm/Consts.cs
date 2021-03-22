using System;


namespace LogicForm
{
    public static class Consts
    {
        public static readonly string actions = "¬∧∨⊕⇒⇿";
        public static readonly char uno = '¬';
        public static readonly string binary = "∧∨⊕⇒⇿";
        public static readonly string abc = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static readonly string allSimbols = "ABC¬∧∨()⊕⇒⇿DEFGHIJKLMNOPQRSTUVWXYZ";
        public static string[] Numeric { get; private set; }
        public static void InicilizeNumeric(int variables)
        {
            string[] numeric = new string[(int)Math.Pow(2,variables)];
            for (int i = 0; i < numeric.Length; i++)
            {
                numeric[i] = Convert.ToString(i, 2);
                while (numeric[i].Length != variables)
                {
                    numeric[i] = '0' + numeric[i];
                }
            }
            Numeric = numeric;
        }
        public static bool CTB(char a)
        {
            if (a == '0')
            {
                return false;
            }

            return true;
        }// char to bool
        public static char BTC(bool a)
        {
            if (a) { return '1'; }
            else { return '0'; }
        } // bool to char
        public static int Priority(char oper)
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

    }
}
