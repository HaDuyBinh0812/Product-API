using AutoMapper;
using Product.Core.Dto;
using Product.Core.Entities;
using Product.Core.Entities.Orders;

namespace Product.API.MyHelper
{
    public class OrderItemUrlResolver : IValueResolver<OrderItems, OrderItemsDto, string>
    {
        private readonly IConfiguration _config;

        public OrderItemUrlResolver(IConfiguration config)
        {
            _config= config;
        }
        public string Resolve(OrderItems source, OrderItemsDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.productItemOrdered.PictureUrl))
            {
                return _config["API_url"]+source.productItemOrdered.PictureUrl;
            }
            return null;
        }
    }
}
