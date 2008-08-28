using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Diagnostics;
using NSight.Workstation.Utils.Validation;

namespace VS.Library.Validation {
	/// <summary>
	/// Implements useful extensions to help validate an object's state
	/// </summary>
    public static class ValidationExtensions {

		#region Common Ensures

		/// <summary>
		/// Ensures object is not null by throwing an exception if it is null
		/// </summary>
		/// <typeparam name="T">Type of object</typeparam>
		/// <param name="obj">Object to be validated</param>
		/// <param name="objName">Name of object to be validated</param>
		/// <returns>Object passed</returns>
		/// <exception cref="ArgumentNullException">obj is null</exception>
		public static T EnsureNotNull<T>(this T obj, string objName) where T : class {
            if (obj == null) {
                throw new ArgumentNullException(objName);
            }
            return obj;
        }

		/// <summary>
		/// Ensures string is not null or empty by throwing an exception if it is null or empty
		/// </summary>
		/// <param name="obj">Object to be validated</param>
		/// <param name="objName">Name of object to be validated</param>
		/// <returns>Object passed</returns>
		/// <exception cref="ArgumentNullException">obj is null</exception>
		/// <exception cref="ArgumentException">obj is an empty string</exception>
		public static string EnsureNotNullOrEmpty(this string obj, string objName) {
			var result = EnsureNotNull(obj, objName);
			if (result.Length <= 0) {
				throw new ArgumentException(objName + " is expected to be a non-empty string");
			}
			return result;
		}

		/// <summary>
		/// Ensures collection is not null or empty by throwing an exception if it is null or empty
		/// </summary>
		/// <param name="obj">Object to be validated</param>
		/// <param name="objName">Name of object to be validated</param>
		/// <returns>Object passed</returns>
		/// <exception cref="ArgumentNullException">obj is null</exception>
		/// <exception cref="ArgumentException">obj is an empty collection</exception>
		public static ICollection EnsureNotNullOrEmpty(this ICollection obj, string objName) {
			var result = EnsureNotNull(obj, objName);
			if (result.Count <= 0) {
				throw new ArgumentException(objName + " is expected to be a non-empty collection");
			}
			return result;
		}

		/// <summary>
		/// Ensures collection is not null or empty by throwing an exception if it is null or empty
		/// </summary>
		/// <param name="obj">Object to be validated</param>
		/// <param name="objName">Name of object to be validated</param>
		/// <returns>Object passed</returns>
		/// <exception cref="ArgumentNullException">obj is null</exception>
		/// <exception cref="ArgumentException">obj is an empty collection</exception>
		public static ICollection<T> EnsureNotNullOrEmpty<T>(this ICollection<T> obj, string objName) {
			var result = EnsureNotNull(obj, objName);
			if (result.Count <= 0) {
				throw new ArgumentException(objName + " is expected to be a non-empty collection");
			}
			return result;
		}

		/// <summary>
		/// Ensures collection is not null or empty by throwing an exception if it is null or empty
		/// </summary>
		/// <param name="obj">Object to be validated</param>
		/// <param name="objName">Name of object to be validated</param>
		/// <returns>Object passed</returns>
		/// <exception cref="ArgumentNullException">obj is null</exception>
		/// <exception cref="ArgumentException">obj is an empty collection</exception>
		public static IEnumerable EnsureNotNullOrEmpty(this IEnumerable obj, string objName) {
			var result = EnsureNotNull(obj, objName);
			if (!result.GetEnumerator().MoveNext()) {
				throw new ArgumentException(objName + " is expected to be a non-empty collection");
			}
			return result;
		}

		#endregion

		#region General Ensures/Requires

		public static void Require(bool condition, string requirement) {
			if (!condition) {
				throw new RequirementNotMetException(requirement);
			}
		}

		public static void Require(bool condition) {
			if (!condition) {
				throw new RequirementNotMetException(null);
			}
		}

		internal static T EnsureInternal<T>(Predicate<T> condition, T obj) {
			Debug.Assert(condition != null);
			Require(condition(obj), condition.ToString());
			return obj;
		}

		public static T Ensure<T>(Predicate<T> condition, T obj) {
			EnsureNotNull(condition, "condition");
			return EnsureInternal(condition, obj);
		}

		#endregion
	}
}
