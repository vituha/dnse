using System;
using VS.Library.Cache;
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
                for (int i = 0; i < 1000000; i++)
                {
                    s = CachedProp;
                }
                Console.WriteLine(s);
            }

            using (CodeSpy.DoSpy("'LOM' access"))
            {
                using (PropLom.Use())
                {
                    for (int i = 0; i < 1000000; i++)
                    {
                        s = PropLom.BeginAccess();
                        s = null;
                        PropLom.EndAccess();
                    }
                }
                Console.WriteLine(s);
            }

            Console.WriteLine("Getter called (times): " + getterCallCount.ToString());

            SimpleProfiler.Deactivate();

            
            // * This is experimental
            Console.WriteLine("Lazy action demo:");
            using (IAsyncAction indent = new LazyAction(Indent, UnIndent))
            {
                for (int i = 1; i < 25; i++)
                {
                    if (i % 10 == 0)
                    {
                        indent.Start();
                    }
                }
            }

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

        static string CachedProp2
        {
            get
            {
                return GetterCache.Get<string>(CalcCachedPropValue);
            }
        }



    }
}
