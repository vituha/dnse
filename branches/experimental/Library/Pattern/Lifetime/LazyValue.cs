using System;
using System.Collections.Generic;
using System.Text;
using VS.Library.Common;
using VS.Library.Diagnostics;
using VS.Library.Diagnostics.Exceptions;
using System.Diagnostics;
using A = VS.Library;

namespace VS.Library.Pattern.Lifetime
{
    using CounterType = System.Int32;

    public class LazyValue<T> : IManagedValue<T>, IDisposable where T : class
    {
        private T _object;
        private D0<T> fabric;
        private CounterType counter;
        private bool active;

        public LazyValue(D0<T> fabric)
        {
            if (fabric == null)
            {
                throw new UnexpectedNullException("fabric");
            }

            this._object = null;
            this.fabric = fabric;
            this.counter = 0;
        }

        ~LazyValue()
        {
            Dispose(false);
        }

#if DEBUG
        public CounterType RefCount
        {
            get
            {
                return this.counter;
            }
        }
#endif

        #region IManageableActivator<T> Members

        public T Value
        {
            get {
                if (Active)
                {
                    if (this._object == null) // instance not yet created
                    {
                        CreateInstance();
                    }
                    return this._object;
                }
                throw new ObjectUnavailableException();
            }
        }

        public void Lock()
        {
            Activate();
        }

        public void Unlock()
        {
            Deactivate();
        }

        #endregion

        #region IActivator Members

        public void Activate()
        {
            IncrementCounter();
            if (this.counter == 1)
            {
                this.active = true;
            }
        }

        public bool Active
        {
            get { return this.active; }
        }

        public void Deactivate()
        {
            DecrementCounter();
            if (this.counter == 0)
            {
                FreeInstance();
                this.active = false;
            }
        }

        #endregion

        #region - Dispose Pattern -
        /// <summary>
        /// This method is overriden in child classes to perform additional cleanup
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            this._object = null;
            this.fabric = null; // Releasing the link to fabric. No more Get() calls!
            if (this.counter > 0)
            {
                string message = A.Text.Formatter.UserFormat("too few calls to Release/EndAccess. {0} more expected.", this.counter);
                Debug.Fail(message);
                this.counter = 0;
            }
        }

        /// <summary>
        /// The rest is identical for all root base classes and 
        /// should ONLY appear in root base classes
        /// </summary>

        private bool disposed;
        protected bool Disposed
        {
            get
            {
                return disposed;
            }
        }
        protected void CheckDisposed()
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }
        }
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, 
        /// or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            disposed = true;
            GC.SuppressFinalize(this);
        }

        #endregion

        /// <summary>
        /// This is a more readable but a much slower method of keeping value active
        /// </summary>
        /// <returns>Disposable that controls lifetime of the value</returns>
        public IDisposable Use()
        {
            return new ActivatorUser(this);
        }

        private void IncrementCounter()
        {
            // Overflow check
            if (this.counter == CounterType.MaxValue)
            {
                throw new OverflowException("Too many calls");
            }
            else
            {
                this.counter++;
            }
        }

        private void DecrementCounter()
        {
            if (this.counter == 0)
            {
                Debug.Fail("too many calls to Release/EndAccess");
                return;
            }
            this.counter--;
        }

        private void CreateInstance()
        {
            Debug.Assert(this.fabric != null, "Fabric is null");
            try
            {
                this._object = this.fabric();
            }
            catch (ApplicationException e)
            {
                if (!ExceptionHub.Handle(e))
                    throw;
            }
        }

        private void FreeInstance()
        {
            this._object = null;
        }
    }
}
