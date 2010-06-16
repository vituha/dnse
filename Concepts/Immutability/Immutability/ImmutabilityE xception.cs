using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Immutability
{
    [Serializable]
    public class ImmutabilityException : Exception
    {
        public ImmutabilityException() { }
        public ImmutabilityException(string message) : base(message) { }
        public ImmutabilityException(string message, Exception inner) : base(message, inner) { }
        protected ImmutabilityException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
