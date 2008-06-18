using System;
namespace VS.Library.Pattern.Lifetime
{
    public interface IUsingScope
    {
        /// <summary>
        /// Notifies the object that is is about to be used
        /// </summary>
        /// <returns>A disposable that controls time of usage</returns>
        IDisposable Use();
    }
}
