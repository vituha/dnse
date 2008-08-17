using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VS.Library.Common
{
    public static class CommonDelegates
    {
        public static Proc0 Empty { get { return () => { }; } }
    }
}
