using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace VS.Library.Diagnostics
{
    public class CodeEventArgs: EventArgs
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

        internal CodeEventArgs(Code.Pin pin)
        {
            this.blockId = pin.GetHashCode();
            this.instance = pin.Instance;
            this.method = pin.Method;
        }
    }

    public class Code
    {
        internal class Pin : IDisposable
        {
            private object _instance;

            public object Instance
            {
                get { return _instance; }
            }
            private MethodBase method;

            public MethodBase Method
            {
                get { return method; }
            }
            private object context;

            public object Context
            {
                get { return context; }
            }
            
            private bool disposed = false;
            public Pin(object _instance, MethodBase method, object context)
            {
                this._instance = _instance;
                this.method = method;
                this.context = context;
            }

            #region IDisposable Members

            public void Dispose()
            {
                if (!disposed)
                {
                    instance.DoMethodFinished(this);
                    this.disposed = true;
                }
            }
            #endregion
        }

        private static Code instance = new Code();
        public static Code Instance
        {
            get { return Code.instance; }
        }

        public static IDisposable Track()
        {
            return instance.DoTrack(new Pin(null, null, null));
        }

        public static IDisposable Track(object context)
        {
            return instance.DoTrack(new Pin(null, null, context));
        }

        public static IDisposable Track(MethodBase method, object context)
        {
            return instance.DoTrack(new Pin(null, method, context));
        }

        public static IDisposable Track(object _instance, MethodBase method, object context)
        {
            return instance.DoTrack(new Pin(_instance, method, context));
        }

        public delegate void CodeTrackerEventHandler(object context, CodeEventArgs args);
        public event CodeTrackerEventHandler CodeBlockEnter;
        public event CodeTrackerEventHandler CodeBlockExit;

        private IDisposable DoTrack(Pin pin)
        {
            DoMethodStarted(pin);
            return pin;
        }

        private void DoMethodStarted(Pin pin)
        {
            if (this.CodeBlockEnter != null)
                CodeBlockEnter(pin.Context, new CodeEventArgs(pin));
        }

        private void DoMethodFinished(Pin pin)
        {
            if (this.CodeBlockExit != null)
                CodeBlockExit(pin.Context, new CodeEventArgs(pin));
        }
    }
}
