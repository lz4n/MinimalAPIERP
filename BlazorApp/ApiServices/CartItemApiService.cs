using BlazorApp.Dtos;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace BlazorApp.ApiServices
{
    public class CartItemApiService : ICartItemApiService
    {
        private const string CartId = "CART_ID";

        private HttpClient _httpClient;

        public CartItemApiService(IConfiguration configuration)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(configuration.GetConnectionString("ApiURI") + "erp/cartitem/")
            };
        }

        public async Task<ICollection<CartItemDto>> GetCart(string cartId = CartId)
        {
            return await _httpClient.GetFromJsonAsync<ICollection<CartItemDto>>($"getAllCart?cartId={cartId}");
        }

        public async Task AddProductToCart(ProductDto product, string cartId = CartId)
        {
            await _httpClient.PutAsync($"addProductToCart?cartId={cartId}&productGuid={product.Guid}", null);

        }

        public async Task DeleteItem(CartItemDto cartItem)
        {
            await _httpClient.DeleteAsync($"{cartItem.Guid}");

        }

        public async Task UpdateItem(CartItemDto cartItem)
        {
            await _httpClient.PutAsJsonAsync($"{cartItem.Guid}", new
            {
                cartId = cartItem.CartId,
                count = cartItem.Count,
                dateCreated = cartItem.DateCreated,
                productGuid = cartItem.Product.Guid
            });
        }
    }
}
