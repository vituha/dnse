using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VS.Library.Booleans;
using VS.Library.Conversions;

namespace VS.Library.Common
{
    public class Disposer : IDisposable
    {
        public Disposer(Proc0 managedDisposer, Proc0 unmanagedDisposer)
        { 
            ManagedDisposer = NullUtils.Coalesce(managedDisposer, CommonDelegates.Empty);
            UnmanagedDisposer = NullUtils.Coalesce(managedDisposer, CommonDelegates.Empty);
        }

        private Proc0 ManagedDisposer { get; set; }
        private Proc0 UnmanagedDisposer { get; set; }

        #region IWrapper<object> Members

        public object Value
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
        }

        public void Dispose(bool disposing) {
            if (disposing)
            {
                UnmanagedDisposer();
            }
            ManagedDisposer();
        }

        ~Disposer() {
            Dispose(false);
        }
        #endregion
    }
}
