using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Core.Entities.Orders
{
    public class Order : BaseEntity<int>
    {
        private ShipAddress shipAddress;
        private DeliveryMethod? deliverymethod;
        private List<OrderItems> items;

        public Order() { }

        public Order(string buyerEmail, ShipAddress shipAddress, DeliveryMethod? deliverymethod, List<OrderItems> items, decimal subtotal)
        {
            BuyerEmail=buyerEmail;
            this.shipAddress=shipAddress;
            this.deliverymethod=deliverymethod;
            this.items=items;
            Subtotal=subtotal;
        }

        public Order(string buyerEmail, DateTime orderDate, ShipAddress shiptoAddress, DeliveryMethod deliveryMethod, IReadOnlyList<OrderItems> orderItems, decimal subtotal, OrderStatus orderStatus, string paymentIntentId)
        {
            BuyerEmail=buyerEmail;
            OrderDate=orderDate;
            this.shiptoAddress=shiptoAddress;
            DeliveryMethod=deliveryMethod;
            this.orderItems=orderItems;
            Subtotal=subtotal;
            this.orderStatus=orderStatus;
            PaymentIntentId=paymentIntentId;
        }
        public int OrderId { set; get; }
        public string BuyerEmail { set; get; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public ShipAddress shiptoAddress { set; get; }
        public DeliveryMethod DeliveryMethod { set; get; }

        public IReadOnlyList<OrderItems> orderItems { set; get; }

        public decimal Subtotal { set; get; }

        public OrderStatus orderStatus { set; get; } = OrderStatus.Pending;

        public string PaymentIntentId { set; get; }

        public decimal GetTotal()
        {
            return Subtotal + DeliveryMethod.Price;
        }

    }
}
