using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VS.Library.Common
{
    public interface IExternallyDisposable
    {
        IDisposable GetDisposer();
    }
}
