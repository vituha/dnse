using System;

namespace Problem.Problems
{
    internal sealed class Problem28 : ProblemBase
    {
        public override void Run()
        {
            RunInternal(1001);
        }

        private void RunInternal(int max)
        {
            int sum = 1;
            int adder = 1;
            int round = 0;

            while (++round * 2 < max)
            {
                for (int i = 0; i < 4; i++)
                {
                    adder += round * 2;
                    sum += adder;
                }
            }

            Console.WriteLine(sum);
        }
    }
}
