using BlazorApp.Dtos;

namespace BlazorApp.ApiServices
{
    public interface ICartItemApiService
    {
        Task AddProductToCart(ProductDto product, string cartId = "CART_ID");
        Task DeleteItem(CartItemDto cartItem);
        Task<ICollection<CartItemDto>> GetCart(string cartId = "CART_ID");
        Task UpdateItem(CartItemDto cartItem);
    }
}