using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace VS.Library.Diagnostics
{
    public sealed class CodeTracker
    {
        public struct CodeBlockInfo : IDisposable
        {
            private object instance;
            public object Instance 
            { 
                get { return Instance; } 
            }

            private MethodBase method;
            public MethodBase Method
            {
                get { return this.method; }
            }

            private string tag;
            public string Tag
            {
                get { return tag; }
            }


            public CodeBlockInfo(object instance, MethodBase method, string tag)
            {
                this.instance = instance;
                this.method = method;
                this.tag = tag;
            }

            #region IDisposable Members

            public void Dispose()
            {
                CodeTracker._instance.DoMethodFinished(this);
                this.method = null;
            }
            #endregion
        }

        private static CodeTracker _instance = new CodeTracker();
        public static CodeTracker Instance
        {
            get { return CodeTracker._instance; }
        }

        #region static Track overloads
        public static CodeBlockInfo Track(object instance)
        {
            return _instance.DoTrack(new CodeBlockInfo(instance, null, null));
        }

        public static CodeBlockInfo Track(object instance, MethodBase method)
        {
            return _instance.DoTrack(new CodeBlockInfo(instance, method, null));
        }

        public static CodeBlockInfo Track(object instance, MethodBase method, string tag)
        {
            return _instance.DoTrack(new CodeBlockInfo(instance, method, tag));
        }

        public static CodeBlockInfo Track(string tag)
        {
            return _instance.DoTrack(new CodeBlockInfo(null, null, tag));
        }

        public static CodeBlockInfo Track(MethodBase method)
        {
            return _instance.DoTrack(new CodeBlockInfo(null, method, null));
        }

        public static CodeBlockInfo Track(MethodBase method, string tag)
        {
            return _instance.DoTrack(new CodeBlockInfo(null, method, tag));
        }
        #endregion

        public event EventHandler CodeBlockEnter;
        public event EventHandler CodeBlockExit;

        private CodeBlockInfo DoTrack(CodeBlockInfo info)
        {
            DoMethodStarted(info);
            return info;
        }

        private void DoMethodStarted(CodeBlockInfo info)
        {
            if (this.CodeBlockEnter != null)
                CodeBlockEnter(info, EventArgs.Empty);
        }

        private void DoMethodFinished(CodeBlockInfo info)
        {
            if (this.CodeBlockExit != null)
                CodeBlockExit(info, EventArgs.Empty);
        }
    }
}
