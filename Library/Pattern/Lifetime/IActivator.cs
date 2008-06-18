using System;
namespace VS.Library.Pattern.Lifetime
{
    /// <summary>
    /// Represents abstract activator for some entity. 
    /// The entity is considered unavailable when <see cref="Active"/> is false
    /// </summary>
    public interface IActivator
    {
        /// <summary>
        /// Activates an entity
        /// </summary>
        void Activate();

        /// <summary>
        /// Indicates whether the entity is active
        /// </summary>
        bool Active { get; }

        /// <summary>
        /// Deactivates an entity
        /// </summary>
        void Deactivate();
    }
}
