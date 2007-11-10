using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Diagnostics;

using VS.Library.Generics.Cache;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Trace.WriteLine("============= Tracing Started ===========");
            DoSomething();
            Trace.WriteLine("=============  Tracing Ended  ===========");

            LimitedCache<int, int> c1 = new LimitedCache<int, int>(20);
            for (int i = 0; i < 100; i++)
            {
                c1.Get(i, GetRandom);
                c1.Get(i % 10, GetRandom);
            }
            DumpCache<int, int>(c1);
        }

        static Random rnd = new Random();
        static int GetRandom()
        {
            return rnd.Next();
        }

        private static void DoSomething()
        {
            using (MethodTraceImp trace = MethodTraceImp.Monitor(MethodBase.GetCurrentMethod()))
            {
                Trace.WriteLine("Inside DoSomething()");
            }
        }

        private static void DumpCache<TKey, TValue>(ICache<TKey, TValue> cache)
        {
            Trace.WriteLine("====== Cache contents =======");
            foreach (TKey key in cache.Keys)
            {
                TValue value;
                cache.TryGetValue(key, out value);
                Trace.WriteLine(String.Format("Key: {0}, Value: {1}", key, value));
            }
        }
    }
}
