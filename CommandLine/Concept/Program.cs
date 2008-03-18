using System;
using System.Collections.Generic;
using System.Text;
using VS.Library.Cache;

namespace LomTest
{
	class Program
	{
		const int listSize = 1000000;

		static void Main(string[] args)
		{
			(new Program()).DoTest();
		}

		void DoTest()
		{
			// When times >= 20 the object is being used in sometimesUsingList

			Console.WriteLine();
			Console.WriteLine("Running non-using pattern: times = 5");
			TestCase(5);
			Console.WriteLine("Finished");

			Console.WriteLine();
			Console.WriteLine("Running using pattern (1 use): times = 15");
			TestCase(15);
			Console.WriteLine("Finished");

			Console.WriteLine();
			Console.WriteLine("Running using pattern (2 use): times = 25");
			TestCase(25);
			Console.WriteLine("Finished");

			Console.WriteLine();
			Console.WriteLine("Done.");
			Console.ReadKey();
		}

		void TestCase(int times)
		{
			listLom = null;
			ListLom.Ensure();
			notUsingList(times);
			ListLom.Release();
			Console.WriteLine("Collecting garbage");
			GC.Collect();
			GC.WaitForPendingFinalizers();
		}

		void sometimesUsingList(bool shouldUse)
		{
			if (shouldUse)
			{
				Console.WriteLine("Asking for LO");
				List<int> lst = ListLom.Get();
				lst[10] = lst[20];
				lst = null;
				ListLom.Release();
				Console.WriteLine("LO Released");
			}
		}

		void notUsingList(int times)
		{
			for (int i = 1; i < times; i++)
			{
				sometimesUsingList((i % 10) == 0);
			}
		}

		public Lom<MyList> ListLom
		{
			get
			{
				if (listLom == null)
				{
					listLom = new Lom<MyList>(CreateIntList);
				}
				return listLom;
			}
		}
		private Lom<MyList> listLom = null;

		MyList CreateIntList()
		{
			MyList result = new MyList();
			for (int i = 0; i < listSize; i++)
			{
				result.Add(i);
			}
			return result;
		}
	}

	public class MyList : List<int>
	{
		public MyList()
		{
			Console.WriteLine("LO instance created");
		}

		~MyList()
		{
			Console.WriteLine("LO instance destroyed");
		}
	}
}
