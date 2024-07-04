using BlazorApp.Dtos;

namespace BlazorApp.ApiServices
{
    public class ProductsApiService : IProductsApiService
    {
        private const int DefaultPageSize = 6;

        private HttpClient _httpClient;

        public ProductsApiService(IConfiguration configuration)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(configuration.GetConnectionString("ApiURI") + "erp/product/")
            };
        }

        public async Task<ProductDto> GetProduct(Guid guid)
        {
            return await _httpClient.GetFromJsonAsync<ProductDto>($"{guid}");
        }

        public async Task<ICollection<ProductDto>> GetProducts(int page, int pageSize = DefaultPageSize)
        {
            return await _httpClient.GetFromJsonAsync<ICollection<ProductDto>>($"paged?pageSize={pageSize}&page={page}");
        }

        public async Task<int> GetProductsTotalPages(int pageSize = DefaultPageSize)
        {
            return await _httpClient.GetFromJsonAsync<int>($"totalPages?pageSize={pageSize}");
        }
    }
}
