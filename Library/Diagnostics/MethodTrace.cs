using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using System.Collections;

using VS.Library.Cache;

namespace VS.Library.Diagnostics
{
    public class MethodTrace: IDisposable
    {
        public static MethodTrace Monitor(MethodBase method)
        {
#if DEBUG
            return new MethodTrace(method);
#else
            return null;
#endif
        }

        private MethodBase method;

        private MethodTrace(MethodBase method) 
        {
            this.method = method;
            TraceMethodBegin();
        }

        #region PermanentCache
        private CacheBase cache = new LimitedCache(2);
        #endregion

        protected virtual string GetFormatedMethodName()
        {
            return (
                this.method == null
                    ? "<unknown method>"
                    : String.Format("{0}.{1}()", this.method.DeclaringType.Name, this.method.Name)
                    );
        }

        protected virtual string FormatedMethodName
        {
            get
            {
                return this.cache.Get<string>(GetFormatedMethodName);
            }
        }

        protected virtual void TraceMethodBegin()
        {
            Trace.WriteLine(this.FormatedMethodName + " Begin");
        }

        protected virtual void TraceMethodEnd()
        {
            Trace.WriteLine(this.FormatedMethodName + " End");
        }


        #region IDisposable Members

        public void Dispose()
        {
            TraceMethodEnd();
            this.method = null;
        }

        #endregion
    }
}
