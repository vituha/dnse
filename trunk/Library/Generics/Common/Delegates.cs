
namespace VS.Library.Generics.Common.Delegates
{
	public delegate void D();

	public delegate TRetVal D0<TRetVal>();
	public delegate TRetVal D1<TRetVal, TParm1>(TParm1 p1);
	public delegate TRetVal D2<TRetVal, TParm1, TParm2>(TParm1 p1, TParm2 p2);
	public delegate TRetVal D3<TRetVal, TParm1, TParm2, TParm3>(TParm1 p1, TParm2 p2, TParm3 p3);
	public delegate TRetVal D4<TRetVal, TParm1, TParm2, TParm3, TParm4>(TParm1 p1, TParm2 p2, TParm3 p3, TParm4 p4);

	public delegate TRetVal DP0<TRetVal>(params object[] args);
	public delegate TRetVal DP1<TRetVal, TParm1>(TParm1 p1, params object[] args);
	public delegate TRetVal DP2<TRetVal, TParm1, TParm2>(TParm1 p1, TParm2 p2, params object[] args);
	public delegate TRetVal DP3<TRetVal, TParm1, TParm2, TParm3>(TParm1 p1, TParm2 p2, TParm3 p3, params object[] args);
}
