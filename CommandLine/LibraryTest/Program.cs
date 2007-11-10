using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Diagnostics;

using VS.Library.Diagnostics;
using VS.Library.Cache;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Trace.WriteLine("============= Tracing Started ===========");
            DoSomething();
            Trace.WriteLine("=============  Tracing Ended  ===========");

            CacheBase c1 = new LimitedCache(10);
            for (int i = 0; i < 100; i++)
            {
                c1.Get<int>(GetRandom);
            }
        }

        static Random rnd = new Random();
        static int GetRandom()
        {
            return rnd.Next();
        }

        private static void DoSomething()
        {
            using (MethodTrace trace = MethodTrace.Monitor(MethodBase.GetCurrentMethod()))
            {
                Trace.WriteLine("Inside DoSomething()");
            }
        }
    }
}
