using System;
using System.Collections.Generic;
using System.Text;
using VS.Library.Generics.Common.Delegates;
using System.Diagnostics;

namespace VS.Library.Cache
{
    /// <summary>
    /// Provides a way to share given object among multiple clients
    /// as well as properly destroy it when no longer used or out of using operator scope
    /// </summary>
    /// <typeparam name="T">Type of object to share</typeparam>
    public class ObjectHolder<T> where T : class
    {
        private T _object;
        private D0<T> getter;
        private ushort refCount;

        /// <summary>
        /// Constructs class instance
        /// </summary>
        /// <param name="getter">Getter of the object to hold</param>
        public ObjectHolder(D0<T> getter)
        {
            Debug.Assert(getter != null, "getter must not be null");
            
            this._object = null;
            this.getter = getter;
            this.refCount = 0;
        }

        /// <summary>
        /// Gets object reference. Increments refcounter
        /// </summary>
        /// <returns>Object's reference</returns>
        public T Hold()
        {
            this.refCount++;
            if (this._object == null)
            {
                this._object = this.getter();
                Debug.Assert(this._object != null, "getter returned null");
            }
            return this._object;
        }

        /// <summary>
        /// Only increments refcounter. No object is being obtained by getter. 
        /// This method helps ensure that the object is cached between non-intersecting code parts.
        /// </summary>
        /// <returns>Object's reference</returns>
        /// <remarks>Use this method to define scope within which 
        /// the object, once created by first call to <see cref="Lock"/>, will be cached</remarks>
        /// <example>
        /// The following example demonstrates how the same object 
        /// can be shared between non-intersecting code parts
        /// <code>
        /// holder.CacheHold();
        /// object o1 = holder.Hold();
        /// object o1 = holder.Release(); // object is not destroyed here because of CacheHold
        /// object o2 = holder.Hold();    // the object is re-used instead of re-obtained from getter
        /// object o2 = holder.Release();
        /// holder.Release();
        /// </code>
        /// </example>
        public void CacheHold()
        {
            this.refCount++;
        }

        /// <summary>
        /// Decrements refcounter and releases object when zero
        /// 
        /// </summary>
        /// <exception cref="ApplicationException">When called more times then <see cref="Lock"/></exception>
        public void Release()
        {
            Debug.Assert(this.refCount > 0, "too many releases");
            if (--this.refCount == 0)
                this._object = null;
        }

        /// <summary>
        /// Current refcounter value
        /// </summary>
        public ushort RefCount
        {
            get
            {
                return this.refCount;
            }
        }

        ~ObjectHolder()
        {
            Debug.Assert(this.refCount == 0, "too few releases");
        }
    }
}
