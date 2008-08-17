using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cache.Enumerable
{
    public delegate TDst ConvertDelegate<TSrc, TDst>(TSrc source);

    public delegate IEnumerable<TDst> ConvertEachDelegate<TSrc, TDst>(IEnumerable<TSrc> sources);

    public delegate ICollection<T> CollectionFactoryDelegate<T>(int capacity);

    public delegate T GetterDelegate<T>();

    public delegate void SetterDelegate<T>(T newValue);

    public class ConverterTest
    {
        public static IEnumerable<TDst> Convert<TSrc, TDst>(IEnumerable<TSrc> sources, ConvertDelegate<TSrc, TDst> converter)
        {
            foreach (var source in sources)
            {
                yield return converter(source);
            }
        }
    }
}
