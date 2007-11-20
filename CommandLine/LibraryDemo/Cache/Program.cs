using System;
using System.Collections.Generic;
using System.Text;
using VS.Library.Generics.Cache;
using VS.Library.Diagnostics;

namespace Cache
{
    class Program
    {
        static void Main(string[] args)
        {
            SimpleProfiler.Activate();
            string s;

            using (Code.Track("normal access"))
            {
                for (int i = 0; i < 1000; i++)
                {
                    s = NonCachedProp;
                }
            }

            using (Code.Track("cached access"))
            {
                for (int i = 0; i < 1000; i++)
                {
                    s = CachedProp;
                }
            }

            SimpleProfiler.Deactivate();

            Console.ReadKey();
        }

        static GetterCache<string> getterCache = new GetterCache<string>();

        static string CalcCachedPropValue()
        {
            string res = "*";
            for (int i = 0; i < 1000; i++)
			{
			    res += "*";
			}
            return res;
        }

        static string CachedProp
        {
            get
            {
                return getterCache.Get(CalcCachedPropValue);
            }
        }

        static string NonCachedProp
        {
            get
            {
                return CalcCachedPropValue();
            }
        }
    }
}
