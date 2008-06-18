using System;
using System.Reflection;
using VS.Library.Diagnostics;
using System.Text;
using VS.Library.Pattern.Lifetime;

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
			mySbHolder = new LoManager<LockHolderMock>(delegate {return new LockHolderMock();});
		}

		private void Run()
		{
			CodeSpyDemo();
			//sfgjsfhgfjsagfk
			LockHolderDemo();
		}

		public class LockHolderMock
		{
			~LockHolderMock()
			{
				Console.WriteLine("In mock destructor");
			}
		}

		private LoManager<LockHolderMock> mySbHolder;
		public LoManager<LockHolderMock> MySbHolder
		{
			get
			{
				return mySbHolder;
			}
		}

		private void LockHolderDemo()
		{
			MySbHolder.Preserve();

			{
				LockHolderMock m1 = MySbHolder.BeginAccess();
				Console.WriteLine("Entering sub");
				LockHolderDemoInt();
				Console.WriteLine("Exited sub");
				MySbHolder.EndAccess();

				using(MySbHolder.Use())
				{
					LockHolderMock m3 = MySbHolder.BeginAccess();
					MySbHolder.EndAccess();
				};
			}

			MySbHolder.EndAccess();

			Console.WriteLine(MySbHolder.RefCount.ToString());
		}

		private void LockHolderDemoInt()
		{
			LockHolderMock m2 = MySbHolder.BeginAccess();
			MySbHolder.EndAccess();
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
