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
				//using (PropAccessor.Use())
				PropLom.Ensure();
				{
					for (int i = 0; i < 1000000; i++)
					{
						s = PropLom.Access();
						PropLom.Release();
					}
				}
				PropLom.Release();
				Console.WriteLine(s);
			}

			Console.WriteLine("Getter called (times): " + getterCallCount.ToString());

			SimpleProfiler.Deactivate();

			using (IAsyncAction indent = new LazyAction(Indent, UnIndent))
			{
				for (int i = 1; i < 5; i++)
				{
					if (i % 10 == 0)
					{
						indent.StartAction();
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
