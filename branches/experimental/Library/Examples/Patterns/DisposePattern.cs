using System;
using System.Collections.Generic;
using System.Text;

namespace VS.Library.Examples.Patterns

{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    class DisposePatternDerived : DisposePatternBase
	{
		#region - Dispose Pattern -
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				// free managed resources here
			}
			// free unmanaged resources here
			base.Dispose(disposing);
		}
		#endregion
	}

	class DisposePatternBase: IDisposable
	{
		#region - Dispose Pattern -
		/// <summary>
		/// This method is overriden in child classes to perform additional cleanup
		/// </summary>
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				// free managed resources here
			}
			// free unmanaged resources here
		}

		/// <summary>
		/// The rest is identical for all root base classes and 
		/// should ONLY appear in root base classes
		/// </summary>
 
		private bool disposed;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        protected bool Disposed
		{
			get
			{
				return disposed;
			}
		}
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        protected void CheckDisposed()
		{
			if (Disposed)
			{
				throw new ObjectDisposedException("this");
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
		~DisposePatternBase()
		{
			Dispose(false);
		}
		#endregion
	}
}
