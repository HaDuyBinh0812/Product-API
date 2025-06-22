namespace Product.Core.Entities.Orders
{
    public class DeliveryMethod : BasicEntity<int>
    {
        public DeliveryMethod() { }
        public DeliveryMethod(string shortName, string deliveryTime, string desciption, decimal price)
        {
            ShortName=shortName;
            DeliveryTime=deliveryTime;
            Desciption=desciption;
            Price=price;
        }
        public int Id { set; get; }
        public string ShortName { set; get; }
        public string DeliveryTime { set; get; }

        public string Desciption { get; set; }

        public decimal Price { get; set; }
    }
}