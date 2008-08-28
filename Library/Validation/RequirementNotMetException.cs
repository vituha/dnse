using System;
using System.Collections.Generic;
using System.Text;

namespace VS.Library.Validation {
	/// <summary>
	/// Indicates that certain requirement was not met at some point of execution 
	/// </summary>
	public class RequirementNotMetException : ApplicationException {
		public RequirementNotMetException(string requirement, Exception innerException)
			: base(FormatMessage(requirement), innerException) { }
		public RequirementNotMetException(string requirement)
			: base(FormatMessage(requirement)) { }
		public RequirementNotMetException()
			: base(FormatMessage(null)) { }

		private static string FormatMessage(string requirement) {
			string staticMessage = "Requirement not met";
			if (String.IsNullOrEmpty(requirement)) {
				return staticMessage;
			}
			return staticMessage + ": " + requirement;
		}
	}
}
