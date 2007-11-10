using System;
using System.Collections.Generic;
using System.Text;
using VS.Library.Diagnostics;
using System.Reflection;

using VS.Library.Generics.Cache;
using System.Diagnostics;

namespace Test
{
    public class MethodTraceImp : MethodTrace
    {
        public static MethodTraceImp Monitor(MethodBase method)
        {
#if DEBUG
            return new MethodTraceImp(method);
#else
            return null;
#endif
        }

        public MethodTraceImp(MethodBase method) : base(method) { }

        #region GetterCache
        private IGetterCache<string> getterCache = new GetterCache<string>();
        #endregion

        protected virtual string FormatedMethodName
        {
            get
            {
                return this.getterCache.Get(
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

        protected override void TraceMethodBegin()
        {
            Trace.WriteLine(this.FormatedMethodName + " Begin");
        }

        protected override void TraceMethodEnd()
        {
            Trace.WriteLine(this.FormatedMethodName + " End");
        }
    }
}
