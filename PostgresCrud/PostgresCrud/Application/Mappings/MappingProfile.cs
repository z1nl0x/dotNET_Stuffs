using AutoMapper;
using PostgresCrud.DTOs;
using PostgresCrud.Entities;

namespace PostgresCrud.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductViewModel>();
        
        CreateMap<ProductInputModel, Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}