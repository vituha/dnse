using System;
using System.Reflection;

namespace VS.Library.Diagnostics
{
    public class CodeSpyEventArgs: EventArgs
    {
        private object instance;

        public object Instance
        {
            get { return instance; }
        }
        private MethodBase method;

        public MethodBase Method
        {
            get { return method; }
        }
        private int blockId;

        public int BlockId
        {
            get { return blockId; }
        }

        internal CodeSpyEventArgs(object instance, MethodBase method, int blockId)
        {
            this.instance = instance;
            this.method = method;
            this.blockId = blockId;
        }
    }

    public class CodeSpy: CodeSpyBase
    {

        private static CodeSpy _default = new CodeSpy();
        public static CodeSpy Default
        {
            get { return _default; }
        }

        public static IDisposable DoSpy()
        {
            return (Default as CodeSpyBase).BeginSpy(null, null, null);
        }

        public static IDisposable DoSpy(object context)
        {
            return (Default as CodeSpyBase).BeginSpy(null, null, context);
        }

        public static IDisposable DoSpy(MethodBase method, object context)
        {
            return (Default as CodeSpyBase).BeginSpy(null, method, context);
        }

        public static IDisposable DoSpy(object instance, MethodBase method, object context)
        {
            return (Default as CodeSpyBase).BeginSpy(instance, method, context);
        }

        public delegate void CodeSpyEventHandler(object context, CodeSpyEventArgs args);
        public event CodeSpyEventHandler CodeBlockEnter;
        public event CodeSpyEventHandler CodeBlockExit;

        protected override void DoBlockEntered(Pin pin)
        {
            if (this.CodeBlockEnter != null)
                CodeBlockEnter(pin.Context, new CodeSpyEventArgs(pin.Instance, pin.Method, pin.GetHashCode()));
        }

        protected override void DoBlockExited(Pin pin)
        {
            if (this.CodeBlockExit != null)
                CodeBlockExit(pin.Context, new CodeSpyEventArgs(pin.Instance, pin.Method, pin.GetHashCode()));
        }
    }
}
