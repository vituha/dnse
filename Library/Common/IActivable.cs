using System;
namespace VS.Library.Pattern.Lifetime
{
    /// <summary>
    /// Represents abstract activator for some entity. 
    /// The entity is considered unavailable when <see cref="Active"/> is false
    /// </summary>
    public interface IActivable
    {
        /// <summary>
        /// Activates an entity
        /// </summary>
        void Activate();

        /// <summary>
        /// Deactivates an entity
        /// </summary>
        void Deactivate();
    }

    /// <summary>
    /// Provides current state property and state change event
    /// </summary>
    public interface IActivableWithState : IActivable
    {
        /// <summary>
        /// Indicates whether the entity is active
        /// </summary>
        bool Active { get; }

        /// <summary>
        /// Fired after <see cref="Active"/> property is changed
        /// </summary>
        event EventHandler ActiveChanged;
    }
}
