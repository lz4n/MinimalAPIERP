using BlazorApp.ApiServices;
using BlazorApp.Dtos;
using Microsoft.AspNetCore.Components;
using System.Net;

namespace BlazorApp.Components.Pages
{
    public partial class CartPage
    {
        [Inject] private ICartItemApiService _cartItemApiService { get; set; }

        private ICollection<CartItemDto>? _items = null;
        private string loadingText = "Cargando...";

        protected override async Task OnInitializedAsync()
        {
            await LoadCart();
        }

        private async Task LoadCart()
        {
            try
            {
                _items = await _cartItemApiService.GetCart();
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode.Equals(HttpStatusCode.NotFound))
                {
                    loadingText = "No tienes productos en el carrito.";
                } else
                {
                    loadingText = "No se han podido leer los productos del carrito.";
                }
                
            }
        }

        private async Task ChangeItemCount(ChangeEventArgs eventArgs, CartItemDto cartItem)
        {
            if (int.TryParse(eventArgs.Value.ToString(), out int newCount))
            {
                cartItem.Count = newCount;

                if (cartItem.Count == 0)
                {
                    _items.Remove(cartItem);
                    await _cartItemApiService.DeleteItem(cartItem);

                    return;
                }

                await _cartItemApiService.UpdateItem(cartItem);
            }
        }
    }
}
