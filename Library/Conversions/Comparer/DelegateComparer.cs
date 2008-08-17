using System.Collections.Generic;
using System.Diagnostics;
using VS.Library.Common;

namespace VS.Library.Operation.Comparison
{
	/// <summary>
	/// Provides a way to specify custom comparer as a method delegate
	/// </summary>
	/// <typeparam name="T">Type of objects to be compared</typeparam>
	public class DelegateComparer<T> : Comparer<T>
	{
		private Func2<int, T, T> comparerDelegate;

		/// <summary>
		/// Delegate for <see cref="Compare"/> method
		/// </summary>
        public Func2<int, T, T> ComparerDelegate
		{
			get { return comparerDelegate; }
			set
			{
				Debug.Assert(value != null);
				comparerDelegate = value;
			}
		}

		/// <summary>
		/// Sets <see cref="ComparerDelegate"/> to always return 0 (considering all compared items as being equal)
		/// </summary>
		public DelegateComparer()
		{
			this.comparerDelegate = delegate { return 0; };
		}

		/// <summary>
		/// Allows to specify a custom <see cref="ComparerDelegate"/>
		/// </summary>
		/// <param name="comparerDelegate">Value for <see cref="ComparerDelegate"/></param>
        public DelegateComparer(Func2<int, T, T> comparerDelegate)
		{
			Debug.Assert(comparerDelegate != null);
			this.comparerDelegate = comparerDelegate;
		}

		public override int Compare(T x, T y)
		{
			return comparerDelegate(x, y);
		}
	}
}
