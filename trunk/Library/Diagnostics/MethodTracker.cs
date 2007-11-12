using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace VS.Library.Diagnostics
{
    public class MethodTracker
    {
        public struct MethodHolder: IDisposable
        {
            private MethodBase method;
            public MethodBase Method
            {
                get { return this.method; }
            }

            public MethodHolder(MethodBase method)
            { 
                this.method = method;
            }

            #region IDisposable Members

            public void Dispose()
            {
                MethodTracker.Instance.DoMethodFinished(this.method);
                this.method = null;
            }
            #endregion
        }

        private static MethodTracker instance = new MethodTracker();
        public static MethodTracker Instance
        {
            get
            {
                return instance;
            }
        }

        public static MethodHolder Track(MethodBase method)
        {
            return Instance.DoTrack(method);
        }

        public MethodHolder DoTrack(MethodBase method)
        {
            DoMethodStarted(method);
            return new MethodHolder(method);
        }

        public struct MethodEventArgs
        {
            public MethodBase Method;
        }

        public event EventHandler MethodStarted;
        public event EventHandler MethodFinished;

        protected void DoMethodStarted(MethodBase method)
        {
            if (this.MethodStarted != null)
                MethodStarted(method, EventArgs.Empty);
        }

        protected void DoMethodFinished(MethodBase method)
        {
            if (this.MethodFinished != null)
                MethodFinished(method, EventArgs.Empty);
        }
    }
}
