namespace Reverse_Polish_Notation
{
    public static class Logic
    {
        public static char[] to_postfix(string infix_exp)
        {
            int size_infix_exp = infix_exp.Length * 2;
            var postfix_exp = new char[size_infix_exp];
            int n = 0;

            var stack_c = new Stack<char>();

            for (int i = 0; i < infix_exp.Length; i++)
            {
                char c = infix_exp[i];

                if (Char.IsDigit(c) || c == 'x')
                {
                    string temp = GetStringNumber(infix_exp.ToCharArray(), ref i);

                    for (int j = 0; j < temp.Length; j++)
                    {
                        postfix_exp[n] = temp[j];
                        n++;
                    }

                    postfix_exp[n] = ' ';
                    n++;
                } 
                else if (c == '(')
                {
                    stack_c.Push(c);
                }
                else if (c == ')')
                {
                    while (stack_c.Count > 0)
                    {
                        char temp = stack_c.Peek();
                        if (temp == '(')
                        {
                            stack_c.Pop();
                            break;
                        }
                        postfix_exp[n] = stack_c.Pop();
                        n++;
                        postfix_exp[n] = ' ';
                        n++;
                    }
                }
                else if (operation_priority(c) != -1)
                {
                    char op = c;
                    if (op == '-' && (i == 0 || (i > 1 && operation_priority(infix_exp[i-1]) != -1)))
                        op = '~';

                    char? next = null;
                    if (stack_c.Count > 0)
                    {
                        next = stack_c.Peek();
                    }

                    while (stack_c.Count > 0 && operation_priority((char)next) >= operation_priority(op))
                    {
                        postfix_exp[n] = stack_c.Pop();
                        n++;
                        postfix_exp[n] = ' ';
                        n++;
                        if (stack_c.Count > 0)
                            next = stack_c.Peek();
                    }

                    if (op == 'c' && infix_exp[i + 1] == 'o')
                        stack_c.Push('c');

                    else if (op == 'c' && infix_exp[i + 1] == 't')
                        stack_c.Push('g');

                    else if (op == 's' && infix_exp[i + 1] == 'q')
                        stack_c.Push('q');

                    else if (op == 'l')
                        stack_c.Push('l');
                    else
                        stack_c.Push(op);

                    if ((op == 's' && infix_exp[i + 1] == 'i') || op == 'c' || op == 't' || op == 'g')
                        i += 2;

                    else if (op == 's' && infix_exp[i + 1] == 'q')
                        i += 3;
                    else if (op == 'l')
                        i += 1;
                    
                }
                
            }

            while (stack_c.Count > 0)
            {
                postfix_exp[n] = stack_c.Pop();
                n++;
            }
            stack_c = null;

            return postfix_exp;
        }

        public static int operation_priority(char c)
        {
            int result = -1;

            if (c == '(')
                result = 0;
            if (c == '+' || c == '-')
                result = 1;
            if (c == '*' || c == '/')
                result = 2;
            if (c == '^' || c == 's' || c == 'c' || c == 't' || c == 'g' || c == 'q' || c == 'l')
                result = 3;
            if (c == '~')
                result = 4;

            return result;
        }

        public static double Dijkstra(char[] postfix_exp, double x)
        {
            var stack_d = new Stack<double>();
            int counter = 0;

            for (int i = 0; i < postfix_exp.Length; i++)
            {
                char c = postfix_exp[i];

                if (Char.IsDigit(c) || c == 'x')
                {
                    if (c != 'x')
                    {
                        string number = GetStringNumber(postfix_exp, ref i);
                        stack_d.Push(Convert.ToDouble(number));

                    } else
                    {
                        stack_d.Push(x);
                    }
                }

                else if (operation_priority(c) != -1)
                {
                    counter++;

                    if (c == '~')
                    {
                        double last = stack_d.Count > 0 ? stack_d.Pop() : 0;
                        stack_d.Push(Execute('-', 0, last));
                        continue;
                    }

                    double second = stack_d.Count > 0 ? stack_d.Pop() : 0;
                    double first = 0; 
                    if (stack_d.Count == 0)
                    {
                        first = 0;

                    } 
                    else if (c != 's' && c != 'c' && c != 't' && c != 'g' && c != 'l')
                    {
                        first = stack_d.Pop();
                    }

                    stack_d.Push(Execute(c, first, second));
                }
            }
            double result = stack_d.Count > 0 ? stack_d.Pop() : 0;
            stack_d = null;
            return result;
        }

        public static double Execute(char op, double first, double second)
        {
            double result = 0;

            if (op == '+')
            {
                result = first + second;
            }
            else if (op == '-')
            {
                result = first - second;
            }
            else if (op == '*')
            {
                result = first * second;
            }
            else if (op == '/')
            {
                result = first / second;
            }
            else if (op == '^')
            {
                result = Math.Pow(first, second);
            }
            else if (op == 's')
            {  
                result = Math.Sin(second);
            }
            else if (op == 'c')
            {  
                result = Math.Cos(second);
            }
            else if (op == 't')
            {  
                result = Math.Tan(second);
            }
            else if (op == 'g')
            {  
                result = Math.Cos(second) / Math.Sin(second);
            }
            else if (op == 'q')
            {  
                result = Math.Pow(second, 0.5);
            }
            else if (op == 'l')
            {  
                result = Math.Log(second);
            }

            return result;
        }

        public static string GetStringNumber(char[] exp, ref int pos)
        {
            string num = "";

            for (; pos < exp.Length; pos++)
            {
                char c = exp[pos];
                if (Char.IsDigit(c) || c == 'x')
                {
                    num += c;
                } else
                {
                    pos--;
                    break;
                }
            }
            return num;
        }
    }
}
