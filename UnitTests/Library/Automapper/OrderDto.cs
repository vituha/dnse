using System.Collections.Generic;

namespace VS.Library.UT.Automapper
{
    public class OrderDto
    {
        public int OrderId { get; set; }

        public string OrderName { get; set; }

        public ICollection<OrderDetailsDto> OrderDetails { get; set; }
    }
}