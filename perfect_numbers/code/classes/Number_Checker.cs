using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace perfect_numbers
{
    class Number_Checker
    {
        public int Start { get; }
        public int End { get; }

        public Number_Checker(int start, int end)
        {
            Start = start;
            End = end;
        }

        //ITERATE THROUGH NUMBER BATCH
        public void Iterate(Action<List<int>> completeCallback)
        {
            var found = new List<int>();

            Parallel.For(Start, End + 1, i =>
            {
                var perfectNumber = IsPerfect(i);
                if (perfectNumber.HasValue)
                {
                    lock (found)
                    {
                        found.Add(perfectNumber.Value);
                    }
                }
            });

            completeCallback(found);
        }

        //CHECK IF CURRENT VALUE IS PERFECT NUMBER
        private static int? IsPerfect(int number)
        {
            if (number <= 1) return null;

            int sum = 1;
            int sqrt = (int)Math.Sqrt(number);

            for (int i = 2; i <= sqrt; i++)
            {
                if (number % i == 0)
                {
                    sum += i;
                    int complement = number / i;
                    if (complement != i)
                    {
                        sum += complement;
                    }
                }
            }

            return number == sum ? number : (int?)null;
        }
    }
}
