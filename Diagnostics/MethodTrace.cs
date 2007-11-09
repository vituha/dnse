using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using System.Collections;

namespace VS.Diagnostics
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
        private Dictionary<Delegate, object> cache = new Dictionary<Delegate, object>();

        protected delegate T ItemRetrieverDelegate<T>();
        protected T GetCachedItem<T>(ItemRetrieverDelegate<T> retriever)
        {
            T value;
            try
            {
                value = (T)this.cache[retriever];
            }
            catch (KeyNotFoundException)
            {
                value = retriever();
                this.cache.Add(retriever, value);
            }
            return value;
        }
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
                return GetCachedItem<string>(GetFormatedMethodName);
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
