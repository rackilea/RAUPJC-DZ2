using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad7
{
    class Program
    {
        static void Main(string[] args)
        {
            // Main method is the only method that
            // can ’t be marked with async .
            // What we are doing here is just a way for us to simulate
            // async - friendly environment you usually have with
            // other .NET application types ( like web apps , win apps etc .)
            // Ignore main method , you can just focus on LetsSayUserClickedAButtonOnGuiMethod() as a
            // first method in call hierarchy .
            var t = Task.Run(() => LetsSayUserClickedAButtonOnGuiMethod());
            }
            private static async void LetsSayUserClickedAButtonOnGuiMethod()
            {
                Console.WriteLine(await GetTheMagicNumber());
            }
            private static async Task<int> GetTheMagicNumber()
            {
                return await IKnowIGuyWhoKnowsAGuy();
            }
            private static async Task<int> IKnowIGuyWhoKnowsAGuy()
            {
                return await IKnowWhoKnowsThis(10) + await IKnowWhoKnowsThis(5);
            }
            private static async Task<int> IKnowWhoKnowsThis(int n)
            {
                return await FactorialDigitSumAsync(n);
            }




    public static async Task<int> FactorialDigitSumAsync(int x)
        {
            int fact = 1;
            int sum = 0;
            for (int i = 1; i <= x; i++)
            {
                fact *= i;
            }
            while (fact != 0)
            {
                sum += fact % 10;
                fact = fact / 10;
            }
            return sum;
        }
    }
  }
