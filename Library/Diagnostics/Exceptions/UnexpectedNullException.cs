using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using VS.Library.Strings;

namespace VS.Library.Diagnostics.Exceptions
{
    [Serializable]
    public class UnexpectedNullException : Exception
    {
        public UnexpectedNullException() 
            : base()
        { }

        public UnexpectedNullException(string message) 
            : base(message)
        { }

        public UnexpectedNullException(string message, Exception innerException)
            : base(message, innerException)
        { }

        protected UnexpectedNullException(
            SerializationInfo info,
            StreamingContext context
        )
            : base(info, context)
        { }

        public static UnexpectedNullException Create()
        {
            string message = Messages.UnexpectedNull;
            return new UnexpectedNullException(message);
        }

        public static UnexpectedNullException Create(string symbol)
        {
            string message = StringUtils.UserFormat(Messages.UnexpectedNull1, symbol);
            return new UnexpectedNullException(message);
        }
    }
}
