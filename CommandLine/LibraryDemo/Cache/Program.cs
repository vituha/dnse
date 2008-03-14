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

			using (CodeSpy.DoSpy("'accessed' access"))
			{
				//using (PropAccessor.Use())
				PropAccessor.BeginCache();
				{
					for (int i = 0; i < 1000000; i++)
					{
						s = PropAccessor.GetLock();
						PropAccessor.UnLock();
					}
				}
				PropAccessor.EndCache();
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

		static Accessor<string> PropAccessor
		{
			get
			{
				return accessor;
			}
		}
		static private Accessor<string> accessor = new Accessor<string>(CalcCachedPropValue);  

	}
}
