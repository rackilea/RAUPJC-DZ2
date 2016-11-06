using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad3
{
    class Integers
    {
        static void Main(string[] args)
        {
            int[] integers = new[] { 1, 2, 2, 2, 3, 3, 4, 5 };
            String[] strings = integers.GroupBy(i => i).Select(i =>"Broj "+i.Key+" ponavlja se "+i.Count()+" puta.").ToArray();

            for (int i =0;i<strings.Length;++i)
            {
                Console.WriteLine("strings ["+i+"] = "+strings[i]);
            }
            // strings [0] = Broj 1 ponavlja se 1 puta
            // strings [1] = Broj 2 ponavlja se 3 puta 
            // strings [2] = Broj 3 ponavlja se 2 puta
            // strings [3] = Broj 4 ponavlja se 1 puta
            // strings [4] = Broj 5 ponavlja se 1 puta
        }
    }
}
