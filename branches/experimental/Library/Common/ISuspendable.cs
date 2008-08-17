using System;
using System.Collections.Generic;
using System.Text;

namespace VS.Library.Common
{
    /// <summary>
    /// Allows temporary disable current behaviour
    /// </summary>
    public interface ISuspendable
    {
        /// <summary>
        /// Disables current behaviour
        /// </summary>
        void Suspend();

        /// <summary>
        /// Enables current behaviour
        /// </summary>
        void Resume();
    }

    /// <summary>
    /// Provides current state property and state change event
    /// </summary>
    public interface ISuspendableWithState : ISuspendable
    {
        /// <summary>
        /// Indicates whether the entity is IsSuspended
        /// </summary>
        bool IsSuspended { get; }

        /// <summary>
        /// Fired after <see cref="IsSuspended"/> property is changed
        /// </summary>
        event EventHandler IsSuspendedChanged;
    }

}
