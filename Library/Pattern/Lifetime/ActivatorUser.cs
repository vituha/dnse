using System;
using System.Collections.Generic;
using System.Text;
using VS.Library.Diagnostics.Exceptions;

namespace VS.Library.Pattern.Lifetime
{
    public class ActivatorUser: IDisposable
    {
        private IActivator activator;

        public ActivatorUser(IActivator activator)
        {
            if (activator == null)
            {
                throw new UnexpectedNullException("activator");
            }
            this.activator = activator;
            activator.Activate();
        }

        ~ActivatorUser()
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
            if (activator != null)
            {
                activator.Deactivate();
                activator = null;
            }
        }
        #endregion    
    }
}
