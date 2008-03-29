using System;
using VS.Library.Generics.Common;
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
	public class Lom<T>
		where T : class
	{
		private T _object;
		private D0<T> fabric;
		private ushort counter;

		/// <summary>
		/// Constructs class instance.
		/// Note, that in order for LOB to be freed in correct time(s), 
		/// LOB instance reference should not be stored anywhere outside 
		/// the outermost Get/Release or Ensure/Release call pair.
		/// </summary>
		/// <param name="fabricDelegate">Fabric delegate to create/initialize the LOB instance</param>
		public Lom(D0<T> fabric)
		{
			if (fabric == null)
			{
				ExceptionHub.Handle(new NullReferenceException("getter must not be null"));
			}
			this._object = null;
			this.fabric = fabric;
			this.counter = 0;
		}

		~Lom()
		{
			Cleanup();
		}

		/// <summary>
		/// Current refcounter value
		/// </summary>
		public ushort RefCount
		{
			get
			{
				return this.counter;
			}
		}

		/// <summary>
		/// Gets object reference. Increments refcounter
		/// </summary>
		/// <returns>Object's reference</returns>
		/// <exception cref="OverflowException" />
		/// <exception cref="ApplicationException" />
		public virtual T Access()
		{
			Ensure(); // increment counter

			if (this._object == null) // LOB not yet created
			{
				if (this.fabric == null) // Can create LOB?
				{
					ExceptionHub.Handle(new NullReferenceException("This instance cannot be used because cleanup has already been done"));
				}
				else
				{
					try
					{
						this._object = this.fabric(); // LOB creation
					}
					catch (ApplicationException e)
					{
						if (!ExceptionHub.Handle(e))
							throw;
					}
				}
			}
			return this._object;
		}
		/// <summary>
		/// Decrements refcounter and releases object when zero
		/// </summary>
		public void Release()
		{
			if (this.counter == 0)
			{
				Trace.TraceWarning("too many Releases");
				return;
			}
			if (--this.counter == 0)
				this._object = null; // free LOB
		}

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
		/// object o1 = accessor.Get();
		/// object o1 = accessor.Release(); // object is not destroyed here because of <see cref="BeginAccess" />
		/// object o2 = accessor.Get();	// the object is re-used instead of re-obtained from getter
		/// object o2 = accessor.Release();
		/// accessor.EndAccess();
		/// </code>
		/// </example>
		public void Ensure()
		{
			// Overflow check
			if (this.counter == ushort.MaxValue)
			{
				ExceptionHub.Handle(new OverflowException("Too many calls"));
			}
			else
			{
				this.counter++;
			}
		}

		/// <summary>
		/// This is for forced freeing of the held object and should normally be the last call to this instance
		/// Any call to <see cref="Get"/> behind this point will throw <see cref="NullReferenceException"/>
		/// </summary>
		public void Cleanup()
		{
			this._object = null;
			this.fabric = null; // Releasing the link to fabric. No more Get() calls!
			if (this.counter > 0)
			{
				Trace.TraceWarning("too few releases. {0} more expected.", this.counter);
				this.counter = 0;
			}
		}

		#region IDisposable and using related
		private class Disposer : IDisposable
		{
			private Lom<T> lom;

			public Disposer(Lom<T> lom)
			{
				this.lom = lom;
				lom.Ensure();
			}

			#region - Dispose Pattern -
			public void Dispose()
			{
				Dispose(true);
				GC.SuppressFinalize(this);
			}

			protected virtual void Dispose(bool disposing)
			{
				if (disposing)
				{
					lom.Release();
					lom = null;
				}
			}
			#endregion

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

		public D0<T> Reuse(D0<T> fabric)
		{
			D0<T> result = this.fabric;
			Cleanup();
			this.fabric = fabric;
			return result;
		}
		#endregion
	}
}
