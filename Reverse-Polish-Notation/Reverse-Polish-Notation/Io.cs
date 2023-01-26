using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Reverse_Polish_Notation
{
    public static class Io
    {
        public static string Input()
        {
            string? buff = ReadLine();

            if (buff != null)
            {
                if (!Check(buff))
                    buff = null;
            }

            return buff;
        }

        public static bool Check(string buff)
        {
            bool flag = true;
            string alpha = "sincotargqlx()+-*/^.0123456789 ";

            for (int i = 0; i < buff.Length; i++)
            {
                if (!alpha.Contains(buff[i]))
                {
                    flag = false;
                    break;
                }
            }

            return flag;
        }

        public static void Fill(int screen_length, char[] post_exp)
        {
            int[] screen = new int[screen_length];

            for (int i = 0; i < screen_length; i++)
            {
                screen[i] = (int)Math.Round(12 * Logic.Dijkstra(post_exp, (4 * Math.PI / 79) * i) + 12);
            }
            Draw(screen);
        }

        public static void Draw(int[] screen)
        {
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 80; j++)
                {
                    if (screen[j] == i)
                        Write("*");
                    else
                        Write(".");
                }
                Write("\n");
            }
        }

    }
}
