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

			using (CodeSpy.DoSpy("'LOM' access"))
			{
				//using (PropAccessor.Use())
				PropLom.Ensure();
				{
					for (int i = 0; i < 1000000; i++)
					{
						s = PropLom.Get();
						PropLom.Release();
					}
				}
				PropLom.Release();
				Console.WriteLine(s);
			}

			Console.WriteLine("Getter called (times): " + getterCallCount.ToString());

			SimpleProfiler.Deactivate();

			Console.ReadKey();
		}

		static string CalcCachedPropValue()
		{
			getterCallCount++;
			return String.Format("Hello, {0} and {1} {2}!", "World", "All", "and everybody");
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

		static Lom<string> PropLom
		{
			get
			{
				return propLom;
			}
		}
		static private Lom<string> propLom = new Lom<string>(CalcCachedPropValue);  

	}
}
