namespace Product.Core.Entities.Orders
{
    public class OrderItems
    {
        public OrderItems() { }
        public OrderItems(ProductItemOrdered productItemOrdered, decimal quantity, decimal price)
        {
            this.productItemOrdered=productItemOrdered;
            this.quantity=quantity;
            this.price=price;
        }

        public int OrderItemsId { set; get; }
        public ProductItemOrdered productItemOrdered { set; get; }

        public decimal price { set; get; }
        public decimal quantity { set; get; }
    }
}