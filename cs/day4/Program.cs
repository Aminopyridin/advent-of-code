using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace day4
{
    class Program
    {
        static void Main(string[] args)
        {
            var min = 138307;
            var max = 654504;
            
            FirstPart(min, max);
            SecondPart(min, max);
        }

        private static void FirstPart(int min, int max)
        {
            var counter = 0;
            for (int i = min; i < max; i++)
            {
                if (CheckNumber(i))
                {
                    counter++;
                }
            }
            
            Console.WriteLine(counter);
        }
        
        private static void SecondPart(int min, int max)
        {
            var counter = 0;
            for (int i = min; i < max; i++)
            {
                if (CheckNumberStrictly(i))
                {
                    counter++;
                }
            }
            
            Console.WriteLine(counter);
        }

        private static bool CheckNumber(int num)
        {
            var doubleSymbols = new Regex(@"(\w)\1");
            var strI = num.ToString();
            if (!doubleSymbols.IsMatch(strI))
            {
                return false;
            }

            for (int i = 0; i < strI.Length - 1; i++)
            {
                var char1 = strI[i];
                var char2 = strI[i + 1];
                if (char1 > char2)
                {
                    return false;
                }
            }

            return true;
        }
        
        private static bool CheckNumberStrictly(int num)
        {
            var doubleSymbols = new Regex(@"(\w)\1");
            var strI = num.ToString();
            if (!doubleSymbols.IsMatch(strI)) return false;

            var tripleSymbols = new Regex(@"(\w)\1\1");
            if (tripleSymbols.IsMatch(strI) && CheckBiggerGroups(strI)) return false;

            for (int i = 0; i < strI.Length - 1; i++)
            {
                var char1 = strI[i];
                var char2 = strI[i + 1];
                if (char1 > char2) return false;
            }

            return true;
        }

        private static bool CheckBiggerGroups(string stringNum)
        {
            var doubleSymbols = new Regex(@"(\w)\1");
            var tripleSymbols = new Regex(@"(\w)\1\1");

            var doubleMatch = doubleSymbols.Matches(stringNum);
            var tripleMatch = tripleSymbols.Matches(stringNum);

            if (doubleMatch.Count == tripleMatch.Count) return true;

            var counter = new int[10]; 

            foreach (var symbol in stringNum)
            {
                var num = int.Parse(symbol.ToString());
                counter[num]++;
            }

            if (!counter.Contains(2)) return true;

            return false;
        }
        
    }
}