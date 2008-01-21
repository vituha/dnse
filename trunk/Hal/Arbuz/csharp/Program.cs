using System;
using System.Collections.Generic;
using System.Text;

namespace csharp
{
    class Program
    {
        const int n = 10;
        const int w = 60;
        int[] b = new int[n] { 17, 20, 20, 20, 15, 17, 20, 20, 20, 15 };
        int[] c = new int[n] { 35, 10, 30, 15, 25, 35, 10, 30, 15, 25 };

        static void Main(string[] args)
        {
            bool[] solution = new bool[n];

            Program p = new Program();
            p.Solve(solution);

            StringBuilder sb = new StringBuilder();

            for(int i = 0; i < solution.Length; i++)
            {
                bool f = solution[i];
                if (f)
                {
                    sb.Append(i.ToString() + ",");
                }
            }
            Console.WriteLine(sb.ToString());
            Console.WriteLine(p.p);
            Console.ReadKey();
        }

        bool[] solution = new bool[n];
        int p;

        private void Solve(bool[] solution)
        {
            callsCnt = 0;
            p = P(n - 1, w, solution);
            Console.WriteLine("Calls count: " + callsCnt.ToString());
        }

        private static int callsCnt;

        private int P(int i, int w, bool[] sol)
        {
            callsCnt++;
            if (i >= 0 && w > 0)
            {
                int p1, p2;
                bool[] sub_sol1 = new bool[i], sub_sol2 = new bool[i];

                p1 = P(i - 1, w, sub_sol1);
                if (w >= b[i])
                {
                    p2 = P(i - 1, w - b[i], sub_sol2) + c[i];
                }
                else p2 = -1;

                if (p2 > p1)
                {
                    sol[i] = true;
                    sub_sol2.CopyTo(sol, 0);
                    return p2;
                }
                else
                {
                    sol[i] = false;
                    sub_sol1.CopyTo(sol, 0);
                    return p1;
                }
            }
            else
            {
               return 0;
            }
        }


    }
}
