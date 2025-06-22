using AutoMapper;
using Product.API.MyHelper;
using Product.Core.Dto;
using Product.Core.Entities.Orders;

namespace Product.API.Models
{
    public class MappingOders : Profile
    {
        public MappingOders()
        {
            CreateMap<ShipAddress, AddressDto>().ReverseMap();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.deliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price))
                .ReverseMap();
            CreateMap<OrderItems, OrderItemsDto>()
                .ForMember(d => d.ProductItemId, o => o.MapFrom(s => s.OrderItemsId))
                .ForMember(d => d.ProductItemName, o => o.MapFrom(s => s.productItemOrdered.ProductItemName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.productItemOrdered.PictureUrl))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemUrlResolver>()).ReverseMap();
        }
    }
}
