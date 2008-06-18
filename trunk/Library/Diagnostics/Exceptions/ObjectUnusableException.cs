using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using VS.Library.Text;

using A = VS.Library;

namespace VS.Library.Diagnostics.Exceptions
{
    [Serializable]
    public class ObjectUnusableException : Exception
    {
        public ObjectUnusableException()
            : base(FormatMessage())
        { }

        public ObjectUnusableException(string symbol) 
            : base(FormatMessage(symbol))
        { }

        public ObjectUnusableException(string symbol, Exception innerException)
            : base(FormatMessage(symbol), innerException)
        { 
        }

        protected ObjectUnusableException(
            SerializationInfo info,
            StreamingContext context
        )
            : base(info, context)
        { }

        private static string FormatMessage()
        {
            string message = Messages.ObjectUnusable;
            return message;
        }

        private static string FormatMessage(string symbol)
        {
            string message = A.Text.Formatter.UserFormat(Messages.ObjectUnusable1, symbol);
            return message;
        }
    }
}
