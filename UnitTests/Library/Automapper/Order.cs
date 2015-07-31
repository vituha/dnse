using System.Collections.ObjectModel;

namespace VS.Library.UT.Automapper
{
    public class Order
    {
        public int OrderId { get; set; }

        public string OrderName { get; set; }

        public ObservableCollection<OrderDetail> OrderDetails { get; set; }
    }
}