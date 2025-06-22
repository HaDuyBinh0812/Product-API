namespace Product.Core.Entities.Orders
{
    public class ProductItemOrdered
    {
        public ProductItemOrdered() { }
        public ProductItemOrdered(int productItemId, string productItemName, string pictureUrl)
        {
            ProductItemId=productItemId;
            ProductItemName=productItemName;
            PictureUrl=pictureUrl;
        }

        public int ProductItemId { set; get; }
        public string ProductItemName { set; get; }

        public string PictureUrl { set; get; }
    }
}