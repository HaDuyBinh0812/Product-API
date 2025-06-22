using Product.Core.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Core.Dto
{
    public class OrderToReturnDto
    {
        public int OrderId { set; get; }
        public string BuyerEmail { set; get; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public ShipAddress shiptoAddress { set; get; }

        public decimal ShippingPrice { set; get; }
        public DeliveryMethod deliveryMethod { set; get; }
        public IReadOnlyList<OrderItemsDto> orderItems { set; get; }

        public decimal Subtotal { set; get; }
        public decimal Total { set; get; }

        public OrderStatus orderStatus { set; get; } = OrderStatus.Pending;

    }
}
