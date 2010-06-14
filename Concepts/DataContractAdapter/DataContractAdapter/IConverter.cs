
using System.Collections.Generic;

namespace DataContractAdapter
{
    public interface IConverter<TInput, TOutput>
    {
        TOutput Convert(TInput source);
        IEnumerable<TOutput> ConvertMany(IEnumerable<TInput> source);
    }
}
