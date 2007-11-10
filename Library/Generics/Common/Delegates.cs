using System;
using System.Collections.Generic;
using System.Text;

namespace VS.Library.Generics.Common
{
    public delegate void Delegate0();
    public delegate TRetValue Delegate1<TRetValue>();
    public delegate TRetValue Delegate2<TRetValue, TParam1>(TParam1 p1);
    public delegate TRetValue Delegate3<TRetValue, TParam1, TParam2>(TParam1 p1, TParam2 p2);
    public delegate TRetValue Delegate4<TRetValue, TParam1, TParam2, TParam3>(TParam1 p1, TParam2 p2, TParam3 p3);

}
