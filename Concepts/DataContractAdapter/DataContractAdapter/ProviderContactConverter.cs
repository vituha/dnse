using System;

namespace DataContractAdapter
{
    public class ProviderContactConverter : ConverterBase
    {
        public override ContactDataExchange Convert(ProviderContact source)
        {
            var result = new ContactDataExchange()
            {
                Name1 = source.FirstName,
                Name2 = String.Empty,
                Name3 = source.LastName
            };
            return result;
        }
    }
}
