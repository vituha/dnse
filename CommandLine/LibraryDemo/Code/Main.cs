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
			mySb = new LazyValue<LockHolderMock>(delegate {return new LockHolderMock();});
		}

		private void Run()
		{
			CodeSpyDemo();
			//sfgjsfhgfjsagfk
			LazyValueDemo();
		}

		public class LockHolderMock
		{
            public LockHolderMock()
            {
                Console.WriteLine("In mock CONstructor");
            }

			~LockHolderMock()
			{
				Console.WriteLine("In mock DEstructor");
			}
		}

		private LazyValue<LockHolderMock> mySb;
		public LazyValue<LockHolderMock> MySb
		{
			get
			{
				return mySb;
			}
		}

		private void LazyValueDemo()
		{
            Console.WriteLine("\nLazyValueDemo begin");
			MySb.Lock();
			{
                MySb.Activate();
                Console.WriteLine("Accessing value for the first time");
                LockHolderMock m1 = MySb.Value;
				Console.WriteLine("Entering sub");
				LockHolderDemoInt();
				Console.WriteLine("Exited sub");
                m1 = null;
				MySb.Deactivate();

				using(new ActivatorUser(MySb))
				{
                    MySb.Activate();
                    LockHolderMock m3 = MySb.Value;
                    m3 = null;
					MySb.Deactivate();
				};
			}

			MySb.Unlock();
            Console.WriteLine("Calling garbage collection");
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Console.WriteLine("Garbage collected!");
            Console.WriteLine("LazyValueDemo end");
        }

		private void LockHolderDemoInt()
		{
            MySb.Activate();
            LockHolderMock m2 = MySb.Value;
			MySb.Deactivate();
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
