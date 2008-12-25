namespace VS.Library.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Reflection;
    using System.Diagnostics;
    using VS.Library.Validation;

	/// <summary>
	/// Provides additional reflection-related functionality for delegates
	/// When moving to .Net Framework 3.5 or above, consider convert everything here into C# 3.0 extension methods
	/// </summary>
	public static class DelegateExtensions {

		#region ResolveName

		/// <summary>
		/// Retrieves name of the method represented by the delegate
		/// </summary>
		/// <param name="delegateInstance">Delegate to examine</param>
		/// <param name="isFullName">Indicates wheteher caller expects full name (including method's declaring type) </param>
		/// <returns>Resolved method name</returns>
		internal static string ResolveNameInternal(Delegate delegateInstance, bool isFullName) {
			Debug.Assert(delegateInstance != null);

			var methodInfo = delegateInstance.Method;
			string methodName = (isFullName ? methodInfo.ReflectedType.FullName + "." : String.Empty) + methodInfo.Name;
			return methodName;
		}

		/// <summary>
		/// Retrieves name of the method represented by the delegate
		/// </summary>
		/// <param name="delegateInstance">Delegate to examine</param>
		/// <param name="isFullName">Indicates wheteher caller expects full name (including method's declaring type) </param>
		/// <returns>Resolved name</returns>
		public static string ResolveName(Delegate delegateInstance, bool isFullName) {
			delegateInstance.RequireArgumentNotNull("delegateInstance");

			return ResolveNameInternal(delegateInstance, isFullName);
		}

		/// <summary>
		/// Retrieves short name of the method represented by the delegate
		/// </summary>
		/// <param name="delegateInstance">Delegate to examine</param>
		/// <returns>Resolved short name</returns>
		public static string ResolveName(Delegate delegateInstance) {
			return ResolveName(delegateInstance, false);
		}

		#endregion ResolveName
	}
}
