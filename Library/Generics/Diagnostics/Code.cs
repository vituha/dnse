using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace VS.Library.Generics.Diagnostics
{
    public class CodeEventArgs<TContext>: EventArgs
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
        private TContext context;

        public TContext Context
        {
            get { return context; }
        }

        public CodeEventArgs(object instance, MethodBase method, TContext context)
        {
            this.instance = instance;
            this.method = method;
            this.context = context;
        }
    }

    public class Code<TContext> where TContext: class
    {
        internal class Pin : CodeEventArgs<TContext>, IDisposable
        {
            private bool disposed = false;
            public Pin(object instance, MethodBase method, TContext context)
                : base(instance, method, context)
            {
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

        private static Code<TContext> instance = new Code<TContext>();
        public static Code<TContext> Instance
        {
            get { return Code<TContext>.instance; }
        }

        public static IDisposable Track()
        {
            return instance.DoTrack(new Pin(null, null, null));
        }

        public static IDisposable Track(TContext context)
        {
            return instance.DoTrack(new Pin(null, null, context));
        }

        public static IDisposable Track(MethodBase method, TContext context)
        {
            return instance.DoTrack(new Pin(null, method, context));
        }

        public static IDisposable Track(object _instance, MethodBase method, TContext context)
        {
            return instance.DoTrack(new Pin(_instance, method, context));
        }

        public delegate void CodeTrackerEventHandler(int blockId, CodeEventArgs<TContext> args);
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
                CodeBlockEnter(pin.GetHashCode(), pin);
        }

        private void DoMethodFinished(Pin pin)
        {
            if (this.CodeBlockExit != null)
                CodeBlockExit(pin.GetHashCode(), pin);
        }
    }
}
