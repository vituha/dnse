namespace Ipreo.Vision.Utils.Collections
{
    /// <summary>
    /// Represents a pair of matching elements from two sequences being joined.
    /// </summary>
    /// <typeparam name="TOuter">Type of outer sequence elements.</typeparam>
    /// <typeparam name="TInner">Type of inner sequence elements.</typeparam>
    public struct JoinMatch<TOuter, TInner>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JoinMatch{TOuter, TInner}"/> struct.
        /// </summary>
        /// <param name="outer">Outer sequence element.</param>
        /// <param name="inner">Inner sequence element.</param>
        public JoinMatch(TOuter outer, TInner inner) : this()
        {
            Inner = inner;
            Outer = outer;
        }

        /// <summary>
        /// Element of outer sequence.
        /// </summary>
        /// <value>
        /// Element instance.
        /// </value>
        public TOuter Outer { get; private set; }

        /// <summary>
        /// Element of inner sequence.
        /// </summary>
        /// <value>
        /// Element instance.
        /// </value>
        public TInner Inner { get; private set; }
    }
}