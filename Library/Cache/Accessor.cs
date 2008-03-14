using System;
using VS.Library.Generics.Common.Delegates;
using System.Diagnostics;
using VS.Library.Diagnostics;

namespace VS.Library.Cache
{
	/// <summary>
	/// Provides a way to share given Large Object (LOB) instance among multiple clients
	/// as well as properly destroy the instance when no longer used or out of using operator scope.
	/// Can be used as a replacement for property exposing LOB
	/// Usage of this pattern may give performace improvements when LOB creation is costly
	/// and/or LOB disposal needs to be done as soon as it is no longer used
	/// Note, that GC is not used by this implementation, so client code is free to call GC whenever needed 
	/// </summary>
	/// <typeparam name="T">Type of LOB</typeparam>
	public class Accessor<T>
		where T : class
	{
		private T _object;
		private D0<T> fabricDelegate;
		private ushort refCount;

		/// <summary>
		/// Constructs class instance.
		/// Note, that in order for LOB to be freed in correct time(s), 
		/// LOB instance reference should not be stored anywhere outside 
		/// the outermost Lock/Unlock or BeginAccess/EndAccess call pair.
		/// </summary>
		/// <param name="fabricDelegate">Fabric delegate to create/initialize the LOB instance</param>
		public Accessor(D0<T> fabricDelegate)
		{
			if (fabricDelegate == null)
			{
				ExceptionHub.Throw(new NullReferenceException("getter must not be null"));
			}
			this._object = null;
			this.fabricDelegate = fabricDelegate;
			this.refCount = 0;
		}

		~Accessor()
		{
			this._object = null;
			this.fabricDelegate = null;
			if (this.refCount > 0)
			{
				Trace.TraceWarning("too few releases. {0} more expected.", this.refCount);
			}
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

		#region Locking
		/// <summary>
		/// Gets object reference. Increments refcounter
		/// </summary>
		/// <returns>Object's reference</returns>
		/// <exception cref="OverflowException" />
		/// <exception cref="ApplicationException" />
		public virtual T GetLock()
		{
			// Overflow check
			if (this.refCount == ushort.MaxValue)
			{
				ExceptionHub.Throw(new OverflowException("Too many calls"));
			}
			else
			{
				this.refCount++;

				if (this._object == null) // LOB not yet created
				{
					if (this.fabricDelegate == null) // Can create LOB?
					{
						ExceptionHub.Throw(new ApplicationException("This instance cannot be used because cleanup has already been done"));
					}
					else
					{
						try
						{
							this._object = this.fabricDelegate(); // LOB creation
						}
						catch (Exception e)
						{
							ExceptionHub.Throw(new ApplicationException("Fabric method threw exception", e));
						}
					}
				}
			}
			return this._object;
		}
		/// <summary>
		/// Decrements refcounter and releases object when zero
		/// </summary>
		public void UnLock()
		{
			if (this.refCount == 0)
			{
				Trace.TraceWarning("too many unlocks");
				return;
			}
			if (--this.refCount == 0)
				this._object = null; // free LOB
		}
		#endregion

		#region Caching
		/// <summary>
		/// Only increments refcounter. No LOB instance is being created. 
		/// This method protects the LOB instance from being freed accross non-intersecting code parts.
		/// Use <see cref="EndAccess" /> to allow it to be freed again.
		/// Note, that calls to this method are stackable, which means <see cref="EndAccess" /> must be called 
		/// for each corresponding <see cref="BeginAccess" /> in order for LOB instance to be actually freed
		/// </summary>
		/// <returns>Object's reference</returns>
		/// <remarks>Use this method to define scope within which 
		/// the LOB instance, once created by first call to <see cref="Lock"/>, will be cached</remarks>
		/// <example>
		/// The following example demonstrates how the same LOB instance 
		/// can be shared between non-intersecting code parts
		/// <code>
		/// accessor.BeginAccess();
		/// object o1 = accessor.GetLock();
		/// object o1 = accessor.UnLock(); // object is not destroyed here because of <see cref="BeginAccess" />
		/// object o2 = accessor.GetLock();	// the object is re-used instead of re-obtained from getter
		/// object o2 = accessor.UnLock();
		/// accessor.EndAccess();
		/// </code>
		/// </example>
		public void BeginCache()
		{
			this.refCount++;
		}
		/// <summary>
		/// Decrements the counter and frees the lob if necessary. Synonym to <see cref="Release"/>. 
		/// Intended to be used in pair with <see cref="BeginAccess" />
		/// </summary>
		public void EndCache()
		{
			UnLock();
		}
		#endregion

		/// <summary>
		/// This is for forced freeing of the held object and should normally be the last call to this instance
		/// Any call to <see cref="GetLock"/> behind this point will throw <see cref="NullReferenceException"/>
		/// </summary>
		public void Cleanup()
		{
			this.refCount = 0;
			this._object = null;
			this.fabricDelegate = null; // Releasing the link to fabric. No more GetLock() calls!
		}

		#region IDisposable and using related
		private class Disposer : IDisposable
		{
			private Accessor<T> accessor;

			public Disposer(Accessor<T> accessor)
			{
				this.accessor = accessor;
				accessor.BeginCache();
			}

			void IDisposable.Dispose()
			{
				accessor.EndCache();
				accessor = null;
			}
		}

		/// <summary>
		/// Can be used in the same way as <see cref="BeginAccess"/>/<see cref="EndAccess"/> pair
		/// but in <see cref="using"/> statement
		/// </summary>
		/// <returns>Disposable wrapper on this instance</returns>
		public IDisposable Use()
		{
			return new Disposer(this);
		}
		#endregion
	}
}
