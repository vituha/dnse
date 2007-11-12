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
    public class MethodTracer
    {
        static MethodTracer() 
        {
        }

        public static void Activate()
        {
            MethodTracker.Instance.MethodStarted += MethodStarted;
            MethodTracker.Instance.MethodFinished += MethodFinished;
        }

        public static void DeActivate()
        {
            MethodTracker.Instance.MethodStarted -= MethodStarted;
            MethodTracker.Instance.MethodFinished -= MethodFinished;
        }

        #region GetterCache
        private static IGetterCache<string> getterCache;
        protected static IGetterCache<string> GetterCache
        {
            get 
            {
                if (getterCache == null)
                {
                    return getterCache = new GetterCache<string>();
                }
                return getterCache;
            }
        }
        #endregion



        private static string GetFormatedMethodName(MethodBase method)
        {
            return (
                method == null
                    ? "<unknown>()"
                    : String.Format("{0}.{1}()", method.DeclaringType.Name, method.Name)
                    );
        }

        private static long startTicks;
        private static void MethodStarted(Object sender, EventArgs args)
        {
            Trace.WriteLine(GetFormatedMethodName((MethodBase)sender) + " Begin");
            startTicks = DateTime.Now.Ticks;
        }

        private static void MethodFinished(Object sender, EventArgs args)
        {
            long ticks = (DateTime.Now.Ticks - startTicks);
            Trace.WriteLine(String.Format(GetFormatedMethodName((MethodBase)sender) + " End. Took {0} ms", ticks / 10000.0));
        }
    }
}
