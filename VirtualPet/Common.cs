using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualPet
{
    internal class Common
    {

        public static void ChangeColor(int color)
        {
            var colors = (ConsoleColor)color;
            Console.ForegroundColor = colors;

        }

        public static void SetStringColor(string text,int color)
        {
            ChangeColor(color); Console.WriteLine(text);
            ChangeColor(5);

        }
        public static int Randomizer(int min, int max)
        {
            Random rng = new Random();
            int num = rng.Next(min, max);
            return num;


        }

    }
}
