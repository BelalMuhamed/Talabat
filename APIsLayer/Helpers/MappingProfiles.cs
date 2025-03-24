using APIsLayer.DTOs;
using AutoMapper;
using CoreLayer.Entities;

namespace APIsLayer.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDTOcs>()
                .ForMember(d => d.Brand, o => o.MapFrom(s => s.Brand.Name))
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<PictureURLResolver>());
            CreateMap<ProductCategory, CategoryDto>()
               .ReverseMap();
            CreateMap<ProductBrand, BrandsDto>()
              .ReverseMap();
        }
    }
}
