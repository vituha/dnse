using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using VS.Library.Generics.Diagnostics;

namespace Test
{
    public class StaticCodeBlockInfo
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

        public virtual bool IsAnonymous
        {
            get { return String.IsNullOrEmpty(this.tag) && this.method == null; }
        }

        public StaticCodeBlockInfo()
            : this(null, null)
        {
        }

        public StaticCodeBlockInfo(string tag)
            :this(null, tag)
        {
        }

        public StaticCodeBlockInfo(MethodBase method, string tag)
        {
            this.tag = tag;
            this.method = method;
        }

        public StaticCodeBlockInfo(MethodBase method)
            :this(method, null)
        {
        }
    }

    public class CodeBlockInfo: StaticCodeBlockInfo
    {
        private object instance;
        public object Instance
        {
            get { return Instance; }
        }

        public override bool IsAnonymous
        {
            get
            {
                return this.instance == null && base.IsAnonymous;
            }
        }

        public CodeBlockInfo(object instance, MethodBase method, string tag)
            : base(method, tag)
        {
            this.instance = instance;
        }
    }

    public class CodeTracker : CodeTracker<StaticCodeBlockInfo>
    { }
}
