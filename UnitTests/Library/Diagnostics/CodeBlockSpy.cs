using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Reflection;

using FixtNS = VS.Library.Diagnostics;

namespace VS.Library.UT.Diagnostics
{
    [TestFixture]
    public class CodeBlockSpy
    {
        Dictionary<string, int> blocks = new Dictionary<string, int>();

        [SetUp]
        public void Init()
        {
            FixtNS.CodeBlockSpy.Default.CodeBlockEnter += OnBlockStart;
            FixtNS.CodeBlockSpy.Default.CodeBlockExit += OnBlockEnd;
        }

        [TearDown]
        public void DeInit()
        {
            FixtNS.CodeBlockSpy.Default.CodeBlockEnter -= OnBlockStart;
            FixtNS.CodeBlockSpy.Default.CodeBlockExit -= OnBlockEnd;
            this.blocks.Clear();
        }

        [Test]
        public void SimpleCodeBlock()
        {
            string blockName = "simple code block";
            blocks.Add(blockName, 0);
            using (FixtNS.CodeBlockSpy.DoSpy(this, MethodBase.GetCurrentMethod(), blockName))
            {
                for (int i = 0; i < 1000; i++)
                {
                    int a = i * i;
                }
            }
            Assert.AreEqual(blocks[blockName], 2);
            blocks.Remove(blockName);
        }

        public void OnBlockStart(object context, FixtNS.CodeBlockSpyEventArgs args)
        {
            blocks[(string)context]++;
            Console.WriteLine("Entered block {0}", 
                FormatBlockName(args.BlockId, args.Instance, args.Method, (string)context));
        }

        public void OnBlockEnd(object context, FixtNS.CodeBlockSpyEventArgs args)
        {
            blocks[(string)context]++;
            Console.WriteLine("Exited block {0}", 
                FormatBlockName(args.BlockId, args.Instance, args.Method, (string)context));
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
