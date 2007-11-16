using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using VS.Library.Generics.Diagnostics;
using System.Reflection;

namespace VS.Generics.UT.Diagnostics
{
    [TestFixture]
    public class Code
    {
        Dictionary<string, int> blocks = new Dictionary<string, int>();

        [SetUp]
        public void Init()
        {
            Code<string>.Instance.CodeBlockEnter += OnBlockStart;
            Code<string>.Instance.CodeBlockExit += OnBlockEnd;
        }

        [TearDown]
        public void DeInit()
        {
            Code<string>.Instance.CodeBlockEnter -= OnBlockStart;
            Code<string>.Instance.CodeBlockExit -= OnBlockEnd;
            this.blocks.Clear();
        }

        [Test]
        public void SimpleCodeBlock()
        {
            string blockName = "simple code block";
            blocks.Add(blockName, 0);
            using(Code<string>.Track(this, MethodBase.GetCurrentMethod(), blockName))
            {
                for (int i = 0; i < 1000; i++)
                {
                    int a = i * i;
                }
            }
            Assert.AreEqual(blocks[blockName], 2);
            blocks.Remove(blockName);
        }

        public void OnBlockStart(int blockId, CodeEventArgs<string> args)
        {
            blocks[args.Context]++;
            Console.WriteLine("Entered block {0}", 
                FormatBlockName(blockId, args.Instance, args.Method, args.Context));
        }

        public void OnBlockEnd(int blockId, CodeEventArgs<string> args)
        {
            blocks[args.Context]++;
            Console.WriteLine("Exited block {0}", 
                FormatBlockName(blockId, args.Instance, args.Method, args.Context));
        }

        public string FormatBlockName(int blockId, object instance, MethodBase method, string context)
        {
            return String.Format(
                    "{0}, instance={1}, method={2}, context={3}",
                    blockId,
                    instance == null ? "null" : instance.ToString(),
                    method == null ? "null" : method.ToString(),
                    String.IsNullOrEmpty(context) ? "<empty>" : context
                );
        }
    }
}
