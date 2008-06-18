using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using VS.Library.Strings;

namespace VS.Library.Diagnostics.Exceptions
{
    [Serializable]
    public class ObjectUnusableException : Exception
    {
        public ObjectUnusableException() 
            : base()
        { }

        public ObjectUnusableException(string message) 
            : base(message)
        { }

        public ObjectUnusableException(string message, Exception innerException)
            : base(message, innerException)
        { }

        protected ObjectUnusableException(
            SerializationInfo info,
            StreamingContext context
        )
            : base(info, context)
        { }

        public static ObjectUnusableException Create()
        {
            string message = Messages.ObjectUnusable;
            return new ObjectUnusableException(message);
        }

        public static ObjectUnusableException Create(string symbol)
        {
            string message = StringUtils.UserFormat(Messages.ObjectUnusable1, symbol);
            return new ObjectUnusableException(message);
        }
    }
}
