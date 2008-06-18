using System;
using System.Collections.Generic;
using System.Text;

namespace VS.Library.Pattern.Lifetime
{
    /// <summary>
    /// Provides access services to a scoped value
    /// </summary>
    /// <typeparam name="T">Type of scoped value</typeparam>
    public interface IManagedValue<T>: IManageableActivator
    {
        /// <summary>
        /// If in scope, returns actual value, otherwise throws <see cref="OutOfScopeException" />
        /// </summary>
        T Value { get; }
    }
}
