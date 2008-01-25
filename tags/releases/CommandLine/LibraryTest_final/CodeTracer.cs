using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

using VS.Library.Generics.Cache;
using System.Diagnostics;
using System.Timers;
using VS.Library.Generics.Diagnostics;

namespace Test
{
    public class SimpleCodeTracker : Code<string> { };

    public class CodeTracer
    {
        public static void Activate()
        {
            SimpleCodeTracker.Instance.CodeBlockEnter += BlockStarted;
            SimpleCodeTracker.Instance.CodeBlockExit += BlockFinished;
            Trace.WriteLine("============= Tracing Activated ===========");
        }

        public static void DeActivate()
        {
            SimpleCodeTracker.Instance.CodeBlockEnter -= BlockStarted;
            SimpleCodeTracker.Instance.CodeBlockExit -= BlockFinished;
            Trace.WriteLine("=============  Tracing DeActivated  ===========");
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

        private static string GetFormatedBlockName(int blockId, object instance, MethodBase method, string tag)
        {
            if (method == null)
            {
                return String.Format("<anonymous block, id={0}>", blockId);
            }
            string pattern = "{0}() [tag: {1}]";

            return (
                String.Format(
                    pattern,
                    method == null ? "<unknown>" : method.DeclaringType.Name + '.' + method.Name,
                    String.IsNullOrEmpty(tag) ? "<empty>" : tag
                    )
                 );
        }

        private static Dictionary<int, long> tickStorage = new Dictionary<int,long>();
        private static void BlockStarted(int blockId, CodeEventArgs<string> args)
        {
            Trace.WriteLine(
                GetFormatedBlockName(blockId, args.Instance, args.Method, args.Context)
                + " Begin"
            );
            tickStorage.Add(blockId, DateTime.Now.Ticks);
        }

        private static void BlockFinished(int blockId, CodeEventArgs<string> args)
        {
            long ticks = (DateTime.Now.Ticks - tickStorage[blockId]);
            Trace.WriteLine(
                String.Format(
                    GetFormatedBlockName(blockId, args.Instance, args.Method, args.Context)
                    + " End. Took {0} ms"
                    , ticks / 10000.0
               )
            );
        }
    }
}
