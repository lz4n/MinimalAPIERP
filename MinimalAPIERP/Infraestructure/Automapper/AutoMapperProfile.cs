using AutoMapper;
using ERP;
using MinimalAPIERP.Dtos;

namespace MinimalAPIERP.Infraestructure.Automapper 
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            MapOrders();
            MapCategories();
            MapProducts();
            MapCartItems();
            MapOrderDetails();
            MapStores();
            MapRainchecks();
        }

        private void MapRainchecks()
        {
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

        private void MapStores()
        {
            CreateMap<StoreDto, Store>()
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<Store, StoreViewDto>()
                .ForMember(dest => dest.Guid, opt => opt.MapFrom(src => src.Guid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }

        private void MapOrderDetails()
        {
            CreateMap<OrderDetailDto, OrderDetail>()
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice));

            CreateMap<OrderDetail, OrderDetailViewDto>()
                .ForMember(dest => dest.Guid, opt => opt.MapFrom(src => src.Guid))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order))
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));
        }

        private void MapCartItems()
        {
            CreateMap<CartItemDto, CartItem>()
                .ForMember(dest => dest.CartId, opt => opt.MapFrom(src => src.CartId))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count))
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.DateCreated));

            CreateMap<CartItem, CartItemViewDto>()
                .ForMember(dest => dest.Guid, opt => opt.MapFrom(src => src.Guid))
                .ForMember(dest => dest.CartId, opt => opt.MapFrom(src => src.CartId))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count))
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.DateCreated))
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));
        }

        private void MapProducts()
        {
            CreateMap<ProductDto, Product>()
                .ForMember(dest => dest.SkuNumber, opt => opt.MapFrom(src => src.SkuNumber))
                .ForMember(dest => dest.RecommendationId, opt => opt.MapFrom(src => src.RecommendationId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.SalePrice, opt => opt.MapFrom(src => src.SalePrice))
                .ForMember(dest => dest.ProductArtUrl, opt => opt.MapFrom(src => src.ProductArtUrl))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.ProductDetails, opt => opt.MapFrom(src => src.ProductDetails))
                .ForMember(dest => dest.Inventory, opt => opt.MapFrom(src => src.Inventory))
                .ForMember(dest => dest.LeadTime, opt => opt.MapFrom(src => src.LeadTime));

            CreateMap<Product, ProductViewDto>()
                .ForMember(dest => dest.Guid, opt => opt.MapFrom(src => src.Guid))
                .ForMember(dest => dest.SkuNumber, opt => opt.MapFrom(src => src.SkuNumber))
                .ForMember(dest => dest.RecommendationId, opt => opt.MapFrom(src => src.RecommendationId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.SalePrice, opt => opt.MapFrom(src => src.SalePrice))
                .ForMember(dest => dest.ProductArtUrl, opt => opt.MapFrom(src => src.ProductArtUrl))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.ProductDetails, opt => opt.MapFrom(src => src.ProductDetails))
                .ForMember(dest => dest.Inventory, opt => opt.MapFrom(src => src.Inventory))
                .ForMember(dest => dest.LeadTime, opt => opt.MapFrom(src => src.LeadTime))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));
        }

        private void MapCategories()
        {
            CreateMap<CategoryDto, Category>()
                            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));

            CreateMap<Category, CategoryViewDto>()
                .ForMember(dest => dest.Guid, opt => opt.MapFrom(src => src.Guid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));
        }

        private void MapOrders()
        {
            CreateMap<OrderDto, Order>()
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total));

            CreateMap<Order, OrderViewDto>()
                .ForMember(dest => dest.Guid, opt => opt.MapFrom(src => src.Guid))
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total));
        }
    }

}

