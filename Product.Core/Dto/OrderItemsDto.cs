namespace Product.Core.Dto
{
    public class OrderItemsDto
    {
        public int ProductItemId { set; get; }
        public string ProductItemName { set; get; }

        public string PictureUrl { set; get; }
        public decimal price { set; get; }
        public decimal quantity { set; get; }
    }
}