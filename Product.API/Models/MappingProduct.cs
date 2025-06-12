using AutoMapper;
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
                .ReverseMap();

            CreateMap<CreateProductDto, Products>().ReverseMap();
        }

    }
}
