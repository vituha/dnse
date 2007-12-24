using System;
using System.Reflection;

namespace VS.Library.Diagnostics
{
    public class CodeBlockSpyEventArgs: EventArgs
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

        internal CodeBlockSpyEventArgs(object instance, MethodBase method, int blockId)
        {
            this.instance = instance;
            this.method = method;
            this.blockId = blockId;
        }
    }

    public class CodeBlockSpy: CodeBlockSpyBase
    {

        private static CodeBlockSpy _default = new CodeBlockSpy();
        public static CodeBlockSpy Default
        {
            get { return _default; }
        }

        public static IDisposable DoSpy()
        {
            return (Default as CodeBlockSpyBase).DoSpy(null, null, null);
        }

        public static IDisposable DoSpy(object context)
        {
            return (Default as CodeBlockSpyBase).DoSpy(null, null, context);
        }

        public static IDisposable DoSpy(MethodBase method, object context)
        {
            return (Default as CodeBlockSpyBase).DoSpy(null, method, context);
        }

        public static IDisposable DoSpy(object instance, MethodBase method, object context)
        {
            return (Default as CodeBlockSpyBase).DoSpy(instance, method, context);
        }

        public delegate void CodeTrackerEventHandler(object context, CodeBlockSpyEventArgs args);
        public event CodeTrackerEventHandler CodeBlockEnter;
        public event CodeTrackerEventHandler CodeBlockExit;

        protected override void DoBlockEntered(Pin pin)
        {
            if (this.CodeBlockEnter != null)
                CodeBlockEnter(pin.Context, new CodeBlockSpyEventArgs(pin.Instance, pin.Method, pin.GetHashCode()));
        }

        protected override void DoBlockExited(Pin pin)
        {
            if (this.CodeBlockExit != null)
                CodeBlockExit(pin.Context, new CodeBlockSpyEventArgs(pin.Instance, pin.Method, pin.GetHashCode()));
        }
    }
}
