using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VS.Library.Collections
{
    public delegate IEnumerable<TDst> CollectionConverter<TSrc, TDst>(IEnumerable<TSrc> collection);
}
