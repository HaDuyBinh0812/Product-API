using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Core.Entities.Orders
{
    public class Order : BaseEntity<int>
    {
        public string BugerEmail { set; get; }
        public DateTime OrderState { set; get; }
        public ShipAddress shiptoAddress { set; get; }
        public DeliveryMethod DeliveryMethod { set; get }

        public IReadOnlyList<OrderItems> orderItems { set; get; }

        public decimal Subtotal { set; get; }

        public OrderStatus orderStatus { set; get }

    }
}
