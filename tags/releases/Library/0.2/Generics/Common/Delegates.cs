using System;
using System.Collections.Generic;
using System.Text;

namespace VS.Library.Generics.Common.Delegates
{
    public delegate void D();
    public delegate TRetVal D0<TRetVal>();
    public delegate TRetVal D1<TRetVal, TParm1>(TParm1 p1);
    public delegate TRetVal D2<TRetVal, TParm1, TParm2>(TParm1 p1, TParm2 p2);
    public delegate TRetVal D3<TRetVal, TParm1, TParm2, TParm3>(TParm1 p1, TParm2 p2, TParm3 p3);
    public delegate TRetVal D4<TRetVal, TParm1, TParm2, TParm3, TParm4>(TParm1 p1, TParm2 p2, TParm3 p3, TParm4 p4);
}
