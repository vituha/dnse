using System;
using System.Collections.Generic;
using System.Text;

namespace VS.Library.Pattern.Process
{
    public interface IAction<TEventArgs> 
        where TEventArgs: EventArgs
    {
        void Do();

        event EventHandler<TEventArgs> ActionStarting;
        event EventHandler<TEventArgs> ActionEnded;
    }

    public interface IDiscreteAction<TEventArgs> : IAction<TEventArgs> 
        where TEventArgs : EventArgs
    {
        event EventHandler<TEventArgs> ActionProgress;
    }
}
