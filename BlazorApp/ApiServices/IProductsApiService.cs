using BlazorApp.Dtos;

namespace BlazorApp.ApiServices
{
    public interface IProductsApiService
    {
        Task<ProductDto> GetProduct(Guid guid);
        Task<ICollection<ProductDto>> GetProducts(int page, int pageSize = 6);
        Task<int> GetProductsTotalPages(int pageSize = 6);
    }
}