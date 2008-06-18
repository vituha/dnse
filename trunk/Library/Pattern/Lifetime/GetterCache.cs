using System;
using System.Collections;
using System.Collections.Generic;
using VS.Library.Common;

namespace VS.Library.Pattern.Lifetime
{
	using BackendCollection = Dictionary<Delegate, object>;
	/// <summary>
	/// Provides a simple static cache which uses the same delegate as a key and a value getter
	/// </summary>
	/// <remarks>
	/// The class is useful for caching "calculate once - use often" properties which are difficult to calculate.
	/// Most obvious case - strings that cannot be calculated at compile time.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	///static string CalcCachedPropValue()
	///{
	///	string res = String.Format("Hello, {0} and {1}!", "World", "All");
	///	return res;
	///}
	///static string CachedProp
	///{
	///	get
	///	{
	///		return GetterCache.Get<string>(CalcCachedPropValue);
	///	}
	///}
	/// ]]></code>
	/// </example>
	public static class GetterCache
	{
		private static BackendCollection cacheInstance = new BackendCollection();
		private static object syncRoot = (cacheInstance as ICollection).SyncRoot;

		/// <summary>
		/// Get the value associated with the given getter
		/// </summary>
		/// <typeparam name="TValue">Type of value</typeparam>
		/// <param name="getter">Getter delegate to be called for the value</param>
		/// <returns>Associated value</returns>
		public static TValue Get<TValue>(D0<TValue> getter)
		{
			object value;
			if(cacheInstance.TryGetValue(getter, out value))
				return (TValue)value;
			TValue tvalue = getter();

			lock (syncRoot) 
			{
				cacheInstance.Add(getter, tvalue);
			}
			return tvalue;
		}

		/// <summary>
		/// Removes value associated with given getter from the cache
		/// </summary>
		/// <typeparam name="TValue">Type of value</typeparam>
		/// <param name="getter">Getter delegate whose value we want to remove</param>
		/// <returns>true if succeeded, false if the getter is not found in the cache</returns>
		public static bool Remove<TValue>(D0<TValue> getter)
		{
			return cacheInstance.Remove(getter);
		}
	}
}
