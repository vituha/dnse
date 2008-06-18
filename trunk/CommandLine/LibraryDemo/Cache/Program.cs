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
            SimpleProfiler.Activate();
            string s = String.Empty;

            using (CodeSpy.DoSpy("normal access"))
            {
                for (int i = 0; i < 1000000; i++)
                {
                    s = NonCachedProp;
                }
                Console.WriteLine(s);
            }

            using (CodeSpy.DoSpy("cached access"))
            {
                for (int i = 0; i < 10000000; i++)
                {
                    s = CachedProp;
                }
                Console.WriteLine(s);
            }

            using (CodeSpy.DoSpy("'LOM' access"))
            {
                using (PropLom.Use())
                {
                    for (int i = 0; i < 10000000; i++)
                    {
                        s = PropLom.BeginAccess();
                        s = null;
                        PropLom.EndAccess();
                    }
                }
                Console.WriteLine(s);
            }

            using (CodeSpy.DoSpy("'LazyValue' access"))
            {
                using (new ActivatorUser(PropLazyValue))
                {
                    for (int i = 0; i < 10000000; i++)
                    {
                        PropLazyValue.Activate();
                        s = PropLazyValue.Value;
                        PropLazyValue.Deactivate();
                    }
                }
                Console.WriteLine(s);
            }

            Console.WriteLine("Getter called (times): " + getterCallCount.ToString());

            SimpleProfiler.Deactivate();

            Console.ReadKey();
        }

        static void Indent()
        {
            Console.WriteLine("==>>");
        }

        static void UnIndent()
        {
            Console.WriteLine("<<==");
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

        static LoManager<string> PropLom
        {
            get
            {
                return propLom;
            }
        }
        static private LoManager<string> propLom = new LoManager<string>(CalcCachedPropValue);

        static LazyValue<string> PropLazyValue
        {
            get
            {
                return propLazyValue;
            }
        }
        static private LazyValue<string> propLazyValue = new LazyValue<string>(CalcCachedPropValue);


        static string CachedProp2
        {
            get
            {
                return GetterCache.Get<string>(CalcCachedPropValue);
            }
        }



    }
}
