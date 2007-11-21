using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using VS.Library.Diagnostics;

namespace CodeDemo
{
    public class Demo
    {
        private static void Main()
        {
            (new Demo()).Run();
            Console.ReadKey();
        }

        private void Run()
        {
            Code.Instance.CodeBlockEnter += OnBlockStart;
            Code.Instance.CodeBlockExit += OnBlockEnd;

            Console.WriteLine("\nLet's try a simple tracking first");
            using (Code.Track("block 1"))
            {
                Console.WriteLine("This is a block 1");
            }

            Console.WriteLine("\nNow let's supply method parameter");
            using (Code.Track(MethodBase.GetCurrentMethod(), "block 2"))
            {
                Console.WriteLine("This is a block 2");
            }

            Console.WriteLine("\nNow let's also add instance parameter");
            using (Code.Track(this, MethodBase.GetCurrentMethod(), "block 3"))
            {
                Console.WriteLine("This is a block 3");
            }
       
        }

        private void OnBlockStart(object context, CodeEventArgs args)
        {
            Console.WriteLine("Entered block {0}",
                FormatBlockName(args.BlockId, args.Instance, args.Method, (string)context));
        }

        private void OnBlockEnd(object context, CodeEventArgs args)
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