using System.Collections.Generic;
using AutoMapper;

namespace VS.Library.UT.Automapper
{
    internal class OrderDetailDtoConverter : ITypeConverter<IEnumerable<OrderDetailsDto>, ICollection<OrderDetail>>
    {
        public ICollection<OrderDetail> Convert(ResolutionContext context)
        {
            var source = (IEnumerable<OrderDetailsDto>)context.SourceValue;
            var destination = (ICollection<OrderDetail>) context.DestinationValue;
            if (destination == null)
            {
                destination = new List<OrderDetail>();
            }

            Merge(source, destination);
        }

        private void Merge(IEnumerable<OrderDetailsDto> source, ICollection<OrderDetail> destination)
        {
            
        }
    }
}