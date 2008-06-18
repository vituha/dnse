
namespace VS.Library.Common
{
	public delegate void D();

	public delegate TResult D0<TResult>();
	public delegate TResult D1<TResult, TParam1>(TParam1 p1);
	public delegate TResult D2<TResult, TParam1, TParam2>(TParam1 p1, TParam2 p2);
	public delegate TResult D3<TResult, TParam1, TParam2, TParam3>(TParam1 p1, TParam2 p2, TParam3 p3);
	public delegate TResult D4<TResult, TParam1, TParam2, TParam3, TParam4>(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4);

	public delegate TResult DP0<TResult>(params object[] args);
	public delegate TResult DP1<TResult, TParam1>(TParam1 p1, params object[] args);
	public delegate TResult DP2<TResult, TParam1, TParam2>(TParam1 p1, TParam2 p2, params object[] args);
	public delegate TResult DP3<TResult, TParam1, TParam2, TParam3>(TParam1 p1, TParam2 p2, TParam3 p3, params object[] args);
}
