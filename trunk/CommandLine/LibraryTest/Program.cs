using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Diagnostics;

using VS.Library.Generics.Cache;
using VS.Library.Generics.Comparison;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            CodeTracer.Activate();

            MethodTrackDemo();
            PopulateAndDisplayCache();
            TestComparison();

            CodeTracer.DeActivate();
        }

        private static void MethodTrackDemo()
        {
#if DEBUG
            using (SimpleCodeTracker.Track(MethodBase.GetCurrentMethod(), "main method block"))
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
            LimitedCache<int, int> c1 = new LimitedCache<int, int>(10);
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
            using (SimpleCodeTracker.Track())
            {
                foreach (TKey key in cache.Keys)
                {
                    TValue value;
                    cache.TryGetValue(key, out value);
                    Trace.WriteLine(String.Format("Key: {0}, Value: {1}", key, value));
                }
            }
        }

        private static void TestComparison()
        {
            List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>();
            list.Add( new KeyValuePair<int,string>(1, "Washington"));
            list.Add( new KeyValuePair<int,string>(2, "Texas"));
            list.Add( new KeyValuePair<int,string>(3, "Ogayo"));
            list.Add( new KeyValuePair<int,string>(4, "Alaska"));
            list.Add( new KeyValuePair<int,string>(5, "California"));

            List<SortKey<KeyValuePair<int, string>>> keys = new List<SortKey<KeyValuePair<int, string>>>();
            keys.Add(
                new SortKey<KeyValuePair<int, string>>(
                    delegate(KeyValuePair<int, string> item) { return item.Key; }, false
                )
            );
            ComplexComparer<KeyValuePair<int, string>> comparer = new ComplexComparer<KeyValuePair<int, string>>();
            comparer.Keys = keys;
            list.Sort(comparer);

            foreach (KeyValuePair<int, string> pair in list)
            {
                Trace.WriteLine(pair.Key);
            }
        }
    }
}
