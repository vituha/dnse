using System;
using VS.Library.Cache;
using VS.Library.Diagnostics;

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

			SimpleProfiler.Deactivate();

			Console.ReadKey();
		}

		static string CalcCachedPropValue()
		{
			string res = String.Format("Hello, {0} and {1}!", "World", "All");
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
