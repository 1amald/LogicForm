using System;


namespace LogicForm
{
    public class Consts
    {
        public static string actions = "¬∧∨⊕⇒⇿";
        public static char uno = '¬';
        public static string binary = "∧∨⊕⇒⇿";
        public static string abc = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static string allSimbols = "ABC¬∧∨()⊕⇒⇿DEFGHIJKLMNOPQRSTUVWXYZ";

        public static string[] Numeric(int variables)
        {
            string[] numeric = new string[variables];
            for (int i = 0; i < variables; i++)
            {
                numeric[i] = Convert.ToString(i, 2);
                while (numeric[i].Length != variables)
                {
                    numeric[i] = '0' + numeric[i];
                }
            }
            return numeric;
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
