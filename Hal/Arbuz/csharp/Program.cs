using System;
using System.Collections.Generic;
using System.Text;

namespace csharp
{
    class Program
    {
        const int n = 5;
        const int w = 60;
        int[] b = new int[n] { 17, 20, 20, 20, 15 };
        int[] c = new int[n] { 35, 10, 30, 15, 25 };

        static void Main(string[] args)
        {
            bool[] solution = new bool[n];

            new Program().Solve(solution);

        }

        bool[] solution = new bool[n];
        int p;

        private void Solve(bool[] solution)
        {
            p = P(n - 1, w, solution);
        }

        private int P(int i, int w, bool[] sol)
        {
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
