using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

using A = VS.Library;

namespace VS.Library.Diagnostics.Exceptions
{
    [Serializable]
    public class UnexpectedNullException : Exception
    {
        public UnexpectedNullException() 
            : base(FormatMessage())
        { }

        public UnexpectedNullException(string symbol) 
            : base(FormatMessage(symbol))
        { }

        public UnexpectedNullException(string symbol, Exception innerException)
            : base(FormatMessage(symbol), innerException)
        { }

        protected UnexpectedNullException(
            SerializationInfo info,
            StreamingContext context
        )
            : base(info, context)
        { }

        private static string FormatMessage()
        {
            string message = Messages.UnexpectedNull;
            return message;
        }

        private static string FormatMessage(string symbol)
        {
            string message = A.Text.Formatter.UserFormat(Messages.UnexpectedNull1, symbol);
            return message;
        }
    }
}
