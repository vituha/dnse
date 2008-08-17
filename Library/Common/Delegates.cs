
namespace VS.Library.Common
{
    public delegate TResult Func0<TResult>();
    public delegate TResult Func1<TResult, TParam1>(TParam1 p1);
    public delegate TResult Func2<TResult, TParam1, TParam2>(TParam1 p1, TParam2 p2);
    public delegate TResult Func3<TResult, TParam1, TParam2, TParam3>(TParam1 p1, TParam2 p2, TParam3 p3);
    public delegate TResult Func4<TResult, TParam1, TParam2, TParam3, TParam4>(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4);

    public delegate void Proc0();
    public delegate void Proc1<TParam1>(TParam1 p1);
    public delegate void Proc2<TParam1, TParam2>(TParam1 p1, TParam2 p2);
    public delegate void Proc3<TParam1, TParam2, TParam3>(TParam1 p1, TParam2 p2, TParam3 p3);
    public delegate void Proc4<TParam1, TParam2, TParam3, TParam4>(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4);

    public delegate TResult VaFunc0<TResult, TVarArgs>(params TVarArgs[] args);
    public delegate TResult VaFunc1<TResult, TParam1, TVarArgs>(TParam1 p1, params TVarArgs[] args);
    public delegate TResult VaFunc2<TResult, TParam1, TParam2, TVarArgs>(TParam1 p1, TParam2 p2, params TVarArgs[] args);
    public delegate TResult VaFunc3<TResult, TParam1, TParam2, TParam3, TVarArgs>(TParam1 p1, TParam2 p2, TParam3 p3, params TVarArgs[] args);
    public delegate TResult VaFunc4<TResult, TParam1, TParam2, TParam3, TParam4, TVarArgs>(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, params TVarArgs[] args);

    public delegate void VaProc0<TVarArgs>(params TVarArgs[] args);
    public delegate void VaProc1<TParam1, TVarArgs>(TParam1 p1, params TVarArgs[] args);
    public delegate void VaProc2<TParam1, TParam2, TVarArgs>(TParam1 p1, TParam2 p2, params TVarArgs[] args);
    public delegate void VaProc3<TParam1, TParam2, TParam3, TVarArgs>(TParam1 p1, TParam2 p2, TParam3 p3, params TVarArgs[] args);
    public delegate void VaProc4<TParam1, TParam2, TParam3, TParam4, TVarArgs>(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, params TVarArgs[] args);
}
