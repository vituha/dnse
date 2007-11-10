using System;
using System.Collections.Generic;
using System.Text;
using VS.Library.Diagnostics;
using System.Reflection;

using VS.Library.Generics.Cache;
using System.Diagnostics;
using System.Timers;

namespace Test
{
    public class MethodTraceImp : MethodTraceBase
    {
        public static MethodTraceImp Monitor(MethodBase method)
        {
#if NO_METHOD_TRACE
            return null;
#else
            return new MethodTraceImp(method);
#endif
        }

        public MethodTraceImp(MethodBase method) : base(method) { }

        #region GetterCache
        private IGetterCache<string> getterCache;
        protected IGetterCache<string> GetterCache
        {
            get 
            {
                if (this.getterCache == null)
                {
                    return this.getterCache = new GetterCache<string>();
                }
                return this.getterCache;
            }
        }
        #endregion



        protected virtual string FormatedMethodName
        {
            get
            {
                return GetterCache.Get(
                    delegate
                    {
                        return (
                            this.Method == null
                                ? "<unknown method>"
                                : String.Format("{0}.{1}()", this.Method.DeclaringType.Name, this.Method.Name)
                                );
                    }
                );
            }
        }

        protected long startTicks;
        protected override void TraceMethodBegin()
        {
            Trace.WriteLine(this.FormatedMethodName + " Begin");
            startTicks = DateTime.Now.Ticks;
        }

        protected override void TraceMethodEnd()
        {
            long ticks = (DateTime.Now.Ticks - this.startTicks);
            Trace.WriteLine(String.Format(this.FormatedMethodName + " End. Took {0} ms", ticks / 10000.0));
        }
    }
}
