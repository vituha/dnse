using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using VS.Library.Generics.Diagnostics;

namespace Test
{
    public class CodeBlockInfo
    {
        private string tag;
        public string Tag
        {
            get { return tag; }
        }

        private MethodBase method;
        public MethodBase Method
        {
            get { return this.method; }
        }

        public CodeBlockInfo()
        {
            this.tag = String.Empty;
            this.method = null;
        }

        public CodeBlockInfo(string tag)
        {
            this.tag = tag;
            this.method = null;
        }

        public CodeBlockInfo(MethodBase method, string tag)
        {
            this.tag = tag;
            this.method = method;
        }

        public CodeBlockInfo(MethodBase method)
        {
            this.tag = String.Empty;
            this.method = method;
        }
    }

    public class InstanceCodeBlockInfo: CodeBlockInfo
    {
        private object instance;
        public object Instance
        {
            get { return Instance; }
        }

        public InstanceCodeBlockInfo(object instance, MethodBase method, string tag)
            : base(method, tag)
        {
            this.instance = instance;
        }
    }

    public class CodeTracker : CodeTracker<CodeBlockInfo>
    { }
}
