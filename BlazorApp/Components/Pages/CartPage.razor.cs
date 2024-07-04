using BlazorApp.ApiServices;
using BlazorApp.Dtos;
using Microsoft.AspNetCore.Components;
using System.Net;

namespace BlazorApp.Components.Pages
{
    public partial class CartPage
    {
        [Inject] private ICartItemApiService cartItemApiService { get; set; }

        private ICollection<CartItemDto>? items = null;
        private string loadingText = "Cargando...";

        protected override async Task OnInitializedAsync()
        {
            await LoadCart();
        }

        private async Task LoadCart()
        {
            try
            {
                items = await cartItemApiService.GetCart();
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
                    items.Remove(cartItem);
                    await cartItemApiService.DeleteItem(cartItem);

                    return;
                }

                await cartItemApiService.UpdateItem(cartItem);
            }
        }
    }
}
