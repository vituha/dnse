using System;
using System.Collections.Generic;
using System.Text;
using VS.Library.Generics.Cache;
using VS.Library.Diagnostics;
using VS.Library.Cache;

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
                for (int i = 0; i < 100000; i++)
                {
                    s = NonCachedProp;
                }
            }

            using (Code.Track("cached access"))
            {
                for (int i = 0; i < 100000; i++)
                {
                    s = CachedProp;
                }
            }

            SimpleProfiler.Deactivate();

            Console.ReadKey();
        }

        static string CalcCachedPropValue()
        {
            string res = "*";
            for (int i = 0; i < 10; i++)
			{
			    res += "*";
			}
            return res;
        }

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
    }
}
