using System;
using System.Linq;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            string sym = "d0";
            string num = "1023";
            if (num.Contains(sym[0]))
            {
                Console.WriteLine("Empty");
            }

            Console.WriteLine(sym.Count('4'.Equals));
            Console.WriteLine();
        }
    }
}