using System;
using System.Collections.Generic;
using System.Linq;

namespace DataContractAdapter
{
    public abstract class ConverterBase : IConverter<ProviderContact, ContactDataExchange>
    {
        public abstract ContactDataExchange Convert(ProviderContact source);


        public virtual IEnumerable<ContactDataExchange> ConvertMany(IEnumerable<ProviderContact> source)
        {
            if (source == null) throw new ArgumentNullException("source");

            return 
                from providerContact in source 
                select Convert(providerContact);
        }
    }
}