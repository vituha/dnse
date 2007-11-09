using System;
using System.Collections.Generic;
using System.Text;
using VS.Diagnostics;
using System.Reflection;
using System.Diagnostics;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Trace.WriteLine("============= Tracing Started ===========");
            DoSomething();
            Trace.WriteLine("=============  Tracing Ended  ===========");
        }

        private static void DoSomething()
        {
            using (MethodTrace trace = MethodTrace.Monitor(MethodBase.GetCurrentMethod()))
            {
                Trace.WriteLine("Inside DoSomething()");
            }
        }
    }
}
