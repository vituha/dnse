using System;
using VS.Library.Pattern.Lifetime;
using VS.Library.Diagnostics;
using System.IO;

namespace Cache
{
    class Program
    {
        static void Main(string[] args)
        {
            int testCount = 5000000;
            SimpleProfiler.Activate();
            string s = String.Empty;

            using (CodeSpy.DoSpy("'normal' access"))
            {
                for (int i = 0; i < testCount; i++)
                {
                    s = NonCachedProp;
                }
                Console.WriteLine(s);
            }
            Console.WriteLine();

            using (CodeSpy.DoSpy("'cached' access"))
            {
                for (int i = 0; i < testCount; i++)
                {
                    s = CachedProp;
                }
                Console.WriteLine(s);
            }
            Console.WriteLine();

            using (CodeSpy.DoSpy("'LazyValue' access"))
            {
                PropLazyValue.Activate();
                for (int i = 0; i < testCount; i++)
                {
                    PropLazyValue.Activate();
                    s = PropLazyValue.Value;
                    PropLazyValue.Deactivate();
                }
                PropLazyValue.Deactivate();
                Console.WriteLine(s);
            }
            Console.WriteLine();

            Console.WriteLine("Getter called (times): " + getterCallCount.ToString());

            SimpleProfiler.Deactivate();

            Console.ReadKey();
        }

        static string CalcCachedPropValue()
        {
            getterCallCount++;
            return String.Format(Cache.Main.HelloMsg, "World", "All", "and Everybody");
        }
        private static int getterCallCount = 0;

        static string CachedProp
        {
            get
            {
                return GetterCache.Get<string>(CalcCachedPropValue);
            }
        }

        static string NonCachedProp
        {
            get
            {
                return CalcCachedPropValue();
            }
        }

        static LazyValue<string> PropLazyValue
        {
            get
            {
                return propLazyValue;
            }
        }
        static private LazyValue<string> propLazyValue = new LazyValue<string>(CalcCachedPropValue);
    }
}
