using AutoMapper;
using Product.API.MyHelper;
using Product.Core.Dto;
using Product.Core.Entities;

namespace Product.API.Models
{
    public class MappingProduct : Profile
    {
        public MappingProduct()
        {
            CreateMap<Products, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.ProductPicture, opt => opt.MapFrom<ProductUrlResolver>())
                .ReverseMap();
            CreateMap<CreateProductDto, Products>().ReverseMap();
            CreateMap<UpdateProductDto, Products>().ReverseMap();
        }

    }
}
