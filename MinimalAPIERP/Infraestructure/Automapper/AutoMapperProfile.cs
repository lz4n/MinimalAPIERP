using AutoMapper;
using ERP;
using MinimalAPIERP.Dtos;

namespace MinimalAPIERP.Infraestructure.Automapper 
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<StoreDto, Store>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));


            CreateMap<Store, StoreViewDto>()
                .ForMember(dest => dest.Guid, opt => opt.MapFrom(src => src.Guid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<RaincheckDto, Raincheck>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count))
                .ForMember(dest => dest.SalePrice, opt => opt.MapFrom(src => src.SalePrice));

            CreateMap<Raincheck, RaincheckViewDto>()
                .ForMember(dest => dest.Guid, opt => opt.MapFrom(src => src.Guid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count))
                .ForMember(dest => dest.SalePrice, opt => opt.MapFrom(src => src.SalePrice))
                .ForMember(dest => dest.Store, opt => opt.MapFrom(src => src.Store))
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));
        }
    }

}

