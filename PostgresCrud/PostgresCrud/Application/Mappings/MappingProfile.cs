using AutoMapper;
using PostgresCrud.Domain.Products;
using PostgresCrud.DTOs;

namespace PostgresCrud.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductResponse>();
        
        CreateMap<ProductRequest, Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}