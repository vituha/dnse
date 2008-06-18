using System;
using System.Collections.Generic;
using System.Text;

namespace VS.Library.Pattern.Lifetime
{
    public interface IManageableActivator: IActivator
    {
        /// <summary>
        /// Prevents value from being freed until Unlock is called
        /// </summary>
        void Lock();

        /// <summary>
        /// Re-enables automatic value creation/freeing
        /// </summary>
        void Unlock();
    }
}
