using AutoMapper;
using NerdStore.Catalog.Application.DTOs;
using NerdStore.Catalog.Domain.Models;

namespace NerdStore.Catalog.Application.AutoMapper;

public class DomainToDTOMappingProfile : Profile
{
    public DomainToDTOMappingProfile()
    {
        CreateMap<Product, ProductDTO>()
            .ForMember(d => d.Width, o => o.MapFrom(s => s.Dimension.Width))
            .ForMember(d => d.Height, o => o.MapFrom(s => s.Dimension.Height))
            .ForMember(d => d.Depth, o => o.MapFrom(s => s.Dimension.Depth));

        CreateMap<Category, CategoryDTO>();
    }
}