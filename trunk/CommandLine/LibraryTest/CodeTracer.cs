using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

using VS.Library.Generics.Cache;
using System.Diagnostics;
using System.Timers;

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

        private static string GetFormatedBlockName(CodeBlockInfo info)
        {
            if (info == null)
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

        private static Dictionary<CodeBlockInfo, long> tickStorage = new Dictionary<CodeBlockInfo,long>();
        private static void BlockStarted(Object sender, EventArgs args)
        {
            CodeBlockInfo info = (CodeBlockInfo)sender;

            Trace.WriteLine(GetFormatedBlockName(info) + " Begin");
            if (info != null)
            {
                tickStorage.Add(info, DateTime.Now.Ticks);
            }
        }

        private static void BlockFinished(Object sender, EventArgs args)
        {
            CodeBlockInfo info = (CodeBlockInfo)sender;
            Trace.Write(GetFormatedBlockName(info) + " End.");
            if (info != null)
            {
                long ticks = (DateTime.Now.Ticks - tickStorage[info]);
                Trace.Write(String.Format(" Took {0} ms", ticks / 10000.0));
            }
            Trace.WriteLine("");
        }
    }
}
