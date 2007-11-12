using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace VS.Library.Generics.Diagnostics
{
    public class CodeTracker<TContext> where TContext: class
    {
        private struct Pin : IDisposable
        {
            public TContext context;

            public Pin(TContext context)
            {
                this.context = context;
            }

            #region IDisposable Members

            public void Dispose()
            {
                instance.DoMethodFinished(this);
                this.context = null;
            }
            #endregion
        }

        private static CodeTracker<TContext> instance = new CodeTracker<TContext>();
        public static CodeTracker<TContext> Instance
        {
            get { return CodeTracker<TContext>.instance; }
        }

        public static IDisposable Track(TContext context)
        {
            return instance.DoTrack(new Pin(context));
        }

        public static IDisposable Track()
        {
            return instance.DoTrack(new Pin(null));
        }

        public event EventHandler CodeBlockEnter;
        public event EventHandler CodeBlockExit;

        private IDisposable DoTrack(Pin pin)
        {
            DoMethodStarted(pin);
            return pin;
        }

        private void DoMethodStarted(Pin pin)
        {
            if (this.CodeBlockEnter != null)
                CodeBlockEnter(pin.context, EventArgs.Empty);
        }

        private void DoMethodFinished(Pin pin)
        {
            if (this.CodeBlockExit != null)
                CodeBlockExit(pin.context, EventArgs.Empty);
        }
    }
}
