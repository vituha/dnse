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
            CodeTracer.Activate();

            MethodTrackDemo();
            PopulateAndDisplayCache();

            CodeTracer.DeActivate();
        }

        private static void MethodTrackDemo()
        {
#if DEBUG
            using (CodeTracker.Track(new CodeBlockInfo(MethodBase.GetCurrentMethod(), "first block")))
#endif
            {
                Trace.WriteLine("Inside 'MethodTrackDemo'");
            }
        }

        static Random rnd = new Random();
        static int GetRandom()
        {
            return rnd.Next();
        }

        private static void PopulateAndDisplayCache()
        {
            LimitedCache<int, int> c1 = new LimitedCache<int, int>(20);
            for (int i = 0; i < 100; i++)
            {
                c1.Get(i, GetRandom);
                c1.Get(i % 10, GetRandom);
            }
            DumpCache<int, int>(c1);
        }


        private static void DumpCache<TKey, TValue>(ICache<TKey, TValue> cache)
        {
            Trace.WriteLine("====== Cache contents =======");
            using (CodeTracker.Track())
            {
                foreach (TKey key in cache.Keys)
                {
                    TValue value;
                    cache.TryGetValue(key, out value);
                    Trace.WriteLine(String.Format("Key: {0}, Value: {1}", key, value));
                }
            }
        }
    }
}
