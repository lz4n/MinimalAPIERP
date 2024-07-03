using BlazorApp.Dtos;

namespace BlazorApp.ApiServices
{
    public class ProductsApiService : IApiService<ProductViewDto>
    {
        private HttpClient _httpClient;

        public ProductsApiService(IConfiguration configuration)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(configuration.GetConnectionString("ApiURI"))
            };
        }

        public async Task<ICollection<ProductViewDto>> Get(int page, int pageSize = 10)
        {
            return await _httpClient.GetFromJsonAsync<ICollection<ProductViewDto>>($"erp/product/paged?pageSize={pageSize}&page={page}");
        }

        public async Task<int> GetTotalPages(int pageSize = 10)
        {
            return await _httpClient.GetFromJsonAsync<int>($"erp/product/totalPages?pageSize={pageSize}");
        }
    }
}
