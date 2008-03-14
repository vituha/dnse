using System;
using System.Reflection;
using VS.Library.Diagnostics;
using System.Text;
using VS.Library.Cache;

namespace CodeDemo
{
	public class Demo
	{
		private static void Main()
		{
			(new Demo()).Run();
			Console.ReadKey();
		}

		private Demo()
		{
			mySbHolder = new ObjectHolder<LockHolderMock>(delegate {return new LockHolderMock();});
		}

		private void Run()
		{
			CodeSpyDemo();
			//sfgjsfhgfjsagfk
			LockHolderDemo();
		}

		public class LockHolderMock
		{
			~ LockHolderMock()
			{
				Console.WriteLine("In mock destructor");
			}
		}

		private ObjectHolder<LockHolderMock> mySbHolder;
		public ObjectHolder<LockHolderMock> MySbHolder
		{
			get
			{
				return mySbHolder;
			}
		}

		private void LockHolderDemo()
		{
			MySbHolder.CacheHold();

			{
				LockHolderMock m1 = MySbHolder.Hold();
				Console.WriteLine("Entering sub");
				LockHolderDemoInt();
				Console.WriteLine("Exited sub");
				MySbHolder.Release();

				{
					LockHolderMock m3 = MySbHolder.Hold();
					MySbHolder.Release();
				};
			}

			MySbHolder.Release();

			Console.WriteLine(MySbHolder.RefCount.ToString());
		}

		private void LockHolderDemoInt()
		{
			LockHolderMock m2 = MySbHolder.Hold();
			MySbHolder.Release();
		}

		private void CodeSpyDemo()
		{
			Console.WriteLine(">>> CodeSpy Demo <<<");

			CodeSpy.Default.CodeBlockEnter += OnBlockStart;
			CodeSpy.Default.CodeBlockExit += OnBlockEnd;

			Console.WriteLine("\nLet's try a simple tracking first");
			using (CodeSpy.DoSpy("block 1"))
			{
				Console.WriteLine("This is a block 1");
			}

			Console.WriteLine("\nNow let's supply method parameter");
			using (CodeSpy.DoSpy(MethodBase.GetCurrentMethod(), "block 2"))
			{
				Console.WriteLine("This is a block 2");
			}

			Console.WriteLine("\nNow let's also add instance parameter");
			using (CodeSpy.DoSpy(this, MethodBase.GetCurrentMethod(), "block 3"))
			{
				Console.WriteLine("This is a block 3");
			}

			CodeSpy.Default.CodeBlockEnter -= OnBlockStart;
			CodeSpy.Default.CodeBlockExit -= OnBlockEnd;
		}

		private void OnBlockStart(object context, CodeSpyEventArgs args)
		{
			Console.WriteLine("Entered block {0}",
				FormatBlockName(args.BlockId, args.Instance, args.Method, (string)context));
		}

		private void OnBlockEnd(object context, CodeSpyEventArgs args)
		{
			Console.WriteLine("Exited block {0}",
				FormatBlockName(args.BlockId, args.Instance, args.Method, (string)context));
		}
		private string FormatBlockName(int blockId, object instance, MethodBase method, string context)
		{
			return String.Format(
					"{0}, instance={1}, method={2}, context={3}",
					blockId,
					instance == null ? "null" : instance.ToString(),
					method == null ? "null" : method.DeclaringType.Name + "." + method.Name,
					String.IsNullOrEmpty(context) ? "<empty>" : context
				);
		}
	}
}
