using System;
using System.Collections.Generic;
using System.Text;
using VS.Library.Diagnostics.Exceptions;
using VS.Library.Common;

namespace VS.Library.Pattern.Lifetime
{
    public class ActivableUser: IDisposable
    {
        private IActivable Activable;

        public ActivableUser(IActivable Activable)
        {
            if (Activable == null)
            {
                throw new UnexpectedNullException("Activable");
            }
            this.Activable = Activable;
            Activable.Activate();
        }

        ~ActivableUser()
        {
            Dispose(false);
        }

        #region - Dispose Pattern -
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (Activable != null)
            {
                Activable.Deactivate();
                Activable = null;
            }
        }
        #endregion    
    }
}
