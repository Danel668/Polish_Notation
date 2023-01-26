using static System.Console;

namespace Reverse_Polish_Notation 
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string? expression = Io.Input();

            if (expression != null)
            {
                var post_exp = Logic.to_postfix(expression);

                int screen_length = 80;
                Io.Fill(screen_length, post_exp);
               
            } else
            {
                WriteLine("n/a");
            }
        }
    }
}


