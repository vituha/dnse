using System;
using System.Collections.Generic;
using System.Text;

namespace VS.Library.Diagnostics
{
	public class ExceptionHub
	{
		public static ExceptionHub Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new ExceptionHub();
				}
				return instance;
			}
			set
			{
				instance = value;
			}
		}
		private static ExceptionHub instance;

		public static bool Handle(Exception exception)
		{
			return Instance.DoHandle(exception);
		}

		protected virtual bool DoHandle(Exception exception)
		{
			return false;
		}
	}
}
