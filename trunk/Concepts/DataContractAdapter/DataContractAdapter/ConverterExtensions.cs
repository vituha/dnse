
using System;

namespace DataContractAdapter
{
    public static class ConverterExtensions
    {
        private static WeakReference _providerContactConverterRef;
        private static ProviderContactConverter ProviderContactConverter
        {
            get
            {
                ProviderContactConverter result = null;
                if (_providerContactConverterRef != null)
                {
                    result = (ProviderContactConverter)_providerContactConverterRef.Target;
                }
                if (result == null)
                {
                    result = new ProviderContactConverter();
                    _providerContactConverterRef = new WeakReference(result);
                }
                return result;
            }
        }

        public static ContactDataExchange ToDataExchange(this ProviderContact contact)
        {
            return ProviderContactConverter.Convert(contact);
        }
    }
}
