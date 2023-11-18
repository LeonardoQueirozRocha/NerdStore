using AutoMapper;
using NerdStore.Catalog.Application.ViewModels;
using NerdStore.Catalog.Domain.Models;

namespace NerdStore.Catalog.Application.AutoMapper;

public class DomainToViewModelMappingProfile : Profile
{
    public DomainToViewModelMappingProfile()
    {
        CreateMap<Product, ProductViewModel>()
            .ForMember(d => d.Width, o => o.MapFrom(s => s.Dimension.Width))
            .ForMember(d => d.Height, o => o.MapFrom(s => s.Dimension.Height))
            .ForMember(d => d.Depth, o => o.MapFrom(s => s.Dimension.Depth));

        CreateMap<Category, CategoryViewModel>();
    }
}