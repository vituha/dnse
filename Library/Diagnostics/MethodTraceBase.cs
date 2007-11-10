using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using System.Collections;

using VS.Library.Generics.Common;
using VS.Library.Generics.Cache;

namespace VS.Library.Diagnostics
{
    public abstract class MethodTraceBase: IDisposable
    {
        private MethodBase method;
        public MethodBase Method
        {
            get { return this.method; }
        }

        protected MethodTraceBase(MethodBase method)
        {
            this.method = method;
            TraceMethodBegin();
        }

        protected abstract void TraceMethodBegin();

        protected abstract void TraceMethodEnd();

        #region IDisposable Members

        public void Dispose()
        {
            TraceMethodEnd();
            this.method = null;
        }

        #endregion
    }
}
