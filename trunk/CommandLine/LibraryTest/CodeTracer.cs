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
    public class CodeTracer
    {
        public static void Activate()
        {
            CodeTracker.Instance.CodeBlockEnter += BlockStarted;
            CodeTracker.Instance.CodeBlockExit += BlockFinished;
            Trace.WriteLine("============= Tracing Activated ===========");
        }

        public static void DeActivate()
        {
            CodeTracker.Instance.CodeBlockEnter -= BlockStarted;
            CodeTracker.Instance.CodeBlockExit -= BlockFinished;
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

        private static string GetFormatedBlockName(StaticCodeBlockInfo info)
        {
            if (info.IsAnonymous)
            {
                return "<anonymous block>";
            }
            string pattern = "{0}() [tag: {1}]";

            return (
                String.Format(
                    pattern,
                    info.Method == null ? "<unknown>" : info.Method.DeclaringType.Name + '.' + info.Method.Name,
                    info.Tag == null ? "<empty>" : info.Tag
                    )
                 );
        }

        private static Dictionary<int, long> tickStorage = new Dictionary<int,long>();
        private static void BlockStarted(int pinHash, StaticCodeBlockInfo context)
        {
            Trace.WriteLine(GetFormatedBlockName(context) + " Begin");
            tickStorage.Add(pinHash, DateTime.Now.Ticks);
        }

        private static void BlockFinished(int pinHash, StaticCodeBlockInfo context)
        {
            long ticks = (DateTime.Now.Ticks - tickStorage[pinHash]);
            Trace.WriteLine(String.Format(GetFormatedBlockName(context) + " End. Took {0} ms", ticks / 10000.0));
        }
    }
}
