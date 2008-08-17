using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VS.Library.Common
{
    #region Primitives
    public interface IIdentifiable<T>
    {
        T Id { get; }
    }

    public interface INamed
    {
        string Name { get; }
    }

    public interface IUINamed
    {
        string UIName { get; }
    }

    /// <summary>
    /// Provides access to a wrapped value
    /// </summary>
    /// <typeparam name="T">Type of wrapped value</typeparam>
    public interface IWrapper<T>
    {
        /// <summary>
        /// Returns wrapped value
        /// </summary>
        T Value { get; }
    }

    #endregion

    #region Complex

    /// <summary>
    /// A combination of <see cref="IWrapper"/> and <see cref="IDisposable"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDisposableWrapper<T> : IWrapper<T>, IDisposable
    { 
    }

    public interface INamedValue<T> : IWrapper<T>, INamed
    { 
    }

    public interface IDisplayableValue<T> : INamedValue<T>, IUINamed
    {
    }


    #endregion

}
