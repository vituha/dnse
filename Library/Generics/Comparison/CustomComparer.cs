using System.Collections.Generic;
using System.Diagnostics;
using VS.Library.Generics.Common.Delegates;

namespace VS.Library.Generics.Comparison
{
	/// <summary>
	/// Provides a way to specify custom comparer as a method delegate
	/// </summary>
	/// <typeparam name="T">Type of objects to be compared</typeparam>
	public class CustomComparer<T> : Comparer<T>
	{
		private D2<int, T, T> comparerDelegate;

		/// <summary>
		/// Delegate for <see cref="Compare"/> method
		/// </summary>
		public D2<int, T, T> ComparerDelegate
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
		public CustomComparer()
		{
			this.comparerDelegate = delegate { return 0; };
		}

		/// <summary>
		/// Allows to specify a custom <see cref="ComparerDelegate"/>
		/// </summary>
		/// <param name="comparerDelegate">Value for <see cref="ComparerDelegate"/></param>
		public CustomComparer(D2<int, T, T> comparerDelegate)
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
