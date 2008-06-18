using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace VS.Library.Diagnostics.Exceptions
{
    [Serializable]
    public class ObjectUnavailableException : Exception
    {
        public ObjectUnavailableException()
            : base(FormatMessage())
        { }

        public ObjectUnavailableException(string message)
            : base(message)
        { }

        public ObjectUnavailableException(string message, Exception innerException)
            : base(message, innerException)
        { }

        protected ObjectUnavailableException(
            SerializationInfo info,
            StreamingContext context
        )
            : base(info, context)
        { }

        private static string FormatMessage()
        {
            string message = Messages.ObjectUnavailable;
            return message;
        }
    }
}
