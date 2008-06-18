using System;
using System.Reflection;
using VS.Library.Diagnostics;

namespace Cache
{
	class SimpleProfiler
	{
		public static void Activate()
		{
			CodeSpy.Default.CodeBlockEnter += OnBlockStart;
			CodeSpy.Default.CodeBlockExit += OnBlockEnd;
		}

		public static void Deactivate()
		{
			CodeSpy.Default.CodeBlockEnter -= OnBlockStart;
			CodeSpy.Default.CodeBlockExit -= OnBlockEnd;
		}

		static long ticks;

		private static void OnBlockStart(object context, CodeSpyEventArgs args)
		{
			Console.WriteLine("Entered block {0}",
				FormatBlockName(args.BlockId, args.Instance, args.Method, (string)context));
			ticks = DateTime.Now.Ticks;
		}

		private static void OnBlockEnd(object context, CodeSpyEventArgs args)
		{
			ticks = DateTime.Now.Ticks - ticks;
			Console.WriteLine("Exited block {0}. Took {1} ms",
				FormatBlockName(args.BlockId, args.Instance, args.Method, (string)context), ticks / 10000.0);
		}
		private static string FormatBlockName(int blockId, object instance, MethodBase method, string context)
		{
			return String.Format(
					"{0}{1}{2}{3}",
					blockId,
					instance == null ? String.Empty : ", instance=" + instance.ToString(),
					method == null ? String.Empty : ", method=" + method.DeclaringType.Name + "." + method.Name,
                    String.IsNullOrEmpty(context) ? String.Empty : ", context=" + context
				);
		}
	}
}
