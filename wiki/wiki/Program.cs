using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wiki
{
    class Program
    {
        static void Main(string[] args) {
            Console.SetWindowSize(80, 22);
            string nev = "kkzs";
            string parancs;
            do
            {
                Console.Clear();
                Console.WriteLine(nev);

                for (int i = 0; i < 20; i++)
                {
                    Console.WriteLine();
                }

                parancs = Console.ReadLine();
                //Console.CursorTop--;
            } while (parancs != "q");
        }
    }
}
