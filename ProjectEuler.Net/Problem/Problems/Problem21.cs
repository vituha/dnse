using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem.Problems
{
    /*
     
Let d(n) be defined as the sum of proper divisors of n (numbers less than n which divide evenly into n).
If d(a) = b and d(b) = a, where a ≠ b, then a and b are an amicable pair and each of a and b are called amicable numbers.

For example, the proper divisors of 220 are 1, 2, 4, 5, 10, 11, 20, 22, 44, 55 and 110; therefore d(220) = 284. The proper divisors of 284 are 1, 2, 4, 71 and 142; so d(284) = 220.

Evaluate the sum of all the amicable numbers under 10000.
     
     */
    internal sealed class Problem21 : ProblemBase
    {
        public override void Run()
        {
            RunInternal(10000);
        }

        private void RunInternal(int n)
        {
            var primes = new List<int>();

            var sums = new List<int> {0, 0};

            for (int i = 2; i < n; i++)
            {
                sums.Add(CalcSum(primes, i));
            }

            int totalSum = 0;
            for (int i = 0; i < sums.Count; i++)
            {
                int sum = sums[i];
                if (sum < n && sum != i && sums[sum] == i)
                {
                    totalSum += i;
                }
            }

            Console.WriteLine(totalSum);
        }

        private int CalcSum(List<int> primes, int number)
        {
            var primeCounts = new List<KeyValuePair<int, int>>();
            bool isPrime = true;

            int current = number;
            foreach (int prime in primes)
            {
                int cnt = 0;
                while ((current % prime) == 0)
                {
                    cnt++;
                    current /= prime;
                }
                if (cnt > 0)
                {
                    primeCounts.Add(new KeyValuePair<int, int>(prime, cnt));
                    isPrime = false;
                }
            }

            if (isPrime)
            {
                primes.Add(number);
                return 1;
            }

            List<int> takenCount = Enumerable.Range(0, primeCounts.Count).Select(i => primeCounts[i].Value).ToList();
            List<int> multipliers = Enumerable.Range(0, primeCounts.Count).Select(i => 1).ToList();

            int sum = 1;
            while (true)
            {
                Decrement(takenCount, primeCounts, multipliers);

                int divider = 1;
                foreach (int multiplier in multipliers)
                {
                    divider *= multiplier;
                }

                if (divider == number)
                {
                    break;
                }

                sum += divider;
            }

            return sum;
        }

        private void Decrement(List<int> takenCount, List<KeyValuePair<int, int>> primeCounts, List<int> multipliers)
        {
            for (int i = 0; i < takenCount.Count; i++)
            {
                if (takenCount[i] > 0)
                {
                    takenCount[i]--;
                    multipliers[i] *= primeCounts[i].Key;
                    break;
                }

                takenCount[i] = primeCounts[i].Value;
                multipliers[i] = 1;
            }
        }
    }
}
